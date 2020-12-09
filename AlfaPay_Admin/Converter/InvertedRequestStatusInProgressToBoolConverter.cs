using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AlfaPay_Admin.Enum;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class InvertedRequestStatusInProgressToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (RequestStatus) value != RequestStatus.InProgress;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}