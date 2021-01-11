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
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Converters
{
    class MouseLeftButtonDownConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ea = (MouseEventArgs)value;
            var originalSource = ea.OriginalSource;


            if (originalSource is Polyline polyline)
            {
                var tag = ((MapPolyline)polyline.Parent).Tag.ToString();
                return tag;
            }
            if (originalSource is Border)
            {
                var map = (Map)ea.Source;
                var mapOpacity = map.Opacity;

                Location mouseLocation = Helper.GetMouseLocation(ea);
                if (mapOpacity == 1)
                {
                    Location closestLocation = Helper.GetCloseLocation(map, mouseLocation);
                    if (closestLocation != null)
                    {
                        ea.Handled = true;

                        var mapPolyline = Helper.GetPolyline(map);
                        // doesn't work 
                        //mapPolyline.Locations = new LocationCollection {closestLocation, mouseLocation};
                        // this is the same, but works
                        //if (mapPolyline.Locations == null)
                        //{
                        //    mapPolyline.Locations = new LocationCollection();
                        //}
                        mapPolyline.Locations.Add(closestLocation);
                        mapPolyline.Locations.Add(mouseLocation);

                        return new List<Location>() {closestLocation, mouseLocation};
                    }
                    else
                    {
                        //ea.Handled = true;
                        return new List<Location>() {mouseLocation};
                    }
                }
                else
                {
                    ea.Handled = true;
                    var mapPolygon = Helper.GetPolygon(map);
                    mapPolygon.Locations.Add(mouseLocation);
                    mapPolygon.Locations.Add(mouseLocation);
                    mapPolygon.Locations.Add(mouseLocation);
                    mapPolygon.Locations.Add(mouseLocation);
                    return new List<Location>() { mouseLocation, mouseLocation, mouseLocation, mouseLocation };
                }

            }

            return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
