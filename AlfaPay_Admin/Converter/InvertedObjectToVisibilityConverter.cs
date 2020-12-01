using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AlfaPay_Admin.Converter
{
    public class InvertedObjectToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}