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
                DumpToFile(ObjectToXml(zoneList), @"aaa\DumpFile");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static void DumpToFile(string text, string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.Write(text);
            }
        }

        private static string ObjectToXml(object output)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            string objectAsXmlString;

            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(output.GetType());
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                try
                {
                    xs.Serialize(sw, output, ns);
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
