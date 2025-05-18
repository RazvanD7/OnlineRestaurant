using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OnlineRestaurantWpf.Converters
{
    public class FirstDishImagePathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var menuDishes = values[0] as System.Collections.IEnumerable;
            if (menuDishes != null)
            {
                foreach (var menuDish in menuDishes)
                {
                    var dishProp = menuDish.GetType().GetProperty("Dish");
                    var dish = dishProp?.GetValue(menuDish, null);
                    if (dish != null)
                    {
                        var imagesProp = dish.GetType().GetProperty("Images");
                        var images = imagesProp?.GetValue(dish, null) as System.Collections.IEnumerable;
                        if (images != null)
                        {
                            foreach (var image in images)
                            {
                                var pathProp = image.GetType().GetProperty("ImagePath");
                                var path = pathProp?.GetValue(image, null) as string;
                                if (!string.IsNullOrWhiteSpace(path))
                                {
                                    try
                                    {
                                        return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
            // Return placeholder as BitmapImage
            return new BitmapImage(new Uri("pack://application:,,,/Assets/Images/placeholder.png", UriKind.Absolute));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 