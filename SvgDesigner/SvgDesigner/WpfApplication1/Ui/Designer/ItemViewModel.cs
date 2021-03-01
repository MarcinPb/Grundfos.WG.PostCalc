using GeometryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Designer
{
    public class ItemViewModel : ViewModelBase
    {
        private int? _id;
        public int? Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set { _x = value; RaisePropertyChanged("X"); }
        }

        private double _y;
        public double Y
        {
            get { return _y; }
            set { _y = value; RaisePropertyChanged("Y"); }
        }

        private List<Point2D> _path;
        public List<Point2D> Path
        {
            get { return _path; }
            set { _path = value; RaisePropertyChanged("Path"); }
        }

        public ItemViewModel(Pipe model)
        {
            Id = model?.ID;
            Name = model?.Label;


        }
    }
}
