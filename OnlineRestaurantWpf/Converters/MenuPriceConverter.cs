using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using OnlineRestaurantWpf.Models;

namespace OnlineRestaurantWpf.Converters
{
    public class MenuPriceConverter : IValueConverter
    {
        public static decimal GlobalMenuDiscountPercentage { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menu = value as Menu;
            if (menu != null && menu.MenuDishes != null)
            {
                decimal total = 0;
                foreach (var md in menu.MenuDishes)
                {
                    if (md?.Dish != null)
                        total += md.Dish.Price;
                }
                decimal discount = GlobalMenuDiscountPercentage;
                decimal finalPrice = total * (1 - discount / 100);
                return finalPrice.ToString("N2", new CultureInfo("ro-RO")) + " RON";
            }
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 