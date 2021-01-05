using Grundfos.GeometryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public interface IPainter
    {
        void Paint(DomainObjectData data, Geometry geometry);
    }
}
