using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAL;
using DAL.Customer;
using DAL.Order;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Order
{
    public class ListViewModel : ViewModelBase
    {
        private readonly IMainRepo _mainRepo;

        public ListViewModel(IMainRepo mainRepo)
        {
            _mainRepo = mainRepo;
            _mainRepo.RepoChanged += OnServiceRepoChanged;
            LoadData();
        }

        private void LoadData()
        {
            OrderList = new ObservableCollection<RowViewModel>(_mainRepo.OrderRepo.GetList().Select(x => new RowViewModel(x, _mainRepo)));
            RowQty = OrderList.Count;
        }

        private void OnServiceRepoChanged(object sender, RepoChangedEventArgs e)
        {
            if (e.Sender is OrderRepository)
            {
                //int id = SelectedRow?.Model.OrderId ?? 0;
                int id = SelectedRow?.Order.OrderId ?? 0;
                LoadData();
                switch (e.ChangeReason)
                {
                    case ChangeReasonList.Updated:
                        SelectedRow = OrderList.FirstOrDefault(x => x.Order.OrderId == id);
                        break;
                    case ChangeReasonList.Added:
                        SelectedRow = OrderList.FirstOrDefault(x => x.Order.OrderId == OrderList.Max(y => y.Order.OrderId));
                        break;
                    case ChangeReasonList.Deleted:
                        SelectedRow = null;
                        break;
                }
            }
            if (e.Sender is CustomerRepository && e.ChangeReason==ChangeReasonList.Updated)
            {
                int id = SelectedRow?.Order.OrderId ?? 0;
                LoadData();
                SelectedRow = OrderList.FirstOrDefault(x => x.Order.OrderId == id);
            }
        }

        private ObservableCollection<RowViewModel> _orderList;
        public ObservableCollection<RowViewModel> OrderList
        {
            get { return _orderList; }
            set
            {
                _orderList = value;
                RaisePropertyChanged("OrderList");               
            }
        }

        private RowViewModel _selectedRow;
        public RowViewModel SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                RaisePropertyChanged("SelectedRow");

                EditedRowVm = null;
                OpenRowCmd.RaiseCanExecuteChanged();
                RemoveRowCmd.RaiseCanExecuteChanged();
            }
        }

        private EditedViewModel _editedRowVm;
        public EditedViewModel EditedRowVm
        {
            get { return _editedRowVm; }
            set
            {
                _editedRowVm = value;
                RaisePropertyChanged("EditedRow");
            }
        }

        private int _rowQty;
        public int RowQty
        {
            get { return _rowQty; }
            set
            {
                _rowQty = value;
                RaisePropertyChanged("RowQty");
            }
        }

        #region Commands: AddRowCmd, OpenRowCmd, RemoveRowCmd

        private RelayCommand _addRowCmd;
        public RelayCommand AddRowCmd
        {
            get { return _addRowCmd ?? (_addRowCmd = new RelayCommand(AddRowCmdExecute, AddRowCmdCanExecute)); }
        }
        private void AddRowCmdExecute()
        {
            //SelectedRow = null;
            EditedRowVm = new EditedViewModel(_mainRepo, 0) { IsNew = true, IsDirty = true };

            var res = DialogUtility.ShowModal(EditedRowVm);
            if (res.HasValue && res == true)
            {
                _mainRepo.OrderRepo.SaveItem(EditedRowVm.Model.Model);
            }
            EditedRowVm = null;
        }
        public bool AddRowCmdCanExecute()
        {
            return true;
        }

        private RelayCommand _openRowCmd;
        public RelayCommand OpenRowCmd
        {
            get { return _openRowCmd ?? (_openRowCmd = new RelayCommand(OpenRowCmdExecute, OpenRowCmdCanExecute)); }
        }
        private void OpenRowCmdExecute()
        {
            if (SelectedRow == null) return;
            EditedRowVm = new EditedViewModel(_mainRepo, SelectedRow.Order.OrderId);

            var res = DialogUtility.ShowModal(EditedRowVm);
            if (res.HasValue && res == true)
            {
                _mainRepo.OrderRepo.SaveItem(EditedRowVm.Model.Model);
            }
            EditedRowVm = null;
        }
        public bool OpenRowCmdCanExecute()
        {
            return SelectedRow != null;
        }

        private RelayCommand _removeRowCmd;
        public RelayCommand RemoveRowCmd
        {
            get { return _removeRowCmd ?? (_removeRowCmd = new RelayCommand(RemoveRowCmdExecute, RemoveRowCmdCanExecute)); }
        }
        private void RemoveRowCmdExecute()
        {
            if (SelectedRow == null) return;
            var res = MessageBox.Show(
                "Are you sure to delete record.",
                "Question",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );
            if (res == MessageBoxResult.Yes)
            {
                var id = SelectedRow.Order.OrderId;
                _mainRepo.OrderRepo.DeleteItem(id);
            }
        }
        public bool RemoveRowCmdCanExecute()
        {
            return SelectedRow != null;
        }

        #endregion


    }
}
