using GeometryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using WpfApplication1.ShapeModel;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Designer
{
    public class EditedViewModel : ViewModelBase
    {
        private ItemViewModel _model;
        public ItemViewModel ItemViewModel
        {
            get => _model;
            set { _model = value; RaisePropertyChanged(nameof(ItemViewModel)); }
        }

        public EditedViewModel()
        {
            try
            {
                Messenger.Default.Register<Shp>(this, OnShpReceived);

                //Model = new ItemViewModel(GlobalConfig.DataRepository.WaterConsumptionListRepository.GetItem(id));
                //Pipe model = new Pipe() { 
                //    ID=8768,
                //    Label="p-9879-uy",
                //};
                ItemViewModel = new ItemViewModel(null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void OnShpReceived(Shp shp)
        {
            ItemViewModel.Id = (int)((LinkMy)shp).LinkId;
            ItemViewModel.Name = $"{shp.X},{shp.Y}";
        }
    }
}
