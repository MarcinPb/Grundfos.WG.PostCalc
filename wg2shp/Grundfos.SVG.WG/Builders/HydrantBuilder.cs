using System;
using System.Collections.Generic;
using System.Linq;

using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;

using Svg;
using Svg.Transforms;

namespace Grundfos.SVG.WG.Builders
{
    public class HydrantBuilder : SymbolBuilderBase, IVisualElementBuilder
    {
        private const float OriginalSize = 60;
        private const float ArrowLineStrokeWidth = 10;
        private const float CircleRadius = 18;

        public Type GeometryType => typeof(Hydrant);

        public HydrantBuilder(ColorServer colorServer, ClosedPathBuilder closedPathBuilder, PathBuilder pathBuilder) : base(colorServer, closedPathBuilder, pathBuilder)
        {
        }

        public SvgVisualElement Build(Geometry geometry)
        {
            var hydrantGeometry = (Hydrant)geometry;
            var hydrant = this.BuildSymbol(hydrantGeometry);
            return hydrant;
        }

        private SvgGroup BuildSymbol(Hydrant geometry)
        {
            var group = new SvgGroup() { ID = geometry.ID.ToString() } ;
            var arrowHeads = this.BuildArrowHeads(geometry).Select(x => this.ClosedPathBuilder.Build(x)).ToList();
            arrowHeads.ForEach(x =>
            {
                var color = this.ColorServer.ToSvgColourServer(geometry.StrokeColor);
                x.Fill = color;
                x.Stroke = color;
            });
            arrowHeads.ForEach(x => group.Children.Add(x));

            var arrowLines = this.BuildArrowLines(geometry).Select(x => this.PathBuilder.Build(x)).ToList();
            arrowLines.ForEach(x => group.Children.Add(x));

            group.Children.Add(new SvgCircle { CenterX = 0, CenterY = 0, Radius = CircleRadius, Fill = this.ColorServer.ToSvgColourServer(geometry.StrokeColor) } );
            group.Transforms.Add(new SvgTranslate((float)geometry.Center.X, -(float)geometry.Center.Y));
            group.Transforms.Add(new SvgScale(geometry.SymbolRadius / OriginalSize));

            return group;
        }

        private List<Polyline> BuildArrowHeads(Hydrant geometry)
        {
            return new List<Polyline>
            {
                new Polyline { Path = new List<Point2D> { new Point2D(10, -20), new Point2D(0, -30), new Point2D(-10, -20) }  },
                new Polyline { Path = new List<Point2D> { new Point2D(20, 10), new Point2D(30, 0), new Point2D(20, -10) }  },
                new Polyline { Path = new List<Point2D> { new Point2D(-10, 20), new Point2D(0, 30), new Point2D(10, 20) }  },
                new Polyline { Path = new List<Point2D> { new Point2D(-20, -10), new Point2D(-30, 0), new Point2D(-20, 10) }  },
            };
        }

        private List<Polyline> BuildArrowLines(Hydrant geometry)
        {
            return new List<Polyline>
            {
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(0, -21), new Point2D(0, 21) },
                    StrokeColor = geometry.StrokeColor,
                    StrokeWidthPoints = ArrowLineStrokeWidth
                },
                new Polyline
                {
                    Path = new List<Point2D> { new Point2D(-21, 0), new Point2D(21, 0) },
                    StrokeColor = geometry.StrokeColor,
                    StrokeWidthPoints = ArrowLineStrokeWidth
                },
            };
        }
    }
}
