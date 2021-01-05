using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class StrokeWidthSettings : ConfigurationElement, IStrokeWidthSettings
    {
        [ConfigurationProperty("defaultWidth")]
        public double DefaultWidth
        {
            get
            {
                return (double)this["defaultWidth"];
            }

            set
            {
                this["defaultWidth"] = value;
            }
        }

        [ConfigurationProperty("symbolWidth")]
        public double SymbolWidth
        {
            get
            {
                return (double)this["symbolWidth"];
            }

            set
            {
                this["symbolWidth"] = value;
            }
        }

        [ConfigurationProperty("pipeDiameterToWidthFactor")]
        public double PipeDiameterToWidthFactor
        {
            get
            {
                return (double)this["pipeDiameterToWidthFactor"];
            }

            set
            {
                this["pipeDiameterToWidthFactor"] = value;
            }
        }

        [ConfigurationProperty("diameterFieldName")]
        public string DiameterFieldName
        {
            get
            {
                return (string)this["diameterFieldName"];
            }

            set
            {
                this["diameterFieldName"] = value;
            }
        }
    }
}
