using System.Configuration;

namespace Grundfos.WB.ReportConnector.Configuration
{
    public class MappingConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("entries")]
        [ConfigurationCollection(typeof(MappingConfigurationCollection))]
        public MappingConfigurationCollection Entries
        {
            get
            {
                return (MappingConfigurationCollection)this["entries"];
            }
        }
    }
}
