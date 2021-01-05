using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using Grundfos.WG2SVG.Configuration.Converters;

namespace Grundfos.WG2SVG.Configuration
{
    public class ValueColor : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the minimum value for this range.
        /// All values considered to belong to this range must be larger or equal to the specified <see cref="GreaterOrEqualTo"/>.
        /// </summary>
        [ConfigurationProperty("greaterOrEqualTo")]
        public double GreaterOrEqualTo
        {
            get
            {
                return (double)base["greaterOrEqualTo"];
            }

            set
            {
                base["greaterOrEqualTo"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum value for this range.
        /// All values considered to belong to this range must be less than the specified <see cref="LessThan"/>.
        /// </summary>
        [ConfigurationProperty("lessThan")]
        public double LessThan
        {
            get
            {
                return (double)base["lessThan"];
            }

            set
            {
                base["lessThan"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the <see cref="Color"/> of this range.
        /// </summary>
        [ConfigurationProperty("color")]
        [TypeConverter(typeof(String2ColorConverter))]
        public Color Color
        {
            get
            {
                return (Color)base["color"];
            }

            set
            {
                base["color"] = value;
            }
        }

        public override string ToString()
        {
            return $"{this.GreaterOrEqualTo} ≥ X > {this.LessThan} ({this.Color.ToString()})";
        }
    }
}