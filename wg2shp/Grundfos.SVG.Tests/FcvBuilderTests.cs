using System.Drawing;
using System.IO;
using Grundfos.GeometryModel;
using Grundfos.SVG.WG.Builders;
using NUnit.Framework;

namespace Grundfos.SVG.Tests
{
    [TestFixture]
    public class FcvBuilderTests
    {
        [Test]
        public void BuildFcv_DumpDrawing()
        {
            var data = new Fcv
            {
                ID = 15003900,
                Center = new Point2D(0, 0),
                StrokeColor = Color.FromArgb(0, 0, 255),
                StrokeWidthPoints = 0.05f,
                SymbolRadius = 100
            };

            var document = new TwSvgDocument();
            var colorServer = new ColorServer();
            var builder = new FcvBuilder(colorServer);

            var result = builder.Build(data);
            document.Children.Add(result);
            var width = result.Bounds.Width;
            using (var file = new FileStream(@"C:\temp\test-fcv.svg", FileMode.Create))
            {
                document.Write(file, false);
            }
        }

    }
}
