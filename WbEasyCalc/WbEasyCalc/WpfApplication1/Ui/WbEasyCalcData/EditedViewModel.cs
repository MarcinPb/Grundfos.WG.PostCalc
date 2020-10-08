using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataModel;
using DataRepository;
//using DataRepository;
//using DataRepository.WbEasyCalcData;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{
    public class EditedViewModel : ViewModelBase
    {
        private ItemViewModel _model;
        public ItemViewModel Model
        {
            get => _model;
            set { _model = value; RaisePropertyChanged(); }
        }

        public List<IdNamePair> YearList { get; set; }
        private IdNamePair _selectedYear;
        public IdNamePair SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                RaisePropertyChanged();
            }
        }

        public List<IdNamePair> MonthList { get; set; }
        private IdNamePair _selectedMonth;
        public IdNamePair SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                RaisePropertyChanged();
            }
        }

        public List<ZoneItem> ZoneItemList { get; set; }
        private ZoneItem _selectedZoneItem;
        public ZoneItem SelectedZoneItem
        {
            get => _selectedZoneItem;
            set
            {
                _selectedZoneItem = value;
                RaisePropertyChanged(nameof(SelectedZoneItem));
            }
        }

        public EditedViewModel(int id)
        {
            Model = new ItemViewModel(GlobalConfig.WbEasyCalcDataRepo.GetItem(id));

            YearList = GlobalConfig.YearList;
            SelectedYear = YearList.FirstOrDefault(x => x.Id==DateTime.Now.Year);
            MonthList = GlobalConfig.MonthList;
            SelectedMonth = MonthList.FirstOrDefault(x => x.Id==DateTime.Now.Month);
            ZoneItemList = GlobalConfig.ZoneList;
            SelectedZoneItem = ZoneItemList.FirstOrDefault(x =>x.ZoneId==Model.Model.ZoneId);
        }

    }
}
