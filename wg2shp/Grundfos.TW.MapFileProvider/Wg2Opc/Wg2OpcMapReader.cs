using System.Collections.Generic;
using System.IO;
using NLog;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using AutoMapper;
using System.Linq;
using System;

namespace Grundfos.TW.DataSourceMap.Wg2Opc
{
    public class Wg2OpcMapReader
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly string fileName;
        private readonly IMapper mapper;

        public Wg2OpcMapReader(string fileName, IMapper mapper)
        {
            this.fileName = fileName;
            this.mapper = mapper;
        }

        public List<Wg2OpcMapEntry> Read()
        {
            log.Info("Start reading WG2OPC map entries from: {0}", this.fileName);
            var result = new List<Wg2OpcMapEntry>();
            XSSFWorkbook hssfwb;
            using (var file = new FileStream(this.fileName, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }

            ISheet sheet = hssfwb.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null)
                {
                    break;
                }

                var entry = this.mapper.Map<Wg2OpcMapEntry>(row);
                result.Add(entry);
            }

            var grouped = result.GroupBy(x => x.OpcTag, StringComparer.OrdinalIgnoreCase).Where(x => x.Count() > 1).ToList();
            if (grouped.Count > 0)
            {
                string duplicates = string.Join(", ", grouped.Select(x => x.Key));
                throw new Exception(string.Format("Duplicate entries found in {0}: {1}.", this.fileName, duplicates));
            }

            log.Info("Finished reading WG2OPC map entries. {0} entries were found.", result.Count);
            return result;
        }
    }
}
