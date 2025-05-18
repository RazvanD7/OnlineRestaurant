using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.BusinessLogicLayer;
using System.Collections.ObjectModel;
using System.Windows;

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class EditDishViewModel : ObservableObject
    {
        private readonly DishBLL _dishBLL;
        private readonly CategoryBLL _categoryBLL;
        private readonly Dish _originalDish;

        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private decimal price;
        [ObservableProperty] private decimal portionQuantity;
        [ObservableProperty] private string unit;
        [ObservableProperty] private ObservableCollection<Category> availableCategories = new();
        [ObservableProperty] private Category selectedCategory;

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public EditDishViewModel(Dish dish, DishBLL dishBLL, CategoryBLL categoryBLL)
        {
            _originalDish = dish;
            _dishBLL = dishBLL;
            _categoryBLL = categoryBLL;

            Name = dish.Name;
            Description = dish.Description;
            Price = dish.Price;
            PortionQuantity = dish.PortionQuantity;
            Unit = dish.Unit;
            LoadCategories();
            SelectedCategory = dish.Category;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private async void LoadCategories()
        {
            var categories = await _categoryBLL.GetAllCategoriesAsync();
            AvailableCategories = new ObservableCollection<Category>(categories);
        }

        private async void Save()
        {
            _originalDish.Name = Name;
            _originalDish.Description = Description;
            _originalDish.Price = Price;
            _originalDish.PortionQuantity = PortionQuantity;
            _originalDish.Unit = Unit;
            _originalDish.CategoryId = SelectedCategory.Id;
            _originalDish.Category = SelectedCategory;
            await _dishBLL.UpdateDishAsync(_originalDish);
            CloseWindow();
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
} 