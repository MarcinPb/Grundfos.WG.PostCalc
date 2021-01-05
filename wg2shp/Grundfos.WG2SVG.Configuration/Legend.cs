using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class Legend : ConfigurationElement
    {
        [ConfigurationProperty("fileName")]
        public string FileName
        {
            get
            {
                return (string)base["fileName"];
            }

            set
            {
                base["fileName"] = value;
            }
        }

        [ConfigurationProperty("width")]
        public int Width
        {
            get
            {
                return (int)base["width"];
            }

            set
            {
                base["width"] = value;
            }
        }

        [ConfigurationProperty("height")]
        public int Height
        {
            get
            {
                return (int)base["height"];
            }

            set
            {
                base["height"] = value;
            }
        }

        [ConfigurationProperty("description")]
        public string Description
        {
            get
            {
                return (string)base["description"];
            }

            set
            {
                base["description"] = value;
            }
        }

        [ConfigurationProperty("horizontalPadding")]
        public int HorizontalPadding
        {
            get
            {
                return (int)base["horizontalPadding"];
            }

            set
            {
                base["horizontalPadding"] = value;
            }
        }

        [ConfigurationProperty("verticalPadding")]
        public int VerticalPadding
        {
            get
            {
                return (int)base["verticalPadding"];
            }

            set
            {
                base["verticalPadding"] = value;
            }
        }

        [ConfigurationProperty("fontSize")]
        public int FontSize
        {
            get
            {
                return (int)base["fontSize"];
            }

            set
            {
                base["fontSize"] = value;
            }
        }
    }
}
