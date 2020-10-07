using System;
using System.ComponentModel;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{

    public class ItemViewModel : ViewModelBase
    {
        public DataModel.WbEasyCalcData Model
        {
            get
            {
                return new DataModel.WbEasyCalcData()
                {
                    WbEasyCalcDataId = this.Id,

                    ZoneId = this.ZoneId,
                    YearNo = this.YearNo,
                    MonthNo = this.MonthNo,
                    Description = this.Description,

                    Start_PeriodDays_M21 = this.Start_PeriodDays_M21,
                };
            }
        }

        private bool _isDirty;

        public bool IsDirty 
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                RaisePropertyChanged();
            }
        }


        private int _id;
        public int Id 
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int _zoneId;
        public int ZoneId 
        {
            get
            {
                return _zoneId;
            }
            set
            {
                _zoneId = value;
                RaisePropertyChanged("ZoneId");
                IsDirty = true;
            }
        }

        private int _yearNo;
        public int YearNo
        {
            get
            {
                return _yearNo;
            }
            set
            {
                _yearNo = value;
                RaisePropertyChanged("YearNo");
                IsDirty = false;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private int _monthNo;
        public int MonthNo
        {
            get
            {
                return _monthNo;
            }
            set
            {
                _monthNo = value;
                RaisePropertyChanged("MonthNo");
            }
        }

        private int _startPeriodDaysM21;
        public int Start_PeriodDays_M21
        {
            get
            {
                return _startPeriodDaysM21;
            }
            set
            {
                _startPeriodDaysM21 = value;
                RaisePropertyChanged("Start_PeriodDays_M21");
            }
        }

        public ItemViewModel(DataModel.WbEasyCalcData customer)
        {
            Id = customer.WbEasyCalcDataId;
            ZoneId = customer.ZoneId;
            YearNo = customer.YearNo;
            MonthNo = customer.MonthNo;
            Description = customer.Description;
            Start_PeriodDays_M21 = customer.Start_PeriodDays_M21;
        }
    }
}
