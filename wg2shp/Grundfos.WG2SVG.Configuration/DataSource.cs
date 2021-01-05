using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class DataSource : ConfigurationElement
    {
        [ConfigurationProperty("waterGemsFileName")]
        public string WaterGemsFileName
        {
            get
            {
                return (string)base["waterGemsFileName"];
            }

            set
            {
                base["waterGemsFileName"] = value;
            }
        }

        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get
            {
                return (string)base["connectionString"];
            }

            set
            {
                base["connectionString"] = value;
            }
        }

        [ConfigurationProperty("tableDiscoveryConfiguration")]
        public TwTableDiscoveryConfiguration TableDiscoveryConfiguration
        {
            get
            {
                return (TwTableDiscoveryConfiguration)this["tableDiscoveryConfiguration"];
            }

            set
            {
                this["tableDiscoveryConfiguration"] = value;
            }
        }

        [ConfigurationProperty("signalDiscoveryConfiguration")]
        public TwSignalDiscoveryConfiguration SignalDiscoveryConfiguration
        {
            get
            {
                return (TwSignalDiscoveryConfiguration)this["signalDiscoveryConfiguration"];
            }

            set
            {
                this["signalDiscoveryConfiguration"] = value;
            }
        }

        [ConfigurationProperty("dataSourceMapConfiguration")]
        public DataSourceMapConfiguration DataSourceMapConfiguration
        {
            get
            {
                return (DataSourceMapConfiguration)this["dataSourceMapConfiguration"];
            }

            set
            {
                this["dataSourceMapConfiguration"] = value;
            }
        }
    }
}
