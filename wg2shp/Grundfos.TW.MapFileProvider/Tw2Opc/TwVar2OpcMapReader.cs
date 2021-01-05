using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using NLog;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Grundfos.TW.DataSourceMap.Tw2Opc
{
    public class TwVar2OpcMapReader
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly string fileName;
        private readonly IMapper mapper;

        public TwVar2OpcMapReader(string fileName, IMapper mapper)
        {
            this.fileName = fileName;
            this.mapper = mapper;
        }

        public List<TwVar2OpcMapEntry> Read()
        {
            log.Info("Start reading TWVar2OPC map entries from: {0}", this.fileName);
            var result = new List<TwVar2OpcMapEntry>();
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

                var entry = this.mapper.Map<TwVar2OpcMapEntry>(row);
                result.Add(entry);
            }

            var grouped = result.GroupBy(x => x.TwOpcTag + ":" + x.VariableName, StringComparer.OrdinalIgnoreCase).Where(x => x.Count() > 1).ToList();
            if (grouped.Count > 0)
            {
                string duplicates = string.Join(", ", grouped.Select(x => x.Key));
                throw new Exception(string.Format("Duplicate entries found in {0}: {1}.", this.fileName, duplicates));
            }


            log.Info("Finished reading TWVar2OPC map entries. {0} entries were found.", result.Count);
            return result;
        }
    }
}
