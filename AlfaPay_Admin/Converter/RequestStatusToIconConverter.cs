using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Enum;

namespace AlfaPay_Admin.Converter
{
    public class RequestStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (BitmapImage) Application.Current.FindResource("TickIcon");
            return (BitmapImage) Application.Current.FindResource("AttentionIcon");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}