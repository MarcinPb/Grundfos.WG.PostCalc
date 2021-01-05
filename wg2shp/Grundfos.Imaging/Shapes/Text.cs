using System;

namespace Grundfos.Imaging.Shapes
{
    public class Text : Shape, ICloneable
    {
        public string Content { get; set; }
        public FontFamily FontFamily { get; set; }
        public int FontSize { get; set; }

        public object Clone()
        {
            return new Text
            {
                PositionX = this.PositionX,
                PositionY = this.PositionY,
                FillColor = this.FillColor,
                StrokeColor = this.StrokeColor,
                StrokeWidth = this.StrokeWidth,
                FontFamily = this.FontFamily,
                FontSize = this.FontSize,
                Content = this.Content,
            };
        }
    }
}
