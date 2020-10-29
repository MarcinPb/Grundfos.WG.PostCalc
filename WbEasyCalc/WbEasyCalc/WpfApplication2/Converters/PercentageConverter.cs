using System;
using System.Windows.Data;

namespace WpfApplication1.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string value_str = value.ToString();
            if (String.IsNullOrWhiteSpace(value_str)) return null;

            return (double)value * 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var ppp = culture.NumberFormat.PercentSymbol;   //.ToCharArray();
            var ddd = culture.NumberFormat.NumberDecimalSeparator;   //.ToCharArray();

            //if (value.ToString().EndsWith(".")) return ".";
            if (value.ToString().EndsWith(culture.NumberFormat.NumberDecimalSeparator)) return culture.NumberFormat.NumberDecimalSeparator;

            string value_str = value.ToString();
            if (String.IsNullOrWhiteSpace(value_str)) return null;

            value_str = value_str.TrimEnd(culture.NumberFormat.PercentSymbol.ToCharArray()).Replace(".", ",");

            double result;
            if (Double.TryParse(value_str, out result))
            {
                return result / 100.0;
            }
            //return (double)value / 100.0;
            return null;
        }
    }
}
