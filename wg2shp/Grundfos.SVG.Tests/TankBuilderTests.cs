using System.Drawing;
using System.IO;
using Grundfos.GeometryModel;
using Grundfos.SVG.Builders;
using Grundfos.SVG.WG.Builders;
using NUnit.Framework;

namespace Grundfos.SVG.Tests
{
    [TestFixture]
    public class TankBuilderTests
    {
        [Test]
        public void BuildTank_DumpDrawing()
        {
            var tankData = new Tank
            {
                ID = 15003900,
                Center = new Point2D(0, 0),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = 100
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var pathBuilder = new PathBuilder(colorServer);
            var builder = new TankBuilder(colorServer, closedPathBuilder, pathBuilder);

            var result = builder.Build(tankData);
            document.Children.Add(result);
            using (var file = new FileStream(@"C:\temp\test-tank.svg", FileMode.Create))
            {
                document.Write(file, false);
            }
        }

        [TestCase(10)]
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(13)]
        [TestCase(23)]
        [TestCase(33)]
        [TestCase(110)]
        [TestCase(120)]
        [TestCase(130)]
        [TestCase(113)]
        [TestCase(123)]
        [TestCase(133)]
        public void BuildTank_SetSize_BoundariesAsExpected(double size)
        {
            var tankData = new Tank
            {
                ID = 15003900,
                Center = new Point2D(0, 0),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = (float)size,
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var pathBuilder = new PathBuilder(colorServer);
            var builder = new TankBuilder(colorServer, closedPathBuilder, pathBuilder);

            var result = builder.Build(tankData);
            document.Children.Add(result);

            var boundaries = result.Bounds;
            var actualSize = boundaries.Right - boundaries.Left;
            Assert.AreEqual(size, actualSize, 0.0001);
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(300, 300)]
        [TestCase(-300, 0)]
        [TestCase(1500, 3900)]
        [TestCase(8791.516, 86.5646)]
        public void BuildTank_SetSize_CenterAsExpected(double x, double y)
        {
            var tankData = new Tank
            {
                ID = 15003900,
                Center = new Point2D(x, y),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = (float)1000,
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var pathBuilder = new PathBuilder(colorServer);
            var builder = new TankBuilder(colorServer, closedPathBuilder, pathBuilder);

            var result = builder.Build(tankData);
            document.Children.Add(result);

            //using (var file = new FileStream(@"C:\temp\test-reservoi-center.svg", FileMode.Create))
            //{
            //    document.Write(file, false);
            //}


            var boundaries = result.Bounds;
            var actualX = (boundaries.Right + boundaries.Left) / 2;
            var actualY = (boundaries.Top + boundaries.Bottom) / 2;
            Assert.AreEqual(x, actualX, 0.001);
            Assert.AreEqual(-y, actualY, 0.001);
        }
    }
}
