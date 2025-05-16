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

        [ObservableProperty]
        private ObservableCollection<DisplayCategory> _menuCategories = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string? _errorMessage;

        public ClientMenuViewModel(CategoryBLL categoryBLL, DishBLL dishBLL, MenuBLL menuBLL)
        {
            _categoryBLL = categoryBLL;
            _dishBLL = dishBLL;
            _menuBLL = menuBLL;
            // LoadMenuDataCommand.Execute(null); // Auto-load data when ViewModel is created
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
                var allDishes = await _dishBLL.GetAllDishesAsync();
                var allMenus = await _menuBLL.GetAllMenusAsync();

                foreach (var cat in categories)
                {
                    var displayCategory = new DisplayCategory(cat);

                    var dishesInCategory = allDishes.Where(d => d.CategoryId == cat.Id && d.IsAvailable);
                    foreach (var dish in dishesInCategory)
                    {
                        displayCategory.Dishes.Add(dish);
                    }

                    var menusInCategory = allMenus.Where(m => m.CategoryId == cat.Id && m.IsAvailable);
                    foreach (var menu in menusInCategory)
                    {
                        bool allComponentsAvailable = true;
                        if (menu.MenuDishes != null)
                        {
                            foreach (var menuDish in menu.MenuDishes)
                            {
                                var componentDish = allDishes.FirstOrDefault(d => d.Id == menuDish.DishId);
                                if (componentDish == null || !componentDish.IsAvailable)
                                {
                                    allComponentsAvailable = false;
                                    break;
                                }
                            }
                        }
                        if (allComponentsAvailable) displayCategory.Menus.Add(menu);
                    }

                    if (displayCategory.Dishes.Any() || displayCategory.Menus.Any())
                    {
                        MenuCategories.Add(displayCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Failed to load menu data. Please try again later.";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}