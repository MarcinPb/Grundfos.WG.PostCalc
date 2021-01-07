using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using DataModel.Db;
using Microsoft.Maps.MapControl.WPF;

namespace DataRepository
{
    public class BaseRepo
    {
        private List<StopItem> _stopItemList;
        private List<StopConnection> _stopConnectionList;

        public BaseRepo()
        {
            _stopItemList = new List<StopItem>();

            _stopConnectionList = new List<StopConnection>();
        }

        public List<StopItem> GetStopItemList()
        {
            return _stopItemList;
        }

        public List<StopConnection> GetStopConnectionList()
        {
            return _stopConnectionList;
        }

        public List<StopItem> GetStopItemListFromArea(double startLat, double startLon, double endLat, double endLon)
        {
            var minLat = Math.Min(startLat, endLat);
            var maxLat = Math.Max(startLat, endLat);
            var minLon = Math.Min(startLon, endLon);
            var maxLon = Math.Max(startLon, endLon);

            var osmNodeList = Methods.GetOsmStopList();
            var ztmList = Methods.GetZtmStopList(Settings.ZtmBusStopListFileName);
            var ztmOsmList = Methods.DeserializeZtmOsmList();

            foreach (var ztm in ztmList.Where(x => x.Lat >= minLat && x.Lat <= maxLat && x.Lon >= minLon && x.Lon <= maxLon))
            {
                _stopItemList.Add(new StopItem()
                {
                    Id = ztm.Id,
                    TypeId = 1,
                    Lat= ztm.Lat,
                    Lon = ztm.Lon,
                    Name=$"{ztm.Name} ({ztm.Id})",
                });
            }
            foreach (var osm in osmNodeList.Where(x => x.Lat >= minLat && x.Lat <= maxLat && x.Lon >= minLon && x.Lon <= maxLon))
            {
                _stopItemList.Add(new StopItem()
                {
                    Id = osm.Id,
                    TypeId = 2,
                    Lat= osm.Lat,
                    Lon = osm.Lon,
                    Name= $"{osm.TagList.FirstOrDefault(y => y.Key == "name")?.Value} ({osm.Id})",
                });
            }
            foreach (var osmNode in ztmOsmList.Where(x => x.Ztm.Lat >= minLat && x.Ztm.Lat <= maxLat && x.Ztm.Lon >= minLon && x.Ztm.Lon <= maxLon))
            {
                _stopConnectionList.Add(

                    new StopConnection()
                    {
                        Id = GetStopConnectionId(),
                        StartStop = _stopItemList.FirstOrDefault(x => x.Id == osmNode.Ztm.Id),
                        EndStop = _stopItemList.FirstOrDefault(x => x.Id == osmNode.Osm.Id),
                    });

                // AddNewPolyLine(osmNode.Ztm.Lat, osmNode.Ztm.Lon, osmNode.Osm.Lat, osmNode.Osm.Lon);
            }
            return _stopItemList;
        }

        public StopConnection AddStopConnection(Location locationStart, Location locationEnd)
        {
            //var maxId = _stopConnectionList.Max(x => x.Id);
            var stopConnection = new StopConnection()
            {
                Id = GetStopConnectionId(),
                StartStop = _stopItemList.FirstOrDefault(x => x.Lat==locationStart.Latitude && x.Lon==locationStart.Longitude),
                EndStop = _stopItemList.FirstOrDefault(x => x.Lat== locationEnd.Latitude && x.Lon== locationEnd.Longitude),
            };
            _stopConnectionList.Add(stopConnection);
            return stopConnection;
        }

        public void RemoveStopConnection(int id)
        {
            var item = _stopConnectionList.FirstOrDefault(x => x.Id == id);
            _stopConnectionList.Remove(item);
        }

        public void Save()
        {
            var list = _stopConnectionList;
            SerializeOsmNodeList(list);
        }

        private void SerializeOsmNodeList(List<StopConnection> list)
        {
            var f = new Root { StopConnectionList = list };
            var ser = new XmlSerializer(typeof(Root));

            using (var fs = new FileStream(Settings.ResultFileName2, FileMode.Create))
            {
                ser.Serialize(fs, f);
            }
        }

        private int GetStopConnectionId()
        {
            return _stopConnectionList.Any() ? _stopConnectionList.Max(x => x.Id) + 1 : 1;
        }
    }
}
