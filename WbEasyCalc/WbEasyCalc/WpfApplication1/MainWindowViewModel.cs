using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataModel;
using DataRepository;
using GlobalRepository;
using WpfApplication1.Utility;
using WpfApplication1.Ui.WbEasyCalcData;
using WpfApplication1.Ui.WaterConsumption;
using NLog;

namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        private Ui.WbEasyCalcData.ListViewModel _customerViewModel;
        public Ui.WbEasyCalcData.ListViewModel WbEasyCalcDataViewModel
        {
            get => _customerViewModel;
            set { _customerViewModel = value; RaisePropertyChanged(); }
        }

        private Ui.WaterConsumption.ListViewModel _waterConsumptionViewModel;
        public Ui.WaterConsumption.ListViewModel WaterConsumptionViewModel
        {
            get => _waterConsumptionViewModel;
            set { _waterConsumptionViewModel = value; RaisePropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            Logger.Info("'MainWindowViewModel' started.");

            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            WbEasyCalcDataViewModel = new Ui.WbEasyCalcData.ListViewModel();
            WaterConsumptionViewModel = new Ui.WaterConsumption.ListViewModel();
        }

    }
}
