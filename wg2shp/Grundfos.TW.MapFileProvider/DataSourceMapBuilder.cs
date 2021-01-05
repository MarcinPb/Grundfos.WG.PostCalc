using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grundfos.TW.DataSourceMap.Tw2Opc;
using Grundfos.TW.DataSourceMap.Wg2Opc;
using NLog;

namespace Grundfos.TW.DataSourceMap
{
    public class DataSourceMapBuilder
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly Wg2OpcMapReader wg2OpcReader;
        private readonly TwVar2OpcMapReader var2OpcReader;
        private readonly TwVar2IDMapReader var2IdReader;

        public DataSourceMapBuilder(Wg2OpcMapReader wg2OpcReader, TwVar2OpcMapReader var2OpcReader, TwVar2IDMapReader var2IdReader)
        {
            this.wg2OpcReader = wg2OpcReader;
            this.var2OpcReader = var2OpcReader;
            this.var2IdReader = var2IdReader;
        }

        public List<DataSourceMapEntry> Build()
        {
            log.Info("Start building data source map.");
            var wg2OpcMap = this.GetWg2OpcMap();
            var var2OpcMap = this.GetTwVar2OpcMap();
            var var2IdMap = this.GetTwVar2IDMap();

            var result = CombineToMap(wg2OpcMap, var2OpcMap, var2IdMap);
            log.Info("Finished building data source map. {0} entries were compiled.", result.Count);

            return result;
        }

        private static List<DataSourceMapEntry> CombineToMap(List<Wg2OpcMapEntry> wg2OpcMap, List<TwVar2OpcMapEntry> var2OpcMap, List<TwVar2IDMapEntry> var2IdMap)
        {
            var map = new List<DataSourceMapEntry>();
            var var2OpcDict = var2OpcMap.ToDictionary(x => x.OpcTag, x => x);
            var var2IdDict = var2IdMap.ToDictionary(x => x.VariableName, x => x);

            foreach (var wg2OpcEntry in wg2OpcMap)
            {
                var var2OpcEntry = var2OpcDict[wg2OpcEntry.OpcTag];
                var var2IdEntry = var2IdDict[var2OpcEntry.VariableName];
                var mapEntry = new DataSourceMapEntry
                {
                    WgObjectID = wg2OpcEntry.ElementID,
                    WgObjectLabel = wg2OpcEntry.ElementLabel,
                    WgAttributeID = wg2OpcEntry.ResultAttributeID,
                    TwVariableID = var2IdEntry.VariableID,
                    TwVariableName = var2IdEntry.VariableName,
                };
                map.Add(mapEntry);
            }

            return map;
        }

        private List<Wg2OpcMapEntry> GetWg2OpcMap()
        {
            var result = this.wg2OpcReader.Read();
            return result;
        }

        private List<TwVar2OpcMapEntry> GetTwVar2OpcMap()
        {
            var result = this.var2OpcReader.Read();
            return result;
        }

        private List<TwVar2IDMapEntry> GetTwVar2IDMap()
        {
            var result = this.var2IdReader.Read();
            return result;
        }
    }
}
