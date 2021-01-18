using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataModel;
using DataRepository;
using GlobalRepository;
using Microsoft.WindowsAPICodePack.Dialogs;
using WbEasyCalc;
using WpfApplication1.Utility;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Ui.WaterConsumption
{
    public class EditedViewModel : ViewModelBase
    {
        private ItemViewModel _model;
        public ItemViewModel Model
        {
            get => _model;
            set { _model = value; RaisePropertyChanged(); }
        }

        public List<IdNamePair> WaterConsumptionCategoryList { get; set; }
        public List<IdNamePair> WaterConsumptionStatusList { get; set; }
        public List<ZoneItem> ZoneItemList { get; set; }


        #region Map

        private Location _center;
        public Location Center
        {
            get => _center;
            set { _center = value; RaisePropertyChanged(); }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            get => _zoomLevel;
            set { _zoomLevel = value; RaisePropertyChanged(); }
        }

        private double _mapOpacity;
        public double MapOpacity
        {
            get => _mapOpacity;
            set { _mapOpacity = value; RaisePropertyChanged(); }
        }

        #endregion

        public EditedViewModel(int id)
        {
            Model = new ItemViewModel(GlobalConfig.DataRepository.WaterConsumptionListRepository.GetItem(id));

            WaterConsumptionCategoryList = GlobalConfig.DataRepository.WaterConsumptionCategoryList;
            WaterConsumptionStatusList = GlobalConfig.DataRepository.WaterConsumptionStatusList;
            ZoneItemList = GlobalConfig.DataRepository.ZoneList;


            MapOpacity = 1;
            Center = new Location(51.20150, 16.17970); 
            ZoomLevel = 15;
        }

    }
}
