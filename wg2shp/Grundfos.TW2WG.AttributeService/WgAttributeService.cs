using System.Collections.Generic;
using System.Linq;
using Grundfos.TW.DataSourceMap;
using Grundfos.TW.SQL.Model;

namespace Grundfos.TW2WG.AttributeService
{
    public class WgAttributeService : IWgAttributeService
    {
        private readonly List<DataSourceMapEntry> dataSourceMap;
        private readonly Dictionary<long, SignalValue> signalValues;

        public WgAttributeService(List<DataSourceMapEntry> dataSourceMap, List<SignalValue> signalValues)
        {
            this.dataSourceMap = dataSourceMap;
            this.signalValues = signalValues.ToDictionary(x => x.ID, x => x);
        }

        public double GetAttributeValue(int wgObjectID, int wgAttributeID)
        {
            var mapEntry = this.dataSourceMap.FirstOrDefault(x => x.WgObjectID == wgObjectID && x.WgAttributeID == wgAttributeID);
            if (mapEntry == null)
            {
                return double.NaN;
            }

            if (!signalValues.TryGetValue(mapEntry.TwVariableID, out SignalValue signalValue))
            {
                return double.NaN;
            }

            return signalValue.Value;
        }
    }
}
