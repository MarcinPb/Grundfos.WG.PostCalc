using System.Configuration;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class ButtonFactoryConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("buttonWidth")]
        public int ButtonWidth
        {
            get
            {
                return (int)this["buttonWidth"];
            }

            set
            {
                this["buttonWidth"] = value;
            }
        }

        [ConfigurationProperty("buttonHeight")]
        public int ButtonHeight
        {
            get
            {
                return (int)this["buttonHeight"];
            }

            set
            {
                this["buttonHeight"] = value;
            }
        }

        [ConfigurationProperty("transformations")]
        public Transformations Transformations
        {
            get
            {
                return (Transformations)base["transformations"];
            }

            set
            {
                base["transformations"] = value;
            }
        }

        [ConfigurationProperty("buttonTemplates")]
        public ButtonTemplates ButtonTemplates
        {
            get
            {
                return (ButtonTemplates)this["buttonTemplates"];
            }

            set
            {
                this["buttonTemplates"] = value;
            }
        }
    }
}
