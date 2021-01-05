using System.Collections.Generic;
using Grundfos.GeometryModel;
using Grundfos.GeometryModel.Builders.Painters;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public class StaticLayerPipePainter : IPainter 
    {
        private readonly IDictionary<ObjectTypes, IStrokeWidthService> strokeWidthServices;
        private readonly IColorService colorService;

        public StaticLayerPipePainter(IDictionary<ObjectTypes, IStrokeWidthService> strokeWidthServices, IColorService colorService)
        {
            this.strokeWidthServices = strokeWidthServices;
            this.colorService = colorService;
        }

        public void Paint(DomainObjectData data, Geometry geometry)
        {
            geometry.StrokeColor = this.colorService.GetColor(data);
            var service = this.strokeWidthServices[data.ObjectType];
            geometry.StrokeWidthPoints = (float)service.GetStrokeWidth(data);
        }
    }
}
