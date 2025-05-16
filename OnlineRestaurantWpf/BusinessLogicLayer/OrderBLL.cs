using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;


namespace OnlineRestaurantWpf.BusinessLogicLayer
{
    public class OrderBLL
    {
        private readonly Func<RestaurantDbContext> _dbContextFactory;
        private readonly ConfigHelper _configHelper;
        private readonly DishBLL _dishBLL;

        public OrderBLL(Func<RestaurantDbContext> dbContextFactory, ConfigHelper configHelper, DishBLL dishBLL)
        {
            _dbContextFactory = dbContextFactory;
            _configHelper = configHelper;
            _dishBLL = dishBLL;
        }

        public async Task<Order> CreateOrderAsync(Order order, List<OrderItem> items)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (items == null || !items.Any()) throw new ArgumentException("Order must contain at least one item.", nameof(items));

            using var context = _dbContextFactory();

            order.OrderDate = DateTime.Now;
            order.Status = "Inregistrata";
            order.OrderCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

            decimal productsCost = 0;
            foreach (var item in items)
            {
                if (item.DishId.HasValue)
                {
                    var dish = await context.Dishes.FindAsync(item.DishId.Value);
                    if (dish == null)
                        throw new InvalidOperationException($"Dish with ID {item.DishId.Value} not found.");
                    if (!dish.IsAvailable || dish.TotalQuantity < (item.Quantity * dish.PortionQuantity))
                        throw new InvalidOperationException($"Dish '{dish.Name}' is unavailable or insufficient stock for {item.Quantity} portions.");
                    item.UnitPriceAtOrder = dish.Price;
                }
                else if (item.MenuId.HasValue)
                {
                    var menu = await context.Menus
                                       .Include(m => m.MenuDishes)
                                           .ThenInclude(md => md.Dish)
                                       .FirstOrDefaultAsync(m => m.Id == item.MenuId.Value);

                    if (menu == null)
                        throw new InvalidOperationException($"Menu with ID {item.MenuId.Value} not found.");
                    if (!menu.IsAvailable)
                        throw new InvalidOperationException($"Menu '{menu.Name}' is unavailable.");

                    decimal menuOriginalComponentPrice = 0;
                    foreach (var menuDish in menu.MenuDishes)
                    {
                        if (menuDish.Dish == null)
                            throw new InvalidOperationException($"Corrupted menu data: Dish for MenuDish (MenuId: {menu.Id}, DishId: {menuDish.DishId}) not found.");

                        if (!menuDish.Dish.IsAvailable || menuDish.Dish.TotalQuantity < (item.Quantity * menuDish.QuantityInMenu))
                            throw new InvalidOperationException($"Component dish '{menuDish.Dish.Name}' in menu '{menu.Name}' is unavailable or has insufficient stock for {item.Quantity} menus.");

                        menuOriginalComponentPrice += menuDish.Dish.Price;
                    }
                    item.UnitPriceAtOrder = menuOriginalComponentPrice * (1 - (menu.DiscountPercentage / 100));
                    item.UnitPriceAtOrder = Math.Round(item.UnitPriceAtOrder, 2);
                }
                else
                {
                    throw new InvalidOperationException("Order item must have either a DishId or a MenuId.");
                }
                item.Subtotal = item.Quantity * item.UnitPriceAtOrder;
                productsCost += item.Subtotal;
            }
            order.ProductsCost = productsCost;
            order.Items = items;

            order.AppliedDiscount = CalculateDiscount(order, await GetUserOrderHistoryAsync(context, order.UserId));
            order.AppliedDiscount = Math.Round(order.AppliedDiscount, 2);


            order.ShippingCost = (order.ProductsCost - order.AppliedDiscount) < _configHelper.MinOrderValueForFreeShippingA
                                 ? _configHelper.ShippingCostB
                                 : 0;

            order.TotalCost = order.ProductsCost - order.AppliedDiscount + order.ShippingCost;
            order.TotalCost = Math.Round(order.TotalCost, 2);


            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();

