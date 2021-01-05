using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Svg;
using Svg.Pathing;

namespace Grundfos.SVG.WG.Builders
{
    public class PipeBuilder : IVisualElementBuilder
    {
        private readonly ColorServer colorServer;
        private const float DashPatternDash = 20;
        private const float DashPatternGap = 4;

        public PipeBuilder(ColorServer colorServer)
        {
            this.colorServer = colorServer;
        }

        public Type GeometryType => typeof(Pipe);

        public SvgVisualElement Build(Geometry geometry)
        {
            var item = (Pipe)geometry;
            var line = new SvgPath
            {
                PathData = this.BuildPathSegmentList(item),
                Stroke = this.colorServer.ToSvgColourServer(item.StrokeColor),
                StrokeOpacity = item.StrokeColor.A / 255,
                StrokeWidth = new SvgUnit(SvgUnitType.Point, item.StrokeWidthPoints),
                Fill = SvgPaintServer.None,
            };

            var arrow = new SvgPath
            {
                PathData = this.BuildArrow(item.ArrowDirection, item.ArrowSize),
                Stroke = SvgPaintServer.None,
                StrokeWidth = new SvgUnit(SvgUnitType.Point, 0),
                Fill = this.colorServer.ToSvgColourServer(item.StrokeColor),
                FillOpacity = item.StrokeColor.A / 255,
            };

            var group = new SvgGroup { ID = item.ID.ToString(), };
            group.Children.Add(line);
            group.Children.Add(arrow);

            return group;
        }

        private SvgPathSegmentList BuildPathSegmentList(Polyline item)
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

        private SvgPathSegmentList BuildArrow(Pointer pointer, double arrowSize)
        {
            var vectorA = pointer.Direction;

            var phiB = (2 * Math.PI) / 3;
            var cosPhiB = Math.Cos(phiB);
            var sinPhiB = Math.Sin(phiB);
            var vectorB = UnitVector2D.Build(
                (pointer.Direction.X * cosPhiB) - (pointer.Direction.Y * sinPhiB),
                (pointer.Direction.X * sinPhiB) + (pointer.Direction.Y * cosPhiB)
            );

            var phiC = (2 * 2 * Math.PI) / 3;
            var cosPhiC = Math.Cos(phiC);
            var sinPhiC = Math.Sin(phiC);
            var vectorC = UnitVector2D.Build(
                (pointer.Direction.X * cosPhiC) - (pointer.Direction.Y * sinPhiC),
                (pointer.Direction.X * sinPhiC) + (pointer.Direction.Y * cosPhiC)
            );

            var a = new PointF((float)(pointer.Point.X + (vectorA.X * arrowSize)), -(float)(pointer.Point.Y + (vectorA.Y * arrowSize)));
            var b = new PointF((float)(pointer.Point.X + (vectorB.X * arrowSize)), -(float)(pointer.Point.Y + (vectorB.Y * arrowSize)));
            var c = new PointF((float)(pointer.Point.X + (vectorC.X * arrowSize)), -(float)(pointer.Point.Y + (vectorC.Y * arrowSize)));
            var pathData = new SvgPathSegmentList
            {
                new SvgMoveToSegment(a),
                new SvgLineSegment(a, b),
                new SvgLineSegment(b, c),
                new SvgClosePathSegment(),
            };

            return pathData;
        }
    }
}
