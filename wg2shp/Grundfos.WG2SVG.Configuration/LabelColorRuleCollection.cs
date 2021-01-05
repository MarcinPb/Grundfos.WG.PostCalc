using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class LabelColorRuleCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "labelColorRule";

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
            return new LabelColorRule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (LabelColorRule)element;
            return item.ObjectTypeID;
        }

        public LabelColorRule this[int i]
        {
            get
            {
                return (LabelColorRule)this.BaseGet(i);
            }
        }
    }
}