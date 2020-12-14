using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace AlfaPay_Admin.Converter
{
    public class MultiBoolConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(value => (bool) value);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}