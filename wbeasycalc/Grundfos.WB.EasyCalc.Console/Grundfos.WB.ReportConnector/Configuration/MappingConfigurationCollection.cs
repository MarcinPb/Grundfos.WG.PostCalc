using System.Configuration;

namespace Grundfos.WB.ReportConnector.Configuration
{
    public class MappingConfigurationCollection : ConfigurationElementCollection
    {
        public MappingConfigurationElement this[int index]
        {
            get
            {
                return (MappingConfigurationElement)this.BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        public new MappingConfigurationElement this[string key]
        {
            get
            {
                return (MappingConfigurationElement)this.BaseGet(key);
            }
            set
            {
                if (this.BaseGet(key) != null)
                {
                    this.BaseRemoveAt(this.BaseIndexOf(this.BaseGet(key)));
                }

                this.BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MappingConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MappingConfigurationElement)element).Source;
        }
    }
}
