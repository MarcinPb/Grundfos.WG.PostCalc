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
        //private ItemViewModel _model;
        //public ItemViewModel ItemViewModel
        //{
        //    get => _model;
        //    set { _model = value; RaisePropertyChanged(nameof(ItemViewModel)); }
        //}

        public EditedViewModel()
        {
            // Messenger.Default.Register<Shp>(this, OnShpReceived);
        }
        public virtual  void OnShpReceived(Shp shp)
        {
            var i = 8;
            //ItemViewModel.Id = shp.Id;
            //ItemViewModel.Name = shp.Name;
            //ItemViewModel.Path = shp is LinkMy ? ((LinkMy)shp).Path : null;
            //ItemViewModel.X = shp.X;
            //ItemViewModel.Y = shp.Y;
        }
    }
}
