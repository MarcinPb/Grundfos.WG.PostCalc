using System.Linq;
using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class PolylineBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public PolylineBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.Pipe;

        public Geometry Build(DomainObjectData data)
        {
            var geometry = new Polyline
            {
                ID = data.ID,
                Label = data.Label,
                Path = data.Geometry.ToList(),
            };

            this.painter.Paint(data, geometry);
            return geometry;
        }
    }
}
