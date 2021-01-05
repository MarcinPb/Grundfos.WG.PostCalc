using System;
using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class Transformations : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "transformation";

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
            return new Transformation();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (Transformation)element;
            return item.SequenceNumber;
        }

        public Transformation this[int i]
        {
            get
            {
                return (Transformation)this.BaseGet(i);
            }
        }
    }
}
