using System;
using System.Drawing;
using System.Globalization;

using Grundfos.GeometryModel;

using Svg;
using Svg.Pathing;

namespace Grundfos.SVG.Builders
{
    public class PathBuilder : IVisualElementBuilder
    {
        private readonly ColorServer colorServer;
        private readonly SvgPaintServer fill;

        public PathBuilder(ColorServer colorServer)
        {
            this.colorServer = colorServer;
            this.fill = SvgPaintServer.None;
        }

        public Type GeometryType => typeof(Polyline);

        public SvgVisualElement Build(Geometry geometry)
        {
            var item = (Polyline)geometry;
            SvgPathSegmentList pathData = this.BuildPathSegmentList(item);

            var path = new SvgPath
            {
                ID = item.ID.ToString(CultureInfo.InvariantCulture),
                PathData = pathData,
                Stroke = this.colorServer.ToSvgColourServer(item.StrokeColor),
                StrokeWidth = new SvgUnit(SvgUnitType.Point, item.StrokeWidthPoints),
                Fill = this.fill,
            };
            return path;
        }

        protected virtual SvgPathSegmentList BuildPathSegmentList(Polyline item)
        {
            var pathData = new SvgPathSegmentList();
            var startPoint = item.Path[0];
            pathData.Add(new SvgMoveToSegment(new PointF((float)startPoint.X, -(float)startPoint.Y)));
            for (int i = 1; i < item.Path.Count; i++)
            {
                var previous = item.Path[i - 1];
                var next = item.Path[i];
                var sg = new SvgLineSegment(new PointF((float)previous.X, -(float)previous.Y), new PointF((float)next.X, -(float)next.Y));
                pathData.Add(sg);
            }

            return pathData;
        }
    }
}
