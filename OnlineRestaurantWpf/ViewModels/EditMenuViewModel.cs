using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.BusinessLogicLayer;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class EditMenuViewModel : ObservableObject
    {
        private readonly MenuBLL _menuBLL;
        private readonly CategoryBLL _categoryBLL;
        private readonly DishBLL _dishBLL;
        private readonly AllergenBLL _allergenBLL;
        private readonly Menu _originalMenu;

        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private ObservableCollection<Category> availableCategories = new();
        [ObservableProperty] private Category selectedCategory;
        [ObservableProperty] private ObservableCollection<Dish> availableDishes = new();
        [ObservableProperty] private Dish selectedAvailableDish;
        [ObservableProperty] private ObservableCollection<MenuDish> menuDishes = new();
        [ObservableProperty] private MenuDish selectedMenuDish;
        [ObservableProperty] private decimal selectedDishQuantity;
        [ObservableProperty] private ObservableCollection<Allergen> allergens = new();

        public RelayCommand AddDishCommand { get; }
        public RelayCommand RemoveDishCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public EditMenuViewModel(Menu menu, MenuBLL menuBLL, CategoryBLL categoryBLL, DishBLL dishBLL, AllergenBLL allergenBLL)
        {
            _originalMenu = menu;
            _menuBLL = menuBLL;
            _categoryBLL = categoryBLL;
            _dishBLL = dishBLL;
            _allergenBLL = allergenBLL;

            Name = menu.Name;
            Description = menu.Description;
            LoadCategories();
            SelectedCategory = menu.Category;
            LoadDishes();
            MenuDishes = new ObservableCollection<MenuDish>(menu.MenuDishes.Select(md => new MenuDish { Dish = md.Dish, DishId = md.DishId, MenuId = md.MenuId, QuantityInMenu = md.QuantityInMenu }));
            UpdateAllergens();

            AddDishCommand = new RelayCommand(AddDish);
            RemoveDishCommand = new RelayCommand(RemoveDish);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private async void LoadCategories()
        {
            var categories = await _categoryBLL.GetAllCategoriesAsync();
            AvailableCategories = new ObservableCollection<Category>(categories);
        }

        private async void LoadDishes()
        {
            var dishes = await _dishBLL.GetAllDishesAsync();
            AvailableDishes = new ObservableCollection<Dish>(dishes);
        }

        private void AddDish()
        {
            if (SelectedAvailableDish != null && !MenuDishes.Any(md => md.DishId == SelectedAvailableDish.Id))
            {
                MenuDishes.Add(new MenuDish { Dish = SelectedAvailableDish, DishId = SelectedAvailableDish.Id, QuantityInMenu = 1 });
                UpdateAllergens();
            }
        }

        private void RemoveDish()
        {
            if (SelectedMenuDish != null)
            {
                MenuDishes.Remove(SelectedMenuDish);
                UpdateAllergens();
            }
        }

        private void UpdateAllergens()
        {
            var allAllergens = MenuDishes
                .SelectMany(md => md.Dish.DishAllergens.Select(da => da.Allergen))
                .Distinct()
                .ToList();
            Allergens = new ObservableCollection<Allergen>(allAllergens);
        }

        private async void Save()
        {
            _originalMenu.Name = Name;
            _originalMenu.Description = Description;
            _originalMenu.CategoryId = SelectedCategory.Id;
            _originalMenu.Category = SelectedCategory;
            var menuDishesList = MenuDishes.ToList();
            await _menuBLL.UpdateMenuAsync(_originalMenu, menuDishesList);
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
