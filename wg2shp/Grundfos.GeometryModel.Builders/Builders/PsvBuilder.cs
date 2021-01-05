using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class PsvBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public PsvBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.PSV;

        public Geometry Build(DomainObjectData data)
        {
            var item = new Psv
            {
                ID = data.ID,
                Label = data.Label,
                Center = data.Geometry[0],
                SymbolRadius = 24, // TODO: Let IPainter service do this!
            };

            this.painter.Paint(data, item);
            return item;
        }
    }
}
