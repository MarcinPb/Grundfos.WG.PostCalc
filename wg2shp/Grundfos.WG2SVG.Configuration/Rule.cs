using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using Grundfos.WG2SVG.Configuration.Converters;

namespace Grundfos.WG2SVG.Configuration
{
    public class Rule : ConfigurationElement
    {
        [ConfigurationProperty("labelToken", IsKey = true)]
        public string LabelToken
        {
            get
            {
                return (string)this["labelToken"];
            }

            set
            {
                this["labelToken"] = value;
            }
        }

        [ConfigurationProperty("color")]
        [TypeConverter(typeof(String2ColorConverter))]
        public Color Color
        {
            get
            {
                return (Color)this["color"];
            }

            set
            {
                this["color"] = value;
            }
        }

        public override string ToString()
        {
            return $"({this.LabelToken}) ({this.Color.ToString()})";
        }

    }
}
