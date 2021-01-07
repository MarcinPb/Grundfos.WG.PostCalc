using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using DataModel.Db;
using DataRepository;
using Microsoft.Maps.MapControl.WPF;
using WpfApplication1.ViewModel;

namespace WpfApplication1.Repository
{
    public class MainRepo
    {
        private readonly BaseRepo _baseRepo;
        private ObservableCollection<IMapItem> _mapItemList;
        private List<StopItem> _stopItemList;
        private List<StopConnection> _stopConnectionList;
        public MainRepo()
        {
            _baseRepo = new BaseRepo();

            _stopItemList = _baseRepo.GetStopItemList();
            List<IMapItem> list1 = _stopItemList.Select(x => (IMapItem)MapStopItemToVm(x)).ToList();

            _stopConnectionList = _baseRepo.GetStopConnectionList();
            List<IMapItem> list2 = _stopConnectionList.Select(x => (IMapItem)MapStopConnectionToVm(x)).ToList();

            _mapItemList = new ObservableCollection<IMapItem>(list1.Union(list2));
        }

        public Location GetCenterLocation()
        {
            return new Location(50.295, 18.92);
        }
        public int GetZoomLevel()
        {
            return 15;
        }

        public ObservableCollection<IMapItem> GetMapItemList()
        {
            return _mapItemList; 
        }

        #region Waste: AddPushpin(), ChangePushpinName(), AddLine(), AddMapItem(MapItem2 mapItem2)
        public void AddPushpin()
        {
            _mapItemList.Add(new MapItem1()
            {
                Id = GetMapItem1Id(_mapItemList),
                Name = "Magda",
                Location= new Location(50.294, 18.92),
                TypeId = 2,
            });
        }
        private long GetMapItem1Id(ObservableCollection<IMapItem> mapItemList)
        {
            return mapItemList.Where(x => x is MapItem2).Max(x => x.Id) + 1;
        }

        public void ChangePushpinName()
        {
            _mapItemList[0].Name = "Greta";
        }

        public void AddLine()
        {
            _mapItemList.Add(new MapItem2()
            {
                Name = "Magda",
                LocationList = new List<Location>()
                {
                    new Location(50.294, 18.92),
                    new Location(50.295, 18.94),
                },
                StrokeThickness = 1,
            });
        }

        public void AddMapItem(MapItem2 mapItem2)
        {
            _mapItemList.Add(mapItem2);
        }

        #endregion

        public void DeleteMapItem(string tag)
        {
            //var stopConnection =_baseRepo.AddStopConnection(locationStart, locationEnd);
            _mapItemList.Remove(_mapItemList.FirstOrDefault(x => x is MapItem2 && x.Id.ToString()==tag));
            _baseRepo.RemoveStopConnection(Convert.ToInt32(tag));
        }
        public StopConnection AddMapItem(Location locationStart, Location locationEnd)
        {
            var stopConnection =_baseRepo.AddStopConnection(locationStart, locationEnd);
            //var item = new MapItem2()
            //{
            //    Id = stopConnection.Id,
            //    //Name = "Rita",
            //    LocationList = new List<Location>(){locationStart, locationEnd},
            //    StrokeThickness = 1,
            //};
            var item = MapStopConnectionToVm(stopConnection);
            _mapItemList.Add(item);
            return stopConnection;
        }

        public void Save()
        {
            _baseRepo.Save();
        }

        public void LoadMapItemList(Location startLocation, Location endLocation)
        {
            var stopItemList = _baseRepo.GetStopItemListFromArea(startLocation.Latitude, startLocation.Longitude, endLocation.Latitude, endLocation.Longitude);
            foreach (var stopItem in stopItemList)
            {
                _mapItemList.Add(MapStopItemToVm(stopItem));
            }

            var stopConnectionListTemp = _baseRepo.GetStopConnectionList();
            var stopConnectionList = stopConnectionListTemp.Where(x => x.StartStop != null && x.EndStop != null && stopItemList.Any(y => x.StartStop?.Id==y.Id || x.EndStop?.Id==y.Id));
            foreach (var stopConnection in stopConnectionList)
            {
                _mapItemList.Add(MapStopConnectionToVm(stopConnection));
            }
        }


        private MapItem1 MapStopItemToVm(StopItem stopItem)
        {
            return new MapItem1()
            {
                Id = stopItem.Id,
                TypeId=stopItem.TypeId,
                Location = new Location(stopItem.Lat, stopItem.Lon),
                Name=stopItem.Name
            };
        }
        private MapItem2 MapStopConnectionToVm(StopConnection stopConnection)
        {
            return new MapItem2()
            {
                Id = stopConnection.Id,
                LocationList = new List<Location>()
                {
                    new Location(stopConnection.StartStop.Lat, stopConnection.StartStop.Lon),
                    new Location(stopConnection.EndStop.Lat, stopConnection.EndStop.Lon),
                },
                StrokeThickness = 1,
                Name = "Noname",
            };
        }
    }
}
