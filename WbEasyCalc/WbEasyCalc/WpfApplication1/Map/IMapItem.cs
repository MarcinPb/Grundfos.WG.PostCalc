using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Map
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
