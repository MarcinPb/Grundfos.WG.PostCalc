using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class AttributeColorRule : ConfigurationElement
    {
        [ConfigurationProperty("objectTypeID")]
        public int ObjectTypeID
        {
            get
            {
                return (int)base["objectTypeID"];
            }

            set
            {
                base["objectTypeID"] = value;
            }
        }

        [ConfigurationProperty("attributeID")]
        public int AttributeID
        {
            get
            {
                return (int)base["attributeID"];
            }

            set
            {
                base["attributeID"] = value;
            }
        }

        [ConfigurationProperty("valueColors")]
        public ValueColorCollection ValueColors
        {
            get
            {
                return (ValueColorCollection)base["valueColors"];
            }

            set
            {
                base["valueColors"] = value;
            }
        }

        public override string ToString()
        {
            return $"{this.ObjectTypeID}:{this.AttributeID}";
        }
    }
}
