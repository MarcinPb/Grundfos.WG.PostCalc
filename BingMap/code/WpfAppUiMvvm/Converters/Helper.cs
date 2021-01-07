using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using WpfApplication1.ViewModel;

namespace WpfApplication1.Converters
{
    public class Helper
    {

        public static MapPolyline GetPolyline(Map map)
        {
            foreach (var child in map.Children)
            {
                if (child is MapPolyline mapPolyline)
                {
                    return mapPolyline;
                }
            }

            return null;
        }
        public static MapPolygon GetPolygon(Map map)
        {
            foreach (var child in map.Children)
            {
                if (child is MapPolygon mapPolygon)
                {
                    return mapPolygon;
                }
            }
            return null;
        }

        public static Location GetCloseLocation(Map map, Location mouseLocation)
        {
            var closestLocation = GetClosestLocation(mouseLocation, GetLocationList(map));
            if (closestLocation == null)
            {
                return null;
            }
            if (IsClose(mouseLocation, closestLocation))
            {
                return closestLocation;
            }
            return null;
        }

        public static Location GetMouseLocation(MouseEventArgs ea)
        {
            var map = (Map)ea.Source;
            Point mousePosition2 = ea.GetPosition(map);
            Location mouseLocation2 = map.ViewportPointToLocation(mousePosition2);
            return mouseLocation2;
        }

        private static List<Location> GetLocationList(Map map)
        {
            var list = new List<Location>();

            foreach (var child in map.Children)
            {
                if (child is MapItemsControl)
                {
                    foreach (var item in ((MapItemsControl) child).Items)
                    {
                        if (item is MapItem1 stopItem)
                        {
                            list.Add(stopItem.Location);
                        }
                    }
                }
            }

            return list;
        }

        private static bool IsClose(Location firstLocation, Location secondLocation)
        {
            return GetDistance(firstLocation, secondLocation) < 0.0001;
        }

        private static Location GetClosestLocation(Location firstLocation, List<Location> locationList)
        {
            if (!locationList.Any())
            {
                return null;
            }
            var minDistance = locationList.Select(x => new { x, Distance = GetDistance(firstLocation, x) }).Min(x => x.Distance);
            return locationList.FirstOrDefault(x => GetDistance(firstLocation, x) == minDistance);
        }


        private static double GetDistance(Location firstLocation, Location secondLocation)
        {
            return Math.Abs(firstLocation.Latitude - secondLocation.Latitude) +
                   Math.Abs(firstLocation.Longitude - secondLocation.Longitude);
        }



        public static bool IsDrawingPolyline(Map map)
        {
            foreach (var child in map.Children)
            {
                if (child is MapPolyline polyline)
                {
                    return polyline.Locations != null &&  polyline.Locations.Any();
                }
            }

            return false;
        }
        public static bool IsDrawingPolygon(Map map)
        {
            foreach (var child in map.Children)
            {
                if (child is MapPolygon polyline)
                {
                    return polyline.Locations != null &&  polyline.Locations.Any();
                }
            }

            return false;
        }

    }
}
