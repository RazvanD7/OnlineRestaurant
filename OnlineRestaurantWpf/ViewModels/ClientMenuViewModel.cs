using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.BusinessLogicLayer;
using OnlineRestaurantWpf.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class DisplayCategory : ObservableObject
    {
        [ObservableProperty]
        private Category _categoryDetails;

        [ObservableProperty]
        private ObservableCollection<Dish> _dishes = new();

        [ObservableProperty]
        private ObservableCollection<Menu> _menus = new();

        public DisplayCategory(Category category)
        {
            CategoryDetails = category;
        }
    }

    public partial class ClientMenuViewModel : ObservableObject
    {
        private readonly CategoryBLL _categoryBLL;
        private readonly DishBLL _dishBLL;
        private readonly MenuBLL _menuBLL;
        private readonly AllergenBLL _allergenBLL;

        [ObservableProperty]
        private ObservableCollection<DisplayCategory> _menuCategories = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private bool _searchByName = true;

        [ObservableProperty]
        private bool _excludeAllergen;

        [ObservableProperty]
        private bool _isAdmin;

        public ClientMenuViewModel(CategoryBLL categoryBLL, DishBLL dishBLL, MenuBLL menuBLL, AllergenBLL allergenBLL)
        {
            _categoryBLL = categoryBLL;
            _dishBLL = dishBLL;
            _menuBLL = menuBLL;
            _allergenBLL = allergenBLL;
        }

        public void SetUserRole(string role)
        {
            IsAdmin = role?.ToLower() == "admin";
        }

        [RelayCommand]
        private async Task DeleteDishAsync(Dish dish)
        {
            if (dish == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the dish '{dish.Name}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _dishBLL.DeleteDishAsync(dish.Id);
                    await LoadMenuDataAsync(); // Reload the menu data
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error deleting dish: {ex.Message}";
                }
            }
        }

        [RelayCommand]
        private async Task DeleteMenuAsync(Menu menu)
        {
            if (menu == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the menu '{menu.Name}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _menuBLL.DeleteMenuAsync(menu.Id);
                    await LoadMenuDataAsync(); // Reload the menu data
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error deleting menu: {ex.Message}";
                }
            }
        }

        [RelayCommand]
        private async Task LoadMenuDataAsync()
        {
            IsLoading = true;
            ErrorMessage = null;
            MenuCategories.Clear();

            try
            {
                var categories = await _categoryBLL.GetAllCategoriesAsync();
                var dishes = await _dishBLL.GetAllDishesAsync();
                var menus = await _menuBLL.GetAllMenusAsync();

                foreach (var category in categories)
                {
                    var displayCategory = new DisplayCategory(category);
                    
                    // Add dishes for this category
                    var categoryDishes = dishes.Where(d => d.CategoryId == category.Id);
                    foreach (var dish in categoryDishes)
                    {
                        displayCategory.Dishes.Add(dish);
                    }

                    // Add menus for this category
                    var categoryMenus = menus.Where(m => m.CategoryId == category.Id);
                    foreach (var menu in categoryMenus)
                    {
                        displayCategory.Menus.Add(menu);
                    }

                    if (displayCategory.Dishes.Any() || displayCategory.Menus.Any())
                    {
                        MenuCategories.Add(displayCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading menu data: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadMenuDataAsync();
                return;
            }

            IsLoading = true;
            ErrorMessage = null;
            MenuCategories.Clear();

            try
            {
                var categories = await _categoryBLL.GetAllCategoriesAsync();
                var dishes = await _dishBLL.GetAllDishesAsync();
                var menus = await _menuBLL.GetAllMenusAsync();

                foreach (var category in categories)
                {
                    var displayCategory = new DisplayCategory(category);
                    var categoryDishes = dishes.Where(d => d.CategoryId == category.Id);
                    var categoryMenus = menus.Where(m => m.CategoryId == category.Id);

                    if (SearchByName)
                    {
                        // Search by name (both dishes and menus)
                        categoryDishes = categoryDishes.Where(d => 
                            d.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
                        
                        categoryMenus = categoryMenus.Where(m => 
                            m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            m.MenuDishes.Any(md => 
                                md.Dish.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
                    }
                    else if (ExcludeAllergen)
                    {
                        // Exclude dishes and menus containing the allergen
                        categoryDishes = categoryDishes.Where(d => 
                            !d.DishAllergens.Any(da => 
                                da.Allergen.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
                        
                        categoryMenus = categoryMenus.Where(m => 
                            !m.MenuDishes.Any(md => 
                                md.Dish.DishAllergens.Any(da => 
                                    da.Allergen.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))));
                    }

                    // Add filtered dishes
                    foreach (var dish in categoryDishes)
                    {
                        displayCategory.Dishes.Add(dish);
                    }

                    // Add filtered menus
                    foreach (var menu in categoryMenus)
                    {
                        displayCategory.Menus.Add(menu);
                    }

                    if (displayCategory.Dishes.Any() || displayCategory.Menus.Any())
                    {
                        MenuCategories.Add(displayCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error searching menu: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void UpdateDish(Dish dish)
        {
            if (dish == null) return;
            var editWindow = new Views.EditDishWindow { DataContext = new ViewModels.EditDishViewModel(dish, _dishBLL, _categoryBLL, _allergenBLL) };
            editWindow.ShowDialog();
            LoadMenuDataCommand.Execute(null); // Refresh after editing
        }

        //[RelayCommand]
        //private void UpdateMenu(Menu menu)
        //{
        //    if (menu == null) return;
        //    var editWindow = new Views.EditMenuWindow { DataContext = new ViewModels.EditMenuViewModel(menu) };
        //    editWindow.ShowDialog();
        //    LoadMenuDataCommand.Execute(null); // Refresh after editing
        //}
    }
}