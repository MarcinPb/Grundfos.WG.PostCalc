using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class Target : ConfigurationElement
    {
        [ConfigurationProperty("fileName")]
        public string FileName
        {
            get
            {
                var fileName = (string)base["fileName"];
                return fileName;
            }

            set
            {
                base["fileName"] = value;
            }
        }

        [ConfigurationProperty("legend")]
        public Legend Legend
        {
            get
            {
                return (Legend)base["legend"];
            }

            set
            {
                base["legend"] = value;
            }
        }
        
        [ConfigurationProperty("transformations")]
        public Transformations Transformations
        {
            get
            {
                var transformations = (Transformations)base["transformations"];
                return transformations;
            }

            set
            {
                base["transformations"] = value;
            }
        }

        [ConfigurationProperty("labelColorRules")]
        public LabelColorRuleCollection LabelColorRules
        {
            get
            {
                var rules = (LabelColorRuleCollection)base["labelColorRules"];
                return rules;
            }

            set
            {
                base["labelColorRules"] = value;
            }
        }

        [ConfigurationProperty("strokeWidthSettings")]
        public StrokeWidthSettings StrokeWidthSettings
        {
            get
            {
                var settings = (StrokeWidthSettings)base["strokeWidthSettings"];
                return settings;
            }

            set
            {
                base["strokeWidthSettings"] = value;
            }
        }

        [ConfigurationProperty("attributeColorRules")]
        public AttributeColorRuleCollection AttributeColorRules
        {
            get
            {
                return (AttributeColorRuleCollection)base["attributeColorRules"];
            }

            set
            {
                base["attributeColorRules"] = value;
            }
        }
    }
}
