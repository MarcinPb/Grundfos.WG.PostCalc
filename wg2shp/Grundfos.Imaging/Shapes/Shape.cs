using System.Drawing;

namespace Grundfos.Imaging.Shapes
{
    public abstract class Shape
    {
        public int StrokeWidth { get; set; }
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
