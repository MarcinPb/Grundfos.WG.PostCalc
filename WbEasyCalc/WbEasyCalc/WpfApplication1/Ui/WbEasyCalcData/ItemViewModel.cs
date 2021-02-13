using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DataRepository;
using GlobalRepository;
using WbEasyCalcModel;
using WbEasyCalcRepository;
using WpfApplication1.Ui.WbEasyCalcData.ViewModel;
using WpfApplication1.Ui.WbEasyCalcData.ViewModel.Tabs;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{

    public class ItemViewModel : ViewModelBase
    {
        public DataModel.WbEasyCalcData Model => new DataModel.WbEasyCalcData()
        {
            WbEasyCalcDataId = this.Id,

            CreateLogin = CreateLogin,
            CreateDate = CreateDate,
            ModifyLogin = ModifyLogin,
            ModifyDate = ModifyDate,

            YearNo = YearNo,
            MonthNo = MonthNo,
            ZoneId = ZoneId,
            Description = Description,
            IsArchive = IsArchive,
            IsAccepted = IsAccepted,

            EasyCalcModel = EasyCalcViewModel?.Model,

        };

        #region Props ViewModel: Id, ZoneId,...

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; RaisePropertyChanged(nameof(Id)); }
        }

        private string _createLogin;
        public string CreateLogin
        {
            get => _createLogin;
            set { _createLogin = value; RaisePropertyChanged(nameof(CreateLogin)); }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        { 
            get => _createDate;
            set { _createDate = value; RaisePropertyChanged(nameof(CreateDate)); }
        }

        private string _modifyLogin;
        public string ModifyLogin
        {
            get => _modifyLogin;
            set { _modifyLogin = value; RaisePropertyChanged(nameof(ModifyLogin)); }
        }

        private DateTime _modifyDate;
        public DateTime ModifyDate
        { 
            get => _modifyDate;
            set { _modifyDate = value; RaisePropertyChanged(nameof(ModifyDate)); }
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
                CalculateDaysNumber();
            }
        }
        public string YearName => GlobalConfig.DataRepository.YearList.FirstOrDefault(x => x.Id == YearNo)?.Name;

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
                CalculateDaysNumber();
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

        private bool _isArchive;
        public bool IsArchive
        {
            get
            {
                return _isArchive;
            }
            set
            {
                _isArchive = value;
                RaisePropertyChanged();
                //if (IsArchive == false)
                //{
                //    IsAccepted = false;
                //}
            }
        }

        private bool _isAccepted;
        public bool IsAccepted
        {
            get
            {
                return _isAccepted;
            }
            set
            {
                _isAccepted = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        private EasyCalcViewModel _easyCalcViewModel;
        public EasyCalcViewModel EasyCalcViewModel
        {
            get => _easyCalcViewModel;
            set { _easyCalcViewModel = value; RaisePropertyChanged(nameof(EasyCalcViewModel)); }
        }


        public ItemViewModel(DataModel.WbEasyCalcData model)
        {
            Id = model.WbEasyCalcDataId;

            if (model.WbEasyCalcDataId != 0)
            {
                ZoneId = model.ZoneId;
                YearNo = model.YearNo;
                MonthNo = model.MonthNo;
                //Start_PeriodDays_M21 = model.EasyCalcModel.StartModel.Start_PeriodDays_M21;
            }
            else
            {
                ZoneId = GlobalConfig.DataRepository.ZoneList.First().ZoneId;
                YearNo = DateTime.Now.Year;
                MonthNo = DateTime.Now.Month;
                //Start_PeriodDays_M21 = model.Start_PeriodDays_M21;
                CalculateDaysNumber();
            }

            CreateLogin = model.CreateLogin;
            CreateDate = model.CreateDate;
            ModifyLogin = model.ModifyLogin;
            ModifyDate = model.ModifyDate;

            Description = model.Description;
            IsArchive = model.IsArchive;
            IsAccepted = model.IsAccepted;

            WbEasyCalc wbEasyCalcRepository = new WbEasyCalcRepository.WbEasyCalc();
            var easyCalcModel = model.EasyCalcModel;
            wbEasyCalcRepository.CalculateNew(easyCalcModel);

            EasyCalcViewModel = new EasyCalcViewModel(easyCalcModel, this);

        }

        //public void CalculateExcel()
        //{
        //    try
        //    {
        //        EasyCalcDataInput easyCalcDataInput = Model.EasyCalcDataInput;
        //        IWbEasyCalc wbEasyCalc = new WbEasyCalcRepository.WbEasyCalc();
        //        EasyCalcDataOutput easyCalcDataOutput = wbEasyCalc.Calculate(easyCalcDataInput);
        //        MapEasyCalcDataOutput(easyCalcDataOutput);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        public void CalculateExcelNew()
        {
            try
            {
                if (Model.EasyCalcModel == null) return;

                var easyCalcModel = Model.EasyCalcModel;
                new WbEasyCalcRepository.WbEasyCalc().CalculateNew(easyCalcModel);
                Model.EasyCalcModel = easyCalcModel;

                EasyCalcViewModel.Refreash(easyCalcModel);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void CalculateDaysNumber()
        {
            if (MonthNo == 0 || YearNo == 0)
            {
                return;
            }
            //Start_PeriodDays_M21 = MonthNo == 13 ? new DateTime(YearNo, 12, 31).DayOfYear : DateTime.DaysInMonth(YearNo, MonthNo);
        }

        //private void MapEasyCalcDataOutput(EasyCalcDataOutput easyCalcDataOutput)
        //{
            //Prs_BestEstimate_F33 = easyCalcDataOutput.Prs_BestEstimate_F33;
            //Prs_Min_F29 = easyCalcDataOutput.Prs_Min_F29;
            //Prs_Max_F31 = easyCalcDataOutput.Prs_Max_F31;

            //SysInput_ErrorMarg_F72 = easyCalcDataOutput.SysInput_ErrorMarg_F72;
            //SysInput_Min_D75 = easyCalcDataOutput.SysInput_Min_D75;
            //SysInput_Max_D77 = easyCalcDataOutput.SysInput_Max_D77;
            //SysInput_BestEstimate_D79 = easyCalcDataOutput.SysInput_BestEstimate_D79;

            //BilledCons_Sum_D28 = easyCalcDataOutput.BilledCons_Sum_D28;
            //BilledCons_Sum_H28 = easyCalcDataOutput.BilledCons_Sum_H28;

            //UnbilledCons_Sum_D32 = easyCalcDataOutput.UnbilledCons_Sum_D32;
            //UnbilledCons_ErrorMarg_J25 = easyCalcDataOutput.UnbilledCons_ErrorMarg_J25;
            //UnbilledCons_Min_H28 = easyCalcDataOutput.UnbilledCons_Min_H28;
            //UnbilledCons_Max_H30 = easyCalcDataOutput.UnbilledCons_Max_H30;
            //UnbilledCons_BestEstimate_H32 = easyCalcDataOutput.UnbilledCons_BestEstimate_H32;

            //UnauthCons_Total_L6 = easyCalcDataOutput.UnauthCons_Total_L6;
            //UnauthCons_Total_L10 = easyCalcDataOutput.UnauthCons_Total_L10;
            //UnauthCons_Total_L14 = easyCalcDataOutput.UnauthCons_Total_L14;
            //UnauthCons_ErrorMarg_F24 = easyCalcDataOutput.UnauthCons_ErrorMarg_F24;
            //UnauthCons_Min_L27 = easyCalcDataOutput.UnauthCons_Min_L27;
            //UnauthCons_Max_L29 = easyCalcDataOutput.UnauthCons_Max_L29;
            //UnauthCons_BestEstimate_L31 = easyCalcDataOutput.UnauthCons_BestEstimate_L31;

            //MetErrors_Total_F8 = easyCalcDataOutput.MetErrors_Total_F8;
            //MetErrors_Total_F32 = easyCalcDataOutput.MetErrors_Total_F32;
            //MetErrors_Total_F34 = easyCalcDataOutput.MetErrors_Total_F34;
            //MetErrors_Total_F38 = easyCalcDataOutput.MetErrors_Total_F38;
            //MetErrors_Total_L8 = easyCalcDataOutput.MetErrors_Total_L8;
            //MetErrors_Total_L32 = easyCalcDataOutput.MetErrors_Total_L32;
            //MetErrors_Total_L34 = easyCalcDataOutput.MetErrors_Total_L34;
            //MetErrors_Total_L38 = easyCalcDataOutput.MetErrors_Total_L38;
            //MetErrors_ErrorMarg_N42 = easyCalcDataOutput.MetErrors_ErrorMarg_N42;
            //MetErrors_Min_L45 = easyCalcDataOutput.MetErrors_Min_L45;
            //MetErrors_Max_L47 = easyCalcDataOutput.MetErrors_Max_L47;
            //MetErrors_BestEstimate_L49 = easyCalcDataOutput.MetErrors_BestEstimate_L49;

            //MetErrors_Total_L12 = easyCalcDataOutput.MetErrors_Total_L12;
            //MetErrors_Total_L13 = easyCalcDataOutput.MetErrors_Total_L13;
            //MetErrors_Total_L14 = easyCalcDataOutput.MetErrors_Total_L14;
            //MetErrors_Total_L15 = easyCalcDataOutput.MetErrors_Total_L15;

            //Network_Total_D28 = easyCalcDataOutput.Network_Total_D28;
            //Network_Total_D32 = easyCalcDataOutput.Network_Total_D32;
            //Network_Min_D39 = easyCalcDataOutput.Network_Min_D39;
            //Network_Max_D41 = easyCalcDataOutput.Network_Max_D41;
            //Network_BestEstimate_D43 = easyCalcDataOutput.Network_BestEstimate_D43;
            //Network_Number_H21 = easyCalcDataOutput.Network_Number_H21;
            //Network_ErrorMarg_J21 = easyCalcDataOutput.Network_ErrorMarg_J21;
            //Network_ErrorMarg_J24 = easyCalcDataOutput.Network_ErrorMarg_J24;
            //Network_Min_H26 = easyCalcDataOutput.Network_Min_H26;
            //Network_Max_H28 = easyCalcDataOutput.Network_Max_H28;
            //Network_BestEstimate_H30 = easyCalcDataOutput.Network_BestEstimate_H30;
            //Network_Number_H39 = easyCalcDataOutput.Network_Number_H39;
            //Network_ErrorMarg_J39 = easyCalcDataOutput.Network_ErrorMarg_J39;

            //UnauthCons_Total_L18 = easyCalcDataOutput.UnauthCons_Total_L18;
            //UnauthCons_Total_L19 = easyCalcDataOutput.UnauthCons_Total_L19;
            //UnauthCons_Total_L20 = easyCalcDataOutput.UnauthCons_Total_L20;
            //UnauthCons_Total_L21 = easyCalcDataOutput.UnauthCons_Total_L21;

            //Interm_BestEstimate_H33 = easyCalcDataOutput.Interm_BestEstimate_H33;
            //Interm_Min_H29 = easyCalcDataOutput.Interm_Min_H29;
            //Interm_Max_H31 = easyCalcDataOutput.Interm_Max_H31;

            //FinancData_G13 = easyCalcDataOutput.FinancData_G13;
            //FinancData_G15 = easyCalcDataOutput.FinancData_G15;
            //FinancData_G17 = easyCalcDataOutput.FinancData_G17;
            //FinancData_G19 = easyCalcDataOutput.FinancData_G19;
            //FinancData_G20 = easyCalcDataOutput.FinancData_G20;
            //FinancData_G22 = easyCalcDataOutput.FinancData_G22;
            //FinancData_D24 = easyCalcDataOutput.FinancData_D24;
            //FinancData_G31 = easyCalcDataOutput.FinancData_G31;
            //FinancData_K8 =  easyCalcDataOutput.FinancData_K8 ;
            //FinancData_K13 = easyCalcDataOutput.FinancData_K13;
            //FinancData_K15 = easyCalcDataOutput.FinancData_K15;
            //FinancData_K17 = easyCalcDataOutput.FinancData_K17;
            //FinancData_K19 = easyCalcDataOutput.FinancData_K19;
            //FinancData_K20 = easyCalcDataOutput.FinancData_K20;
            //FinancData_K22 = easyCalcDataOutput.FinancData_K22;
            //FinancData_K31 = easyCalcDataOutput.FinancData_K31;
            //FinancData_K35 = easyCalcDataOutput.FinancData_K35;


            //WaterBalanceDayViewModel = new WaterBalanceViewModel(easyCalcDataOutput.WaterBalanceDay);
            //WaterBalancePeriodViewModel = new WaterBalanceViewModel(easyCalcDataOutput.WaterBalancePeriod);
            //WaterBalanceYearViewModel = new WaterBalanceViewModel(easyCalcDataOutput.WaterBalanceYear);
            //PisViewModel = new PisViewModel(easyCalcDataOutput.Pis);
        //}
    }
}
