﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApplication1.Converters
{
    public class FloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // return an invalid value in case of the value ends with a point
            return value.ToString().EndsWith(culture.NumberFormat.NumberDecimalSeparator) ? culture.NumberFormat.NumberDecimalSeparator : value;
        }
    }
}
