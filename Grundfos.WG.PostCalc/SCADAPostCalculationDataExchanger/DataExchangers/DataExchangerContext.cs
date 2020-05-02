using Haestad.DataIntegration;
using Haestad.Support.OOP.Configuration;
using Haestad.Support.OOP.Logging;

namespace Grundfos.WG.PostCalc.DataExchangers
{
    class DataExchangerContext : DataExchangeContextBase
    {
        public DataExchangerContext(ActionLogger logger, IConfigurationReader configurationReader)
            : base(logger, configurationReader)
        {
        }

        public override object DataExchangeSource
        {
            get
            {
                return null;
            }
        }

        public override object DataExchangeDestination
        {
            get
            {
                return null;
            }
        }

        public string GetString(string key, string defaultValue = null)
        {
            return this.ConfigurationReader.ReadString(key, defaultValue);
        }

        public double GetDouble(string key, double defaultValue = double.NaN)
        {
            return this.ConfigurationReader.ReadRealNumber(key, defaultValue);
        }
    }
}
