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

        public async Task<Allergen> AddAllergenAsync(Allergen allergen)
        {
            if (allergen == null) throw new ArgumentNullException(nameof(allergen));
            if (string.IsNullOrWhiteSpace(allergen.Name))
                throw new ArgumentException("Allergen name cannot be empty.", nameof(allergen.Name));

            using var context = _dbContextFactory();
            bool nameExists = await context.Allergens.AnyAsync(a => a.Name == allergen.Name);
            if (nameExists)
                throw new InvalidOperationException($"Allergen with name '{allergen.Name}' already exists.");

            context.Allergens.Add(allergen);
            await context.SaveChangesAsync();
            return allergen;
        }

        public async Task<Allergen> UpdateAllergenAsync(Allergen allergen)
        {
            if (allergen == null) throw new ArgumentNullException(nameof(allergen));
            if (string.IsNullOrWhiteSpace(allergen.Name))
                throw new ArgumentException("Allergen name cannot be empty.", nameof(allergen.Name));

            using var context = _dbContextFactory();
            var existingAllergen = await context.Allergens.FindAsync(allergen.Id);
            if (existingAllergen == null)
                throw new KeyNotFoundException($"Allergen with ID {allergen.Id} not found.");

            bool nameExists = await context.Allergens.AnyAsync(a => a.Name == allergen.Name && a.Id != allergen.Id);
            if (nameExists)
                throw new InvalidOperationException($"Another allergen with name '{allergen.Name}' already exists.");

            existingAllergen.Name = allergen.Name;
            await context.SaveChangesAsync();
            return existingAllergen;
        }

        public async Task DeleteAllergenAsync(int allergenId)
        {
            using var context = _dbContextFactory();
            var allergen = await context.Allergens
                                    .Include(a => a.DishAllergens)
                                    .FirstOrDefaultAsync(a => a.Id == allergenId);
            if (allergen == null)
                throw new KeyNotFoundException($"Allergen with ID {allergenId} not found.");

            // OnDelete.Restrict in DbContext for Allergen -> DishAllergen handles this.
            // This check is for a more user-friendly message.
            if (allergen.DishAllergens.Any())
            {
                throw new InvalidOperationException("Cannot delete allergen. It is associated with one or more dishes.");
            }

            context.Allergens.Remove(allergen);
            await context.SaveChangesAsync();
        }
    }
}
