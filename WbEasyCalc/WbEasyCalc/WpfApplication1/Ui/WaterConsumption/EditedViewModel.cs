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



        #region Commands: LoadDataFromSystemCmd, ImportFromExcelExecute, ImportFromExcelCanExecute

        //public RelayCommand LoadDataFromSystemCmd => new RelayCommand(LoadDataFromSystemExecute, LoadDataFromSystemCanExecute);
        //private void LoadDataFromSystemExecute()
        //{
        //    LoadDataFromSystem();
        //}
        //public bool LoadDataFromSystemCanExecute()
        //{
        //    return true;
        //}

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
            Model = new ItemViewModel(GlobalConfig.DataRepository.WaterConsumptionListRepository.GetItem(id));

            WaterConsumptionCategoryList = GlobalConfig.DataRepository.WaterConsumptionCategoryList;
            WaterConsumptionStatusList = GlobalConfig.DataRepository.WaterConsumptionStatusList;
            ZoneItemList = GlobalConfig.DataRepository.ZoneList;


            MapOpacity = 1;
            Center = new Location(51.20150, 16.17970); 
            ZoomLevel = 15;
        }

        //private void LoadDataFromSystem()
        //{
        //    try
        //    {
        //        // Invoke spGisModelScadaData on SQL Server.
        //        var wbEasyCalcData = GlobalConfig.DataRepository.GetAutomaticData(Model.YearNo, Model.MonthNo, Model.ZoneId);

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private void ExportToExcel(string excelFileName)
        {
            try
            {
                //EasyCalcDataInput easyCalcDataInput = Model.Model.EasyCalcDataInput;
                //GlobalConfig.WbEasyCalcExcel.SaveToExcelFile(excelFileName, easyCalcDataInput);
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
        }
    }
}