                foreach (var item in order.Items)
                {
                    if (item.DishId.HasValue)
                    {
                        await _dishBLL.UpdateStockAsync(item.DishId.Value, item.Quantity);
                    }
                    else if (item.MenuId.HasValue)
                    {
                        var menuWithComponents = await context.Menus
                                                       .Include(m => m.MenuDishes)
                                                       .ThenInclude(md => md.Dish)
                                                       .AsNoTracking()
                                                       .FirstOrDefaultAsync(m => m.Id == item.MenuId.Value);

                        if (menuWithComponents != null)
                        {
                            foreach (var menuDish in menuWithComponents.MenuDishes)
                            {
                                if (menuDish.Dish.PortionQuantity > 0)
                                {
                                    decimal portionsOfComponentDish = (item.Quantity * menuDish.QuantityInMenu) / menuDish.Dish.PortionQuantity;
                                    await _dishBLL.UpdateStockAsync(menuDish.DishId, portionsOfComponentDish);
                                }
                                else if (menuDish.QuantityInMenu > 0 && menuDish.Dish.PortionQuantity == 0 && menuDish.Dish.Unit.Equals("pcs", StringComparison.OrdinalIgnoreCase))
                                {
                                    await _dishBLL.UpdateStockAsync(menuDish.DishId, item.Quantity * menuDish.QuantityInMenu);
                                }
                                else
                                {
                                    Debug.WriteLine($"[OrderBLL.CreateOrderAsync] Warning: Cannot determine portions for stock update for dish ID {menuDish.DishId} in menu ID {menuWithComponents.Id} due to zero portion quantity.");
                                }
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return order;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Debug.WriteLine($"[OrderBLL.CreateOrderAsync] EXCEPTION during order creation/stock update: {ex.ToString()}");
                throw;
            }
        }

        private async Task<List<Order>> GetUserOrderHistoryAsync(RestaurantDbContext context, int userId)
        {
            return await context.Orders
                .Where(o => o.UserId == userId && o.Status == "Livrata")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        private decimal CalculateDiscount(Order currentOrder, List<Order> userOrderHistory)
        {
            decimal discount = 0;
            if (currentOrder.ProductsCost > _configHelper.OrderDiscountThresholdY)
            {
                discount = currentOrder.ProductsCost * (_configHelper.OrderDiscountPercentageW / 100);
            }

            var timeLimit = DateTime.Now.AddDays(-_configHelper.OrderTimeIntervalT);
            int recentOrdersCount = userOrderHistory.Count(o => o.OrderDate >= timeLimit);

            if (recentOrdersCount >= _configHelper.OrderCountForDiscountZ)
            {
                decimal loyaltyDiscount = currentOrder.ProductsCost * (_configHelper.OrderDiscountPercentageW / 100);
                if (loyaltyDiscount > discount) discount = loyaltyDiscount;
            }
            return Math.Round(discount, 2);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            using var context = _dbContextFactory();
            return await context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Dish)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Menu)
                        .ThenInclude(m => m!.MenuDishes)
                            .ThenInclude(md => md.Dish)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            using var context = _dbContextFactory();
            return await context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Dish)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Menu)
                        .ThenInclude(m => m!.MenuDishes)
                            .ThenInclude(md => md.Dish)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync(bool activeOnly = false)
        {
            using var context = _dbContextFactory();
            var query = context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Dish)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Menu)
                        .ThenInclude(m => m!.MenuDishes)
                            .ThenInclude(md => md.Dish)
                .AsQueryable();

            if (activeOnly)
            {
                query = query.Where(o => o.Status != "Livrata" && o.Status != "Anulata");
            }

            return await query.OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<Order?> UpdateOrderStatusAsync(int orderId, string newStatus, DateTime? estimatedDeliveryTime = null)
        {
            using var context = _dbContextFactory();
            var order = await context.Orders.FindAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");

            order.Status = newStatus;
            if (estimatedDeliveryTime.HasValue)
            {
                order.EstimatedDeliveryTime = estimatedDeliveryTime;
            }

            await context.SaveChangesAsync();
            return order;
        }
        public async Task<List<Dish>> GetLowStockDishesAsync()
        {
            using var context = _dbContextFactory();
            int threshold = _configHelper.LowStockThresholdC;
            return await context.Dishes
                .Where(d => d.TotalQuantity <= (decimal)threshold && d.IsAvailable)
                .OrderBy(d => d.TotalQuantity)
                .ToListAsync();
        }
    }
}