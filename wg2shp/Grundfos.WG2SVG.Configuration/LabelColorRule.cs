using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class LabelColorRule : ConfigurationElementCollection
    {
        [ConfigurationProperty("objectTypeID", IsKey = true)]
        public int ObjectTypeID
        {
            get
            {
                return (int)this["objectTypeID"];
            }

            set
            {
                this["objectTypeID"] = value;
            }
        }

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "rule";

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
            return new Rule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (Rule)element;
            return item.LabelToken;
        }

        public Rule this[int i]
        {
            get
            {
                return (Rule)this.BaseGet(i);
            }
        }
    }
}