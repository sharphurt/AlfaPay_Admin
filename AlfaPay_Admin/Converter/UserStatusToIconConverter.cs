using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class UserStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string) value)
            {
                case "LOCKED":
                    return (BitmapImage) Application.Current.FindResource("LockIcon");
                case "DELETED":
                    return (BitmapImage) Application.Current.FindResource("DeleteIcon");
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}