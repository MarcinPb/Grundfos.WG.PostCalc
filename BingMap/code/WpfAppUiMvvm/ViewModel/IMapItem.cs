using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.ViewModel
{
    public interface IMapItem
    {
        long Id { get; set; }
        string Name { get; set; }
        int TypeId { get; set; }
        Location Location { get; set; }
        List<Location> LocationList { get; set; }
    }
}
