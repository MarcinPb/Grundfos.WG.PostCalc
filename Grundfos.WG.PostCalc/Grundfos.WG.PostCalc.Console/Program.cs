using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using Grundfos.OPC;
using Grundfos.OPC.Model;
using Grundfos.WG.PostCalc.Persistence.MapperProfiles;
using Grundfos.WG.PostCalc.Persistence.Model;
using Grundfos.WG.PostCalc.Persistence.Repositories;
using Grundfos.WG.PostCalc.SQLiteEf;

namespace Grundfos.WG.PostCalc.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new OpcWriter("Kepware.KEPServerEX.V6");
            var requests = new OpcWriteValue[]
            {
                new OpcWriteValue { TagName = "JunctionConcentration.DEV.j-1-010_2759", Value = 789.654 },
                new OpcWriteValue { TagName = "JunctionConcentration.DEV.j-1-011_2758", Value = 321.456 },
            };
            publisher.Publish(requests);

            string totalDemandTag = "Control.DEV.ScadaTotalDemand";
            var signals = new string[]
            {
                "JunctionDemand.DEV.j-1-024_2745",
                "JunctionDemand.DEV.j-1-025_2743",
                "JunctionDemand.DEV.j-1-026-M13_2742",
                "JunctionDemand.DEV.j-1-027_2741",
                totalDemandTag,
            };
            using (var opc = new OpcReader("Kepware.KEPServerEX.V6"))
            {
                foreach (var item in signals)
                {
                    var value = opc.GetDouble(item);
                }
            }

            var db = new DatabaseContext(@"C:\Users\75643\AppData\Local\Bentley\WaterGEMS\10\ResultCache.sqlite");
            var mapper = BuildMapper();
            var repo = new PostCalcRepository(db, mapper);

            repo.ClearResultsByAttribute("WaterTrace");
            var timestamp = DateTime.Now;
            var results = new List<Result>
            {
                new Result
                {
                    ObjectID = 15,
                    Value = 1500.3900,
                    Timestamp = timestamp,
                }
            };

            repo.SetResults("WaterTrace", results);
            repo.SaveChanges();

            var currentProcess = Process.GetCurrentProcess();
            var data = repo.GetResultsByAttribute("WaterTrace");
            foreach (var item in data)
            {
                System.Console.WriteLine($"ObjectID : {item.ObjectID}  Value : {item.Value}");
            }
        }

        private static IMapper BuildMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ResultProfile).Assembly);
            });
            var mapper = new Mapper(mapperConfig);
            return mapper;
        }
    }
}
