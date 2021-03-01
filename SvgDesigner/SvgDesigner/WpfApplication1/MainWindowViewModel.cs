using WpfApplication1.Ui.Designer;
using WpfApplication1.Utility;

namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {

        public DesignerViewModel DesignerViewModel { get; set; }
        public EditedViewModel PropertyGridViewModel { get; set; }

        public MainWindowViewModel()
        {
            DesignerViewModel = new DesignerViewModel();
            PropertyGridViewModel = new EditedViewModel();
        }
    }
}
