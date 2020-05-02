using System.Collections.Generic;
using Haestad.Domain;

namespace Grundfos.WG.OPC.Publisher.Configuration
{
    public class PublisherConfiguration
    {
        public string ResultRecordName { get; set; }
        public string ResultAttributeRecordName { get; set; }
        public AlternativeType Alternative { get; set; }
        public string FieldName { get; set; }
        public double ConversionFactor { get; set; }
        public DomainElementType[] ElementTypes { get; set; }
        public IDictionary<int, OpcMappingEntry> Mappings { get; set; }
    }
}
