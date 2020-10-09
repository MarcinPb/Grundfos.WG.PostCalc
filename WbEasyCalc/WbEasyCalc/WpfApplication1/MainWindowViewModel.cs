using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataModel;
using DataRepository;
using WpfApplication1.Utility;

namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Ui.WbEasyCalcData.ListViewModel _customerViewModel;
        public Ui.WbEasyCalcData.ListViewModel WbEasyCalcDataViewModel
        {
            get => _customerViewModel;
            set { _customerViewModel = value; RaisePropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            WbEasyCalcDataViewModel = new Ui.WbEasyCalcData.ListViewModel();
        }

    }
}
