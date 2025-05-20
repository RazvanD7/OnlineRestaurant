using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.BusinessLogicLayer
{
    public class MenuBLL
    {
        private readonly Func<RestaurantDbContext> _dbContextFactory;
        private readonly ConfigHelper _configHelper;

        public MenuBLL(Func<RestaurantDbContext> dbContextFactory, ConfigHelper configHelper)
        {
            _dbContextFactory = dbContextFactory;
            _configHelper = configHelper;
        }

        public async Task<List<Menu>> GetAllMenusAsync()
        {
            using var context = _dbContextFactory();
            return await context.Menus
                .Include(m => m.Category)
                .Include(m => m.MenuDishes)
                    .ThenInclude(md => md.Dish)
                        .ThenInclude(d => d.Images)
                .Include(m => m.MenuDishes)
                    .ThenInclude(md => md.Dish)
                        .ThenInclude(d => d.DishAllergens)
                            .ThenInclude(da => da.Allergen)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<Menu?> GetMenuByIdAsync(int menuId)
        {
            using var context = _dbContextFactory();
            return await context.Menus
                .Include(m => m.Category)
                .Include(m => m.MenuDishes)
                    .ThenInclude(md => md.Dish)
                        .ThenInclude(d => d.DishAllergens)
                            .ThenInclude(da => da.Allergen)
                .FirstOrDefaultAsync(m => m.Id == menuId);
        }

        public async Task<Menu> UpdateMenuAsync(Menu menu, List<MenuDish> menuDishes)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));
            if (menuDishes == null || !menuDishes.Any()) throw new ArgumentException("Menu must contain at least one dish.", nameof(menuDishes));

            using var context = _dbContextFactory();
            var existingMenu = await context.Menus
                .Include(m => m.MenuDishes)
                .FirstOrDefaultAsync(m => m.Id == menu.Id);

            if (existingMenu == null)
                throw new KeyNotFoundException($"Menu with ID {menu.Id} not found.");

            existingMenu.Name = menu.Name;
            existingMenu.Description = menu.Description;
            existingMenu.CategoryId = menu.CategoryId;

            existingMenu.MenuDishes.Clear();
            foreach (var mdEntry in menuDishes)
            {
                var dish = await context.Dishes.FindAsync(mdEntry.DishId);
                if (dish == null) throw new InvalidOperationException($"Dish with ID {mdEntry.DishId} not found.");
                existingMenu.MenuDishes.Add(new MenuDish
                {
                    DishId = mdEntry.DishId,
                    QuantityInMenu = mdEntry.QuantityInMenu
                });
            }

            existingMenu.IsAvailable = true;

            await context.SaveChangesAsync();
            return existingMenu;
        }

        public async Task DeleteMenuAsync(int menuId)
        {
            using var context = _dbContextFactory();
            var menu = await context.Menus.FindAsync(menuId);
            if (menu == null)
                throw new KeyNotFoundException($"Menu with ID {menuId} not found.");

            context.Menus.Remove(menu);
            await context.SaveChangesAsync();
        }
    }
}