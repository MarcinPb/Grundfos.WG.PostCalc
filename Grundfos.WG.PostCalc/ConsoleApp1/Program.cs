using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ConsoleApp1.Model;

// Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var waterDemandData = new WaterDemandData()
                {
                    ActualDemandValue = 0.22,
                    DemandPatternName = "DemandPatternName1"
                };

                var zone1 = new ZoneDemandData()
                {
                    DemandAdjustmentRatio = 3.5,
                    OpcTag = "j-999-U",
                    Demands =
                    {
                        new WaterDemandData()
                        {
                            ActualDemandValue = 0.22,
                            DemandPatternName = "DemandPatternName1"
                        },
                        new WaterDemandData()
                        {
                            ActualDemandValue = 1.72,
                            DemandPatternName = "DemandPatternName2"
                        },
                    }
                };
                var zone2 = new ZoneDemandData()
                {
                    DemandAdjustmentRatio = 3.5,
                    OpcTag = "j-999-U",
                    Demands =
                    {
                        new WaterDemandData()
                        {
                            ActualDemandValue = 0.22,
                            DemandPatternName = "DemandPatternName1"
                        },
                        new WaterDemandData()
                        {
                            ActualDemandValue = 1.72,
                            DemandPatternName = "DemandPatternName2"
                        },
                    }
                };

                List<ZoneDemandData> zoneList = new List<ZoneDemandData>() { zone1, zone2 };

                //Console.Write(ObjectToXml(zone1));
                Console.Write($"{ObjectToXml(zoneList)}\n\n");
                DumpToFile(ObjectToXml(zoneList), @"DumpFile");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"ggggggggggg_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fffffff")}_ggggg");

            Console.ReadKey();
        }

        private static void DumpToFile(string text, string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.Write(text);
            }
        }

        private static string ObjectToXml(object inputObject)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            string objectAsXmlString;

            XmlSerializer xs = new XmlSerializer(inputObject.GetType());
            using (StringWriter sw = new StringWriter())
            {
                try
                {
                    xs.Serialize(sw, inputObject, ns);
                    objectAsXmlString = sw.ToString();
                }
                catch (Exception ex)
                {
                    objectAsXmlString = ex.ToString();
                }
            }

            return objectAsXmlString;
        }
    }
}
