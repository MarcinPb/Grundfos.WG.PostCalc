﻿using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;
using WpfApplication1.Utility;

namespace WpfApplication1.ViewModel
{
    public class MapItem1 : ViewModelBase, IMapItem
    {
        public long Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(); }
        }

        public int TypeId { get; set; }
        public Location Location { get; set; }
        public List<Location> LocationList { get; set; }

    }
}
