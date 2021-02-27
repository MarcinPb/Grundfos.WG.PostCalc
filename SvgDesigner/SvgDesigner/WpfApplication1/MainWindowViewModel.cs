using WpfApplication1.Ui.Designer;
using WpfApplication1.Utility;

namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {

        public DataRepo DataRepo { get; set; }
        public EditedViewModel PropertyGridViewModel { get; set; }

        public MainWindowViewModel()
        {
            DataRepo = new DataRepo();
            PropertyGridViewModel = new EditedViewModel();
        }
    }
}
