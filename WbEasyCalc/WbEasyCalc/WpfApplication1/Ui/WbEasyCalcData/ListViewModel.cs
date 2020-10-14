using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataRepository;
using GlobalRepository;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{
    public class ListViewModel : ViewModelBase
    {
        #region Props: List, SelectedRow, RowsQty

        private ObservableCollection<RowViewModel> _list;
        public ObservableCollection<RowViewModel> List
        {
            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged();
            }
        }

        private RowViewModel _selectedRow;
        public RowViewModel SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                RaisePropertyChanged();

                OpenRowCmd.RaiseCanExecuteChanged();
                RemoveRowCmd.RaiseCanExecuteChanged();
                CloneCmd.RaiseCanExecuteChanged();

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
            WbEasyCalcDataEditedViewModel = new EditedViewModel(SelectedRow.WbEasyCalcData.WbEasyCalcDataId);
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
                GlobalConfig.DataRepository.WbEasyCalcDataListRepository.DeleteItem(SelectedRow.WbEasyCalcData.WbEasyCalcDataId);
                LoadData();
                SelectedRow = null;
            }
        }
        public bool RemoveRowCmdCanExecute()
        {
            return SelectedRow != null && SelectedRow.WbEasyCalcData.IsArchive==false;
        }

        public RelayCommand SaveRowCmd { get; }
        private void SaveRowCmdExecute()
        {
            DataModel.WbEasyCalcData row = GlobalConfig.DataRepository.WbEasyCalcDataListRepository.SaveItem(WbEasyCalcDataEditedViewModel.Model.Model);
            LoadData();
            SelectedRow = List.FirstOrDefault(x => x.WbEasyCalcData.WbEasyCalcDataId == row.WbEasyCalcDataId);
        }
        public bool SaveRowCmdCanExecute()
        {
            return true;
        }


        public RelayCommand CloneCmd { get; }

        private void CloneCmdExecute()
        {
            if (SelectedRow == null) return;
            int id = GlobalConfig.DataRepository.WbEasyCalcDataListRepository.Clone(SelectedRow.WbEasyCalcData.WbEasyCalcDataId);
            LoadData();
            SelectedRow = List.FirstOrDefault(x => x.WbEasyCalcData.WbEasyCalcDataId == id);
        }

        public bool CloneCmdCanExecute()
        {
            return SelectedRow != null;
        }

        #endregion

        public ListViewModel()
        {
            AddRowCmd = new RelayCommand(AddRowCmdExecute, AddRowCmdCanExecute);
            OpenRowCmd = new RelayCommand(OpenRowCmdExecute, OpenRowCmdCanExecute);
            RemoveRowCmd = new RelayCommand(RemoveRowCmdExecute, RemoveRowCmdCanExecute);
            CloneCmd = new RelayCommand(CloneCmdExecute, CloneCmdCanExecute);
            SaveRowCmd = new RelayCommand(SaveRowCmdExecute, SaveRowCmdCanExecute);

            LoadData();
        }

        private void LoadData()
        {
            List = new ObservableCollection<RowViewModel>(GlobalConfig.DataRepository.WbEasyCalcDataListRepository.GetList().Select(x => new RowViewModel(x)).ToList());
            RowsQty = List.Count;
        }

    }
}
