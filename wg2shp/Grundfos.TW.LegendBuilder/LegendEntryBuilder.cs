using System.Drawing;
using Grundfos.Imaging.Groups;
using Grundfos.Imaging.Shapes;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.TW.LegendBuilder
{
    public static class LegendEntryBuilder
    {
        public const string LegendEntryFormat = "{0} ÷ {1}";

        public static LegendBox Build(Legend legend, ValueColor color, int x, int y, string legendEntryFormat)
        {
            var legendEntry = new LegendBox
            {
                PositionX = x,
                PositionY = y,
                Box = new Imaging.Shapes.Rectangle
                {
                    PositionX = 0,
                    PositionY = 0,
                    FillColor = color.Color,
                    StrokeColor = Color.Black,
                    StrokeWidth = 4,
                    Height = legend.FontSize + 6,
                    Width = legend.FontSize + 6,
                },
                Description = new Text
                {
                    PositionX = legend.HorizontalPadding + legend.FontSize,
                    PositionY = 0,
                    FillColor = Color.Black,
                    FontSize = legend.FontSize,
                    FontFamily = Imaging.Shapes.FontFamily.GenericSansSerif,
                    StrokeWidth = 0,
                    Content = string.Format(legendEntryFormat ?? LegendEntryFormat , Resolve(color.GreaterOrEqualTo), Resolve(color.LessThan)),
                },
                FillColor = Color.White,
                StrokeWidth = 0,
            };

            return legendEntry;
        }

        private static object Resolve(double value)
        {
            if (value == double.MinValue)
            {
                return "-∞";
            }

            if (value == double.MaxValue)
            {
                return "∞";
            }

            return value;
        }
    }
}
