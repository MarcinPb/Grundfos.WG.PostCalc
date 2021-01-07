using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Converters
{
    class MouseLeftButtonUpConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ea = (MouseEventArgs)value;
            var originalSource = ea.OriginalSource;
            if (originalSource is Polyline)
            {
                var map = (Map) ea.Source;

                var mapPolyline = Helper.GetPolyline(map);
                mapPolyline.Locations.Clear();

                Location mouseLocation = Helper.GetMouseLocation(ea);
                Location closestLocation = Helper.GetCloseLocation(map, mouseLocation);
                if (closestLocation != null)
                {
                    return closestLocation;
                }
            }
            if (originalSource is Polygon)
            {
                var map = (Map) ea.Source;

                var mapPolygon = Helper.GetPolygon(map);
                mapPolygon.Locations.Clear();

                Location mouseLocation = Helper.GetMouseLocation(ea);
                return mouseLocation;
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
