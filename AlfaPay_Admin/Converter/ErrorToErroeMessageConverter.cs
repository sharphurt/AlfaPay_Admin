using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Model;

namespace AlfaPay_Admin.Converter
{
    public class ErrorToErrorMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ApiError) value)?.Message ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new ApiError(value.ToString(), 0, "");
        }
    }
}