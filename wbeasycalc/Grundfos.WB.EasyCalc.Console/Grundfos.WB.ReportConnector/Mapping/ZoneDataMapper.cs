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
        private readonly Dictionary<string, string> zoneMappings;
        private readonly Dictionary<string, string> fieldMappings;
        private readonly string zoneNameColumn;

        public ZoneDataMapper(IList<IMappingDefinition> zoneMappings, IList<IMappingDefinition> fieldMappings, string zoneNameColumn)
        {
            this.zoneMappings = zoneMappings.ToDictionary(x => x.Source, x => x.Destination, StringComparer.OrdinalIgnoreCase);
            this.fieldMappings = fieldMappings.ToDictionary(x => x.Source, x => x.Destination, StringComparer.OrdinalIgnoreCase);
            this.zoneNameColumn = zoneNameColumn;
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
                if (!this.zoneMappings.TryGetValue((string)row[this.zoneNameColumn], out string zoneName))
                {
                    continue;
                }


                foreach (var field in this.fieldMappings)
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
