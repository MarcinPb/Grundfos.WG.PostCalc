using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class Wg2SvgConfigurationSection :  ConfigurationSection, IWg2SvgConfiguration
    {
        [ConfigurationProperty("datasource")]
        public DataSource DataSource
        {
            get
            {
                return (DataSource)base["datasource"];
            }

            set
            {
                base["datasource"] = value;
            }
        }

        [ConfigurationProperty("targets")]
        public TargetCollection Targets
        {
            get
            {
                return (TargetCollection)base["targets"];
            }

            set
            {
                base["targets"] = value;
            }
        }

    }
}
