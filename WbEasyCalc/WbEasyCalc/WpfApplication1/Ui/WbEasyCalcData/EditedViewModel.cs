﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataModel;
using DataRepository;
using GlobalRepository;
using Microsoft.WindowsAPICodePack.Dialogs;
using WbEasyCalcModel;
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
        public List<IdNamePair> J6_List { get; set; }

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

            J6_List = new List<IdNamePair> { 
                new IdNamePair(){ Id=1, Name="1"},
                new IdNamePair(){ Id=2, Name="2"},
            };
        }

        private void LoadDataFromSystem()
        {
            try
            {
                // Invoke spGisModelScadaData on SQL Server.
                var wbEasyCalcData = GlobalConfig.DataRepository.GetAutomaticData(Model.YearNo, Model.MonthNo, Model.ZoneId);

                if (Model.MonthNo < 13)
                {
                    Model.SysInput_SystemInputVolumeM3_D6 = wbEasyCalcData.SysInput_SystemInputVolumeM3_D6;
                    Model.BilledCons_BilledMetConsBulkWatSupExpM3_D6 = wbEasyCalcData.BilledCons_BilledMetConsBulkWatSupExpM3_D6;
                    Model.Network_DistributionAndTransmissionMains_D7 = wbEasyCalcData.Network_DistributionAndTransmissionMains_D7;
                    Model.Network_NoOfConnOfRegCustomers_H10 = wbEasyCalcData.Network_NoOfConnOfRegCustomers_H10;
                }
                // Sum parameters for all year.
                else
                {
                    Model.Start_PeriodDays_M21 = wbEasyCalcData.Start_PeriodDays_M21;

                    Model.SysInput_Desc_B6 = wbEasyCalcData.SysInput_Desc_B6;
                    Model.SysInput_Desc_B7 = wbEasyCalcData.SysInput_Desc_B7;
                    Model.SysInput_Desc_B8 = wbEasyCalcData.SysInput_Desc_B8;
                    Model.SysInput_Desc_B9 = wbEasyCalcData.SysInput_Desc_B9;
                    Model.SysInput_SystemInputVolumeM3_D6 = wbEasyCalcData.SysInput_SystemInputVolumeM3_D6;                                     // @SystemInputVolume
                    Model.SysInput_SystemInputVolumeError_F6 = wbEasyCalcData.SysInput_SystemInputVolumeError_F6;
                    Model.SysInput_SystemInputVolumeM3_D7 = wbEasyCalcData.SysInput_SystemInputVolumeM3_D7;                                     
                    Model.SysInput_SystemInputVolumeError_F7 = wbEasyCalcData.SysInput_SystemInputVolumeError_F7;
                    Model.SysInput_SystemInputVolumeM3_D8 = wbEasyCalcData.SysInput_SystemInputVolumeM3_D8;                                     
                    Model.SysInput_SystemInputVolumeError_F8 = wbEasyCalcData.SysInput_SystemInputVolumeError_F8;
                    Model.SysInput_SystemInputVolumeM3_D9 = wbEasyCalcData.SysInput_SystemInputVolumeM3_D9;                                     
                    Model.SysInput_SystemInputVolumeError_F9 = wbEasyCalcData.SysInput_SystemInputVolumeError_F9;

                    Model.BilledCons_Desc_B8 = wbEasyCalcData.BilledCons_Desc_B8;
                    Model.BilledCons_Desc_B9 = wbEasyCalcData.BilledCons_Desc_B9;
                    Model.BilledCons_Desc_B10 = wbEasyCalcData.BilledCons_Desc_B10;
                    Model.BilledCons_Desc_B11 = wbEasyCalcData.BilledCons_Desc_B11;
                    Model.BilledCons_Desc_F8 = wbEasyCalcData.BilledCons_Desc_F8;
                    Model.BilledCons_Desc_F9 = wbEasyCalcData.BilledCons_Desc_F9;
                    Model.BilledCons_Desc_F10 = wbEasyCalcData.BilledCons_Desc_F10;
                    Model.BilledCons_Desc_F11 = wbEasyCalcData.BilledCons_Desc_F11;
                    Model.UnbilledCons_Desc_D8 = wbEasyCalcData.UnbilledCons_Desc_D8;
                    Model.UnbilledCons_Desc_D9 = wbEasyCalcData.UnbilledCons_Desc_D9;
                    Model.UnbilledCons_Desc_D10 = wbEasyCalcData.UnbilledCons_Desc_D10;
                    Model.UnbilledCons_Desc_D11 = wbEasyCalcData.UnbilledCons_Desc_D11;
                    Model.UnbilledCons_Desc_F6 = wbEasyCalcData.UnbilledCons_Desc_F6;
                    Model.UnbilledCons_Desc_F7 = wbEasyCalcData.UnbilledCons_Desc_F7;
                    Model.UnbilledCons_Desc_F8 = wbEasyCalcData.UnbilledCons_Desc_F8;
                    Model.UnbilledCons_Desc_F9 = wbEasyCalcData.UnbilledCons_Desc_F9;
                    Model.UnbilledCons_Desc_F10 = wbEasyCalcData.UnbilledCons_Desc_F10;
                    Model.UnbilledCons_Desc_F11 = wbEasyCalcData.UnbilledCons_Desc_F11;
                    Model.UnauthCons_Desc_B18 = wbEasyCalcData.UnauthCons_Desc_B18;
                    Model.UnauthCons_Desc_B19 = wbEasyCalcData.UnauthCons_Desc_B19;
                    Model.UnauthCons_Desc_B20 = wbEasyCalcData.UnauthCons_Desc_B20;
                    Model.UnauthCons_Desc_B21 = wbEasyCalcData.UnauthCons_Desc_B21;
                    Model.MetErrors_Desc_D12 = wbEasyCalcData.MetErrors_Desc_D12;
                    Model.MetErrors_Desc_D13 = wbEasyCalcData.MetErrors_Desc_D13;
                    Model.MetErrors_Desc_D14 = wbEasyCalcData.MetErrors_Desc_D14;
                    Model.MetErrors_Desc_D15 = wbEasyCalcData.MetErrors_Desc_D15;
                    Model.Network_Desc_B7 = wbEasyCalcData.Network_Desc_B7;
                    Model.Network_Desc_B8 = wbEasyCalcData.Network_Desc_B8;
                    Model.Network_Desc_B9 = wbEasyCalcData.Network_Desc_B9;
                    Model.Network_Desc_B10 = wbEasyCalcData.Network_Desc_B10;
                    Model.Interm_Area_B7 = wbEasyCalcData.Interm_Area_B7;
                    Model.Interm_Area_B8 = wbEasyCalcData.Interm_Area_B8;
                    Model.Interm_Area_B9 = wbEasyCalcData.Interm_Area_B9;
                    Model.Interm_Area_B10 = wbEasyCalcData.Interm_Area_B10;

                    Model.BilledCons_BilledMetConsBulkWatSupExpM3_D6 = wbEasyCalcData.BilledCons_BilledMetConsBulkWatSupExpM3_D6;               // @ZoneSale
                    Model.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6  = wbEasyCalcData.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6; 

                    Model.BilledCons_UnbMetConsM3_D8 = wbEasyCalcData.BilledCons_UnbMetConsM3_D8;
                    Model.BilledCons_UnbMetConsM3_D9 = wbEasyCalcData.BilledCons_UnbMetConsM3_D9;
                    Model.BilledCons_UnbMetConsM3_D10 = wbEasyCalcData.BilledCons_UnbMetConsM3_D10;
                    Model.BilledCons_UnbMetConsM3_D11 = wbEasyCalcData.BilledCons_UnbMetConsM3_D11;
                    Model.BilledCons_UnbUnmetConsM3_H8 = wbEasyCalcData.BilledCons_UnbUnmetConsM3_H8;
                    Model.BilledCons_UnbUnmetConsM3_H9 = wbEasyCalcData.BilledCons_UnbUnmetConsM3_H9;
                    Model.BilledCons_UnbUnmetConsM3_H10 = wbEasyCalcData.BilledCons_UnbUnmetConsM3_H10;
                    Model.BilledCons_UnbUnmetConsM3_H11 = wbEasyCalcData.BilledCons_UnbUnmetConsM3_H11;

                    Model.UnbilledCons_MetConsBulkWatSupExpM3_D6 = wbEasyCalcData.UnbilledCons_MetConsBulkWatSupExpM3_D6;

                    Model.UnbilledCons_UnbMetConsM3_D8 = wbEasyCalcData.UnbilledCons_UnbMetConsM3_D8;
                    Model.UnbilledCons_UnbMetConsM3_D9 = wbEasyCalcData.UnbilledCons_UnbMetConsM3_D9;
                    Model.UnbilledCons_UnbMetConsM3_D10 = wbEasyCalcData.UnbilledCons_UnbMetConsM3_D10;
                    Model.UnbilledCons_UnbMetConsM3_D11 = wbEasyCalcData.UnbilledCons_UnbMetConsM3_D11;
                    Model.UnbilledCons_UnbUnmetConsM3_H6 = wbEasyCalcData.UnbilledCons_UnbUnmetConsM3_H6;
                    Model.UnbilledCons_UnbUnmetConsM3_H7 = wbEasyCalcData.UnbilledCons_UnbUnmetConsM3_H7;
                    Model.UnbilledCons_UnbUnmetConsM3_H8 = wbEasyCalcData.UnbilledCons_UnbUnmetConsM3_H8;
                    Model.UnbilledCons_UnbUnmetConsM3_H9 = wbEasyCalcData.UnbilledCons_UnbUnmetConsM3_H9;
                    Model.UnbilledCons_UnbUnmetConsM3_H10 = wbEasyCalcData.UnbilledCons_UnbUnmetConsM3_H10;
                    Model.UnbilledCons_UnbUnmetConsM3_H11 = wbEasyCalcData.UnbilledCons_UnbUnmetConsM3_H11;
                    Model.UnbilledCons_UnbUnmetConsError_J6 = wbEasyCalcData.UnbilledCons_UnbUnmetConsError_J6;
                    Model.UnbilledCons_UnbUnmetConsError_J7 = wbEasyCalcData.UnbilledCons_UnbUnmetConsError_J7;
                    Model.UnbilledCons_UnbUnmetConsError_J8 = wbEasyCalcData.UnbilledCons_UnbUnmetConsError_J8;
                    Model.UnbilledCons_UnbUnmetConsError_J9 = wbEasyCalcData.UnbilledCons_UnbUnmetConsError_J9;
                    Model.UnbilledCons_UnbUnmetConsError_J10 = wbEasyCalcData.UnbilledCons_UnbUnmetConsError_J10;
                    Model.UnbilledCons_UnbUnmetConsError_J11 = wbEasyCalcData.UnbilledCons_UnbUnmetConsError_J11;

                    Model.UnauthCons_OthersErrorMargin_F18 = wbEasyCalcData.UnauthCons_OthersErrorMargin_F18;
                    Model.UnauthCons_OthersErrorMargin_F19 = wbEasyCalcData.UnauthCons_OthersErrorMargin_F19;
                    Model.UnauthCons_OthersErrorMargin_F20 = wbEasyCalcData.UnauthCons_OthersErrorMargin_F20;
                    Model.UnauthCons_OthersErrorMargin_F21 = wbEasyCalcData.UnauthCons_OthersErrorMargin_F21;
                    Model.UnauthCons_OthersM3PerDay_J18 = wbEasyCalcData.UnauthCons_OthersM3PerDay_J18;
                    Model.UnauthCons_OthersM3PerDay_J19 = wbEasyCalcData.UnauthCons_OthersM3PerDay_J19;
                    Model.UnauthCons_OthersM3PerDay_J20 = wbEasyCalcData.UnauthCons_OthersM3PerDay_J20;
                    Model.UnauthCons_OthersM3PerDay_J21 = wbEasyCalcData.UnauthCons_OthersM3PerDay_J21;

                    Model.UnauthCons_IllegalConnDomEstNo_D6 = wbEasyCalcData.UnauthCons_IllegalConnDomEstNo_D6;
                    Model.UnauthCons_IllegalConnDomPersPerHouse_H6 = wbEasyCalcData.UnauthCons_IllegalConnDomPersPerHouse_H6;
                    Model.UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = wbEasyCalcData.UnauthCons_IllegalConnDomConsLitPerPersDay_J6;
                    Model.UnauthCons_IllegalConnDomErrorMargin_F6 = wbEasyCalcData.UnauthCons_IllegalConnDomErrorMargin_F6;
                    Model.UnauthCons_IllegalConnOthersErrorMargin_F10 = wbEasyCalcData.UnauthCons_IllegalConnOthersErrorMargin_F10;

                    Model.IllegalConnectionsOthersEstimatedNumber_D10 = wbEasyCalcData.IllegalConnectionsOthersEstimatedNumber_D10;
                    Model.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = wbEasyCalcData.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10;

                    Model.UnauthCons_MeterTampBypEtcEstNo_D14 = wbEasyCalcData.UnauthCons_MeterTampBypEtcEstNo_D14;
                    Model.UnauthCons_MeterTampBypEtcErrorMargin_F14 = wbEasyCalcData.UnauthCons_MeterTampBypEtcErrorMargin_F14;
                    Model.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = wbEasyCalcData.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14;
                    Model.MetErrors_DetailedManualSpec_J6 = wbEasyCalcData.MetErrors_DetailedManualSpec_J6;
                    Model.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = wbEasyCalcData.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8;
                    Model.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = wbEasyCalcData.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8;

                    Model.MetErrors_Total_F12 = wbEasyCalcData.MetErrors_Total_F12;
                    Model.MetErrors_Total_F13 = wbEasyCalcData.MetErrors_Total_F13;
                    Model.MetErrors_Total_F14 = wbEasyCalcData.MetErrors_Total_F14;
                    Model.MetErrors_Total_F15 = wbEasyCalcData.MetErrors_Total_F15;
                    Model.MetErrors_Meter_H12 = wbEasyCalcData.MetErrors_Meter_H12;
                    Model.MetErrors_Meter_H13 = wbEasyCalcData.MetErrors_Meter_H13;
                    Model.MetErrors_Meter_H14 = wbEasyCalcData.MetErrors_Meter_H14;
                    Model.MetErrors_Meter_H15 = wbEasyCalcData.MetErrors_Meter_H15;
                    Model.MetErrors_Error_N12 = wbEasyCalcData.MetErrors_Error_N12;
                    Model.MetErrors_Error_N13 = wbEasyCalcData.MetErrors_Error_N13;
                    Model.MetErrors_Error_N14 = wbEasyCalcData.MetErrors_Error_N14;
                    Model.MetErrors_Error_N15 = wbEasyCalcData.MetErrors_Error_N15;

                    Model.MeteredBulkSupplyExportErrorMargin_N32 = wbEasyCalcData.MeteredBulkSupplyExportErrorMargin_N32;
                    Model.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = wbEasyCalcData.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34;
                    Model.CorruptMeterReadingPracticessErrorMargin_N38 = wbEasyCalcData.CorruptMeterReadingPracticessErrorMargin_N38;
                    Model.DataHandlingErrorsOffice_L40 = wbEasyCalcData.DataHandlingErrorsOffice_L40;
                    Model.DataHandlingErrorsOfficeErrorMargin_N40 = wbEasyCalcData.DataHandlingErrorsOfficeErrorMargin_N40;

                    Model.MetErrors_MetBulkSupExpMetUnderreg_H32 = wbEasyCalcData.MetErrors_MetBulkSupExpMetUnderreg_H32;
                    Model.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34  = wbEasyCalcData.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34;
                    Model.MetErrors_CorruptMetReadPractMetUndrreg_H38 = wbEasyCalcData.MetErrors_CorruptMetReadPractMetUndrreg_H38;
                    Model.Network_DistributionAndTransmissionMains_D7 = wbEasyCalcData.Network_DistributionAndTransmissionMains_D7;              // @NetworkLength 
                    Model.Network_DistributionAndTransmissionMains_D8 = wbEasyCalcData.Network_DistributionAndTransmissionMains_D8;
                    Model.Network_DistributionAndTransmissionMains_D9 = wbEasyCalcData.Network_DistributionAndTransmissionMains_D9;
                    Model.Network_DistributionAndTransmissionMains_D10 = wbEasyCalcData.Network_DistributionAndTransmissionMains_D10;
                    Model.Network_NoOfConnOfRegCustomers_H10 = wbEasyCalcData.Network_NoOfConnOfRegCustomers_H10;                                // @CustomersQuantity 
                    Model.Network_NoOfInactAccountsWSvcConns_H18 = wbEasyCalcData.Network_NoOfInactAccountsWSvcConns_H18;
                    Model.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = wbEasyCalcData.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32;

                    Model.Network_PossibleUnd_D30 = wbEasyCalcData.Network_PossibleUnd_D30;
                    Model.Network_NoCustomers_H7 =  wbEasyCalcData.Network_NoCustomers_H7;
                    Model.Network_ErrorMargin_J7 =  wbEasyCalcData.Network_ErrorMargin_J7;
                    Model.Network_ErrorMargin_J10 = wbEasyCalcData.Network_ErrorMargin_J10;
                    Model.Network_ErrorMargin_J18 = wbEasyCalcData.Network_ErrorMargin_J18;
                    Model.Network_ErrorMargin_J32 = wbEasyCalcData.Network_ErrorMargin_J32;
                    Model.Network_ErrorMargin_D35 = wbEasyCalcData.Network_ErrorMargin_D35;

                    Model.FinancData_G6 =  wbEasyCalcData.FinancData_G6;
                    Model.FinancData_K6 =  wbEasyCalcData.FinancData_K6;
                    Model.FinancData_G8 =  wbEasyCalcData.FinancData_G8;
                    Model.FinancData_D26 = wbEasyCalcData.FinancData_D26;
                    Model.FinancData_G35 = wbEasyCalcData.FinancData_G35;

                    Model.Prs_Area_B7 = wbEasyCalcData.Prs_Area_B7;
                    Model.Prs_Area_B8 = wbEasyCalcData.Prs_Area_B8;
                    Model.Prs_Area_B9 = wbEasyCalcData.Prs_Area_B9;
                    Model.Prs_Area_B10 = wbEasyCalcData.Prs_Area_B10;
                    Model.Prs_ApproxNoOfConn_D7 = wbEasyCalcData.Prs_ApproxNoOfConn_D7;
                    Model.Prs_DailyAvgPrsM_F7 = wbEasyCalcData.Prs_DailyAvgPrsM_F7;
                    Model.Prs_ApproxNoOfConn_D8 = wbEasyCalcData.Prs_ApproxNoOfConn_D8;
                    Model.Prs_DailyAvgPrsM_F8 = wbEasyCalcData.Prs_DailyAvgPrsM_F8;
                    Model.Prs_ApproxNoOfConn_D9 = wbEasyCalcData.Prs_ApproxNoOfConn_D9;
                    Model.Prs_DailyAvgPrsM_F9 = wbEasyCalcData.Prs_DailyAvgPrsM_F9;
                    Model.Prs_ApproxNoOfConn_D10 = wbEasyCalcData.Prs_ApproxNoOfConn_D10;
                    Model.Prs_DailyAvgPrsM_F10 = wbEasyCalcData.Prs_DailyAvgPrsM_F10;
                    Model.Prs_ErrorMarg_F26 = wbEasyCalcData.Prs_ErrorMarg_F26;

                    Model.Interm_Conn_D7 =  wbEasyCalcData.Interm_Conn_D7;
                    Model.Interm_Conn_D8 =  wbEasyCalcData.Interm_Conn_D8;
                    Model.Interm_Conn_D9 =  wbEasyCalcData.Interm_Conn_D9;
                    Model.Interm_Conn_D10 = wbEasyCalcData.Interm_Conn_D10;
                    Model.Interm_Days_F7 =  wbEasyCalcData.Interm_Days_F7;
                    Model.Interm_Days_F8 =  wbEasyCalcData.Interm_Days_F8;
                    Model.Interm_Days_F9 =  wbEasyCalcData.Interm_Days_F9;
                    Model.Interm_Days_F10 = wbEasyCalcData.Interm_Days_F10;
                    Model.Interm_Hour_H7 =  wbEasyCalcData.Interm_Hour_H7;
                    Model.Interm_Hour_H8 =  wbEasyCalcData.Interm_Hour_H8;
                    Model.Interm_Hour_H9 =  wbEasyCalcData.Interm_Hour_H9;
                    Model.Interm_Hour_H10 = wbEasyCalcData.Interm_Hour_H10;

                    //Model.PIs_IliBestEstimate_F25 = wbEasyCalcData.PIs_IliBestEstimate_F25;
                }

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

            Model.SysInput_Desc_B6 = easyCalcDataInput.SysInput_Desc_B6;
            Model.SysInput_Desc_B7 = easyCalcDataInput.SysInput_Desc_B7;
            Model.SysInput_Desc_B8 = easyCalcDataInput.SysInput_Desc_B8;
            Model.SysInput_Desc_B9 = easyCalcDataInput.SysInput_Desc_B9;
            Model.SysInput_SystemInputVolumeM3_D6 = easyCalcDataInput.SysInput_SystemInputVolumeM3_D6;
            Model.SysInput_SystemInputVolumeError_F6 = easyCalcDataInput.SysInput_SystemInputVolumeError_F6;
            Model.SysInput_SystemInputVolumeM3_D7 = easyCalcDataInput.SysInput_SystemInputVolumeM3_D7;
            Model.SysInput_SystemInputVolumeError_F7 = easyCalcDataInput.SysInput_SystemInputVolumeError_F7;
            Model.SysInput_SystemInputVolumeM3_D8 = easyCalcDataInput.SysInput_SystemInputVolumeM3_D8;
            Model.SysInput_SystemInputVolumeError_F8 = easyCalcDataInput.SysInput_SystemInputVolumeError_F8;
            Model.SysInput_SystemInputVolumeM3_D9 = easyCalcDataInput.SysInput_SystemInputVolumeM3_D9;
            Model.SysInput_SystemInputVolumeError_F9 = easyCalcDataInput.SysInput_SystemInputVolumeError_F9;

            Model.BilledCons_Desc_B8 = easyCalcDataInput.BilledCons_Desc_B8;
            Model.BilledCons_Desc_B9 = easyCalcDataInput.BilledCons_Desc_B9;
            Model.BilledCons_Desc_B10 = easyCalcDataInput.BilledCons_Desc_B10;
            Model.BilledCons_Desc_B11 = easyCalcDataInput.BilledCons_Desc_B11;
            Model.BilledCons_Desc_F8 = easyCalcDataInput.BilledCons_Desc_F8;
            Model.BilledCons_Desc_F9 = easyCalcDataInput.BilledCons_Desc_F9;
            Model.BilledCons_Desc_F10 = easyCalcDataInput.BilledCons_Desc_F10;
            Model.BilledCons_Desc_F11 = easyCalcDataInput.BilledCons_Desc_F11;
            Model.UnbilledCons_Desc_D8 = easyCalcDataInput.UnbilledCons_Desc_D8;
            Model.UnbilledCons_Desc_D9 = easyCalcDataInput.UnbilledCons_Desc_D9;
            Model.UnbilledCons_Desc_D10 = easyCalcDataInput.UnbilledCons_Desc_D10;
            Model.UnbilledCons_Desc_D11 = easyCalcDataInput.UnbilledCons_Desc_D11;
            Model.UnbilledCons_Desc_F6 = easyCalcDataInput.UnbilledCons_Desc_F6;
            Model.UnbilledCons_Desc_F7 = easyCalcDataInput.UnbilledCons_Desc_F7;
            Model.UnbilledCons_Desc_F8 = easyCalcDataInput.UnbilledCons_Desc_F8;
            Model.UnbilledCons_Desc_F9 = easyCalcDataInput.UnbilledCons_Desc_F9;
            Model.UnbilledCons_Desc_F10 = easyCalcDataInput.UnbilledCons_Desc_F10;
            Model.UnbilledCons_Desc_F11 = easyCalcDataInput.UnbilledCons_Desc_F11;
            Model.UnauthCons_Desc_B18 = easyCalcDataInput.UnauthCons_Desc_B18;
            Model.UnauthCons_Desc_B19 = easyCalcDataInput.UnauthCons_Desc_B19;
            Model.UnauthCons_Desc_B20 = easyCalcDataInput.UnauthCons_Desc_B20;
            Model.UnauthCons_Desc_B21 = easyCalcDataInput.UnauthCons_Desc_B21;
            Model.MetErrors_Desc_D12 = easyCalcDataInput.MetErrors_Desc_D12;
            Model.MetErrors_Desc_D13 = easyCalcDataInput.MetErrors_Desc_D13;
            Model.MetErrors_Desc_D14 = easyCalcDataInput.MetErrors_Desc_D14;
            Model.MetErrors_Desc_D15 = easyCalcDataInput.MetErrors_Desc_D15;
            Model.Network_Desc_B7 = easyCalcDataInput.Network_Desc_B7;
            Model.Network_Desc_B8 = easyCalcDataInput.Network_Desc_B8;
            Model.Network_Desc_B9 = easyCalcDataInput.Network_Desc_B9;
            Model.Network_Desc_B10 = easyCalcDataInput.Network_Desc_B10;
            Model.Interm_Area_B7 = easyCalcDataInput.Interm_Area_B7;
            Model.Interm_Area_B8 = easyCalcDataInput.Interm_Area_B8;
            Model.Interm_Area_B9 = easyCalcDataInput.Interm_Area_B9;
            Model.Interm_Area_B10 = easyCalcDataInput.Interm_Area_B10;

            Model.BilledCons_BilledMetConsBulkWatSupExpM3_D6 = easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6;
            Model.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6;

            Model.BilledCons_UnbMetConsM3_D8 = easyCalcDataInput.BilledCons_UnbMetConsM3_D8;
            Model.BilledCons_UnbMetConsM3_D9 = easyCalcDataInput.BilledCons_UnbMetConsM3_D9;
            Model.BilledCons_UnbMetConsM3_D10 = easyCalcDataInput.BilledCons_UnbMetConsM3_D10;
            Model.BilledCons_UnbMetConsM3_D11 = easyCalcDataInput.BilledCons_UnbMetConsM3_D11;
            Model.BilledCons_UnbUnmetConsM3_H8 = easyCalcDataInput.BilledCons_UnbUnmetConsM3_H8;
            Model.BilledCons_UnbUnmetConsM3_H9 = easyCalcDataInput.BilledCons_UnbUnmetConsM3_H9;
            Model.BilledCons_UnbUnmetConsM3_H10 = easyCalcDataInput.BilledCons_UnbUnmetConsM3_H10;
            Model.BilledCons_UnbUnmetConsM3_H11 = easyCalcDataInput.BilledCons_UnbUnmetConsM3_H11;

            Model.UnbilledCons_MetConsBulkWatSupExpM3_D6 = easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6;

            Model.UnbilledCons_UnbMetConsM3_D8 = easyCalcDataInput.UnbilledCons_UnbMetConsM3_D8;
            Model.UnbilledCons_UnbMetConsM3_D9 = easyCalcDataInput.UnbilledCons_UnbMetConsM3_D9;
            Model.UnbilledCons_UnbMetConsM3_D10 = easyCalcDataInput.UnbilledCons_UnbMetConsM3_D10;
            Model.UnbilledCons_UnbMetConsM3_D11 = easyCalcDataInput.UnbilledCons_UnbMetConsM3_D11;
            Model.UnbilledCons_UnbUnmetConsM3_H6 = easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H6;
            Model.UnbilledCons_UnbUnmetConsM3_H7 = easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H7;
            Model.UnbilledCons_UnbUnmetConsM3_H8 = easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H8;
            Model.UnbilledCons_UnbUnmetConsM3_H9 = easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H9;
            Model.UnbilledCons_UnbUnmetConsM3_H10 = easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H10;
            Model.UnbilledCons_UnbUnmetConsM3_H11 = easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H11;
            Model.UnbilledCons_UnbUnmetConsError_J6 = easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J6;
            Model.UnbilledCons_UnbUnmetConsError_J7 = easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J7;
            Model.UnbilledCons_UnbUnmetConsError_J8 = easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J8;
            Model.UnbilledCons_UnbUnmetConsError_J9 = easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J9;
            Model.UnbilledCons_UnbUnmetConsError_J10 = easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J10;
            Model.UnbilledCons_UnbUnmetConsError_J11 = easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J11;

            Model.UnauthCons_OthersErrorMargin_F18 = easyCalcDataInput.UnauthCons_OthersErrorMargin_F18;
            Model.UnauthCons_OthersErrorMargin_F19 = easyCalcDataInput.UnauthCons_OthersErrorMargin_F19;
            Model.UnauthCons_OthersErrorMargin_F20 = easyCalcDataInput.UnauthCons_OthersErrorMargin_F20;
            Model.UnauthCons_OthersErrorMargin_F21 = easyCalcDataInput.UnauthCons_OthersErrorMargin_F21;
            Model.UnauthCons_OthersM3PerDay_J18 = easyCalcDataInput.UnauthCons_OthersM3PerDay_J18;
            Model.UnauthCons_OthersM3PerDay_J19 = easyCalcDataInput.UnauthCons_OthersM3PerDay_J19;
            Model.UnauthCons_OthersM3PerDay_J20 = easyCalcDataInput.UnauthCons_OthersM3PerDay_J20;
            Model.UnauthCons_OthersM3PerDay_J21 = easyCalcDataInput.UnauthCons_OthersM3PerDay_J21;

            Model.UnauthCons_IllegalConnDomEstNo_D6 = easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6;
            Model.UnauthCons_IllegalConnDomPersPerHouse_H6 = easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6;
            Model.UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6;
            Model.UnauthCons_IllegalConnDomErrorMargin_F6 = easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6;
            Model.UnauthCons_IllegalConnOthersErrorMargin_F10 = easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10;

            Model.IllegalConnectionsOthersEstimatedNumber_D10 = easyCalcDataInput.IllegalConnectionsOthersEstimatedNumber_D10;
            Model.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = easyCalcDataInput.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10;

            Model.UnauthCons_MeterTampBypEtcEstNo_D14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14;
            Model.UnauthCons_MeterTampBypEtcErrorMargin_F14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14;
            Model.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14;
            Model.MetErrors_DetailedManualSpec_J6 = easyCalcDataInput.MetErrors_DetailedManualSpec_J6 ? 2 : 1;
            Model.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8;
            Model.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8;

            Model.MetErrors_Total_F12 = easyCalcDataInput.MetErrors_Total_F12;
            Model.MetErrors_Total_F13 = easyCalcDataInput.MetErrors_Total_F13;
            Model.MetErrors_Total_F14 = easyCalcDataInput.MetErrors_Total_F14;
            Model.MetErrors_Total_F15 = easyCalcDataInput.MetErrors_Total_F15;
            Model.MetErrors_Meter_H12 = easyCalcDataInput.MetErrors_Meter_H12;
            Model.MetErrors_Meter_H13 = easyCalcDataInput.MetErrors_Meter_H13;
            Model.MetErrors_Meter_H14 = easyCalcDataInput.MetErrors_Meter_H14;
            Model.MetErrors_Meter_H15 = easyCalcDataInput.MetErrors_Meter_H15;
            Model.MetErrors_Error_N12 = easyCalcDataInput.MetErrors_Error_N12;
            Model.MetErrors_Error_N13 = easyCalcDataInput.MetErrors_Error_N13;
            Model.MetErrors_Error_N14 = easyCalcDataInput.MetErrors_Error_N14;
            Model.MetErrors_Error_N15 = easyCalcDataInput.MetErrors_Error_N15;

            Model.MeteredBulkSupplyExportErrorMargin_N32 = easyCalcDataInput.MeteredBulkSupplyExportErrorMargin_N32;
            Model.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = easyCalcDataInput.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34;
            Model.CorruptMeterReadingPracticessErrorMargin_N38 = easyCalcDataInput.CorruptMeterReadingPracticessErrorMargin_N38;
            Model.DataHandlingErrorsOffice_L40 = easyCalcDataInput.DataHandlingErrorsOffice_L40;
            Model.DataHandlingErrorsOfficeErrorMargin_N40 = easyCalcDataInput.DataHandlingErrorsOfficeErrorMargin_N40;

            Model.MetErrors_MetBulkSupExpMetUnderreg_H32 = easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32;
            Model.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34;
            Model.MetErrors_CorruptMetReadPractMetUndrreg_H38 = easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38;
            Model.Network_DistributionAndTransmissionMains_D7 = easyCalcDataInput.Network_DistributionAndTransmissionMains_D7;               
            Model.Network_DistributionAndTransmissionMains_D8 = easyCalcDataInput.Network_DistributionAndTransmissionMains_D8;
            Model.Network_DistributionAndTransmissionMains_D9 = easyCalcDataInput.Network_DistributionAndTransmissionMains_D9;
            Model.Network_DistributionAndTransmissionMains_D10 = easyCalcDataInput.Network_DistributionAndTransmissionMains_D10;
            Model.Network_NoOfConnOfRegCustomers_H10 = easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10;
            Model.Network_NoOfInactAccountsWSvcConns_H18 = easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18;
            Model.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32;

            Model.Network_PossibleUnd_D30 = easyCalcDataInput.Network_PossibleUnd_D30;
            Model.Network_NoCustomers_H7 =  easyCalcDataInput.Network_NoCustomers_H7;
            Model.Network_ErrorMargin_J7 =  easyCalcDataInput.Network_ErrorMargin_J7;
            Model.Network_ErrorMargin_J10 = easyCalcDataInput.Network_ErrorMargin_J10;
            Model.Network_ErrorMargin_J18 = easyCalcDataInput.Network_ErrorMargin_J18;
            Model.Network_ErrorMargin_J32 = easyCalcDataInput.Network_ErrorMargin_J32;
            Model.Network_ErrorMargin_D35 = easyCalcDataInput.Network_ErrorMargin_D35;

            Model.FinancData_G6 =  easyCalcDataInput.FinancData_G6;
            Model.FinancData_K6 =  easyCalcDataInput.FinancData_K6;
            Model.FinancData_G8 =  easyCalcDataInput.FinancData_G8;
            Model.FinancData_D26 = easyCalcDataInput.FinancData_D26;
            Model.FinancData_G35 = easyCalcDataInput.FinancData_G35;

            Model.Prs_Area_B7 = easyCalcDataInput.Prs_Area_B7;
            Model.Prs_Area_B8 = easyCalcDataInput.Prs_Area_B8;
            Model.Prs_Area_B9 = easyCalcDataInput.Prs_Area_B9;
            Model.Prs_Area_B10 = easyCalcDataInput.Prs_Area_B10;
            Model.Prs_ApproxNoOfConn_D7 = easyCalcDataInput.Prs_ApproxNoOfConn_D7;
            Model.Prs_DailyAvgPrsM_F7 = easyCalcDataInput.Prs_DailyAvgPrsM_F7;
            Model.Prs_ApproxNoOfConn_D8 = easyCalcDataInput.Prs_ApproxNoOfConn_D8;
            Model.Prs_DailyAvgPrsM_F8 = easyCalcDataInput.Prs_DailyAvgPrsM_F8;
            Model.Prs_ApproxNoOfConn_D9 = easyCalcDataInput.Prs_ApproxNoOfConn_D9;
            Model.Prs_DailyAvgPrsM_F9 = easyCalcDataInput.Prs_DailyAvgPrsM_F9;
            Model.Prs_ApproxNoOfConn_D10 = easyCalcDataInput.Prs_ApproxNoOfConn_D10;
            Model.Prs_DailyAvgPrsM_F10 = easyCalcDataInput.Prs_DailyAvgPrsM_F10;
            Model.Prs_ErrorMarg_F26 = easyCalcDataInput.Prs_ErrorMarg_F26;

            Model.Interm_Conn_D7 =  easyCalcDataInput.Interm_Conn_D7;
            Model.Interm_Conn_D8 =  easyCalcDataInput.Interm_Conn_D8;
            Model.Interm_Conn_D9 =  easyCalcDataInput.Interm_Conn_D9;
            Model.Interm_Conn_D10 = easyCalcDataInput.Interm_Conn_D10;
            Model.Interm_Days_F7 =  easyCalcDataInput.Interm_Days_F7;
            Model.Interm_Days_F8 =  easyCalcDataInput.Interm_Days_F8;
            Model.Interm_Days_F9 =  easyCalcDataInput.Interm_Days_F9;
            Model.Interm_Days_F10 = easyCalcDataInput.Interm_Days_F10;
            Model.Interm_Hour_H7 =  easyCalcDataInput.Interm_Hour_H7;
            Model.Interm_Hour_H8 =  easyCalcDataInput.Interm_Hour_H8;
            Model.Interm_Hour_H9 =  easyCalcDataInput.Interm_Hour_H9;
            Model.Interm_Hour_H10 = easyCalcDataInput.Interm_Hour_H10;
            Model.Interm_ErrorMarg_H26 = easyCalcDataInput.Interm_ErrorMarg_H26;
        }
    }
}
