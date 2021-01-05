using System;
using System.Drawing;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Svg;
using Svg.Pathing;
using Svg.Transforms;

namespace Grundfos.SVG.WG.Builders
{
    public class PumpBuilder : IVisualElementBuilder
    {
        private readonly ColorServer colorServer;

        public PumpBuilder(ColorServer colorServer)
        {
            this.colorServer = colorServer;
            this.OriginalSize = 2f;
        }

        protected float OriginalSize { get; set; }

        public virtual Type GeometryType => typeof(Pump);

        public SvgVisualElement Build(Geometry geometry)
        {
            var pump = (SymbolGeometry)geometry;
            SvgPathSegmentList pathData = this.BuildPathSegmentList();
            var path = new SvgPath
            {
                ID = pump.ID.ToString(),
                PathData = pathData,
                Stroke = this.colorServer.ToSvgColourServer(pump.StrokeColor),
                StrokeOpacity = pump.StrokeColor.A / 255,
                StrokeWidth = new SvgUnit(SvgUnitType.Point, pump.StrokeWidthPoints),
                Fill = SvgPaintServer.None,
            };
            var group = new SvgGroup { ID = pump.ID.ToString() };
            group.Children.Add(path);
            group.Transforms.Insert(0, new SvgScale(pump.SymbolRadius / OriginalSize));
            group.Transforms.Insert(0, new SvgTranslate((float)pump.Center.X, -(float)pump.Center.Y));
            return group;
        }

        protected virtual SvgPathSegmentList BuildPathSegmentList()
        {
            var startPoint = new PointF(-0.4848126f, -0.846031f);
            var pathData = new SvgPathSegmentList
            {
                new SvgMoveToSegment(startPoint),
                new SvgArcSegment(startPoint, 1f, 1f, 0f, SvgArcSize.Large, SvgArcSweep.Positive, new PointF(-1f, 0f)),
                new SvgLineSegment(new PointF(-1f, 0f), new PointF(-1f, -1.5f)),
                new SvgLineSegment(new PointF(-1f, -1.5f), new PointF(0f, -1.5f)),
                new SvgLineSegment(new PointF(0f, -1.5f), new PointF(0f, -1f))
            };
            return pathData;
        }
    }
}
