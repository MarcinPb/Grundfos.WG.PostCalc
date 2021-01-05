using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class Wg2TwConfigurationSection : ConfigurationSection
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

        [ConfigurationProperty("buttonFactoryConfiguration")]
        public ButtonFactoryConfiguration ButtonFactoryConfiguration
        {
            get
            {
                return (ButtonFactoryConfiguration)base["buttonFactoryConfiguration"];
            }
            set
            {
                base["buttonFactoryConfiguration"] = value;
            }
        }

        [ConfigurationProperty("xmlProcessorConfiguration")]
        public XmlProcessorConfiguration XmlProcessorConfiguration
        {
            get
            {
                return (XmlProcessorConfiguration)base["xmlProcessorConfiguration"];
            }
            set
            {
                base["xmlProcessorConfiguration"] = value;
            }
        }
    }
}
