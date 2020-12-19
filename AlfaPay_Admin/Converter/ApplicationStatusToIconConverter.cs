using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class ApplicationStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string) value)
            {
                case "REJECTED":
                    return (BitmapImage) Application.Current.FindResource("RejectedPlate");
                case "ACCEPTED":
                    return (BitmapImage) Application.Current.FindResource("AcceptedPlate");
                case "NOT_CONSIDERED":
                    return (BitmapImage) Application.Current.FindResource("NewPlate");
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}