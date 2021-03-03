using Database.DataRepository;
using GeometryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Designer.CustomerNode
{
    public class ItemViewModel : Ui.Designer.ItemXyViewModel
    {
        //private double _x;
        //public double X
        //{
        //    get { return _x; }
        //    set { _x = value; RaisePropertyChanged("X"); }
        //}

        //private double _y;
        //public double Y
        //{
        //    get { return _y; }
        //    set { _y = value; RaisePropertyChanged("Y"); }
        //}

        public ItemViewModel(int id) : base(id)
        {
            //X = (double)_model.Geometry[0].X;
            //Y = (double)_model.Geometry[0].Y;
        }
    }
}
