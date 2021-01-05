using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class TargetCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "target";

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
            return new Target();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (Target)element;
            return item.FileName;
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
