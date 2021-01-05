using System;
using System.Drawing;
using Grundfos.Imaging.Shapes;

namespace Grundfos.Imaging.Drawers
{
    public class TextDrawer : AbstractDrawer
    {
        public override Type SupportedShapeType => typeof(Text);

        public override void Draw(Bitmap bitmap, Shape shape)
        {
            var text = shape as Shapes.Text;
            if (text == null)
            {
                throw new NotSupportedException("Not supported shape type: " + shape.GetType());
            }

            this.DrawText(bitmap, text);
        }

        private void DrawText(Bitmap bitmap, Text text)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var font = new Font(Converters.FontFamilyConverter.Convert(text.FontFamily), text.FontSize))
                {
                    var brush = new SolidBrush(text.FillColor);
                    graphics.DrawString(text.Content, font, brush, new PointF(text.PositionX, text.PositionY));
                }
            }
        }
    }
}
