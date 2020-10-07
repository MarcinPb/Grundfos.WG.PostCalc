using System;
using System.Windows;
using DataRepository;
//using DataRepository;
//using DataRepository.WbEasyCalcData;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{
    public class EditedViewModel : ViewModelBase
    {
        private ItemViewModel _model;
        public ItemViewModel Model
        {
            get => _model;
            set { _model = value; RaisePropertyChanged(); }
        }

        public EditedViewModel(int id)
        {
            Model = new ItemViewModel(GlobalConfig.WbEasyCalcDataRepo.GetItem(id));
        }
    }
}
