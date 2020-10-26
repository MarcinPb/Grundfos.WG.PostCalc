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
        public List<IdNamePair> MonthList { get; set; }
        public List<ZoneItem> ZoneItemList { get; set; }

        #region Commands: LoadDataFromSystemCmd, ImportFromExcelExecute, ImportFromExcelCanExecute

        public RelayCommand LoadDataFromSystemCmd => new RelayCommand(LoadDataFromSystemExecute, LoadDataFromSystemCanExecute);
        private void LoadDataFromSystemExecute()
        {
            LoadDataFromSystem();
        }
        public bool LoadDataFromSystemCanExecute()
        {
            return true;
        }

        public RelayCommand ImportFromExcelCmd => new RelayCommand(ImportFromExcelExecute, ImportFromExcelCanExecute);
        private void ImportFromExcelExecute()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                EnsurePathExists = true,
                EnsureFileExists = true,
                Filters =
                {
                    new CommonFileDialogFilter("Excel Files", "*.xls,*.xlsm"),
                }
            };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string fileName = dialog.FileName;
                ImportFromExcel(fileName);
            }

        }
        public bool ImportFromExcelCanExecute()
        {
            return true;
        }

        public RelayCommand ExportToExcelCmd => new RelayCommand(ExportToExcelExecute, ExportToExcelCanExecute);
        private void ExportToExcelExecute()
        {
            CommonSaveFileDialog dialog = new CommonSaveFileDialog
            {
                EnsurePathExists = true,
                EnsureFileExists = true,
                DefaultExtension = ".xlsm",
                AlwaysAppendDefaultExtension = true,
                Filters =
                {
                    //new CommonFileDialogFilter("Excel Files", "*.xls,*.xlsm"),
                    new CommonFileDialogFilter("Excel Files", "*.xlsm"),
                }
            };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string fileName = dialog.FileName;
                ExportToExcel(fileName);
            }
        }
        public bool ExportToExcelCanExecute()
        {
            return true;
        }


        #endregion


        public EditedViewModel(int id)
        {
            Model = new ItemViewModel(GlobalConfig.DataRepository.WbEasyCalcDataListRepository.GetItem(id));

            YearList = GlobalConfig.DataRepository.YearList;
            MonthList = GlobalConfig.DataRepository.MonthList;
            ZoneItemList = GlobalConfig.DataRepository.ZoneList;
        }

        private void LoadDataFromSystem()
        {
            try
            {
                var gisModelScadaData = GlobalConfig.DataRepository.GetGisModelScadaData(Model.YearNo, Model.MonthNo, Model.ZoneId);

                Model.SysInput_SystemInputVolumeM3_D6 = gisModelScadaData.SystemInputVolume;
                Model.BilledCons_BilledMetConsBulkWatSupExpM3_D6 = gisModelScadaData.ZoneSale;
                Model.Network_DistributionAndTransmissionMains_D7 = gisModelScadaData.NetworkLength;
                Model.Network_NoOfConnOfRegCustomers_H10 = gisModelScadaData.CustomersQuantity;

                Model.CalculateExcel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToExcel(string excelFileName)
        {
            try
            {
                EasyCalcDataInput easyCalcDataInput = Model.Model.EasyCalcDataInput;
                GlobalConfig.WbEasyCalcExcel.SaveToExcelFile(excelFileName, easyCalcDataInput);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ImportFromExcel(string excelFileName)
        {
            try
            {
                EasyCalcDataInput easyCalcDataInput = GlobalConfig.WbEasyCalcExcel.LoadFromExcelFile(excelFileName);
                MapEasyCalcDataInput(easyCalcDataInput);
                //Model.Model.EasyCalcDataInput = easyCalcDataInput;
                //Model.CalculateExcel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MapEasyCalcDataInput(EasyCalcDataInput easyCalcDataInput)
        {
            Model.Start_PeriodDays_M21 = easyCalcDataInput.Start_PeriodDays_M21;
            Model.SysInput_SystemInputVolumeM3_D6 = easyCalcDataInput.SysInput_SystemInputVolumeM3_D6;
            Model.SysInput_SystemInputVolumeError_F6 = easyCalcDataInput.SysInput_SystemInputVolumeError_F6;
            Model.BilledCons_BilledMetConsBulkWatSupExpM3_D6 = easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6;
            Model.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6;
            Model.UnbilledCons_MetConsBulkWatSupExpM3_D6 = easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6;
            Model.UnauthCons_IllegalConnDomEstNo_D6 = easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6;
            Model.UnauthCons_IllegalConnDomPersPerHouse_H6 = easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6;
            Model.UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6;
            Model.UnauthCons_IllegalConnDomErrorMargin_F6 = easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6;
            Model.UnauthCons_IllegalConnOthersErrorMargin_F10 = easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10;
            Model.UnauthCons_MeterTampBypEtcEstNo_D14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14;
            Model.UnauthCons_MeterTampBypEtcErrorMargin_F14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14;
            Model.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14;
            Model.MetErrors_DetailedManualSpec_J6 = easyCalcDataInput.MetErrors_DetailedManualSpec_J6 ? 2 : 1;
            Model.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8;
            Model.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8;
            Model.MetErrors_MetBulkSupExpMetUnderreg_H32 = easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32;
            Model.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34;
            Model.MetErrors_CorruptMetReadPractMetUndrreg_H38 = easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38;
            Model.Network_DistributionAndTransmissionMains_D7 = easyCalcDataInput.Network_DistributionAndTransmissionMains_D7;
            Model.Network_NoOfConnOfRegCustomers_H10 = easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10;
            Model.Network_NoOfInactAccountsWSvcConns_H18 = easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18;
            Model.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32;
            Model.Prs_ApproxNoOfConn_D7 = easyCalcDataInput.Prs_ApproxNoOfConn_D7;
            Model.Prs_DailyAvgPrsM_F7 = easyCalcDataInput.Prs_DailyAvgPrsM_F7;
        }
    }
}
