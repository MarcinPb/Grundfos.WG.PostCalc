using System;
using System.Collections.Generic;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Svg;
using Svg.Transforms;

namespace Grundfos.SVG.WG.Builders
{
    public class ReservoirBuilder : SymbolBuilderBase, IVisualElementBuilder
    {
        public float OriginalSize = 6.66666666666666f;

        public ReservoirBuilder(ColorServer colorServer, ClosedPathBuilder closedPathBuilder, PathBuilder pathBuilder)
            : base(colorServer, closedPathBuilder, pathBuilder)
        {
        }

        public Type GeometryType => typeof(Reservoir);

        public SvgVisualElement Build(Geometry geometry)
        {
            var reservoirData = (Reservoir)geometry;
            var visualElement = this.BuildSymbol(reservoirData);
            return visualElement;
        }

        private SvgGroup BuildSymbol(Reservoir geometry)
        {
            var group = new SvgGroup { ID = geometry.ID.ToString(), };
            this.BuildOpenLines(geometry).ForEach(x =>
            {
                var svgItem = this.PathBuilder.Build(x);
                var color = this.ColorServer.ToSvgColourServer(x.StrokeColor);
                svgItem.Stroke = color;
                group.Children.Add(svgItem);
            });
            this.BuildTriangle(geometry).ForEach(x =>
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

        private List<Polyline> BuildOpenLines(Reservoir reservoirData)
        {
            return new List<Polyline>
            {
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-3.333333, 2), new Point2D(-2, -2), new Point2D(2, -2), new Point2D(3.333333, 2) },
                    StrokeWidthPoints = reservoirData.StrokeWidthPoints,
                    StrokeColor = reservoirData.StrokeColor,
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-3, 1), new Point2D(3, 1) },
                    StrokeWidthPoints = reservoirData.StrokeWidthPoints,
                    StrokeColor = reservoirData.StrokeColor,
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-0.5, 0.75), new Point2D(0.5, 0.75) },
                    StrokeWidthPoints = reservoirData.StrokeWidthPoints,
                    StrokeColor = reservoirData.StrokeColor,
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-0.25, 0.5), new Point2D(0.25, 0.5) },
                    StrokeWidthPoints = reservoirData.StrokeWidthPoints,
                    StrokeColor = reservoirData.StrokeColor,
                },
            };
        }

        private List<Polyline> BuildTriangle(Reservoir reservoirData)
        {
            return new List<Polyline>
            {
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-0.75, 1.75), new Point2D(0, 1), new Point2D(0.75, 1.75), },
                    StrokeWidthPoints = reservoirData.StrokeWidthPoints,
                    StrokeColor = reservoirData.StrokeColor,
                }
            };
        }
    }
}
