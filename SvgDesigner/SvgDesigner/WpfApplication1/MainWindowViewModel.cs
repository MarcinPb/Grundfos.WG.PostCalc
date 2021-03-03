using GeometryModel;
using WpfApplication1.ShapeModel;
using WpfApplication1.Ui.Designer;
using WpfApplication1.Utility;

namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {

        public DesignerViewModel DesignerViewModel { get; set; }

        private Ui.Designer.EditedViewModel _propertyGridViewModel;
        public Ui.Designer.EditedViewModel PropertyGridViewModel
        {
            get { return _propertyGridViewModel; }
            set { _propertyGridViewModel = value; RaisePropertyChanged(nameof(PropertyGridViewModel)); }
        }


        public MainWindowViewModel()
        {
            DesignerViewModel = new DesignerViewModel();
            PropertyGridViewModel = new Ui.Designer.EditedViewModel();

            Messenger.Default.Register<Shp>(this, OnShpReceived);
        }

        private void OnShpReceived(Shp shp)
        {
            if (shp is LinkMy)
            {
                PropertyGridViewModel = new Ui.Designer.Pipe.EditedViewModel(shp.Id);
            }
            else if (shp is ObjMy)
            {
                PropertyGridViewModel = new Ui.Designer.Junction.EditedViewModel(shp.Id);
            }
            else if (shp is CnShp)
            {
                PropertyGridViewModel = new Ui.Designer.CustomerNode.EditedViewModel(shp.Id);
            }
        }
    }
}
