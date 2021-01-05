using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class HydrantBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public HydrantBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.IdahoHydrant;

        public Geometry Build(DomainObjectData data)
        {
            var item = new Hydrant
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
