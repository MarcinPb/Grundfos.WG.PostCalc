using System;
using Grundfos.Imaging.Shapes;

namespace Grundfos.Imaging.Groups
{
    public class LegendBox : Shape, ICloneable
    {
        public Rectangle Box { get; set; }
        public Text Description { get; set; }

        public object Clone()
        {
            return new LegendBox
            {
                Box = (Rectangle)this.Box.Clone(),
                Description = (Text)this.Description.Clone(),
                FillColor = this.FillColor,
                PositionX = this.PositionX,
                PositionY = this.PositionY,
                StrokeColor = this.StrokeColor,
                StrokeWidth = this.StrokeWidth
            };
        }
    }
}
