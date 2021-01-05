using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class ValueColorCollection : ConfigurationElementCollection
    {

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "valueColor";

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
            return new ValueColor();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (ValueColor)element;
            return item.ToString();
        }

        public ValueColor this[int i]
        {
            get
            {
                return (ValueColor)this.BaseGet(i);
            }
        }
    }
}
