using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Grundfos.GeometryModel;
using Grundfos.SVG.WG.Builders;
using NUnit.Framework;

namespace Grundfos.SVG.Tests
{
    [TestFixture]
    public class PipeBuilderTests
    {
        [Test]
        public void BuildPipe_DumpDrawing1()
        {
            var itemData = new Pipe
            {
                ID = 15003900,
                Path = new List<Point2D>
                {
                    new Point2D(0, 0),
                    new Point2D(100, 100),
                    new Point2D(200, 0),
                },
                StrokeColor = Color.FromArgb(255, 0, 0),
                StrokeWidthPoints = 2,
                ArrowDirection = new Pointer
                {
                    Point = new Point2D(100, 100),
                    Direction = UnitVector2D.Build(1, 0),
                },
                ArrowSize = 10,
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var builder = new PipeBuilder(colorServer);

            var result = builder.Build(itemData);
            document.Children.Add(result);
            var width = result.Bounds.Width;
            using (var file = new FileStream(@"C:\temp\test-pipe1.svg", FileMode.Create))
            {
                document.Write(file, false);
            }
        }

        [Test]
        public void BuildPipe_DumpDrawing2()
        {
            var itemData = new Pipe
            {
                ID = 15003900,
                Path = new List<Point2D>
                {
                    new Point2D(0, 0),
                    new Point2D(100, 100),
                },
                StrokeColor = Color.FromArgb(255, 0, 0),
                StrokeWidthPoints = 2,
                ArrowDirection = new Pointer
                {
                    Point = new Point2D(50, 50),
                    Direction = UnitVector2D.Build(1, 1),
                },
                ArrowSize = 10,
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var builder = new PipeBuilder(colorServer);

            var result = builder.Build(itemData);
            document.Children.Add(result);
            var width = result.Bounds.Width;
            using (var file = new FileStream(@"C:\temp\test-pipe2.svg", FileMode.Create))
            {
                document.Write(file, false);
            }
        }

    }
}
