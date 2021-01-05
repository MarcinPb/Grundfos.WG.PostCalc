using System;
using System.Drawing;
using Grundfos.Imaging.Shapes;

namespace Grundfos.Imaging.Drawers
{
    public class RectangleDrawer : AbstractDrawer
    {
        public override Type SupportedShapeType => typeof(Shapes.Rectangle);

        public override void Draw(Bitmap bitmap, Shape shape)
        {
            var rectangle = shape as Shapes.Rectangle;
            if (rectangle == null)
            {
                throw new NotSupportedException("Not supported shape type: " + shape.GetType());
            }

            this.DrawRectangle(bitmap, rectangle);
        }

        private void DrawRectangle(Bitmap bitmap, Shapes.Rectangle rectangle)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                var rectangleGraphics = Converters.ShapeConverter.Convert(rectangle);
                var strokeColor = rectangle.StrokeColor;
                var fillColor = rectangle.FillColor;
                var pen = new Pen(strokeColor, rectangle.StrokeWidth);
                var brush = new SolidBrush(fillColor);
                graphics.DrawRectangle(pen, rectangle.PositionX, rectangle.PositionY, rectangle.Width, rectangle.Height);
                graphics.FillRectangle(brush, rectangleGraphics);
            }
        }
    }
}
