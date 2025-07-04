﻿using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OnlineRestaurantWpf.BusinessLogicLayer
{
    public class DishBLL
    {
        private readonly Func<RestaurantDbContext> _dbContextFactory;

        public DishBLL(Func<RestaurantDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Dish>> GetAllDishesAsync()
        {
            using var context = _dbContextFactory();
            return await context.Dishes
                .Include(d => d.Category)
                .Include(d => d.DishAllergens)
                    .ThenInclude(da => da.Allergen)
                .Include(d => d.Images)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<List<Dish>> GetDishesByCategoryIdAsync(int categoryId)
        {
            using var context = _dbContextFactory();
            return await context.Dishes
                .Where(d => d.CategoryId == categoryId)
                .Include(d => d.Category)
                .Include(d => d.DishAllergens)
                    .ThenInclude(da => da.Allergen)
                .Include(d => d.Images)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<Dish?> GetDishByIdAsync(int dishId)
        {
            using var context = _dbContextFactory();
            return await context.Dishes
                .Include(d => d.Category)
                .Include(d => d.DishAllergens)
                    .ThenInclude(da => da.Allergen)
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Id == dishId);
        }


        public async Task<Dish> UpdateDishAsync(Dish dish, List<int>? allergenIds = null, List<string>? imagePaths = null)
        {
            if (dish == null) throw new ArgumentNullException(nameof(dish));
            if (string.IsNullOrWhiteSpace(dish.Unit)) throw new ArgumentException("Dish unit cannot be empty.", nameof(dish.Unit));

            using var context = _dbContextFactory();
            var existingDish = await context.Dishes
                .Include(d => d.DishAllergens)
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Id == dish.Id);

            if (existingDish == null)
                throw new KeyNotFoundException($"Dish with ID {dish.Id} not found.");

            existingDish.Name = dish.Name;
            existingDish.Price = dish.Price;
            existingDish.PortionQuantity = dish.PortionQuantity;
            existingDish.TotalQuantity = dish.TotalQuantity;
            existingDish.Unit = dish.Unit;
            existingDish.Description = dish.Description;
            existingDish.CategoryId = dish.CategoryId;
            existingDish.IsAvailable = dish.TotalQuantity > 0;

            existingDish.DishAllergens.Clear();
            if (allergenIds != null && allergenIds.Any())
            {
                foreach (var allergenId in allergenIds)
                {
                    var allergenExists = await context.Allergens.AnyAsync(a => a.Id == allergenId);
                    if (!allergenExists)
                        throw new InvalidOperationException($"Allergen with ID {allergenId} does not exist.");
                    existingDish.DishAllergens.Add(new DishAllergen { AllergenId = allergenId });
                }
            }

            existingDish.Images.Clear();
            if (imagePaths != null && imagePaths.Any())
            {
                bool isFirstImage = true;
                foreach (var path in imagePaths)
                {
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        existingDish.Images.Add(new DishImage { ImagePath = path, IsMain = isFirstImage });
                        isFirstImage = false;
                    }
                }
            }

            await context.SaveChangesAsync();
            return existingDish;
        }

        public async Task DeleteDishAsync(int dishId)
        {
            using var context = _dbContextFactory();
            var dish = await context.Dishes.FindAsync(dishId);
            if (dish == null)
                throw new KeyNotFoundException($"Dish with ID {dishId} not found.");

            context.Dishes.Remove(dish);
            await context.SaveChangesAsync();
        }
    }
}