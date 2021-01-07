using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataModel.Files;
using DataRepository;
using Microsoft.Maps.MapControl.WPF;

namespace WpfAppUi
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow()
        {
            InitializeComponent();
            //this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Loaded");
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            var workArea = "50.2435,18.9899,50.2826,19.0707";
            var workAreaList = workArea.Split(',').Select(x => Convert.ToDouble(x, CultureInfo.InvariantCulture)).ToList();
            var minLat = workAreaList[0];
            var maxLat = workAreaList[2];
            var minLon = workAreaList[1];
            var maxLon = workAreaList[3];
            AddAreaPolygon(minLat, minLon, maxLat, maxLon);

            var osmNodeList = Methods.GetOsmStopList();
            var ztmList = Methods.GetZtmStopList(Settings.ZtmBusStopListFileName);
            var ztmOsmList = Methods.DeserializeZtmOsmList();
            //foreach (var osmNode in osmNodeList.Take(10))
            //{
            //    Point mousePosition = new Point(osmNode.Lat, osmNode.Lon);
            //    Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            //    var pin = new Pushpin
            //    {
            //        Location = pinLocation,
            //        Background = Brushes.Green,
            //        ToolTip = $"{osmNode.Id}: {osmNode.Lat}-{osmNode.Lon}"
            //    };

            //    myMap.Children.Add(pin);
            //}
            foreach (var ztm in ztmList.Where(x => x.Lat >= minLat && x.Lat <= maxLat && x.Lon >= minLon && x.Lon <= maxLon))
            {
                AddNewPolygon(ztm.Lat, ztm.Lon, $"{ztm.Name} ({ztm.Id})", Colors.Red);
            }
            foreach (var osm in osmNodeList.Where(x => x.Lat >= minLat && x.Lat <= maxLat &&  x.Lon >= minLon && x.Lon <= maxLon ))
            {
                AddNewPolygon(osm.Lat, osm.Lon, $"{osm.TagList.FirstOrDefault(y => y.Key == "name")?.Value} ({osm.Id})", Colors.Yellow);
            }
            foreach (var osmNode in ztmOsmList.Where(x => x.Ztm.Lat >= minLat && x.Ztm.Lat <= maxLat && x.Ztm.Lon >= minLon && x.Ztm.Lon <= maxLon))
            {
                AddNewPolyLine(osmNode.Ztm.Lat, osmNode.Ztm.Lon, osmNode.Osm.Lat, osmNode.Osm.Lon);
            }
        }
        void AddAreaPolygon(double startLat, double startLon, double endLat, double endLon)
        {
            MapPolygon polygon = new MapPolygon
            {
                //Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(Colors.Gray),
                StrokeThickness = 1,
                //Opacity = 0.3,
                //ToolTip= toolTip,
                Locations = new LocationCollection() {
                    new Location(endLat, startLon),
                    new Location(startLat, startLon),
                    new Location(startLat, endLon),
                    new Location(endLat, endLon),
                }
            };

            myMap.Children.Add(polygon);
        }
 
        void AddNewPolygon(double lat, double lon, string toolTip, Color fillColor)
        {
            double dLat = 0.0001, dLon = 0.00004;

            MapPolygon polygon = new MapPolygon
            {
                Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                Opacity = 0.4,
                ToolTip= toolTip,
                Locations = new LocationCollection() {
                    new Location(lat, lon),
                    new Location(lat+dLat, lon-dLon),
                    new Location(lat+dLat, lon+dLon),
                }
            };

            myMap.Children.Add(polygon);
        }
        void AddNewPolyLine(double startLat, double startLon,  double endLat, double endLon)
        {
            MapPolyline polygon = new MapPolyline
            {
                //Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                //Opacity = 0.3,
                //ToolTip= toolTip,
                Locations = new LocationCollection() {
                    new Location(startLat, startLon),
                    new Location(endLat, endLon),
                }
            };

            myMap.Children.Add(polygon);
        }


        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
