using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Grundfos.Imaging.Groups;
using Grundfos.Imaging.Shapes;

namespace Grundfos.Imaging.Drawers
{
    public class LegendBoxDrawer : AbstractDrawer
    {
        private readonly Dictionary<Type, AbstractDrawer> drawers;

        public override Type SupportedShapeType => typeof(LegendBox);

        public LegendBoxDrawer(ICollection<AbstractDrawer> drawers)
        {
            this.drawers = drawers.ToDictionary(x => x.SupportedShapeType, x => x);
        }

        public override void Draw(Bitmap bitmap, Shape shape)
        {
            var legendBox = shape as LegendBox;
            if (legendBox == null)
            {
                throw new NotSupportedException("Not supported shape type: " + shape.GetType());
            }

            this.DrawLegendBox(bitmap, legendBox);
        }

        private void DrawLegendBox(Bitmap bitmap, LegendBox legendBox)
        {
            var projectedRectangle = new Shapes.Rectangle
            {
                PositionX = legendBox.PositionX + legendBox.Box.PositionX,
                PositionY = legendBox.PositionY + legendBox.Box.PositionY,
                FillColor = legendBox.Box.FillColor,
                Height = legendBox.Box.Height,
                Width = legendBox.Box.Width,
                StrokeColor = legendBox.Box.StrokeColor,
                StrokeWidth = legendBox.Box.StrokeWidth,
            };
            this.DrawInternal(bitmap, projectedRectangle);

            var projectedText = new Shapes.Text
            {
                PositionX = legendBox.PositionX + legendBox.Description.PositionX,
                PositionY = legendBox.PositionY + legendBox.Description.PositionY,
                FillColor = legendBox.Description.FillColor,
                StrokeColor = legendBox.Description.StrokeColor,
                StrokeWidth = legendBox.Description.StrokeWidth,
                FontSize = legendBox.Description.FontSize,
                FontFamily = legendBox.Description.FontFamily,
                Content = legendBox.Description.Content,
            };
            this.DrawInternal(bitmap, projectedText);
        }

        private void DrawInternal(Bitmap bitmap, Shape shape)
        {
            this.drawers[shape.GetType()].Draw(bitmap, shape);
        }
    }
}
