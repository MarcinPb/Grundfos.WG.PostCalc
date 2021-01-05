using System.Drawing;
using System.IO;
using Grundfos.GeometryModel;
using Grundfos.SVG.WG.Builders;
using NUnit.Framework;

namespace Grundfos.SVG.Tests
{
    [TestFixture]
    public class PumpBuilderTests
    {
        [Test]
        public void BuildPump_DumpDrawing()
        {
            var tankData = new Pump
            {
                ID = 15003900,
                Center = new Point2D(0, 0),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = 100
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var builder = new PumpBuilder(colorServer);

            var result = builder.Build(tankData);
            document.Children.Add(result);
            var width = result.Bounds.Width;
            using (var file = new FileStream(@"C:\temp\test-pump.svg", FileMode.Create))
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
        public void BuildPump_SetSize_BoundariesAsExpected(double size)
        {
            var data = new Pump
            {
                ID = 15003900,
                Center = new Point2D(0, 0),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = (float)size,
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var builder = new PumpBuilder(colorServer);

            var result = builder.Build(data);
            document.Children.Add(result);

            var boundaries = result.Bounds;
            var actualSize = boundaries.Right - boundaries.Left;
            Assert.AreEqual(size, actualSize, size * 0.06);
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(300, 300)]
        [TestCase(-300, 0)]
        [TestCase(1500, 3900)]
        [TestCase(8791.516, 86.5646)]
        public void BuildPump_SetSize_CenterAsExpected(double x, double y)
        {
            var data = new Pump
            {
                ID = 15003900,
                Center = new Point2D(x, y),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = (float)1000,
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var builder = new PumpBuilder(colorServer);

            var result = builder.Build(data);
            document.Children.Add(result);
            using (var file = new FileStream(@"C:\temp\test-pump.svg", FileMode.Create))
            {
                document.Write(file, false);
            }

            var boundaries = result.Bounds;
            var actualX = (boundaries.Right + boundaries.Left) / 2;
            var actualY = (boundaries.Top + boundaries.Bottom) / 2;
            Assert.AreEqual(x, actualX - 19.1361f, 0.001);
        }
    }
}
