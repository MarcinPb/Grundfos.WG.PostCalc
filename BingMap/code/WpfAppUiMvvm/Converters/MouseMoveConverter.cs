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
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Converters
{

    class MouseMoveConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ea = (MouseEventArgs)value;
            var map = (Map)ea.Source;
            if (Helper.IsDrawingPolyline(map))
            {
                ea.Handled = true;
                Location mouseLocation = Helper.GetMouseLocation(ea);

                var mapPolyline = Helper.GetPolyline(map);
                mapPolyline.Locations[1] = mouseLocation;

                return mouseLocation;
            }
            if (Helper.IsDrawingPolygon(map))
            {
                ea.Handled = true;
                Location mouseLocation = Helper.GetMouseLocation(ea);

                var mapPolygon = Helper.GetPolygon(map);
                var startLocation = mapPolygon.Locations[0];
                mapPolygon.Locations[1] = new Location(startLocation.Latitude, mouseLocation.Longitude);
                mapPolygon.Locations[2] = new Location(mouseLocation.Latitude, mouseLocation.Longitude);
                mapPolygon.Locations[3] = new Location(mouseLocation.Latitude, startLocation.Longitude);

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
