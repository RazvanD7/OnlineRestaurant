using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.BusinessLogicLayer
{
    public class AllergenBLL
    {
        private readonly Func<RestaurantDbContext> _dbContextFactory;

        public AllergenBLL(Func<RestaurantDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Allergen>> GetAllAllergensAsync()
        {
            using var context = _dbContextFactory();
            return await context.Allergens.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Allergen?> GetAllergenByIdAsync(int allergenId)
        {
            using var context = _dbContextFactory();
            return await context.Allergens.FindAsync(allergenId);
        }

    }
}
