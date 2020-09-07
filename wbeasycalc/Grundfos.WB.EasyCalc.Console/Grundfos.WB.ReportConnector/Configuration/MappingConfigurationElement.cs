using System.Configuration;

namespace Grundfos.WB.ReportConnector.Configuration
{
    public class MappingConfigurationElement : ConfigurationElement, IMappingDefinition
    {
        [ConfigurationProperty("source", IsKey = true, IsRequired = true)]
        public string Source
        {
            get
            {
                return (string)base["source"];
            }
            set
            {
                base["source"] = value;
            }
        }

        [ConfigurationProperty("destination", IsKey = false, IsRequired = true)]
        public string Destination
        {
            get
            {
                return (string)base["destination"];
            }
            set
            {
                base["destination"] = value;
            }
        }

    }
}
