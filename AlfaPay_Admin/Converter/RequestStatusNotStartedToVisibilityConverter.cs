﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AlfaPay_Admin.Enum;
using AlfaPay_Admin.Properties;

namespace AlfaPay_Admin.Converter
{
    public class RequestStatusNotStartedToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (RequestStatus) value == RequestStatus.NotStarted ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}