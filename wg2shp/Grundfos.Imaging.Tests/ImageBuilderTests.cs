using System.Collections.Generic;
using System.Drawing;
using Grundfos.Imaging.Drawers;
using Grundfos.Imaging.Groups;
using Grundfos.Imaging.Shapes;
using NUnit.Framework;

namespace Grundfos.Imaging.Tests
{
    [TestFixture]
    public class ImageBuilderTests
    {
        [Test]
        public void Draw_Test()
        {
            var builders = new List<AbstractDrawer>
            {
                new RectangleDrawer(),
                new TextDrawer(),
            };
            var legendDrawer = new LegendBoxDrawer(builders);
            builders.Add(legendDrawer);

            using (var builder = new ImageBuilder(builders))
            {
                builder.InitializeImage(170, 190);
                var frame = new Shapes.Rectangle(2, 2, 166, 186, Color.White, 4, Color.Black);
                builder.Draw(frame);

                var text = new Text
                {
                    PositionX = 10,
                    PositionY = 10,
                    Content = "Ciśnienie [m H₂O]",
                    FillColor = Color.Black,
                    FontFamily = Shapes.FontFamily.GenericSansSerif,
                    FontSize = 10,
                };
                builder.Draw(text);

                var legendTemplate = new LegendBox
                {
                    PositionX = 14,
                    PositionY = 40,
                    Box = new Shapes.Rectangle
                    {
                        PositionX = 0,
                        PositionY = 0,
                        FillColor = Color.Blue,
                        StrokeColor = Color.Black,
                        StrokeWidth = 4,
                        Height = 18,
                        Width = 18,
                    },
                    Description = new Text
                    {
                        PositionX = 25,
                        PositionY = 2,
                        FillColor = Color.Black,
                        FontFamily = Shapes.FontFamily.GenericSansSerif,
                        FontSize = 10,
                        Content = "",
                    }
                };

                int position = 40;
                var blueBox = (LegendBox)legendTemplate.Clone();
                blueBox.Description.Content = "0 ÷ 36";
                blueBox.Box.FillColor = Color.Blue;
                blueBox.PositionY = position;
                builder.Draw(blueBox);

                position += 28;
                var greenBox = (LegendBox)legendTemplate.Clone();
                greenBox.Description.Content = "36 ÷ 39";
                greenBox.Box.FillColor = Color.DarkGreen;
                greenBox.PositionY = position;
                builder.Draw(greenBox);

                position += 28;
                var orangeBox = (LegendBox)legendTemplate.Clone();
                orangeBox.Description.Content = "39 ÷ 41";
                orangeBox.Box.FillColor = Color.Orange;
                orangeBox.PositionY = position;
                builder.Draw(orangeBox);

                position += 28;
                var redBox = (LegendBox)legendTemplate.Clone();
                redBox.Description.Content = "41 ÷ 45";
                redBox.Box.FillColor = Color.Red;
                redBox.PositionY = position;
                builder.Draw(redBox);

                position += 28;
                var claretBox = (LegendBox)legendTemplate.Clone();
                claretBox.Description.Content = "45 ÷ ∞";
                claretBox.Box.FillColor = Color.Chocolate;
                claretBox.PositionY = position;
                builder.Draw(claretBox);

                builder.Save(@"C:\Temp\test.png", ImageFormats.Png);
            }
        }
    }
}
