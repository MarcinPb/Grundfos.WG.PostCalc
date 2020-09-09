using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Grundfos.OPC.Model;
using Grundfos.WB.ReportConnector.Configuration;

namespace Grundfos.WB.ReportConnector.Mapping
{
    public class ZoneDataMapper : IMapper<ICollection<DataRow>, ICollection<OpcValue>>
    {
        private readonly Dictionary<string, string> _zoneMappings;
        private readonly Dictionary<string, string> _zoneFieldMappings;
        private readonly string _zoneColumnName;

        public ZoneDataMapper(IList<IMappingDefinition> zoneMappings, IList<IMappingDefinition> fieldMappings, string zoneColumnName)
        {
            _zoneMappings = zoneMappings.ToDictionary(x => x.Source, x => x.Destination, StringComparer.OrdinalIgnoreCase);
            _zoneFieldMappings = fieldMappings.ToDictionary(x => x.Source, x => x.Destination, StringComparer.OrdinalIgnoreCase);
            _zoneColumnName = zoneColumnName;
        }

        public ICollection<OpcValue> Map(ICollection<DataRow> source)
        {
            var result = new List<OpcValue>();
            if (source.Count == 0)
            {
                return result;
            }

            foreach (var row in source)
            {
                if (!this._zoneMappings.TryGetValue((string)row[_zoneColumnName], out string zoneName))
                {
                    continue;
                }


                foreach (var field in this._zoneFieldMappings)
                {
                    var fieldValue = row[field.Key];
                    string tag = string.Format(field.Value, zoneName);
                    var opcValue = new OpcValue { Tag = tag, Value = fieldValue };
                    result.Add(opcValue);
                }
            }

            return result;
        }
    }
}
