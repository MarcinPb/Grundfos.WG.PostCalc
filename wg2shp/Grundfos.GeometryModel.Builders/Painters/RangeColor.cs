using System.Drawing;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public class RangeColor
    {
        /// <summary>
        /// Gets or sets the minimum value for this range.
        /// All values considered to belong to this range must be larger or equal to the specified <see cref="GreaterOrEqualTo"/>.
        /// </summary>
        public double GreaterOrEqualTo { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for this range.
        /// All values considered to belong to this range must be less than the specified <see cref="LessThan"/>.
        /// </summary>
        public double LessThan { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> of this range.
        /// </summary>
        public Color Color { get; set; }

        public override string ToString()
        {
            return $"{this.GreaterOrEqualTo} ≥ X > {this.LessThan} ({this.Color.ToString()})";
        }
    }
}
