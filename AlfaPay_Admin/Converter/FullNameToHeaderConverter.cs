using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class FullNameToHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Подробнее о {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}