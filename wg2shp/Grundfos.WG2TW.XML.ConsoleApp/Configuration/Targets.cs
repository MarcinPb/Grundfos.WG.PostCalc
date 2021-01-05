using System;
using System.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class Targets : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "target";

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(this.ElementName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Target();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (Target)element;
            return item.DestinationFileName;
        }

        public Target this[int i]
        {
            get
            {
                return (Target)this.BaseGet(i);
            }
        }
    }
}
