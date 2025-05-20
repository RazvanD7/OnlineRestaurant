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

    }
}