using System.Drawing;
using System.IO;

using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Grundfos.SVG.WG.Builders;

using NUnit.Framework;

namespace Grundfos.SVG.Tests
{
    [TestFixture]
    public class HydrantBuilderTests
    {
        [Test]
        public void BuildHydrant_Test()
        {
            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var pathBuilder = new PathBuilder(colorServer);

            var hydrant = new Hydrant
            {
                Center = new Point2D(0, 0),
                SymbolRadius = 10,
                StrokeColor = Color.FromArgb(0, 0, 255),
            };

            var hydrantBuilder = new HydrantBuilder(colorServer, closedPathBuilder, pathBuilder);
            var hydrantSvg = hydrantBuilder.Build(hydrant);
            hydrantSvg.Transforms.Add(new Svg.Transforms.SvgTranslate(30, 30));

            document.Children.Add(hydrantSvg);

            using (var file = new FileStream(@"C:\temp\test-hydrant.svg", FileMode.Create))
            {
                document.Write(file, false);
            }
        }

        [TestCase(0.001f, 0.00001f)]
        [TestCase(0.01f, 0.00001f)]
        [TestCase(0.1f, 0.00001f)]
        [TestCase(1f, 0.00001f)]
        [TestCase(10f, 0.00001f)]
        [TestCase(100f, 0.00001f)]
        [TestCase(1000f, 0.0001f)]
        [TestCase(10000f, 0.00001f)]
        [TestCase(0.0007f, 0.00001f)]
        [TestCase(0.007f, 0.00001f)]
        [TestCase(0.07f, 0.00001f)]
        [TestCase(0.7f, 0.00001f)]
        [TestCase(7f, 0.00001f)]
        [TestCase(70f, 0.00001f)]
        [TestCase(700f, 0.00001f)]
        [TestCase(7000f, 0.001f)]
        [TestCase(70000f, 0.01f)]
        public void BuildHydrant_SetSymbolSize_BoundariesAsExpected(float size, float tolerance)
        {
            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var pathBuilder = new PathBuilder(colorServer);

            var hydrant = new Hydrant
            {
                Center = new Point2D(0, 0),
                SymbolRadius = (float)size,
                StrokeColor = Color.FromArgb(0, 0, 255),
            };

            var hydrantBuilder = new HydrantBuilder(colorServer, closedPathBuilder, pathBuilder);
            var hydrantSvg = hydrantBuilder.Build(hydrant);

            var bounds = hydrantSvg.Bounds;
            Assert.AreEqual(size, bounds.Size.Height, tolerance);
            Assert.AreEqual(size, bounds.Size.Width, tolerance);
        }

        [TestCase(0f, 0f)]
        [TestCase(3f, 3f)]
        [TestCase(0.000001f, 0.000001f)]
        [TestCase(10000000f, 10000000f)]
        [TestCase(3.3333f, 15.7854569f)]
        public void BuildHydrant_SetSymbolSize_CenterAsExpected(float centerX, float centerY)
        {
            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var pathBuilder = new PathBuilder(colorServer);

            var hydrant = new Hydrant
            {
                Center = new Point2D(centerX, centerY),
                SymbolRadius = (float)60,
                StrokeColor = Color.FromArgb(0, 0, 255),
            };

            var hydrantBuilder = new HydrantBuilder(colorServer, closedPathBuilder, pathBuilder);
            var hydrantSvg = hydrantBuilder.Build(hydrant);

            var bounds = hydrantSvg.Bounds;
            Assert.AreEqual(centerX, (bounds.Left + bounds.Right) / 2, 0.001, "Center X not as expected.");
            Assert.AreEqual(-centerY, (bounds.Top + bounds.Bottom) / 2, 0.001, "Center Y not as expected.");
        }
    }
}
