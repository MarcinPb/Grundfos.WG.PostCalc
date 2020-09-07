using System.Linq;
using Grundfos.OPC;
using Grundfos.WB.EasyCalc.Calculations;
using Grundfos.WB.EasyCalc.Calculations.Model;
using Grundfos.WB.EasyCalc.Console.Mapping;

namespace Grundfos.WB.EasyCalc.Console
{
    public class EasyCalcOpcDataReader : IEasyCalcDataReader
    {
        private readonly OpcReader client;
        private readonly EasyCalcDataMapper mapper;
        private readonly string tagFormat;

        public EasyCalcOpcDataReader(OpcReader client, EasyCalcDataMapper mapper, string tagFormat)
        {
            this.client = client;
            this.mapper = mapper;
            this.tagFormat = tagFormat;
        }

        public EasyCalcSheetData ReadSheetData(string zone)
        {
            var zoneTags = Configuration.OpcTagNames.OpcRequestTags.Select(x => this.GetTagFormat(zone, x)).ToArray();
            var values = this.client.GetValues(zoneTags);
            this.mapper.Zone = zone;
            var mapped = this.mapper.Map(values);
            return mapped;
        }

        public void PublishSheetData(EasyCalcSheetData data, string zone)
        {
            this.mapper.Zone = zone;
            var values = this.mapper.Map(data);
            this.client.WriteValues(values);
        }

        private string GetTagFormat(string zone, string tag)
        {
            return string.Format(this.tagFormat, zone, tag);
        }
    }
}
