using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.BusinessLogicLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class EditDishViewModel : ObservableObject
    {
        private readonly DishBLL _dishBLL;
        private readonly CategoryBLL _categoryBLL;
        private readonly AllergenBLL _allergenBLL;
        private readonly Dish _originalDish;

        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private decimal price;
        [ObservableProperty] private decimal portionQuantity;
        [ObservableProperty] private string unit;
        [ObservableProperty] private ObservableCollection<Category> availableCategories = new();
        [ObservableProperty] private Category selectedCategory;
        [ObservableProperty] private ObservableCollection<Allergen> availableAllergens = new();
        [ObservableProperty] private Allergen selectedAllergen;
        [ObservableProperty] private ObservableCollection<Allergen> selectedAllergens = new();
        [ObservableProperty] private ObservableCollection<DishImage> images = new();
        [ObservableProperty] private DishImage selectedImage;
        [ObservableProperty] private string newImagePath;

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand AddAllergenCommand { get; }
        public RelayCommand RemoveAllergenCommand { get; }
        public RelayCommand AddImageCommand { get; }
        public RelayCommand RemoveImageCommand { get; }

        public EditDishViewModel(Dish dish, DishBLL dishBLL, CategoryBLL categoryBLL, AllergenBLL allergenBLL)
        {
            _originalDish = dish;
            _dishBLL = dishBLL;
            _categoryBLL = categoryBLL;
            _allergenBLL = allergenBLL;

            Name = dish.Name;
            Description = dish.Description;
            Price = dish.Price;
            PortionQuantity = dish.PortionQuantity;
            Unit = dish.Unit;
            LoadCategories();
            SelectedCategory = dish.Category;

            // Allergens
            LoadAllergens();
            SelectedAllergens = new ObservableCollection<Allergen>(dish.DishAllergens.Select(da => da.Allergen));

            // Images
            Images = new ObservableCollection<DishImage>(dish.Images);

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            AddAllergenCommand = new RelayCommand(AddAllergen);
            RemoveAllergenCommand = new RelayCommand(RemoveAllergen);
            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage);
        }

        private async void LoadCategories()
        {
            var categories = await _categoryBLL.GetAllCategoriesAsync();
            AvailableCategories = new ObservableCollection<Category>(categories);
        }

        private async void LoadAllergens()
        {
            var allAllergens = await _allergenBLL.GetAllAllergensAsync();
            AvailableAllergens = new ObservableCollection<Allergen>(allAllergens);
        }

        private void AddAllergen()
        {
            if (SelectedAllergen != null && !SelectedAllergens.Contains(SelectedAllergen))
                SelectedAllergens.Add(SelectedAllergen);
        }

        private void RemoveAllergen()
        {
            if (SelectedAllergen != null && SelectedAllergens.Contains(SelectedAllergen))
                SelectedAllergens.Remove(SelectedAllergen);
        }

        private void AddImage()
        {
            if (!string.IsNullOrWhiteSpace(NewImagePath) && !Images.Any(img => img.ImagePath == NewImagePath))
                Images.Add(new DishImage { ImagePath = NewImagePath, IsMain = Images.Count == 0 });
        }

        private void RemoveImage()
        {
            if (SelectedImage != null && Images.Contains(SelectedImage))
                Images.Remove(SelectedImage);
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
            var allergenIds = SelectedAllergens.Select(a => a.Id).ToList();
            var imagePaths = Images.Select(img => img.ImagePath).ToList();
            await _dishBLL.UpdateDishAsync(_originalDish, allergenIds, imagePaths);
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