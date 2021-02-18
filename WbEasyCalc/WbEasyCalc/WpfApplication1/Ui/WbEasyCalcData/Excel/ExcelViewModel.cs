using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WbEasyCalcModel;
using WbEasyCalcModel.WbEasyCalc;
using WpfApplication1.Ui.WbEasyCalcData.Excel.WaterBalance;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData.Excel
{
    public class ExcelViewModel : ViewModelBase
    {

        public readonly ItemViewModel _parentViewModel;

        private Start.ViewModel _startViewModel;
        public Start.ViewModel StartViewModel
        {
            get => _startViewModel;
            set { _startViewModel = value; RaisePropertyChanged(nameof(StartViewModel)); }
        }

        private SysInput.ViewModel _sysInputViewModel;
        public SysInput.ViewModel SysInputViewModel
        {
            get => _sysInputViewModel;
            set { _sysInputViewModel = value; RaisePropertyChanged(nameof(SysInputViewModel)); }
        }

        //private ViewModel _billedConsViewModel;
        //public ViewModel ViewModel
        //{
        //    get => _billedConsViewModel;
        //    set { _billedConsViewModel = value; RaisePropertyChanged(nameof(ViewModel)); }
        //}
        public BilledCons.ViewModel BilledConsViewModel { get; set; }


        private UnbCons.ViewModel _unbConsViewModel;
        public UnbCons.ViewModel UnbConsViewModel
        {
            get => _unbConsViewModel;
            set { _unbConsViewModel = value; RaisePropertyChanged(nameof(UnbConsViewModel)); }
        }

        public UnauthCons.ViewModel UnauthConsViewModel { get; set; }
        public MeterErrors.ViewModel MeterErrorsViewModel { get; set; }
        public Network.ViewModel NetworkViewModel { get; set; }
        public Pressure.ViewModel PressureViewModel { get; set; }
        public IntermittentSupply.ViewModel IntermittentSupplyViewModel { get; set; }
        public FinancialData.ViewModel FinancialDataViewModel { get; set; }

        private ViewModel _waterBalanceDayViewModel;
        public ViewModel WaterBalanceDayViewModel
        {
            get => _waterBalanceDayViewModel;
            set { _waterBalanceDayViewModel = value; RaisePropertyChanged(nameof(WaterBalanceDayViewModel)); }
        }
        private ViewModel _waterBalancePeriodViewModel;
        public ViewModel WaterBalancePeriodViewModel
        {
            get => _waterBalancePeriodViewModel;
            set { _waterBalancePeriodViewModel = value; RaisePropertyChanged(nameof(WaterBalancePeriodViewModel)); }
        }
        private ViewModel _waterBalanceYearViewModel;
        public ViewModel WaterBalanceYearViewModel
        {
            get => _waterBalanceYearViewModel;
            set { _waterBalanceYearViewModel = value; RaisePropertyChanged(nameof(WaterBalanceYearViewModel)); }
        }
        private Pis.ViewModel _pisViewModel;
        public Pis.ViewModel PisViewModel
        {
            get => _pisViewModel;
            set { _pisViewModel = value; RaisePropertyChanged(nameof(PisViewModel)); }
        }


        public EasyCalcModel Model { get; set; }

        //public EasyCalcModel Model => new EasyCalcModel()
        //{
        //    StartModel = ViewModel.Model,
        //    SysInputModel = ViewModel.Model,
        //    BilledConsModel = ViewModel.Model,

        //    UnbilledConsModel = ViewModel.Model,
        //    UnauthConsModel = ViewModel.Model,
        //    MetErrorsModel = ViewModel.Model,
        //    NetworkModel = ViewModel.Model,
        //    PressureModel = ViewModel.Model,
        //    IntermModel = ViewModel.Model,
        //    FinancDataModel = ViewModel.Model,

        //    WaterBalanceDay = WaterBalanceDayViewModel.Model,
        //    WaterBalancePeriod = WaterBalancePeriodViewModel.Model,
        //    WaterBalanceYear = WaterBalanceYearViewModel.Model,
        //    Pis = ViewModel.Model,
        //};

        public ExcelViewModel(EasyCalcModel model, ItemViewModel parentViewModel)
        {
            if (model == null) return;
            Model = model;

            _parentViewModel = parentViewModel;

            StartViewModel = new Start.ViewModel(model.StartModel, this);
            SysInputViewModel = new SysInput.ViewModel(Model.SysInputModel, this);
            BilledConsViewModel = new BilledCons.ViewModel(model.BilledConsModel, this);

            UnbConsViewModel = new UnbCons.ViewModel(model.UnbilledConsModel, this);
            UnauthConsViewModel = new UnauthCons.ViewModel(model.UnauthConsModel, this);
            MeterErrorsViewModel = new MeterErrors.ViewModel(model.MetErrorsModel, this);
            NetworkViewModel = new Network.ViewModel(model.NetworkModel, this);
            PressureViewModel = new Pressure.ViewModel(model.PressureModel, this);
            IntermittentSupplyViewModel = new IntermittentSupply.ViewModel(model.IntermModel, this);
            FinancialDataViewModel = new FinancialData.ViewModel(model.FinancDataModel, this);

            WaterBalanceDayViewModel = new ViewModel(model.WaterBalanceDay);
            WaterBalancePeriodViewModel = new ViewModel(model.WaterBalancePeriod);
            WaterBalanceYearViewModel = new ViewModel(model.WaterBalanceYear);
            PisViewModel = new Pis.ViewModel(model.Pis);
        }

        public void Calculate()
        {
            //_parentViewModel.CalculateExcelNew();

            new WbEasyCalcRepository.WbEasyCalc().CalculateNew(Model);
            RefreashViewModel(Model);
        }

        internal void RefreashViewModel(EasyCalcModel easyCalcModel)
        {
            //ViewModel.Refreash(easyCalcModel);
            SysInputViewModel?.Refreash(easyCalcModel.SysInputModel);
            BilledConsViewModel?.Refreash(easyCalcModel.BilledConsModel);
            UnbConsViewModel?.Refreash(easyCalcModel.UnbilledConsModel);
            UnauthConsViewModel?.Refreash(easyCalcModel.UnauthConsModel);
            MeterErrorsViewModel?.Refreash(easyCalcModel.MetErrorsModel);
            NetworkViewModel?.Refreash(easyCalcModel.NetworkModel);
            PressureViewModel?.Refreash(easyCalcModel.PressureModel);
            IntermittentSupplyViewModel?.Refreash(easyCalcModel.IntermModel);
            FinancialDataViewModel?.Refreash(easyCalcModel.FinancDataModel);

            WaterBalanceDayViewModel?.Refreash(easyCalcModel.WaterBalanceDay);
            WaterBalancePeriodViewModel?.Refreash(easyCalcModel.WaterBalancePeriod);
            WaterBalanceYearViewModel?.Refreash(easyCalcModel.WaterBalanceYear);
            PisViewModel?.Refreash(easyCalcModel.Pis);
        }
    }
}
