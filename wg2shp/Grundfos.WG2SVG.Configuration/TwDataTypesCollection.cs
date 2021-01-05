using System;
using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class TwDataTypesCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "item";

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
            return new TwDataType();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (TwDataType)element;
            return item.Value.ToString();
        }

        public TwDataType this[int i]
        {
            get
            {
                return (TwDataType)this.BaseGet(i);
            }
        }
    }
}
