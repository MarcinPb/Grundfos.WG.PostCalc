using System.Collections.ObjectModel;
using System.Windows;
using DAL;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Order
{
    public class EditedViewModel : ViewModelBase, IViewModelValidate
    {
        private ItemViewModel _model;
        public ItemViewModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
                IsDirty = true;
            }
        }

        public bool IsNew { get; set; }

        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

        private ObservableCollection<Model.Customer> _customerList;
        public ObservableCollection<Model.Customer> CustomerList
        {
            get { return _customerList; }
            set
            {
                _customerList = value;
                RaisePropertyChanged("CustomerList");
            }
        }

        private readonly IMainRepo _mainRepo;
        public EditedViewModel(IMainRepo mainRepo, int id)
        {
            _mainRepo = mainRepo;
            CustomerList = new ObservableCollection<Model.Customer>(_mainRepo.CustomerRepo.GetList());
            if (id > 0)
            {
                Model = new ItemViewModel(_mainRepo.OrderRepo.GetItem(id), _mainRepo);                
            }
            else
            {
                Model = new ItemViewModel(new Model.Order(), _mainRepo);                
            }
        }

        public bool Validate()
        {
            if (Model.Price >= 10000)
            {
                MessageBox.Show("To expensive.");
                return false;
            }
            return true;
        }
    }
}
