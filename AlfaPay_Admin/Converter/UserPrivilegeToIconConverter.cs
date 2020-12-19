using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class UserPrivilegeToIconConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains("CLIENT"))
            {
                if (values.Contains("ACTIVE"))
                    return (BitmapImage) Application.Current.FindResource("ClientPlate");
                return (BitmapImage) Application.Current.FindResource("ClientPlateGray");
            }

            if (values.Contains("ADMIN"))
            {
                if (values.Contains("ACTIVE"))
                    return (BitmapImage) Application.Current.FindResource("AdminPlate");
                return (BitmapImage) Application.Current.FindResource("AdminPlateGray");
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}