using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class ReservoirBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public ReservoirBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.Reservoir;

        public Geometry Build(DomainObjectData data)
        {
            var item = new Reservoir
            {
                ID = data.ID,
                Label = data.Label,
                Center = data.Geometry[0],
                SymbolRadius = 25, // TODO: Let IPainter service do this!
            };

            this.painter.Paint(data, item);
            return item;
        }
    }
}
