using System;
using System.Collections.Generic;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Svg;
using Svg.Transforms;

namespace Grundfos.SVG.WG.Builders
{
    public class TankBuilder : SymbolBuilderBase, IVisualElementBuilder
    {
        public const float OriginalSize = 3;

        public TankBuilder(ColorServer colorServer, ClosedPathBuilder closedPathBuilder, PathBuilder pathBuilder)
            : base(colorServer, closedPathBuilder, pathBuilder)
        {
        }

        public Type GeometryType => typeof(Tank);

        public SvgVisualElement Build(Geometry geometry)
        {
            var reservoirData = (Tank)geometry;
            var visualElement = this.BuildSymbol(reservoirData);
            return visualElement;
        }

        private SvgGroup BuildSymbol(Tank geometry)
        {
            var group = new SvgGroup { ID = geometry.ID.ToString(), };
            this.BuildOpenLines(geometry).ForEach(x =>
            {
                var svgItem = this.PathBuilder.Build(x);
                var color = this.ColorServer.ToSvgColourServer(x.StrokeColor);
                svgItem.Stroke = color;
                group.Children.Add(svgItem);
            });
            this.BuildClosedLines(geometry).ForEach(x =>
            {
                var svgItem = this.ClosedPathBuilder.Build(x);
                var color = this.ColorServer.ToSvgColourServer(x.StrokeColor);
                svgItem.Stroke = color;
                group.Children.Add(svgItem);
            });

            group.Transforms.Add(new SvgTranslate((float)geometry.Center.X, -(float)geometry.Center.Y));
            group.Transforms.Add(new SvgScale(geometry.SymbolRadius / OriginalSize));
            return group;
        }

        private List<Polyline> BuildOpenLines(Tank tank)
        {
            return new List<Polyline>
            {
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-1.5, 0.75), new Point2D(1.5, 0.75) },
                    StrokeWidthPoints = tank.StrokeWidthPoints,
                    StrokeColor = tank.StrokeColor,
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-0.5, 0.5), new Point2D(0.5, 0.5) },
                    StrokeWidthPoints = tank.StrokeWidthPoints,
                    StrokeColor = tank.StrokeColor,
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-0.25, 0.25), new Point2D(0.25, 0.25) },
                    StrokeWidthPoints = tank.StrokeWidthPoints,
                    StrokeColor = tank.StrokeColor,
                },
            };
        }

        private List<Polyline> BuildClosedLines(Tank tank)
        {
            return new List<Polyline>
            {
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-1.5, 2), new Point2D(-1.5, -2), new Point2D(1.5, -2), new Point2D(1.5, 2) },
                    StrokeWidthPoints = tank.StrokeWidthPoints,
                    StrokeColor = tank.StrokeColor,
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-0.75, 1.5), new Point2D(0, 0.75), new Point2D(0.75, 1.5), },
                    StrokeWidthPoints = tank.StrokeWidthPoints,
                    StrokeColor = tank.StrokeColor,
                }
            };
        }

    }
}
