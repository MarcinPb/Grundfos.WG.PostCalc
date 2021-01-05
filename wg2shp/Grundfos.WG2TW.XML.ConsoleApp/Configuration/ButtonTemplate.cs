using System.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration
{
    public class ButtonTemplate : ConfigurationElement
    {
        [ConfigurationProperty("objectType")]
        public Grundfos.GeometryModel.ObjectTypes ObjectType
        {
            get
            {
                return (Grundfos.GeometryModel.ObjectTypes)this["objectType"];
            }

            set
            {
                this["objectType"] = value;
            }
        }

        [ConfigurationProperty("buttonTemplatePath")]
        public string ButtonTemplatePath
        {
            get
            {
                return (string)this["buttonTemplatePath"];
            }

            set
            {
                this["buttonTemplatePath"] = value;
            }
        }
    }
}
