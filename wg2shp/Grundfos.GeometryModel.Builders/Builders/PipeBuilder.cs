using System;
using System.Linq;
using Grundfos.GeometryModel.Builders.Utils;
using Grundfos.TW2WG.AttributeService;
using Grundfos.WG2SVG.ConsoleApp.Painters;

namespace Grundfos.GeometryModel.Builders
{
    public class PipeBuilder : IGeometryBuilder
    {
        public const double DefaultArrowSize = 5;

        private readonly PathLenghtResolver pathLengthResolver;
        private readonly PathTraveller pathTraveller;
        private readonly IPainter painter;
        private readonly IWgAttributeService attributeService;

        public PipeBuilder(IPainter painter, IWgAttributeService attributeService)
        {
            this.pathLengthResolver = new PathLenghtResolver();
            this.pathTraveller = new PathTraveller();
            this.painter = painter;
            this.attributeService = attributeService;
        }

        public ObjectTypes GeometryType => ObjectTypes.Pipe;

        public Geometry Build(DomainObjectData data)
        {
            double segmentLength = this.pathLengthResolver.GetLength(data.Geometry);
            double halfLength = segmentLength / 2;
            var pointer = this.pathTraveller.GetPointerAt(data.Geometry, halfLength);
            // TODO: make flow representation more configurable.
            var flow = this.attributeService.GetAttributeValue(data.ID, (int)WgAttributes.PipeFlow);
            var isPointerVisible = this.attributeService.GetAttributeValue(data.ID, (int)WgAttributes.IsOpen)!=1;
            if (flow < 0)
            {
                pointer.Direction = UnitVector2D.Build(-pointer.Direction.X, -pointer.Direction.Y);
            }

            var geometry = new Pipe
            {
                ID = data.ID,
                Label = data.Label,
                Path = data.Geometry.ToList(),
                ArrowDirection = pointer,
                //ArrowSize = DefaultArrowSize,
                //ArrowSize = 0,
                ArrowSize = isPointerVisible ? DefaultArrowSize : 0,
            };

            this.painter.Paint(data, geometry);
            return geometry;
        }
    }
}
