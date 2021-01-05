using Grundfos.GeometryModel;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public class PipeStrokeWidthService : IStrokeWidthService
    {
        public PipeStrokeWidthService()
        {
            this.DefaultWidth = 1f;
            this.PipeDiameterToWidthFactor = 0.4f;
            this.DiameterFieldName = "Physical_PipeDiameter";
        }

        public float DefaultWidth { get; set; }
        public float PipeDiameterToWidthFactor { get; set; }
        public string DiameterFieldName { get; set; }

        public double GetStrokeWidth(DomainObjectData item)
        {
            if (item.Fields.TryGetValue(this.DiameterFieldName, out object raw))
            {
                var diameter = (float)(double)raw;
                return this.PipeDiameterToWidthFactor * diameter;
            }

            return this.DefaultWidth;
        }
    }
}
