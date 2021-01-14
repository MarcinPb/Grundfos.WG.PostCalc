using System;
using System.Collections;
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
using WpfApplication1.Ui.WaterConsumption;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WaterConsumption
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

                WaterConsumptionEditedViewModel = null;
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
        public EditedViewModel WaterConsumptionEditedViewModel
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
            WaterConsumptionEditedViewModel = new EditedViewModel(0);
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

            WaterConsumptionEditedViewModel = new EditedViewModel(SelectedRow.Model.WaterConsumptionId);
        }
        public bool OpenRowCmdCanExecute()
        {
            return SelectedRow != null;
        }

        public RelayCommand RemoveRowCmd { get; }
        private void RemoveRowCmdExecute()
        {
            try
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
                    GlobalConfig.DataRepository.WaterConsumptionListRepository.DeleteItem(SelectedRow.Model.WaterConsumptionId);
                    LoadData();
                    SelectedRow = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool RemoveRowCmdCanExecute()
        {
            return SelectedRow != null && SelectedRow.Model.IsArchive == false;
        }

        public RelayCommand SaveRowCmd { get; }
        private void SaveRowCmdExecute()
        {
            try
            {
                DataModel.WaterConsumption row = GlobalConfig.DataRepository.WaterConsumptionListRepository.SaveItem(WaterConsumptionEditedViewModel.Model.Model);
                LoadData();
                SelectedRow = List.FirstOrDefault(x => x.Model.WaterConsumptionId == row.WaterConsumptionId);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool SaveRowCmdCanExecute()
        {
            return true;
        }


        public RelayCommand CloneCmd { get; }

        private void CloneCmdExecute()
        {
            try
            {
                if (SelectedRow == null) return;
                int id = GlobalConfig.DataRepository.WaterConsumptionListRepository.Clone(SelectedRow.Model.WaterConsumptionId);
                LoadData();
                SelectedRow = List.FirstOrDefault(x => x.Model.WaterConsumptionId == id);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CloneCmdCanExecute()
        {
            return SelectedRow != null;
        }


        public RelayCommand<IList> ReadSelectedItemsCmd { get; }

        private void ReadSelectedItemsExecute(object selectedItems)
        {
            var idListString = ((IList<object>)selectedItems).Select(x => (RowViewModel)x).Select(y => y.Model.WaterConsumptionId.ToString()).Aggregate((p, n) => p + "," + n);
            MessageBox.Show($"Selected Id list: {idListString}.");
        }
        #endregion

        public ListViewModel()
        {
            try
            {
                AddRowCmd = new RelayCommand(AddRowCmdExecute, AddRowCmdCanExecute);
                OpenRowCmd = new RelayCommand(OpenRowCmdExecute, OpenRowCmdCanExecute);
                RemoveRowCmd = new RelayCommand(RemoveRowCmdExecute, RemoveRowCmdCanExecute);
                SaveRowCmd = new RelayCommand(SaveRowCmdExecute, SaveRowCmdCanExecute);
                CloneCmd = new RelayCommand(CloneCmdExecute, CloneCmdCanExecute);

                ReadSelectedItemsCmd = new RelayCommand<IList>(ReadSelectedItemsExecute);

                LoadData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            List = new ObservableCollection<RowViewModel>(GlobalConfig.DataRepository.WaterConsumptionListRepository.GetList().Select(x => new RowViewModel(x)).ToList());
            RowsQty = List.Count;
        }

    }
}
