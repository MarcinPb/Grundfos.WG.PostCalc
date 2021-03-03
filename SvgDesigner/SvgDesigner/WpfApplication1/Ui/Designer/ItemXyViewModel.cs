﻿using Database.DataRepository;
using GeometryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Designer
{
    public class ItemXyViewModel : Ui.Designer.ItemViewModel
    {
        private double _x;
        [Category("<Geometry>")]
        [DisplayName("X")]
        public double X
        {
            get { return _x; }
            set { _x = value; RaisePropertyChanged("X"); }
        }

        private double _y;
        [Category("<Geometry>")]
        [DisplayName("Y")]
        public double Y
        {
            get { return _y; }
            set { _y = value; RaisePropertyChanged("Y"); }
        }


        public ItemXyViewModel(int id) : base(id)
        {
            X = (double)_model.Geometry[0].X;
            Y = (double)_model.Geometry[0].Y;
        }
    }
}
