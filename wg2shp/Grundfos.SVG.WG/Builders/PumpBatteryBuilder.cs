using System;
using System.Drawing;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Svg.Pathing;

namespace Grundfos.SVG.WG.Builders
{
    public class PumpBatteryBuilder : PumpBuilder, IVisualElementBuilder
    {
        public PumpBatteryBuilder(ColorServer colorServer) : base(colorServer)
        {
            this.OriginalSize = 5f;
        }

        public override Type GeometryType => typeof(PumpBattery);

        protected override SvgPathSegmentList BuildPathSegmentList()
        {
            var basePath = base.BuildPathSegmentList();
            basePath.Add(new SvgMoveToSegment(new PointF(2.5f, 2.5f)));
            basePath.Add(new SvgLineSegment(new PointF(2.5f, 2.5f), new PointF(2.5f, -3f)));
            basePath.Add(new SvgLineSegment(new PointF(2.5f, -3f), new PointF(-2.5f, -3f)));
            basePath.Add(new SvgLineSegment(new PointF(-2.5f, -3f), new PointF(-2.5f, 2.5f)));
            basePath.Add(new SvgClosePathSegment());
            return basePath;
        }
    }
}
