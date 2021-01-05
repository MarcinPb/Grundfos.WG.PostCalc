using System;
using System.Collections.Generic;
using System.Drawing;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Svg;
using Svg.Pathing;
using Svg.Transforms;

namespace Grundfos.SVG.WG.Builders
{
    public class PsvBuilder : IVisualElementBuilder
    {
        public const float OriginalSize = 3f;
        private readonly ColorServer colorServer;

        public PsvBuilder(ColorServer colorServer)
        {
            this.colorServer = colorServer;
        }

        public Type GeometryType => typeof(Psv);

        public SvgVisualElement Build(Geometry geometry)
        {
            var item = (Psv)geometry;
            var paths = this.BuildPaths(item);
            var group = new SvgGroup { ID = item.ID.ToString() };
            paths.ForEach(group.Children.Add);
            group.Transforms.Insert(0, new SvgScale(item.SymbolRadius / OriginalSize));
            group.Transforms.Insert(0, new SvgTranslate((float)item.Center.X, -(float)item.Center.Y));
            return group;
        }

        protected virtual List<SvgPath> BuildPaths(Psv item)
        {
            return new List<SvgPath>
            {
                new SvgPath
                {
                    PathData = new SvgPathSegmentList
                    {
                        new SvgMoveToSegment(new PointF(0f, 0f)),
                        new SvgLineSegment(new PointF(0f, 0f), new PointF(1f, 1f)),
                        new SvgLineSegment(new PointF(1f, 1f), new PointF(1f, -1f)),
                        new SvgClosePathSegment()
                    },
                    Stroke = this.colorServer.ToSvgColourServer(item.StrokeColor),
                    StrokeOpacity = item.StrokeColor.A / 255,
                    StrokeWidth = new SvgUnit(SvgUnitType.Point, item.StrokeWidthPoints),
                    Fill = SvgPaintServer.None,
                },
                new SvgPath
                {
                    PathData = new SvgPathSegmentList
                    {
                        new SvgMoveToSegment(new PointF(0f, 0f)),
                        new SvgLineSegment(new PointF(0f, 0f), new PointF(-1f, 1f)),
                        new SvgLineSegment(new PointF(-1f, 1f), new PointF(-1f, -1f)),
                        new SvgClosePathSegment()
                    },
                    Stroke = this.colorServer.ToSvgColourServer(item.StrokeColor),
                    StrokeOpacity = item.StrokeColor.A / 255,
                    StrokeWidth = new SvgUnit(SvgUnitType.Point, item.StrokeWidthPoints),
                    Fill = this.colorServer.ToSvgColourServer(item.StrokeColor),
                    FillOpacity = item.StrokeColor.A / 255,
                },
                new SvgPath
                {
                    PathData = new SvgPathSegmentList
                    {
                        new SvgMoveToSegment(new PointF(0f, 0f)),
                        new SvgLineSegment(new PointF(0f, 0f), new PointF(0f, -1.3f)),
                        new SvgLineSegment(new PointF(0f, -1.3f), new PointF(-1.4f, -1.3f)),
                        new SvgLineSegment(new PointF(-1.4f, -1.3f), new PointF(-1.4f, 0)),
                        new SvgClosePathSegment()
                    },
                    Stroke = this.colorServer.ToSvgColourServer(item.StrokeColor),
                    StrokeOpacity = item.StrokeColor.A / 255,
                    StrokeWidth = new SvgUnit(SvgUnitType.Point, item.StrokeWidthPoints),
                    Fill = SvgPaintServer.None,
                },
            };
        }
    }
}
