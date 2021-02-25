using WpfApplication1.Utility;

namespace WpfApplication1.ShapeModel
{
    public class Shp : ViewModelBase
    {
        private double _x;
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged("X");
            }
        }
        public double Y { get; set; }
    }
}
