using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WbEasyCalcModel.WbEasyCalc;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData.ViewModel.Tabs 
{
    public class StartViewModel : ViewModelBase
    {
        private readonly EasyCalcViewModel _parentViewModel;

        #region Input props

        private int _startPeriodDaysM21;
        public int Start_PeriodDays_M21
        {
            get => _startPeriodDaysM21;
            set { _startPeriodDaysM21 = value; RaisePropertyChanged(nameof(Start_PeriodDays_M21)); CalculateExcel(); }
        }

        #endregion

        #region Output props
        #endregion

        public StartModel Model => new StartModel()
        {
            Start_PeriodDays_M21 = Start_PeriodDays_M21,
        };

        public StartViewModel(StartModel model, EasyCalcViewModel parentViewModel)
        {
            if (model == null) return;

            _parentViewModel = parentViewModel;

            // Input
            Start_PeriodDays_M21 = model.Start_PeriodDays_M21;
        }
        private void CalculateExcel()
        {
            _parentViewModel.Calculate();
        }
    }
}
