using System.Configuration;

namespace Grundfos.WB.EasyCalc.Console.Configuration
{
    public class ZoneConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(ZoneConfigurationCollection))]
        public ZoneConfigurationCollection Zones
        {
            get
            {
                return (ZoneConfigurationCollection)this["instances"];
            }
        }
    }
}
