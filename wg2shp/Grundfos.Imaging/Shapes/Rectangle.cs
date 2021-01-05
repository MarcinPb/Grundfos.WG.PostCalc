using System;
using System.Drawing;

namespace Grundfos.Imaging.Shapes
{
    public class Rectangle : Shape, ICloneable
    {
        public Rectangle()
        {
        }

        public Rectangle(int x, int y, int width, int height, Color fill, int strokeWidth, Color strokeColor)
        {
            this.PositionX = x;
            this.PositionY = y;
            this.Width = width;
            this.Height = height;
            this.FillColor = fill;
            this.StrokeWidth = strokeWidth;
            this.StrokeColor = strokeColor;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public object Clone()
        {
            return new Rectangle(this.PositionX, this.PositionY, this.Width, this.Height, this.FillColor, this.StrokeWidth, this.StrokeColor);
        }
    }
}
