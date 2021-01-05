using System.Collections.Generic;
using System.Drawing;
using Grundfos.Imaging;
using Grundfos.Imaging.Shapes;
using Grundfos.WG2SVG.Configuration;
using NLog;

namespace Grundfos.TW.LegendBuilder
{
    public class LegendBuilder
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private readonly ImageBuilder imageBuilder;
        private readonly string legendEntryFormat;

        public LegendBuilder(ImageBuilder imageBuilder, string legendEntryFormat)
        {
            this.imageBuilder = imageBuilder;
            this.legendEntryFormat = legendEntryFormat;
        }

        public IList<Shape> BuildLegend(Target target, List<ValueColor> valueColors)
        {
            var shapes = new List<Shape>();
            var frame = new Imaging.Shapes.Rectangle
            {
                Width = target.Legend.Width - 4,
                Height = target.Legend.Height - 4,
                FillColor = Color.White,
                PositionX = 2,
                PositionY = 2,
                StrokeColor = Color.Black,
                StrokeWidth = 2,
            };
            shapes.Add(frame);
            var description = new Text
            {
                Content = target.Legend.Description,
                FontFamily = Imaging.Shapes.FontFamily.GenericSansSerif,
                FillColor = Color.Black,
                FontSize = target.Legend.FontSize,
                PositionX = target.Legend.HorizontalPadding - 4,
                PositionY = target.Legend.VerticalPadding / 2,
            };
            shapes.Add(description);
            for (int i = 0; i < valueColors.Count; i++)
            {
                int positionY = (target.Legend.VerticalPadding * i) + (target.Legend.FontSize * i) + target.Legend.VerticalPadding * 2;
                var legendEntry = LegendEntryBuilder.Build(target.Legend, valueColors[i], target.Legend.HorizontalPadding, positionY, this.legendEntryFormat);
                shapes.Add(legendEntry);
            }

            return shapes;
        }

        public void BuildLegendFile(Target target, List<ValueColor> valueColors)
        {
            var shapes = this.BuildLegend(target, valueColors);

            this.imageBuilder.InitializeImage(target.Legend.Width, target.Legend.Height);
            foreach (var shape in shapes)
            {
                imageBuilder.Draw(shape);
            }

            log.Info("Saving legend file: {0}", target.Legend.FileName);
            imageBuilder.Save(target.Legend.FileName, ImageFormats.Png);
        }
    }
}
