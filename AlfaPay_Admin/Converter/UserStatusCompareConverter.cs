using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Properties;
using Application = AlfaPay_Admin.Entity.Application;
using TabControl = System.Windows.Controls.TabControl;

namespace AlfaPay_Admin.Converter
{
    public class UserStatusCompareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && ((User) value).UserStatus.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}