using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class PumpBatteryBuilder : IGeometryBuilder
    {
        private readonly IPainter painter;

        public PumpBatteryBuilder(IPainter painter)
        {
            this.painter = painter;
        }

        public ObjectTypes GeometryType => ObjectTypes.VariableSpeedPumpBattery;

        public Geometry Build(DomainObjectData data)
        {
            var item = new PumpBattery
            {
                ID = data.ID,
                Label = data.Label,
                Center = data.Geometry[0],
                SymbolRadius = 12, // TODO: Let IPainter service do this!
            };

            this.painter.Paint(data, item);
            return item;
        }
    }
}
