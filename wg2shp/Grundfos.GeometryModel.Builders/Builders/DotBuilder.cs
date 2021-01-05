using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class DotBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public DotBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.Junction;

        public Geometry Build(DomainObjectData data)
        {
            var dot = new Dot
            {
                ID = data.ID,
                Label = data.Label,
                Center = data.Geometry[0],
            };

            this.painter.Paint(data, dot);
            return dot;
        }
    }
}
