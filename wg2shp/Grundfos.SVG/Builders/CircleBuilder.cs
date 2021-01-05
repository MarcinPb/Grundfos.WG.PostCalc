using System;
using Grundfos.GeometryModel;

using Svg;

namespace Grundfos.SVG.Builders
{
    public class CircleBuilder : IVisualElementBuilder
    {
        private readonly ColorServer colorServer;

        public CircleBuilder(ColorServer colorServer)
        {
            this.colorServer = colorServer;
        }

        public Type GeometryType => typeof(Dot);

        public SvgVisualElement Build(Geometry geometry)
        {
            var item = (Dot)geometry;
            var paint = this.colorServer.ToSvgColourServer(item.StrokeColor);
            var circle = new SvgCircle
            {
                ID = geometry.ID.ToString(),
                CenterX = (float)item.Center.X,
                CenterY = (float)-item.Center.Y,
                Radius = item.StrokeWidthPoints,
                Stroke = paint,
                StrokeWidth = item.StrokeWidthPoints,
                Fill = paint,
            };

            return circle;
        }
    }
}
