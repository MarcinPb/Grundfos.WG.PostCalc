using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WbEasyCalcModel;
using WbEasyCalcModel.WbEasyCalc;
using WpfApplication1.Ui.WbEasyCalcData.ViewModel.Tabs;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData.ViewModel
{
    public class EasyCalcViewModel : ViewModelBase
    {

        public readonly ItemViewModel _parentViewModel;

        private StartViewModel _startViewModel;
        public StartViewModel StartViewModel
        {
            get => _startViewModel;
            set { _startViewModel = value; RaisePropertyChanged(nameof(StartViewModel)); }
        }

        private SysInputViewModel _sysInputViewModel;
        public SysInputViewModel SysInputViewModel
        {
            get => _sysInputViewModel;
            set { _sysInputViewModel = value; RaisePropertyChanged(nameof(SysInputViewModel)); }
        }
        private BilledConsViewModel _billedConsViewModel;
        public BilledConsViewModel BilledConsViewModel
        {
            get => _billedConsViewModel;
            set { _billedConsViewModel = value; RaisePropertyChanged(nameof(BilledConsViewModel)); }
        }


        public UnbConsViewModel UnbConsViewModel { get; set; }
        public UnauthConsViewModel UnauthConsViewModel { get; set; }
        public MeterErrorsViewModel MeterErrorsViewModel { get; set; }
        public NetworkViewModel NetworkViewModel { get; set; }
        public PressureViewModel PressureViewModel { get; set; }
        public IntermittentSupplyViewModel IntermittentSupplyViewModel { get; set; }
        public FinancialDataViewModel FinancialDataViewModel { get; set; }

        private WaterBalanceViewModel _waterBalanceDayViewModel;
        public WaterBalanceViewModel WaterBalanceDayViewModel
        {
            get => _waterBalanceDayViewModel;
            set { _waterBalanceDayViewModel = value; RaisePropertyChanged(nameof(WaterBalanceDayViewModel)); }
        }
        private WaterBalanceViewModel _waterBalancePeriodViewModel;
        public WaterBalanceViewModel WaterBalancePeriodViewModel
        {
            get => _waterBalancePeriodViewModel;
            set { _waterBalancePeriodViewModel = value; RaisePropertyChanged(nameof(WaterBalancePeriodViewModel)); }
        }
        private WaterBalanceViewModel _waterBalanceYearViewModel;
        public WaterBalanceViewModel WaterBalanceYearViewModel
        {
            get => _waterBalanceYearViewModel;
            set { _waterBalanceYearViewModel = value; RaisePropertyChanged(nameof(WaterBalanceYearViewModel)); }
        }
        private PisViewModel _pisViewModel;
        public PisViewModel PisViewModel
        {
            get => _pisViewModel;
            set { _pisViewModel = value; RaisePropertyChanged(nameof(PisViewModel)); }
        }



        public EasyCalcModel Model => new EasyCalcModel()
        {
            StartModel = StartViewModel.Model,
            SysInputModel = SysInputViewModel.Model,
            BilledConsModel = BilledConsViewModel.Model,

            UnbilledConsModel = new UnbilledConsModel(),
            UnauthConsModel = new UnauthConsModel(),
            MetErrorsModel = new MetErrorsModel(),
            NetworkModel = new NetworkModel(),
            PressureModel = new PressureModel(),
            IntermModel = new IntermModel(),
            FinancDataModel = new FinancDataModel(),

            WaterBalanceDay = WaterBalanceDayViewModel.Model,
            WaterBalancePeriod = WaterBalancePeriodViewModel.Model,
            WaterBalanceYear = WaterBalanceYearViewModel.Model,
            Pis = PisViewModel.Model,
        };

        public EasyCalcViewModel(EasyCalcModel model, ItemViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;

            if (model == null) return;

            StartViewModel = new StartViewModel(model.StartModel, this);
            SysInputViewModel = new SysInputViewModel(model.SysInputModel, this);
            BilledConsViewModel = new BilledConsViewModel(model.BilledConsModel, this);

            UnbConsViewModel = new UnbConsViewModel(model.UnbilledConsModel, this);
            UnauthConsViewModel = new UnauthConsViewModel(model.UnauthConsModel, this);
            MeterErrorsViewModel = new MeterErrorsViewModel(model.MetErrorsModel, this);
            NetworkViewModel = new NetworkViewModel(model.NetworkModel, this);
            PressureViewModel = new PressureViewModel(model.PressureModel, this);
            IntermittentSupplyViewModel = new IntermittentSupplyViewModel(model.IntermModel, this);
            FinancialDataViewModel = new FinancialDataViewModel(model.FinancDataModel, this);

            WaterBalanceDayViewModel = new WaterBalanceViewModel(model.WaterBalanceDay);
            WaterBalancePeriodViewModel = new WaterBalanceViewModel(model.WaterBalancePeriod);
            WaterBalanceYearViewModel = new WaterBalanceViewModel(model.WaterBalanceYear);
            PisViewModel = new PisViewModel(model.Pis);
        }

        public void Calculate()
        {
            _parentViewModel.CalculateExcelNew();
        }

        internal void Refreash(EasyCalcModel easyCalcModel)
        {
            //StartViewModel.Refreash(easyCalcModel);
            SysInputViewModel.Refreash(easyCalcModel.SysInputModel);
            BilledConsViewModel.Refreash(easyCalcModel.BilledConsModel);
            UnbConsViewModel.Refreash(easyCalcModel.UnbilledConsModel);
            UnauthConsViewModel.Refreash(easyCalcModel.UnauthConsModel);
            MeterErrorsViewModel.Refreash(easyCalcModel.MetErrorsModel);
            NetworkViewModel.Refreash(easyCalcModel.NetworkModel);
            PressureViewModel.Refreash(easyCalcModel.PressureModel);
            IntermittentSupplyViewModel.Refreash(easyCalcModel.IntermModel);
            FinancialDataViewModel.Refreash(easyCalcModel.FinancDataModel);

            WaterBalanceDayViewModel.Refreash(easyCalcModel.WaterBalanceDay);
            WaterBalancePeriodViewModel.Refreash(easyCalcModel.WaterBalancePeriod);
            WaterBalanceYearViewModel.Refreash(easyCalcModel.WaterBalanceYear);
            PisViewModel.Refreash(easyCalcModel.Pis);
        }
    }
}
