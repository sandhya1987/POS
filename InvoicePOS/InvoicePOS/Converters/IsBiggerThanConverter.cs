using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InvoicePOS.Converters
{
    public class IsBiggerThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var x = System.Convert.ToDouble(parameter);
            var v = System.Convert.ToDouble(value);
            return (v > x);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility) && value is bool)
            {
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new FormatException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
