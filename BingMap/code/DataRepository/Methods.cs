using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DataModel.Files;
using DataModel.Files.Osm;
using DataModel.Files.Ztm;
using DataRepository.Services;
using Grundfos.GeometryModel;

namespace DataRepository
{
    public class Methods
    {
        public static async Task DownloadBusStopListToFileAsync()
        {
            var filter = Settings.OsmFilter;
            var response = await DownloadPostWebsiteAsync(filter);
            File.WriteAllText(Settings.OsmBusStopListFileName, response);
        }
        private static async Task<string> DownloadPostWebsiteAsync(string filter)
        {
            WebClient client = new WebClient
            {
                Headers = { [HttpRequestHeader.ContentType] = Settings.OsmContentType },
                Encoding = Encoding.UTF8,
            };
            //var response = await Task.Run(() => client.UploadString(
            var response = await client.UploadStringTaskAsync(
                Settings.OsmUrl,
                filter
            );

            return response;
        }


        public static string GetZtmArea()
        {
            List<string> urlList = new List<string>();
            var ztmStopList = GetZtmStopList(Settings.ZtmBusStopListFileName);

            var minLat = ztmStopList.Min(x => x.Lat);
            var maxLat = ztmStopList.Max(x => x.Lat);
            var minLon = ztmStopList.Min(x => x.Lon);
            var maxLon = ztmStopList.Max(x => x.Lon);

            return Invariant($"{minLon},{minLat},{maxLon},{maxLat}");
        }
        static string Invariant(FormattableString formattable)
        {
            return formattable.ToString(CultureInfo.InvariantCulture);
        }

        public static void CreateZtmOsmFile()
        {
            var xElement = File.ReadAllText(Settings.OsmBusStopListFileName);
            var osmStopNodeList = GetOsmNodeList(xElement);
            var ztmStopList = GetZtmStopList(Settings.ZtmBusStopListFileName);
            var diffList = ztmStopList.Select(z => new { Ztm = z, MinDistance = osmStopNodeList.Min(o => Distance(z.Lat, o.Lat, z.Lon, o.Lon))});
            var ztmOsmList = diffList.Select(zo => new ZtmOsm(zo.Ztm, osmStopNodeList.FirstOrDefault(o => Math.Abs(Distance(zo.Ztm.Lat, o.Lat, zo.Ztm.Lon, o.Lon) - zo.MinDistance) < 0.001), zo.MinDistance)).ToList();
            SerializeOsmNodeList(ztmOsmList);
        }
        private static double Distance(double startLat, double endLat, double startLon, double endLon  )
        {
            //return Math.Sqrt(Math.Pow(Math.Abs(startLat - endLat), 2) + Math.Pow(Math.Abs(startLon - endLon), 2));

            var latMid = (startLat+ endLat)/2;
            var mPerDegLat = 111132.954 - 559.822 * Math.Cos(2.0 * latMid) + 1.175 * Math.Cos(4.0 * latMid);
            var mPerDegLon = (3.14159265359 / 180) * 6367449 * Math.Cos(latMid);

            var deltaLat = Math.Abs(startLat - endLat);
            var deltaLon = Math.Abs(startLon - endLon);

            var mDist = Math.Sqrt(Math.Pow(deltaLat * mPerDegLat, 2) + Math.Pow(deltaLon * mPerDegLon, 2));

            return mDist;
        }




        public static List<Stop> GetZtmStopList(string fileName)
        {
            var csvStopList = CsvParser.Parse(fileName);
            var ztmStopList = csvStopList.Select(x => new Stop(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7])).ToList();
            return ztmStopList;
        }
        private static List<Node> GetOsmNodeList(string xElement)
        {
            var newElement = XElement.Parse(xElement);
            var result = newElement.Elements("node").ToList();

            var nodeList = result.Select(x => new Node
            {
                Id = Convert.ToInt64(x.Attribute("id")?.Value),
                Lat = Convert.ToDouble(x.Attribute("lat")?.Value, CultureInfo.InvariantCulture),
                Lon = Convert.ToDouble(x.Attribute("lon")?.Value, CultureInfo.InvariantCulture),
                TagList = x.Elements("tag").Select(t => new Tag
                {
                    Key = t.Attribute("k")?.Value,
                    Value = t.Attribute("v")?.Value,
                }).ToList(),
            }).ToList();

            return nodeList;
        }
        private static void SerializeOsmNodeList(List<ZtmOsm> list)
        {
            var f = new Root { OsmZtmList = list };
            var ser = new XmlSerializer(typeof(Root));

            using (var fs = new FileStream(Settings.ResultFileName, FileMode.Create))
            {
                ser.Serialize(fs, f);
            }
        }




        public static void CreateReportFile01()
        {
            var ztmOsmList = DeserializeZtmOsmList();
            string ztmOsmFilteredList = CreateReport01(ztmOsmList);
            File.WriteAllText(Settings.Report01FileName, ztmOsmFilteredList, new UTF8Encoding(true));
        }

        public static string CreateReport01(List<ZtmOsm> ztmOsmList)
        {
            var delimiter = ";";
            var csvHeader = string.Concat(
                $"ID ZTM{delimiter}Name ZTM{delimiter}Lat ZTM{delimiter}Lon ZTM{delimiter}StopCode ZTM{delimiter}",
                $"ID OSM{delimiter}Name OSM{delimiter}Lat OSM{delimiter}Lon OSM{delimiter}TypeId OSM{delimiter}",
                $"Distance{delimiter}Status\n"
            );
                
            var csvLine = ztmOsmList
                .Select(x => string.Concat(
                    $"{x.Ztm.Id}{delimiter}\"{x.Ztm.Name}\"{delimiter}{x.Ztm.Lat}{delimiter}{x.Ztm.Lon}{delimiter}{x.Ztm.Code}{delimiter}",
                    $"{x.Osm.Id}{delimiter}\"{GetOsmName(x)}\"{delimiter}{x.Osm.Lat}{delimiter}{x.Osm.Lon}{delimiter}{GetOsmTypeId(x)}{delimiter}",
                    $"{x.Distance}{delimiter}\"{GetZtmOsmStatus(x, ztmOsmList)}\"")
                )
                .Aggregate((c, n) => c + "\n" + n);

            return string.Concat(csvHeader, csvLine);
        }

