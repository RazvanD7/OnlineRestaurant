using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OnlineRestaurantWpf.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                try
                {
                    return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Assets/Images/placeholder.png", UriKind.Absolute));
                }
            }
            return new BitmapImage(new Uri("pack://application:,,,/Assets/Images/placeholder.png", UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 