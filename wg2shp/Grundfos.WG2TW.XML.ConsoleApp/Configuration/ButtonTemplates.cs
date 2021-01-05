using System;
using System.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class ButtonTemplates : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => "buttonTemplate";

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
            return new ButtonTemplate();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var item = (ButtonTemplate)element;
            return item.ObjectType.ToString();
        }

        public ButtonTemplate this[int i]
        {
            get
            {
                return (ButtonTemplate)this.BaseGet(i);
            }
        }
    }
}
