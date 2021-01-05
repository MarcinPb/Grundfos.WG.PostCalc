using System.Drawing;

namespace Grundfos.Imaging.Converters
{
    public static class ShapeConverter
    {
        public static RectangleF Convert(Shapes.Rectangle rectangle)
        {
            return new RectangleF(rectangle.PositionX, rectangle.PositionY, rectangle.Width, rectangle.Height);
        }
    }
}
