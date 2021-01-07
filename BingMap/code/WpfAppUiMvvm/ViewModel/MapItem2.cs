using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;
using WpfApplication1.Utility;

namespace WpfApplication1.ViewModel
{
    public class MapItem2 : ViewModelBase, IMapItem 
    {

        public long Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(); }
        }
        private int _strokeThickness;
        public int StrokeThickness
        {
            get => _strokeThickness;
            set { _strokeThickness = value; RaisePropertyChanged(); }
        }
        public int TypeId { get; set; }

        public MapItem1 StartPoint { get; set; }
        public MapItem1 EndPoint { get; set; }

        public Location Location { get; set; }
        public List<Location> LocationList { get; set; }
    }
}
