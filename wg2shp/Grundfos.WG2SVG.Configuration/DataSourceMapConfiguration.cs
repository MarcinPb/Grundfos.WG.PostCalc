using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class DataSourceMapConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("wg2OpcMapFileName")]
        public string Wg2OpcMapFileName
        {
            get
            {
                return (string)this["wg2OpcMapFileName"];
            }

            set
            {
                this["wg2OpcMapFileName"] = value;
            }
        }

        [ConfigurationProperty("twVar2OpcMapFileName")]
        public string TwVar2OpcMapFileName
        {
            get
            {
                return (string)this["twVar2OpcMapFileName"];
            }

            set
            {
                this["twVar2OpcMapFileName"] = value;
            }
        }

        [ConfigurationProperty("twVar2IDMapFileName")]
        public string TwVar2IDMapFileName
        {
            get
            {
                return (string)this["twVar2IDMapFileName"];
            }

            set
            {
                this["twVar2IDMapFileName"] = value;
            }
        }
    }
}
