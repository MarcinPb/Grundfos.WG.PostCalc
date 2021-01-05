using System.ComponentModel;
using System.Configuration;
using Grundfos.GeometryModel;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class Target : ConfigurationElement
    {
        [ConfigurationProperty("templateFileName")]
        public string TemplateFileName
        {
            get
            {
                return (string)this["templateFileName"];
            }

            set
            {
                this["templateFileName"] = value;
            }
        }

        [ConfigurationProperty("destinationFileName")]
        public string DestinationFileName
        {
            get
            {
                return (string)this["destinationFileName"];
            }

            set
            {
                this["destinationFileName"] = value;
            }
        }

        [ConfigurationProperty("objectTypes")]
        [TypeConverter(typeof(Converters.String2ObjectTypesConverter))]
        public ObjectTypes[] ObjectTypes
        {
            get
            {
                return (ObjectTypes[])this["objectTypes"];
            }

            set
            {
                this["objectTypes"] = value;
            }
        }
    }
}
