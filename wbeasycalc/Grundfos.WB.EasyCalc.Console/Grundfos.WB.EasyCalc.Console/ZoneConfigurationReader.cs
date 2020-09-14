using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.WB.DataAccess;
using Grundfos.WB.EasyCalc.Console.Configuration;

namespace Grundfos.WB.EasyCalc.Console
{
    public class ZoneConfigurationReader : IConfigurationReader
    {
        private List<ZoneConfigurationElement> zones;

        public ZoneConfigurationReader(List<ZoneConfigurationElement> zones)
        {
            this.zones = zones;
        }

        public DataAccess.Configuration GetConfiguration(string zoneID)
        {
            var zoneData = this.zones.First(x => x.Name.Equals(zoneID, StringComparison.OrdinalIgnoreCase));
            return this.Map(zoneData);
        }

        private DataAccess.Configuration Map(ZoneConfigurationElement zoneData)
        {
            var now = DateTime.UtcNow;
            var zoneConfiguration = new DataAccess.Configuration
            {
                //TimeFrom = new DateTime(now.Year, now.Month, 1),
                //TimeTo =
            };

            return zoneConfiguration;
        }
    }
}
