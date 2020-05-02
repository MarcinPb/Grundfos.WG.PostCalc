using Haestad.Domain;

namespace Grundfos.WG.PostCalc.DataExchangers
{
    public class DataExchangerConfiguration
    {
        public string ResultRecordName { get; set; }
        public string ResultAttributeRecordName { get; set; }
        public AlternativeType Alternative { get; set; }
        public string FieldName { get; set; }
        public double ConversionFactor { get; set; }
    }
}
