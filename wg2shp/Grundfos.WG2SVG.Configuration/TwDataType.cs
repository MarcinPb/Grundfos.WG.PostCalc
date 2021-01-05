using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class TwDataType : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public int Value
        {
            get
            {
                return (int)base["value"];
            }

            set
            {
                base["value"] = value;
            }
        }
    }
}
