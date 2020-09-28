﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Grundfos.OPC;
using Grundfos.OPC.Model;
using Grundfos.WB.EasyCalc.Calculations;
using Grundfos.Workbooks;
using Microsoft.WindowsAPICodePack.Dialogs;
using WpfApp1.Utility;

// User can't type '.' in the textbox that have been bound to a float value while UpdateSourceTrigger is PropertyChanged in WPF
// https://stackoverflow.com/questions/17142217/user-cant-type-in-the-textbox-that-have-been-bound-to-a-float-value-while-u

namespace WpfApp1
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ICollection<string> _opcTagList = new List<string>()
        {
            "SysInput_SystemInputVolumeM3_D6",
            "SysInput_SystemInputVolumeError_F6",
            "BilledCons_BilledMetConsBulkWatSupExpM3_D6",
            "BilledCons_BilledUnmetConsBulkWatSupExpM3_H6",
            "UnbilledCons_MetConsBulkWatSupExpM3_D6",
            "UnauthCons_IllegalConnDomEstNo_D6",
            "UnauthCons_IllegalConnDomPersPerHouse_H6",
            "UnauthCons_IllegalConnDomConsLitPerPersDay_J6",
            "UnauthCons_IllegalConnDomErrorMargin_F6",
            "UnauthCons_IllegalConnOthersErrorMargin_F10",
            "UnauthCons_MeterTampBypEtcEstNo_D14",
            "UnauthCons_MeterTampBypEtcErrorMargin_F14",
            "UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14",
            "MetErrors_DetailedManualSpec_J6",
            "MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8",
            "MetErrors_BilledMetConsWoBulkSupErrorMargin_N8",
            "MetErrors_MetBulkSupExpMetUnderreg_H32",
            "MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34",
            "MetErrors_CorruptMetReadPractMetUndrreg_H38",
            "Network_DistributionAndTransmissionMains_D7",
            "Network_NoOfConnOfRegCustomers_H10",
            "Network_NoOfInactAccountsWSvcConns_H18",
            "Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32",
            "Prs_ApproxNoOfConn_D7",
            "Prs_DailyAvgPrsM_F7",
        };

        #region Props input 

        private int _startPeriodDaysM21;
        private double _sysInputSystemInputVolumeM3D6;
        private double _sysInputSystemInputVolumeErrorF6;
        private double _billedConsBilledMetConsBulkWatSupExpM3D6;
        private double _billedConsBilledUnmetConsBulkWatSupExpM3H6;
        private double _unbilledConsMetConsBulkWatSupExpM3D6;
        private int _unauthConsIllegalConnDomEstNoD6;
        private double _unauthConsIllegalConnDomPersPerHouseH6;
        private double _unauthConsIllegalConnDomConsLitPerPersDayJ6;
        private double _unauthConsIllegalConnDomErrorMarginF6;
        private double _unauthConsIllegalConnOthersErrorMarginF10;
        private double _unauthConsMeterTampBypEtcEstNoD14;
        private double _unauthConsMeterTampBypEtcErrorMarginF14;
        private double _unauthConsMeterTampBypEtcConsLitPerCustDayJ14;
        private double _metErrorsDetailedManualSpecJ6;
        private double _metErrorsBilledMetConsWoBulkSupMetUndrregH8;
        private double _metErrorsBilledMetConsWoBulkSupErrorMarginN8;
        private double _metErrorsMetBulkSupExpMetUnderregH32;
        private double _metErrorsUnbillMetConsWoBulkSupplMetUndrregH34;
        private double _metErrorsCorruptMetReadPractMetUndrregH38;
        private double _networkDistributionAndTransmissionMainsD7;
        private double _networkNoOfConnOfRegCustomersH10;
        private double _networkNoOfInactAccountsWSvcConnsH18;
        private double _networkAvgLenOfSvcConnFromBoundaryToMeterMH32;
        private double _prsApproxNoOfConnD7;
        private double _prsDailyAvgPrsMF7;


        public int Start_PeriodDays_M21
        {
            get => _startPeriodDaysM21;
            set { _startPeriodDaysM21 = value; RaisePropertyChanged(nameof(Start_PeriodDays_M21)); CalculateExcel(); }
        }

        public double SysInput_SystemInputVolumeM3_D6
        {
            get => _sysInputSystemInputVolumeM3D6;
            set { _sysInputSystemInputVolumeM3D6 = value; RaisePropertyChanged(nameof(SysInput_SystemInputVolumeM3_D6)); CalculateExcel(); }
        }

        public double SysInput_SystemInputVolumeError_F6
        {
            get => _sysInputSystemInputVolumeErrorF6;
            set { _sysInputSystemInputVolumeErrorF6 = value; RaisePropertyChanged(nameof(SysInput_SystemInputVolumeError_F6)); CalculateExcel(); }
        }

        public double BilledCons_BilledMetConsBulkWatSupExpM3_D6
        {
            get => _billedConsBilledMetConsBulkWatSupExpM3D6;
            set { _billedConsBilledMetConsBulkWatSupExpM3D6 = value; RaisePropertyChanged(nameof(BilledCons_BilledMetConsBulkWatSupExpM3_D6)); CalculateExcel(); }
        }

        public double BilledCons_BilledUnmetConsBulkWatSupExpM3_H6
        {
            get => _billedConsBilledUnmetConsBulkWatSupExpM3H6;
            set { _billedConsBilledUnmetConsBulkWatSupExpM3H6 = value; RaisePropertyChanged(nameof(BilledCons_BilledUnmetConsBulkWatSupExpM3_H6)); CalculateExcel(); }
        }

        public double UnbilledCons_MetConsBulkWatSupExpM3_D6
        {
            get => _unbilledConsMetConsBulkWatSupExpM3D6;
            set { _unbilledConsMetConsBulkWatSupExpM3D6 = value; RaisePropertyChanged(nameof(UnbilledCons_MetConsBulkWatSupExpM3_D6)); CalculateExcel(); }
        }

        public int UnauthCons_IllegalConnDomEstNo_D6
        {
            get => _unauthConsIllegalConnDomEstNoD6;
            set { _unauthConsIllegalConnDomEstNoD6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomEstNo_D6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnDomPersPerHouse_H6
        {
            get => _unauthConsIllegalConnDomPersPerHouseH6;
            set { _unauthConsIllegalConnDomPersPerHouseH6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomPersPerHouse_H6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnDomConsLitPerPersDay_J6
        {
            get => _unauthConsIllegalConnDomConsLitPerPersDayJ6;
            set { _unauthConsIllegalConnDomConsLitPerPersDayJ6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomConsLitPerPersDay_J6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnDomErrorMargin_F6
        {
            get => _unauthConsIllegalConnDomErrorMarginF6;
            set { _unauthConsIllegalConnDomErrorMarginF6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomErrorMargin_F6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnOthersErrorMargin_F10
        {
            get => _unauthConsIllegalConnOthersErrorMarginF10;
            set { _unauthConsIllegalConnOthersErrorMarginF10 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnOthersErrorMargin_F10)); CalculateExcel(); }
        }

        public double UnauthCons_MeterTampBypEtcEstNo_D14
        {
            get => _unauthConsMeterTampBypEtcEstNoD14;
            set { _unauthConsMeterTampBypEtcEstNoD14 = value; RaisePropertyChanged(nameof(UnauthCons_MeterTampBypEtcEstNo_D14)); CalculateExcel(); }
        }

        public double UnauthCons_MeterTampBypEtcErrorMargin_F14
        {
            get => _unauthConsMeterTampBypEtcErrorMarginF14;
            set { _unauthConsMeterTampBypEtcErrorMarginF14 = value; RaisePropertyChanged(nameof(UnauthCons_MeterTampBypEtcErrorMargin_F14)); CalculateExcel(); }
        }

        public double UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14
        {
            get => _unauthConsMeterTampBypEtcConsLitPerCustDayJ14;
            set { _unauthConsMeterTampBypEtcConsLitPerCustDayJ14 = value; RaisePropertyChanged(nameof(UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14)); CalculateExcel(); }
        }

        public double MetErrors_DetailedManualSpec_J6
        {
            get => _metErrorsDetailedManualSpecJ6;
            set { _metErrorsDetailedManualSpecJ6 = value; RaisePropertyChanged(nameof(MetErrors_DetailedManualSpec_J6)); CalculateExcel(); }
        }

        public double MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8
        {
            get => _metErrorsBilledMetConsWoBulkSupMetUndrregH8;
            set { _metErrorsBilledMetConsWoBulkSupMetUndrregH8 = value; RaisePropertyChanged(nameof(MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8)); CalculateExcel(); }
        }

        public double MetErrors_BilledMetConsWoBulkSupErrorMargin_N8
        {
            get => _metErrorsBilledMetConsWoBulkSupErrorMarginN8;
            set { _metErrorsBilledMetConsWoBulkSupErrorMarginN8 = value; RaisePropertyChanged(nameof(MetErrors_BilledMetConsWoBulkSupErrorMargin_N8)); CalculateExcel(); }
        }

        public double MetErrors_MetBulkSupExpMetUnderreg_H32
        {
            get => _metErrorsMetBulkSupExpMetUnderregH32;
            set { _metErrorsMetBulkSupExpMetUnderregH32 = value; RaisePropertyChanged(nameof(MetErrors_MetBulkSupExpMetUnderreg_H32)); CalculateExcel(); }
        }

        public double MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34
        {
            get => _metErrorsUnbillMetConsWoBulkSupplMetUndrregH34;
            set { _metErrorsUnbillMetConsWoBulkSupplMetUndrregH34 = value; RaisePropertyChanged(nameof(MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34)); CalculateExcel(); }
        }

        public double MetErrors_CorruptMetReadPractMetUndrreg_H38
        {
            get => _metErrorsCorruptMetReadPractMetUndrregH38;
            set { _metErrorsCorruptMetReadPractMetUndrregH38 = value; RaisePropertyChanged(nameof(MetErrors_CorruptMetReadPractMetUndrreg_H38)); CalculateExcel(); }
        }

        public double Network_DistributionAndTransmissionMains_D7
        {
            get => _networkDistributionAndTransmissionMainsD7;
            set { _networkDistributionAndTransmissionMainsD7 = value; RaisePropertyChanged(nameof(Network_DistributionAndTransmissionMains_D7)); CalculateExcel(); }
        }

        public double Network_NoOfConnOfRegCustomers_H10
        {
            get => _networkNoOfConnOfRegCustomersH10;
            set { _networkNoOfConnOfRegCustomersH10 = value; RaisePropertyChanged(nameof(Network_NoOfConnOfRegCustomers_H10)); CalculateExcel(); }
        }

        public double Network_NoOfInactAccountsWSvcConns_H18
        {
            get => _networkNoOfInactAccountsWSvcConnsH18;
            set { _networkNoOfInactAccountsWSvcConnsH18 = value; RaisePropertyChanged(nameof(Network_NoOfInactAccountsWSvcConns_H18)); CalculateExcel(); }
        }

        public double Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32
        {
            get => _networkAvgLenOfSvcConnFromBoundaryToMeterMH32;
            set { _networkAvgLenOfSvcConnFromBoundaryToMeterMH32 = value; RaisePropertyChanged(nameof(Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32)); CalculateExcel(); }
        }

        public double Prs_ApproxNoOfConn_D7
        {
            get => _prsApproxNoOfConnD7;
            set { _prsApproxNoOfConnD7 = value; RaisePropertyChanged(nameof(Prs_ApproxNoOfConn_D7)); CalculateExcel(); }
        }

        public double Prs_DailyAvgPrsM_F7
        {
            get => _prsDailyAvgPrsMF7;
            set { _prsDailyAvgPrsMF7 = value; RaisePropertyChanged(nameof(Prs_DailyAvgPrsM_F7)); CalculateExcel(); }
        }

        #endregion Props input

        #region Props output

        private double _systemInputVolume_B19;
        private double _systemInputVolumeErrorMargin_B21;
        private double _authorizedConsumption_K12;
        private double _authorizedConsumptionErrorMargin_K15;
        private double _waterLosses_K29;
        private double _waterLossesErrorMargin_K31;
        private double _billedAuthorizedConsumption_T8;
        private double _unbilledAuthorizedConsumption_T16;
        private double _unbilledAuthorizedConsumptionErrorMargin_T20;
        private double _commercialLosses_T26;
        private double _commercialLossesErrorMargin_T29;
        private double _physicalLossesM3T34;
        private double _physcialLossesErrorMarginAh35;
        private double _billedMeteredConsumptionAc4;
        private double _billedUnmeteredConsumptionAc9;
        private double _unbilledMeteredConsumptionAc14;
        private double _unbilledUnmeteredConsumptionAc19;
        private double _unbilledUnmeteredConsumptionErrorMarginAo20;
        private double _unauthorizedConsumptionAc24;
        private double _unauthorizedConsumptionErrorMarginAo25;
        private double _customerMeterInaccuraciesAndErrorsM3Ac29;
        private double _customerMeterInaccuraciesAndErrorsErrorMarginAo30;
        private double _revenueWaterM3Ay8;
        private double _nonRevenueWaterM3Ay24;
        private double _nonRevenueWaterErrorMarginAy26;

        public double SystemInputVolume_B19
        {
            get { return _systemInputVolume_B19; }
            set { _systemInputVolume_B19 = value; RaisePropertyChanged(nameof(SystemInputVolume_B19)); }
        }

        public double SystemInputVolumeErrorMargin_B21
        {
            get { return _systemInputVolumeErrorMargin_B21; }
            set { _systemInputVolumeErrorMargin_B21 = value; RaisePropertyChanged(nameof(SystemInputVolumeErrorMargin_B21)); }
        }

        public double AuthorizedConsumption_K12
        {
            get => _authorizedConsumption_K12;
            set { _authorizedConsumption_K12 = value; RaisePropertyChanged(nameof(AuthorizedConsumption_K12)); }
        }
        public double AuthorizedConsumptionErrorMargin_K15
        {
            get => _authorizedConsumptionErrorMargin_K15;
            set { _authorizedConsumptionErrorMargin_K15 = value; RaisePropertyChanged(nameof(AuthorizedConsumptionErrorMargin_K15)); }
        }

        public double WaterLosses_K29
        {
            get => _waterLosses_K29;
            set { _waterLosses_K29 = value; RaisePropertyChanged(nameof(WaterLosses_K29)); }
        }

        public double WaterLossesErrorMargin_K31
        {
            get => _waterLossesErrorMargin_K31;
            set { _waterLossesErrorMargin_K31 = value; RaisePropertyChanged(nameof(WaterLossesErrorMargin_K31)); }
        }

        public double BilledAuthorizedConsumption_T8
        {
            get => _billedAuthorizedConsumption_T8;
            set { _billedAuthorizedConsumption_T8 = value; RaisePropertyChanged(nameof(BilledAuthorizedConsumption_T8)); }
        }

        public double UnbilledAuthorizedConsumption_T16
        {
            get => _unbilledAuthorizedConsumption_T16;
            set { _unbilledAuthorizedConsumption_T16 = value; RaisePropertyChanged(nameof(UnbilledAuthorizedConsumption_T16)); }
        }

        public double UnbilledAuthorizedConsumptionErrorMargin_T20
        {
            get => _unbilledAuthorizedConsumptionErrorMargin_T20;
            set { _unbilledAuthorizedConsumptionErrorMargin_T20 = value; RaisePropertyChanged(nameof(UnbilledAuthorizedConsumptionErrorMargin_T20)); }
        }

        public double CommercialLosses_T26
        {
            get => _commercialLosses_T26;
            set { _commercialLosses_T26 = value; RaisePropertyChanged(nameof(CommercialLosses_T26)); }
        }

        public double CommercialLossesErrorMargin_T29
        {
            get => _commercialLossesErrorMargin_T29;
            set { _commercialLossesErrorMargin_T29 = value; RaisePropertyChanged(nameof(CommercialLossesErrorMargin_T29)); }
        }

        public double PhysicalLossesM3_T34
        {
            get => _physicalLossesM3T34;
            set { _physicalLossesM3T34 = value; RaisePropertyChanged(nameof(PhysicalLossesM3_T34)); }
        }

        public double PhyscialLossesErrorMargin_AH35
        {
            get => _physcialLossesErrorMarginAh35;
            set { _physcialLossesErrorMarginAh35 = value; RaisePropertyChanged(nameof(PhyscialLossesErrorMargin_AH35)); }
        }

        public double BilledMeteredConsumption_AC4
        {
            get => _billedMeteredConsumptionAc4;
            set { _billedMeteredConsumptionAc4 = value; RaisePropertyChanged(nameof(BilledMeteredConsumption_AC4)); }
        }

        public double BilledUnmeteredConsumption_AC9
        {
            get => _billedUnmeteredConsumptionAc9;
            set { _billedUnmeteredConsumptionAc9 = value; RaisePropertyChanged(nameof(BilledUnmeteredConsumption_AC9)); }
        }

        public double UnbilledMeteredConsumption_AC14
        {
            get => _unbilledMeteredConsumptionAc14;
            set { _unbilledMeteredConsumptionAc14 = value; RaisePropertyChanged(nameof(UnbilledMeteredConsumption_AC14)); }
        }

        public double UnbilledUnmeteredConsumption_AC19
        {
            get => _unbilledUnmeteredConsumptionAc19;
            set { _unbilledUnmeteredConsumptionAc19 = value; RaisePropertyChanged(nameof(UnbilledUnmeteredConsumption_AC19)); }
        }

        public double UnbilledUnmeteredConsumptionErrorMargin_AO20
        {
            get => _unbilledUnmeteredConsumptionErrorMarginAo20;
            set { _unbilledUnmeteredConsumptionErrorMarginAo20 = value; RaisePropertyChanged(nameof(UnbilledUnmeteredConsumptionErrorMargin_AO20)); }
        }

        public double UnauthorizedConsumption_AC24
        {
            get => _unauthorizedConsumptionAc24;
            set { _unauthorizedConsumptionAc24 = value; RaisePropertyChanged(nameof(UnauthorizedConsumption_AC24)); }
        }

        public double UnauthorizedConsumptionErrorMargin_AO25
        {
            get => _unauthorizedConsumptionErrorMarginAo25;
            set { _unauthorizedConsumptionErrorMarginAo25 = value; RaisePropertyChanged(nameof(UnauthorizedConsumptionErrorMargin_AO25)); }
        }

        public double CustomerMeterInaccuraciesAndErrorsM3_AC29
        {
            get => _customerMeterInaccuraciesAndErrorsM3Ac29;
            set { _customerMeterInaccuraciesAndErrorsM3Ac29 = value; RaisePropertyChanged(nameof(CustomerMeterInaccuraciesAndErrorsM3_AC29)); }
        }

        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30
        {
            get => _customerMeterInaccuraciesAndErrorsErrorMarginAo30;
            set { _customerMeterInaccuraciesAndErrorsErrorMarginAo30 = value; RaisePropertyChanged(nameof(CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30)); }
        }

        public double RevenueWaterM3_AY8
        {
            get => _revenueWaterM3Ay8;
            set { _revenueWaterM3Ay8 = value; RaisePropertyChanged(nameof(RevenueWaterM3_AY8)); }
        }

        public double NonRevenueWaterM3_AY24
        {
            get => _nonRevenueWaterM3Ay24;
            set { _nonRevenueWaterM3Ay24 = value; RaisePropertyChanged(nameof(NonRevenueWaterM3_AY24)); }
        }

        public double NonRevenueWaterErrorMargin_AY26
        {
            get => _nonRevenueWaterErrorMarginAy26;
            set { _nonRevenueWaterErrorMarginAy26 = value; RaisePropertyChanged(nameof(NonRevenueWaterErrorMargin_AY26)); }
        }

        #endregion

        public List<string> YearList { get; set; }
        private string _selectedYear;
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                RaisePropertyChanged(nameof(SelectedYear));
            }
        }

        public List<string> MonthList { get; set; }
        private string _selectedMonth;
        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                RaisePropertyChanged(nameof(SelectedMonth));
            }
        }

        //public List<string> ZoneList { get; set; }
        //private string _selectedZoneId;
        //public string SelectedZoneId
        //{
        //    get => _selectedZoneId;
        //    set
        //    {
        //        _selectedZoneId = value;
        //        RaisePropertyChanged(nameof(SelectedZoneId));
        //    }
        //}

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

        public RelayCommand CountBaseOnExcelCmd { get; }
        private void CountBaseOnExcelExecute()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                //IsFolderPicker = true,
                //InitialDirectory = folderName
                EnsurePathExists = true,
                EnsureFileExists = true,
                Filters =
                {
                    new CommonFileDialogFilter("Excel Files", "*.xls,*.xlsm"),
                    //new CommonFileDialogFilter("XML Files", "*.xml"),
                }
            };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string fileName = dialog.FileName;
                ImportFromExcel(fileName);
            }

        }
        public bool CountBaseOnExcelCanExecute()
        {
            return true;
        }

        public RelayCommand ExportToExcelCmd { get; }
        private void ExportToExcelExecute()
        {
            ExportToExcel();
        }
        public bool ExportToExcelCanExecute()
        {
            return true;
        }

        public RelayCommand LoadDataFromSystemCmd { get; }
        private void LoadDataFromSystemExecute()
        {
            LoadDataFromSystem();
        }
        public bool LoadDataFromSystemCanExecute()
        {
            return true;
        }

        public RelayCommand LoadDataFromScadaCmd { get; }
        private void LoadDataFromScadaExecute()
        {
            MessageBox.Show("Not implemented yet.", "Info");
        }
        public bool LoadDataFromScadaCanExecute()
        {
            return true;
        }

        public RelayCommand SaveDataToScadaCmd { get; }
        private void SaveDataToScadaExecute()
        {
            SaveDataToScada();
        }
        public bool SaveDataToScadaCanExecute()
        {
            return true;
        }

        private void SaveDataToScada()
        {
            RunOpcPublish();
        }
        private void RunOpcPublish()
        {
            try
            {
                string zoneId = SelectedZoneItem.ZoneRomanNo;
                ICollection<OpcValue> values = new List<OpcValue>()
                {
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.SysInput_SystemInputVolumeM3_D6", Value = SysInput_SystemInputVolumeM3_D6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.SysInput_SystemInputVolumeError_F6", Value = SysInput_SystemInputVolumeError_F6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.BilledCons_BilledMetConsBulkWatSupExpM3_D6", Value = BilledCons_BilledMetConsBulkWatSupExpM3_D6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6", Value = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnbilledCons_MetConsBulkWatSupExpM3_D6", Value = UnbilledCons_MetConsBulkWatSupExpM3_D6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_IllegalConnDomEstNo_D6", Value = UnauthCons_IllegalConnDomEstNo_D6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_IllegalConnDomPersPerHouse_H6", Value = UnauthCons_IllegalConnDomPersPerHouse_H6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_IllegalConnDomConsLitPerPersDay_J6", Value = UnauthCons_IllegalConnDomConsLitPerPersDay_J6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_IllegalConnDomErrorMargin_F6", Value = UnauthCons_IllegalConnDomErrorMargin_F6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_IllegalConnOthersErrorMargin_F10", Value = UnauthCons_IllegalConnOthersErrorMargin_F10},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_MeterTampBypEtcEstNo_D14", Value = UnauthCons_MeterTampBypEtcEstNo_D14},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_MeterTampBypEtcErrorMargin_F14", Value = UnauthCons_MeterTampBypEtcErrorMargin_F14},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14", Value = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.MetErrors_DetailedManualSpec_J6", Value = MetErrors_DetailedManualSpec_J6},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8", Value = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8", Value = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.MetErrors_MetBulkSupExpMetUnderreg_H32", Value = MetErrors_MetBulkSupExpMetUnderreg_H32},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34", Value = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.MetErrors_CorruptMetReadPractMetUndrreg_H38", Value = MetErrors_CorruptMetReadPractMetUndrreg_H38},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.Network_DistributionAndTransmissionMains_D7", Value = Network_DistributionAndTransmissionMains_D7},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.Network_NoOfConnOfRegCustomers_H10", Value = Network_NoOfConnOfRegCustomers_H10},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.Network_NoOfInactAccountsWSvcConns_H18", Value = Network_NoOfInactAccountsWSvcConns_H18},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32", Value = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.Prs_ApproxNoOfConn_D7", Value = Prs_ApproxNoOfConn_D7},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.Prs_DailyAvgPrsM_F7", Value = Prs_DailyAvgPrsM_F7},

                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_SystemInputVolume_B19", Value = SystemInputVolume_B19},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_SystemInputVolumeErrorMargin_B21", Value = SystemInputVolumeErrorMargin_B21},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_AuthorizedConsumption_K12", Value = AuthorizedConsumption_K12},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_AuthorizedConsumptionErrorMargin_K15", Value = AuthorizedConsumptionErrorMargin_K15},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_WaterLosses_K29", Value = WaterLosses_K29},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_WaterLossesErrorMargin_K31", Value = WaterLossesErrorMargin_K31},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_BilledAuthorizedConsumption_T8", Value = BilledAuthorizedConsumption_T8},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_UnbilledAuthorizedConsumption_T16", Value = UnbilledAuthorizedConsumption_T16},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_CommercialLosses_T26", Value = CommercialLosses_T26},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_CommercialLossesErrorMargin_T29", Value = CommercialLossesErrorMargin_T29},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_PhysicalLossesM3_T34", Value = PhysicalLossesM3_T34},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_PhyscialLossesErrorMargin_AH35", Value = PhyscialLossesErrorMargin_AH35},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_BilledMeteredConsumption_AC4", Value = BilledMeteredConsumption_AC4},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_BilledUnmeteredConsumption_AC9", Value = BilledUnmeteredConsumption_AC9},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_UnbilledMeteredConsumption_AC14", Value = UnbilledMeteredConsumption_AC14},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_UnbilledUnmeteredConsumption_AC19", Value = UnbilledUnmeteredConsumption_AC19},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_UnbilledUnmeteredConsumptionErrorMargin_AO20", Value = UnbilledUnmeteredConsumptionErrorMargin_AO20},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_UnauthorizedConsumption_AC24", Value = UnauthorizedConsumption_AC24},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_UnauthorizedConsumptionErrorMargin_AO25", Value = UnauthorizedConsumptionErrorMargin_AO25},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_CustomerMeterInaccuraciesAndErrorsM3_AC29", Value = CustomerMeterInaccuraciesAndErrorsM3_AC29},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", Value = CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_RevenueWaterM3_AY8", Value = RevenueWaterM3_AY8},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_NonRevenueWaterM3_AY24", Value = NonRevenueWaterM3_AY24},
                    new OpcValue(){Tag=$"WbEasyCalc_{zoneId}.DEV.WatBal_NonRevenueWaterErrorMargin_AY26", Value = NonRevenueWaterErrorMargin_AY26},
                };
                string opcAddress = ConfigurationManager.AppSettings["opcAddress"];
                using (var client = new OpcReader(opcAddress))
                {
                    //log.Log(LogLevel.Info, "OpcValue start ---------------------------");
                    //foreach (var opcValue in values)
                    //{
                    //    log.Log(LogLevel.Info, opcValue);
                    //}
                    //log.Log(LogLevel.Info, "OpcValue end -----------------------------");
                    ////client.WriteValues(values);
     
                    //foreach (var opcValue in values)
                    //{
                    //    opcValue.Value = 0;
                    //}
                    client.WriteValues(values);
                    MessageBox.Show("Data were saved in SCADA.","Info");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "OPC Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }


        private void LoadDataFromSystem()
        {
            try
            {
                SystemDataRepo systemDataRepo = new SystemDataRepo();
                //var dt = systemDataRepo.GetSystemData(Convert.ToInt32(SelectedYear), Convert.ToInt32(SelectedMonth), Convert.ToInt32(SelectedZoneId.Substring(0,1)));
                var dt = systemDataRepo.GetSystemData(Convert.ToInt32(SelectedYear), Convert.ToInt32(SelectedMonth), SelectedZoneItem.ZoneId);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data for this zone in this time period.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                SysInput_SystemInputVolumeM3_D6 = Convert.ToInt32(dt.Rows[0][6].ToString());
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = Convert.ToInt32(dt.Rows[0][3].ToString());
                Network_DistributionAndTransmissionMains_D7 = (double)Convert.ToDecimal(dt.Rows[0][4].ToString());
                Network_NoOfConnOfRegCustomers_H10 = Convert.ToInt32(dt.Rows[0][5].ToString());

                CalculateExcel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public MainWindowViewModel()
        {
            CountBaseOnExcelCmd = new RelayCommand(CountBaseOnExcelExecute, CountBaseOnExcelCanExecute);
            ExportToExcelCmd = new RelayCommand(ExportToExcelExecute, ExportToExcelCanExecute);
            LoadDataFromSystemCmd = new RelayCommand(LoadDataFromSystemExecute, LoadDataFromSystemCanExecute);
            LoadDataFromScadaCmd = new RelayCommand(LoadDataFromScadaExecute, LoadDataFromScadaCanExecute);
            SaveDataToScadaCmd = new RelayCommand(SaveDataToScadaExecute, SaveDataToScadaCanExecute);

            YearList = new List<string>() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025",};
            SelectedYear = "2020";
            MonthList = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
            SelectedMonth = "9";

            //ZoneList = new List<string>()
            //{
            //    "1 - Przybków",
            //    "2 - Stare Miasto",
            //    "3 - Kopernik",
            //    "4 - Piekary",
            //    "5 - Północna",
            //    "6 - ZPW",
            //    "7 - Tranzyt",
            //    "8 - Zbiorniki",
            //    "9 - Huta",
            //};
            //SelectedZoneId = "1 - Przybków";

            ZoneItemList = new List<ZoneItem>()
            {
                new ZoneItem(){ZoneId=1,ZoneName = "1 - Przybków", ZoneRomanNo="I"},
                new ZoneItem(){ZoneId=1,ZoneName = "2 - Stare Miasto", ZoneRomanNo="II"},
                new ZoneItem(){ZoneId=1,ZoneName = "3 - Kopernik", ZoneRomanNo="III"},
                new ZoneItem(){ZoneId=1,ZoneName = "4 - Piekary", ZoneRomanNo="IV"},
                new ZoneItem(){ZoneId=1,ZoneName = "5 - Północna", ZoneRomanNo="V"},
                new ZoneItem(){ZoneId=1,ZoneName = "6 - ZPW", ZoneRomanNo="VI"},
                new ZoneItem(){ZoneId=1,ZoneName = "7 - Tranzyt", ZoneRomanNo="VII"},
                new ZoneItem(){ZoneId=1,ZoneName = "8 - Zbiorniki", ZoneRomanNo="VIII"},
                new ZoneItem(){ZoneId=1,ZoneName = "9 - Huta", ZoneRomanNo="IX"},
            };
            SelectedZoneItem = ZoneItemList[0];

            //SystemInputVolume_B19 = 7234234.23;
            //AuthorizedConsumption_K12 = 0.0;
        }


        private void ExportToExcel()
        {
            EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
            {
                Start_PeriodDays_M21 = Start_PeriodDays_M21,
                SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
                SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,
                UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
                UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
                UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,
                UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
                MetErrors_DetailedManualSpec_J6 = Math.Abs(MetErrors_DetailedManualSpec_J6 - 2) < 0.1,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,
                MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
                Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
                Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
                Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
                Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
                Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7
            };

            var ddddd = easyCalcDataInput;
        }

        private void ImportFromExcel(string excelFileName)
        {
            try
            {
                var excelReader = new ExcelReader(excelFileName);

                EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
                {
                    Start_PeriodDays_M21 = excelReader.ReadCell<int>("Start", "M21"),
                    SysInput_SystemInputVolumeM3_D6 = excelReader.ReadCell<double>("Sys. Input", "D6"),
                    SysInput_SystemInputVolumeError_F6 = excelReader.ReadCell<double>("Sys. Input", "F6"),
                    BilledCons_BilledMetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Billed Cons", "D6"),
                    BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = excelReader.ReadCell<double>("Billed Cons", "H6"),
                    UnbilledCons_MetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Unb. Cons.", "D6"),
                    UnauthCons_IllegalConnDomEstNo_D6 = excelReader.ReadCell<int>("Unauth. Cons.", "D6"),
                    UnauthCons_IllegalConnDomPersPerHouse_H6 = excelReader.ReadCell<double>("Unauth. Cons.", "H6"),
                    UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = excelReader.ReadCell<double>("Unauth. Cons.", "J6"),
                    UnauthCons_IllegalConnDomErrorMargin_F6 = excelReader.ReadCell<double>("Unauth. Cons.", "F6"),
                    UnauthCons_IllegalConnOthersErrorMargin_F10 = excelReader.ReadCell<double>("Unauth. Cons.", "F10"),
                    UnauthCons_MeterTampBypEtcEstNo_D14 = excelReader.ReadCell<double>("Unauth. Cons.", "D14"),
                    UnauthCons_MeterTampBypEtcErrorMargin_F14 = excelReader.ReadCell<double>("Unauth. Cons.", "F14"),
                    UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = excelReader.ReadCell<double>("Unauth. Cons.", "J14"),
                    MetErrors_DetailedManualSpec_J6 = excelReader.ReadCell<int>("Meter Errors", "J6") == 2,
                    MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = excelReader.ReadCell<double>("Meter Errors", "H8"),
                    MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = excelReader.ReadCell<double>("Meter Errors", "N8"),
                    MetErrors_MetBulkSupExpMetUnderreg_H32 = excelReader.ReadCell<double>("Meter Errors", "H32"),
                    MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = excelReader.ReadCell<double>("Meter Errors", "H34"),
                    MetErrors_CorruptMetReadPractMetUndrreg_H38 = excelReader.ReadCell<double>("Meter Errors", "H38"),
                    Network_DistributionAndTransmissionMains_D7 = excelReader.ReadCell<double>("Network", "D7"),
                    Network_NoOfConnOfRegCustomers_H10 = excelReader.ReadCell<double>("Network", "H10"),
                    Network_NoOfInactAccountsWSvcConns_H18 = excelReader.ReadCell<double>("Network", "H18"),
                    Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = excelReader.ReadCell<double>("Network", "H32"),
                    Prs_ApproxNoOfConn_D7 = excelReader.ReadCell<double>("Pressure", "D7"),
                    Prs_DailyAvgPrsM_F7 = excelReader.ReadCell<double>("Pressure", "F7")
                };

                //Start_PeriodDays_M21 = excelReader.ReadCell<int>("Start", "M21");
                //SysInput_SystemInputVolumeM3_D6 = excelReader.ReadCell<double>("Sys. Input", "D6");
                //SysInput_SystemInputVolumeError_F6 = excelReader.ReadCell<double>("Sys. Input", "F6");
                //BilledCons_BilledMetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Billed Cons", "D6");
                //BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = excelReader.ReadCell<double>("Billed Cons", "H6");
                //UnbilledCons_MetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Unb. Cons.", "D6");
                //UnauthCons_IllegalConnDomEstNo_D6 = excelReader.ReadCell<int>("Unauth. Cons.", "D6");
                //UnauthCons_IllegalConnDomPersPerHouse_H6 = excelReader.ReadCell<double>("Unauth. Cons.", "H6");
                //UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = excelReader.ReadCell<double>("Unauth. Cons.", "J6");
                //UnauthCons_IllegalConnDomErrorMargin_F6 = excelReader.ReadCell<double>("Unauth. Cons.", "F6");
                //UnauthCons_IllegalConnOthersErrorMargin_F10 = excelReader.ReadCell<double>("Unauth. Cons.", "F10");
                //UnauthCons_MeterTampBypEtcEstNo_D14 = excelReader.ReadCell<double>("Unauth. Cons.", "D14");
                //UnauthCons_MeterTampBypEtcErrorMargin_F14 = excelReader.ReadCell<double>("Unauth. Cons.", "F14");
                //UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = excelReader.ReadCell<double>("Unauth. Cons.", "J14");
                //MetErrors_DetailedManualSpec_J6 = excelReader.ReadCell<int>("Meter Errors", "J6");
                //MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = excelReader.ReadCell<double>("Meter Errors", "H8");
                //MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = excelReader.ReadCell<double>("Meter Errors", "N8");
                //MetErrors_MetBulkSupExpMetUnderreg_H32 = excelReader.ReadCell<double>("Meter Errors", "H32");
                //MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = excelReader.ReadCell<double>("Meter Errors", "H34");
                //MetErrors_CorruptMetReadPractMetUndrreg_H38 = excelReader.ReadCell<double>("Meter Errors", "H38");
                //Network_DistributionAndTransmissionMains_D7 = excelReader.ReadCell<double>("Network", "D7");
                //Network_NoOfConnOfRegCustomers_H10 = excelReader.ReadCell<double>("Network", "H10");
                //Network_NoOfInactAccountsWSvcConns_H18 = excelReader.ReadCell<double>("Network", "H18");
                //Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = excelReader.ReadCell<double>("Network", "H32");
                //Prs_ApproxNoOfConn_D7 = excelReader.ReadCell<double>("Pressure", "D7");
                //Prs_DailyAvgPrsM_F7 = excelReader.ReadCell<double>("Pressure", "F7");

                Start_PeriodDays_M21 = easyCalcDataInput.Start_PeriodDays_M21;
                SysInput_SystemInputVolumeM3_D6 = easyCalcDataInput.SysInput_SystemInputVolumeM3_D6;
                SysInput_SystemInputVolumeError_F6 = easyCalcDataInput.SysInput_SystemInputVolumeError_F6;
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6;
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6;
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6;
                UnauthCons_IllegalConnDomEstNo_D6 = easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6;
                UnauthCons_IllegalConnDomPersPerHouse_H6 = easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6;
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6;
                UnauthCons_IllegalConnDomErrorMargin_F6 = easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6;
                UnauthCons_IllegalConnOthersErrorMargin_F10 = easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10;
                UnauthCons_MeterTampBypEtcEstNo_D14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14;
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14;
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14;
                MetErrors_DetailedManualSpec_J6 = easyCalcDataInput.MetErrors_DetailedManualSpec_J6 ? 2 :1;
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8;
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8;
                MetErrors_MetBulkSupExpMetUnderreg_H32 = easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32;
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34;
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38;
                Network_DistributionAndTransmissionMains_D7 = easyCalcDataInput.Network_DistributionAndTransmissionMains_D7;
                Network_NoOfConnOfRegCustomers_H10 = easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10;
                Network_NoOfInactAccountsWSvcConns_H18 = easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18;
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32;
                Prs_ApproxNoOfConn_D7 = easyCalcDataInput.Prs_ApproxNoOfConn_D7;
                Prs_DailyAvgPrsM_F7 = easyCalcDataInput.Prs_DailyAvgPrsM_F7;

                CalculateExcel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CalculateExcel()
        {
            try
            {
                EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
                {
                    Start_PeriodDays_M21 = Start_PeriodDays_M21,
                    SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
                    SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
                    BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                    BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
                    UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,
                    UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
                    UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
                    UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
                    UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                    UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,
                    UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
                    UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
                    UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
                    MetErrors_DetailedManualSpec_J6 = Math.Abs(MetErrors_DetailedManualSpec_J6 - 2) < 0.1,
                    MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                    MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,
                    MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
                    MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                    MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
                    Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
                    Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
                    Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
                    Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
                    Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
                    Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7
                };

                EasyCalcDataReaderMoq easyCalcDataReaderMoq = new EasyCalcDataReaderMoq();
                EasyCalcDataOutput easyCalcDataOutput = easyCalcDataReaderMoq.ReadEasyCalcDataOutput(easyCalcDataInput);

                SystemInputVolume_B19 = easyCalcDataOutput.SystemInputVolume_B19;
                SystemInputVolumeErrorMargin_B21 = easyCalcDataOutput.SystemInputVolumeErrorMargin_B21;

                AuthorizedConsumption_K12 = easyCalcDataOutput.AuthorizedConsumption_K12;
                AuthorizedConsumptionErrorMargin_K15 = easyCalcDataOutput.AuthorizedConsumptionErrorMargin_K15;
                WaterLosses_K29 = easyCalcDataOutput.WaterLosses_K29;
                WaterLossesErrorMargin_K31 = easyCalcDataOutput.WaterLossesErrorMargin_K31;

                BilledAuthorizedConsumption_T8 = easyCalcDataOutput.BilledAuthorizedConsumption_T8;
                UnbilledAuthorizedConsumption_T16 = easyCalcDataOutput.UnbilledAuthorizedConsumption_T16;
                UnbilledAuthorizedConsumptionErrorMargin_T20 = easyCalcDataOutput.UnbilledAuthorizedConsumptionErrorMargin_T20;
                CommercialLosses_T26 = easyCalcDataOutput.CommercialLosses_T26;
                CommercialLossesErrorMargin_T29 = easyCalcDataOutput.CommercialLossesErrorMargin_T29;
                PhysicalLossesM3_T34 = easyCalcDataOutput.PhysicalLossesM3_T34;
                PhyscialLossesErrorMargin_AH35 = easyCalcDataOutput.PhyscialLossesErrorMargin_AH35;

                BilledMeteredConsumption_AC4 = easyCalcDataOutput.BilledMeteredConsumption_AC4;
                BilledUnmeteredConsumption_AC9 = easyCalcDataOutput.BilledUnmeteredConsumption_AC9;
                UnbilledMeteredConsumption_AC14 = easyCalcDataOutput.UnbilledMeteredConsumption_AC14;

                UnbilledUnmeteredConsumption_AC19 = easyCalcDataOutput.UnbilledUnmeteredConsumption_AC19;
                UnbilledUnmeteredConsumptionErrorMargin_AO20 = easyCalcDataOutput.UnbilledUnmeteredConsumptionErrorMargin_AO20;
                UnauthorizedConsumption_AC24 = easyCalcDataOutput.UnauthorizedConsumption_AC24;
                UnauthorizedConsumptionErrorMargin_AO25 = easyCalcDataOutput.UnauthorizedConsumptionErrorMargin_AO25;
                CustomerMeterInaccuraciesAndErrorsM3_AC29 = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsM3_AC29;
                CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30;

                RevenueWaterM3_AY8 = easyCalcDataOutput.RevenueWaterM3_AY8;
                NonRevenueWaterM3_AY24 = easyCalcDataOutput.NonRevenueWaterM3_AY24;
                NonRevenueWaterErrorMargin_AY26 = easyCalcDataOutput.NonRevenueWaterErrorMargin_AY26;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
