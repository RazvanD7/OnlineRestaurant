using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OnlineRestaurantWpf.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = false;
            if (value is bool b)
            {
                boolValue = b;
            }

            // Simple NotNullOrEmpty check for strings
            if (parameter as string == "NotNullOrEmpty" && value is string s)
            {
                boolValue = !string.IsNullOrEmpty(s);
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            // Simple GreaterThanZero check for counts (int)
            if (parameter as string == "GreaterThanZero" && value is int count)
            {
                boolValue = count > 0;
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }


            if (parameter != null &&
                (parameter.ToString().Equals("invert", StringComparison.OrdinalIgnoreCase) ||
                 parameter.ToString().Equals("inverted", StringComparison.OrdinalIgnoreCase) ||
                 parameter.ToString().Equals("Not", StringComparison.OrdinalIgnoreCase)
                ))
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}