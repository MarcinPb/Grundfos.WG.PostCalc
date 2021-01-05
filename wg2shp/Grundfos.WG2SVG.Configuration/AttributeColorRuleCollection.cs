using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class AttributeColorRuleCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "attributeColorRule";

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(this.ElementName, System.StringComparison.OrdinalIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AttributeColorRule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (AttributeColorRule)element;
            return item.ToString();
        }

        public AttributeColorRule this[int i]
        {
            get
            {
                return (AttributeColorRule)this.BaseGet(i);
            }
        }
    }
}
