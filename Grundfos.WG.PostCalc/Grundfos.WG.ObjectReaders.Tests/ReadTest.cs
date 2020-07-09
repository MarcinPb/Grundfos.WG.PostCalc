using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.WG.Model;
//using Haestad.Support.OOP.FileSystem;
//using Haestad.Support.OOP.Logging;
using NUnit.Framework;

namespace Grundfos.WG.ObjectReaders.Tests
{
    [TestFixture]
    public class ReadTest
    {
        private string _sqliteDbFile = @"C:\WG2TW\WGModel\testOPC.wtg.sqlite";
        private string _logFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Grundfos.WG.ObjectReaders.Tests.log");


        [TestCase]
        public void Read_Tests()
        {

            SqliteProxy sqliteProxy = new SqliteProxy(_sqliteDbFile);
            var idahoPatternList = sqliteProxy.GetIdahoPatternList();
            var idahoPatternPatternCurveList = sqliteProxy.GetIdahoPatternPatternCurveList();
            
            
            //var logger = new ActionLogger();
            //logger.InitializeLogger(new FileLogger(new FilePath(_logFolder), 10000), OutputLevel.Info);

            //using (var dataSetProvider = new DomainDataSetProxy(_sqliteDbFile))
            //using (var dataSet = dataSetProvider.OpenDomainDataSet())
            //{
            //    var waterDemandDataReader = new WaterDemandDataReader(dataSet);
            //    var demands = waterDemandDataReader.GetWaterDemands();

            //    var zoneReader = new ZoneReader(dataSet);
            //    var zones = zoneReader.GetZones();

            //    var demandPatternReader = new WaterDemandPatternCurveReader(dataSet);
            //    var patterns = demandPatternReader.GetIdahoPatternList();
            //    var patternCurve = demandPatternReader.GetIdahoPatternPatternCurveList(patterns.Where(x => x.Value >= 0).Select(x => x.Value).ToList());

            //    FillPatternNames(demands, patterns.ToDictionary(x => x.Value, x => x.Key));
            //    FillZoneNamesInWaterDemands(demands, zones);

            //    //DumpWaterDemandData(demands, _destinationPath);
            //}
        }

        [TestCase]
        public void Read_Tests02()
        {
            SqliteProxy sqliteProxy = new SqliteProxy(_sqliteDbFile);
            //var idahoPatternList = sqliteProxy.GetIdahoPatternList();
            //var idahoPatternPatternCurveList = sqliteProxy.GetIdahoPatternPatternCurveList();

            //sqliteProxy.GetNode(518);
            //sqliteProxy.GetNode(147655, 23);
            //sqliteProxy.GetNode(5516, 69);
            sqliteProxy.GetSimulationResult();
        }

        //private static IList<WaterDemandData> GetWgObjects(string waterGemsFileName)
        //{
        //    using (var dataSetProvider = new DomainDataSetProxy(waterGemsFileName))
        //    {
        //        var dataSet = dataSetProvider.OpenDomainDataSet();
        //        var waterDemandDataReader = new WaterDemandDataReader(dataSet);
        //        var demands = waterDemandDataReader.GetWaterDemands();
        //        return demands;
        //    }
        //}

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
