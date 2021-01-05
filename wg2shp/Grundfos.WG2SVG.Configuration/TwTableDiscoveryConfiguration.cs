using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class TwTableDiscoveryConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("getTableSql")]
        public string GetTableSql
        {
            get
            {
                return (string)this["getTableSql"];
            }

            set
            {
                this["getTableSql"] = value;
            }
        }

        [ConfigurationProperty("tableNameColumn")]
        public string TableNameColumn
        {
            get
            {
                return (string)this["tableNameColumn"];
            }

            set
            {
                this["tableNameColumn"] = value;
            }
        }
    }
}
