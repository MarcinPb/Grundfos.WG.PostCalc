using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double)value*100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string value_str = value.ToString();
            if (String.IsNullOrWhiteSpace(value_str)) return null;

            value_str = value_str.TrimEnd(culture.NumberFormat.PercentSymbol.ToCharArray());

            double result;
            if (Double.TryParse(value_str, out result))
            {
                return result / 100.0;
            }
            return value;
        }
    }
}
