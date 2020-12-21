using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Enum;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class ApplicationStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ApplicationStatus) value switch
            {
                ApplicationStatus.Rejected => (BitmapImage) Application.Current.FindResource("RejectedPlate"),
                ApplicationStatus.Accepted => (BitmapImage) Application.Current.FindResource("AcceptedPlate"),
                ApplicationStatus.NotConsidered => (BitmapImage) Application.Current.FindResource("NewPlate"),
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}