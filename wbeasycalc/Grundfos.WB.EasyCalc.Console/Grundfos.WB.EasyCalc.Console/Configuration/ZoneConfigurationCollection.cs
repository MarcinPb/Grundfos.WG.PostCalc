using System.Configuration;

namespace Grundfos.WB.EasyCalc.Console.Configuration
{
    public class ZoneConfigurationCollection : ConfigurationElementCollection
    {
        public ZoneConfigurationElement this[int index]
        {
            get
            {
                return (ZoneConfigurationElement)this.BaseGet(index);
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

        public new ZoneConfigurationElement this[string key]
        {
            get
            {
                return (ZoneConfigurationElement)this.BaseGet(key);
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
            return new ZoneConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ZoneConfigurationElement)element).Name;
        }
    }
}
