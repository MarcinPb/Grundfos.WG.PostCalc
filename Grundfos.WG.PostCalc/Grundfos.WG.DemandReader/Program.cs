using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grundfos.WG.Model;
using Grundfos.WG.ObjectReaders;

namespace Grundfos.WG.DemandReader
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Execute(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors occurred when executing the application:");
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Execute(string[] args)
        {
            var cmd = new CommandLineParser(args);
            if (!cmd.TryGetValue("-src", out string sourcePath))
            {
                Console.WriteLine("No source file name was provided.");
                return;
            }

            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("Source file does not exist.");
                return;
            }


            if (!cmd.TryGetValue("-dest", out string destinationPath))
            {
                Console.WriteLine("No destination file name was provided.");
                return;
            }

            using (var dataSetProvider = new DomainDataSetProxy(sourcePath))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                var waterDemandDataReader = new WaterDemandDataReader(dataSet);
                var demands = waterDemandDataReader.GetWaterDemands();

                var zoneReader = new ZoneReader(dataSet);
                var zones = zoneReader.GetZones();

                var demandPatternReader = new WaterDemandPatternCurveReader(dataSet);
                var patterns = demandPatternReader.GetPatterns();

                FillPatternNames(demands, patterns.ToDictionary(x => x.Value, x => x.Key));
                FillZoneNamesInWaterDemands(demands, zones);

                DumpWaterDemandData(demands, destinationPath);
            }
        }

        private static IList<WaterDemandData> GetWgObjects(string waterGemsFileName)
        {
            using (var dataSetProvider = new DomainDataSetProxy(waterGemsFileName))
            {
                var dataSet = dataSetProvider.OpenDomainDataSet();
                var waterDemandDataReader = new WaterDemandDataReader(dataSet);
                var demands = waterDemandDataReader.GetWaterDemands();
                return demands;
            }
        }

        private static void FillPatternNames(IList<WaterDemandData> demands, Dictionary<int, string> patterns)
        {
            foreach (var item in demands)
            {
                if (!patterns.TryGetValue(item.DemandPatternID, out string patternName))
                {
                    throw new Exception(string.Format("Could not find pattern definition for pattern ID: {0}.", item.DemandPatternID));
                }

                item.DemandPatternName = patternName;
            }
        }

        private static void FillZoneNamesInWaterDemands(IList<WaterDemandData> demands, Dictionary<int, string> zones)
        {
            foreach (var item in demands.Where(x => x.ZoneID > 0))
            {
                if (!zones.TryGetValue(item.ZoneID, out string zoneName))
                {
                    throw new Exception(string.Format("Could not find pattern definition for pattern ID: {0}.", item.DemandPatternID));
                }

                item.ZoneName = zoneName;
            }
        }

        private static void DumpWaterDemandData(IList<WaterDemandData> demandData, string filePath)
        {
            using (var file = new StreamWriter(filePath))
            {
                file.WriteLine("ObjectID\tObjectTypeID\tDemandPatternID\tDemandPatternName\tZoneID\tZoneName\tBaseDemandValue");
                foreach (var item in demandData)
                {
                    file.WriteLine($"{item.ObjectID}\t{item.ObjectTypeID}\t{item.DemandPatternID}\t{item.DemandPatternName}\t{item.ZoneID}\t{item.ZoneName}\t{item.BaseDemandValue}");
                }
            }
        }
    }
}
