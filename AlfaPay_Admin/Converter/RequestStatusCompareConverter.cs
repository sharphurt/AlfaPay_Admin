using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using AlfaPay_Admin.Enum;
using AlfaPay_Admin.Properties;
using Application = AlfaPay_Admin.Entity.Application;
using TabControl = System.Windows.Controls.TabControl;

namespace AlfaPay_Admin.Converter
{
    public class RequestStatusCompareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && ((RequestStatus) value).ToString().Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}