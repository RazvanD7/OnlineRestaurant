using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.BusinessLogicLayer
{
    public class CategoryBLL
    {
        private readonly Func<RestaurantDbContext> _dbContextFactory;

        public CategoryBLL(Func<RestaurantDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using var context = _dbContextFactory();
            return await context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            using var context = _dbContextFactory();
            return await context.Categories.FindAsync(categoryId);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name cannot be empty.", nameof(category.Name));

            using var context = _dbContextFactory();
            bool nameExists = await context.Categories.AnyAsync(c => c.Name == category.Name);
            if (nameExists)
                throw new InvalidOperationException($"Category with name '{category.Name}' already exists.");

            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name cannot be empty.", nameof(category.Name));

            using var context = _dbContextFactory();
            var existingCategory = await context.Categories.FindAsync(category.Id);
            if (existingCategory == null)
                throw new KeyNotFoundException($"Category with ID {category.Id} not found.");

            bool nameExists = await context.Categories.AnyAsync(c => c.Name == category.Name && c.Id != category.Id);
            if (nameExists)
                throw new InvalidOperationException($"Another category with name '{category.Name}' already exists.");

            existingCategory.Name = category.Name;
            await context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            using var context = _dbContextFactory();
            var category = await context.Categories
                                    .Include(c => c.Dishes)
                                    .Include(c => c.Menus)
                                    .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            if (category.Dishes.Any() || category.Menus.Any())
            {
                throw new InvalidOperationException("Cannot delete category. It has associated dishes or menus. Please reassign or delete them first.");
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}