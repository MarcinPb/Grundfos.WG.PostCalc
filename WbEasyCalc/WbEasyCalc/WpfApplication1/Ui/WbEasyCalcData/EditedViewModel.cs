using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataModel;
using DataRepository;
using DataRepository.DataAccess;
using Grundfos.WB.EasyCalc.Calculations;
using Microsoft.WindowsAPICodePack.Dialogs;
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

        /*
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
        */
        public List<IdNamePair> YearList { get; set; }
        public List<IdNamePair> MonthList { get; set; }
        public List<ZoneItem> ZoneItemList { get; set; }

        #region Commands

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
            Model = new ItemViewModel(GlobalConfig.WbEasyCalcDataRepo.GetItem(id));

            YearList = GlobalConfig.YearList;
            //SelectedYear = YearList.FirstOrDefault(x => x.Id==DateTime.Now.Year);
            MonthList = GlobalConfig.MonthList;
            //SelectedMonth = MonthList.FirstOrDefault(x => x.Id==DateTime.Now.Month);
            ZoneItemList = GlobalConfig.ZoneList;
            //SelectedZoneItem = ZoneItemList.FirstOrDefault(x =>x.ZoneId==Model.Model.ZoneId);
        }

        private void LoadDataFromSystem()
        {
            try
            {
                var gisModelScadaData = GlobalConfig.GetGisModelScadaData(Model.YearNo, Model.MonthNo, Model.ZoneId);

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
                EasyCalcDataInput easyCalcDataInput = Model.MapEasyCalcDataInput();
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
                Model.MapEasyCalcDataInput(easyCalcDataInput);
                Model.CalculateExcel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
