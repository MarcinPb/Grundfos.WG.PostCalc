using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class PrvBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public PrvBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.PRV;

        public Geometry Build(DomainObjectData data)
        {
            var item = new Prv
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
