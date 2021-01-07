using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Converters
{
    class LocationListToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && ((List<Location>)value).Any())
            {
                 
                var res = ((List<Location>)value).Select(x => $"{x.Latitude.ToString(CultureInfo.InvariantCulture)},{x.Longitude.ToString(CultureInfo.InvariantCulture)}").Aggregate((p, n) => p + " " + n);
                return res;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
