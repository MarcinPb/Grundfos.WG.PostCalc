﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataRepository;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{
    public class ListViewModel : ViewModelBase
    {
        #region Props: List, SelectedRow, RowsQty

        private ObservableCollection<DataModel.WbEasyCalcData> _list;
        public ObservableCollection<DataModel.WbEasyCalcData> List
        {
            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged();
            }
        }

        private DataModel.WbEasyCalcData _selectedRow;
        public DataModel.WbEasyCalcData SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                RaisePropertyChanged();

                OpenRowCmd.RaiseCanExecuteChanged();
                RemoveRowCmd.RaiseCanExecuteChanged();
                SetUpRankCmd.RaiseCanExecuteChanged();

                //WbEasyCalcDataEditedViewModel?.Close();
                WbEasyCalcDataEditedViewModel = null;
                OpenRowCmdExecute();
            }
        }

        private int _rowsQty;
        public int RowsQty
        {
            get { return _rowsQty; }
            set
            {
                _rowsQty = value;
                RaisePropertyChanged();
            }
        }



        private EditedViewModel _customerEditedViewModel;
        public EditedViewModel WbEasyCalcDataEditedViewModel
        {
            get => _customerEditedViewModel;
            set
            {
                _customerEditedViewModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands: AddRowCmd, OpenRowCmd, RemoveRowCmd

        public RelayCommand AddRowCmd { get; }
        private void AddRowCmdExecute()
        {
            if (SelectedRow != null)
            {
                SelectedRow = null;
            }
            WbEasyCalcDataEditedViewModel = new EditedViewModel(0);
        }
        public bool AddRowCmdCanExecute()
        {
            return true;
        }

        public RelayCommand OpenRowCmd { get; }
     
        private void OpenRowCmdExecute()
        {
            if (SelectedRow == null)
            {
                return;
            }
            WbEasyCalcDataEditedViewModel = new EditedViewModel(SelectedRow.WbEasyCalcDataId);
        }
        public bool OpenRowCmdCanExecute()
        {
            return SelectedRow != null;
        }

        public RelayCommand RemoveRowCmd { get; }
        private void RemoveRowCmdExecute()
        {
            if (SelectedRow == null) return;
            var res = MessageBox.Show(
                "Are you sure to delete record?",
                "Question",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (res == MessageBoxResult.Yes)
            {
                GlobalConfig.WbEasyCalcDataRepo.DeleteItem(SelectedRow.WbEasyCalcDataId);
                LoadData();
                SelectedRow = null;
            }
        }
        public bool RemoveRowCmdCanExecute()
        {
            return SelectedRow != null;
        }

        public RelayCommand SaveRowCmd { get; }
        private void SaveRowCmdExecute()
        {
            DataModel.WbEasyCalcData customer = GlobalConfig.WbEasyCalcDataRepo.SaveItem(WbEasyCalcDataEditedViewModel.Model.Model);
            LoadData();
            SelectedRow = List.FirstOrDefault(x => x.WbEasyCalcDataId == customer.WbEasyCalcDataId);
        }
        public bool SaveRowCmdCanExecute()
        {
            return true;
        }


        public RelayCommand SetUpRankCmd { get; }

        private void SetUpRankCmdExecute()
        {
            if (SelectedRow == null) return;
            var res = MessageBox.Show(
                "Are you sure to set up Description?",
                "Question",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (res == MessageBoxResult.Yes)
            {
                GlobalConfig.WbEasyCalcDataRepo.SetUpRank(SelectedRow.WbEasyCalcDataId);
            }
        }

        public bool SetUpRankCmdCanExecute()
        {
            return SelectedRow != null;
        }

        #endregion

        public ListViewModel()
        {
            AddRowCmd = new RelayCommand(AddRowCmdExecute, AddRowCmdCanExecute);
            OpenRowCmd = new RelayCommand(OpenRowCmdExecute, OpenRowCmdCanExecute);
            RemoveRowCmd = new RelayCommand(RemoveRowCmdExecute, RemoveRowCmdCanExecute);
            SetUpRankCmd = new RelayCommand(SetUpRankCmdExecute, SetUpRankCmdCanExecute);
            SaveRowCmd = new RelayCommand(SaveRowCmdExecute, SaveRowCmdCanExecute);

            LoadData();
        }

        private void LoadData()
        {
            List = new ObservableCollection<DataModel.WbEasyCalcData>(GlobalConfig.WbEasyCalcDataRepo.GetList());
            RowsQty = List.Count;
        }

    }
}
