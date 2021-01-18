using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Utility;

namespace WpfApplication1.Map
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

        private Location _location;
        public Location Location 
        {
            get => _location;
            set { _location = value; RaisePropertyChanged(); }
        }



        public List<Location> LocationList { get; set; }

    }
}