        public static List<ZtmOsm> DeserializeZtmOsmList()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            Root root;
            using (XmlReader reader = XmlReader.Create(Settings.ResultFileName))
            {
                root = (Root)ser.Deserialize(reader);
            }

            return root.OsmZtmList;
        }

        private static bool IsInRightDistance(ZtmOsm ztmOsm)
        {
            return ztmOsm.Distance <= Settings.MaxMeterDistance;
        }

        private static string GetOsmName(ZtmOsm ztmOsm)
        {
            return ztmOsm.Osm.TagList.FirstOrDefault(y => y.Key == "name")?.Value;
        }

        private static int GetOsmTypeId(ZtmOsm ztmOsm)
        {
            // 1 - bus, 2 - tram, 3 - metro
            return ztmOsm.Osm.TagList.Any(y => (y.Key == "highway" || y.Key == "railway") &&  y.Value== "bus_stop") ? 1 : 2;
        }

        private static string GetZtmOsmStatus(ZtmOsm ztmOsm, List<ZtmOsm>  ztmOsmList)
        {
            string result = string.Empty;
            if (!IsInRightDistance(ztmOsm)) { result = "1"; }
            if (ztmOsm.Ztm.Name != GetOsmName(ztmOsm)) { result = string.IsNullOrEmpty(result) ? "2" : result + "|2"; }
            if (IsDuplicate(ztmOsm, ztmOsmList)) { result = string.IsNullOrEmpty(result) ? "3" : result + "|3"; }

            if (string.IsNullOrEmpty(result)) { result = "0"; }
            
            return result;
        }

        public static bool IsDuplicate(ZtmOsm ztmOsm, List<ZtmOsm>  ztmOsmList)
        {
            return ztmOsmList
               .GroupBy(x => x.Osm.Id)
               .FirstOrDefault(y => y.Any(z => z.Ztm.Id==ztmOsm.Ztm.Id))?
               .Count() > 1;
        }




        public static List<Node> GetOsmStopList()
        {
            var xElement = File.ReadAllText(Settings.OsmBusStopListFileName);
            var osmStopNodeList = GetOsmNodeList(xElement);
            return osmStopNodeList;
        }


        public static Dictionary<ObjectTypes, List<DomainObjectData>> GetWgObjectTypeList()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            return domainGrouppedObjects;
        }

        public static List<Dot> GetJunctionList()
        {
            // Vertical
            Point2D topLeft = new Point2D() { X = 5581000, Y = 5679728 };
            Point2D bottomRight = new Point2D() { X = 5582585, Y = 5670339 };

            // Horizontal
            //Point2D topLeft = new Point2D() { X = 5570422, Y = 5676000 };
            //Point2D bottomRight = new Point2D() { X = 5590485, Y = 5674000 };

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            var dotList = domainGrouppedObjects[ObjectTypes.Junction]
                .Where(y => y.Geometry[0].X >= topLeft.X && y.Geometry[0].X <= bottomRight.X && y.Geometry[0].Y <= topLeft.Y && y.Geometry[0].Y >= bottomRight.Y )
                .Select(x => new Dot() 
                    {
                        ID= x.ID,
                        Label=x.Label,
                        Center= XyToLonLat(x.Geometry[0]),
                        //Center= x.Geometry[0],
                    })
                .ToList();
            return dotList;
        }

        public static List<Pipe> GetPipeList()
        {
            // Vertical
            //Point2D topLeft = new Point2D() { X = 5581000, Y = 5679728 };
            //Point2D bottomRight = new Point2D() { X = 5582585, Y = 5670339 };
            
            // Vertical2
            Point2D topLeft = new Point2D() { X = 5570422, Y = 5679728 };
            Point2D bottomRight = new Point2D() { X = 5590485, Y = 5670339 };

            // Horizontal
            //Point2D topLeft = new Point2D() { X = 5570422, Y = 5676000 };
            //Point2D bottomRight = new Point2D() { X = 5590485, Y = 5674000 };

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            var dotList = domainGrouppedObjects[ObjectTypes.Pipe]
                //.Where(y => y.Geometry[0].X >= topLeft.X && y.Geometry[0].X <= bottomRight.X && y.Geometry[0].Y <= topLeft.Y && y.Geometry[0].Y >= bottomRight.Y )
                //.Where(y => y.Label.StartsWith("p-3-5"))   //p-3-534            
                .Select(x => new Pipe() 
                    {
                        ID= x.ID,
                        Label=x.Label,
                        Path = x.Geometry.Select(p => XyToLonLat(p)).ToList(),
                    })
                .Take(300)
                .ToList();
            return dotList;
        }

        private static Point2D XyToLonLat(Point2D point2D)
        {
            //var xDiv = 345026.4027225150;
            //var yDiv = 110831.2189204590;

            var moveXx = -63.4400885695583;
            var multiXx = 0.0000142625456859728;

            var moveYy = 0.562678511340267;
            var multiYy = 0.00000892357598435176;

            return new Point2D() { X = moveXx + point2D.X*multiXx, Y = moveYy + point2D.Y*multiYy };
        }


    }
}

