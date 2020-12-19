using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class UserStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string) value)
            {
                case "ACTIVE":
                    return new SolidColorBrush(Colors.Black);
                case "LOCKED":
                case "DELETED":     
                    return new SolidColorBrush(Color.FromRgb(183, 189, 195));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}