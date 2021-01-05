using System.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class XmlProcessorConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("elementsXPath")]
        public string ElementsXPath
        {
            get
            {
                return (string)this["elementsXPath"];
            }

            set
            {
                this["elementsXPath"] = value;
            }
        }

        [ConfigurationProperty("buttonPrefix")]
        public string ButtonPrefix
        {
            get
            {
                return (string)this["buttonPrefix"];
            }

            set
            {
                this["buttonPrefix"] = value;
            }
        }

        [ConfigurationProperty("changeListXPath")]
        public string ChangeListXPath
        {
            get
            {
                return (string)this["changeListXPath"];
            }

            set
            {
                this["changeListXPath"] = value;
            }
        }

        [ConfigurationProperty("pointBoxesXPath")]
        public string PointBoxesXPath
        {
            get
            {
                return (string)this["pointBoxesXPath"];
            }

            set
            {
                this["pointBoxesXPath"] = value;
            }
        }

        [ConfigurationProperty("targets")]
        public Targets Targets
        {
            get
            {
                return (Targets)this["targets"];
            }

            set
            {
                this["targets"] = value;
            }
        }
    }
}
