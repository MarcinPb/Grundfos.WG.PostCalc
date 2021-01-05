using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class TwSignalDiscoveryConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("unionCommand")]
        public string UnionCommand
        {
            get
            {
                return (string)this["unionCommand"];
            }

            set
            {
                this["unionCommand"] = value;
            }
        }

        [ConfigurationProperty("selectCommand")]
        public string SelectCommand
        {
            get
            {
                return (string)this["selectCommand"];
            }

            set
            {
                this["selectCommand"] = value;
            }
        }

        [ConfigurationProperty("pageSize")]
        public int PageSize
        {
            get
            {
                return (int)this["pageSize"];
            }

            set
            {
                this["pageSize"] = value;
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

        [ConfigurationProperty("floatValueColumn")]
        public string FloatValueColumn
        {
            get
            {
                return (string)this["floatValueColumn"];
            }

            set
            {
                this["floatValueColumn"] = value;
            }

        }

        [ConfigurationProperty("integerValueColumn")]
        public string IntegerValueColumn
        {
            get
            {
                return (string)this["integerValueColumn"];
            }

            set
            {
                this["integerValueColumn"] = value;
            }
        }

        [ConfigurationProperty("idColumn")]
        public string IdColumn
        {
            get
            {
                return (string)this["idColumn"];
            }

            set
            {
                this["idColumn"] = value;
            }
        }

        [ConfigurationProperty("timeColumn")]
        public string TimeColumn
        {
            get
            {
                return (string)this["timeColumn"];
            }

            set
            {
                this["timeColumn"] = value;
            }
        }

        [ConfigurationProperty("dataTypeColumn")]
        public string DataTypeColumn
        {
            get
            {
                return (string)this["dataTypeColumn"];
            }

            set
            {
                this["dataTypeColumn"] = value;
            }
        }

        [ConfigurationProperty("intervalSeconds")]
        public int IntervalSeconds
        {
            get
            {
                return (int)this["intervalSeconds"];
            }

            set
            {
                this["intervalSeconds"] = value;
            }
        }

        [ConfigurationProperty("integerDataTypes")]
        public TwDataTypesCollection IntegerDataTypes
        {
            get
            {
                return (TwDataTypesCollection)base["integerDataTypes"];
            }

            set
            {
                base["integerDataTypes"] = value;
            }
        }

        [ConfigurationProperty("floatDataTypes")]
        public TwDataTypesCollection FloatDataTypes
        {
            get
            {
                return (TwDataTypesCollection)base["floatDataTypes"];
            }

            set
            {
                base["floatDataTypes"] = value;
            }
        }
    }
}
