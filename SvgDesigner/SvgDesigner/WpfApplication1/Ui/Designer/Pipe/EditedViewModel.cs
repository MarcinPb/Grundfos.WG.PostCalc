﻿using GeometryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using WpfApplication1.ShapeModel;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Designer.Pipe
{
    public class EditedViewModel : Ui.Designer.EditedViewModel
    {
        private ItemViewModel _model;
        public ItemViewModel ItemViewModel
        {
            get => _model;
            set { _model = value; RaisePropertyChanged(nameof(ItemViewModel)); }
        }

        public EditedViewModel(int id) 
        {
            try
            {
                //Messenger.Default.Register<Shp>(this, OnShpReceived);
                ItemViewModel = new ItemViewModel(id);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
