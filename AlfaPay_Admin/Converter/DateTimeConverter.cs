using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AlfaPay_Admin.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        private string _format = "dd.MM.yyyy HH:mm:ss";
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime) value).ToString(_format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.ParseExact(value.ToString(), _format, new DateTimeFormatInfo());
        }
    }
}