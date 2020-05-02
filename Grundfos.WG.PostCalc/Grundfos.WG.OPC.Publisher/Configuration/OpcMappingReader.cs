using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.Workbooks;
using Haestad.Support.OOP.Logging;
using NPOI.SS.UserModel;

namespace Grundfos.WG.OPC.Publisher.Configuration
{
    public class OpcMappingReader
    {
        private const string SheetName = "OpcMapping";
        private readonly ActionLogger logger;
        private readonly ExcelReader excel;

        public OpcMappingReader(ActionLogger logger, ExcelReader excelReader)
        {
            this.logger = logger;
            this.excel = excelReader;
        }

        public ICollection<OpcMapping> ReadMappings()
        {
            var sheet = this.excel.Workbook.GetSheet(SheetName);
            if (sheet == null)
            {
                return new List<OpcMapping>();
            }

            var rawMappings = GetRawMappings(sheet);
            List<OpcMapping> mappings = GetMappings(rawMappings);

            return mappings;
        }

        private static List<OpcMapping> GetMappings(List<RawMappingEntry> rawMappings)
        {
            var mappings = new List<OpcMapping>();
            var grouped = rawMappings.GroupBy(x => x.FieldName);
            foreach (var group in grouped)
            {
                var first = group.First();
                var mapping = new OpcMapping
                {
                    FieldName = first.FieldName,
                    Mappings = GetMappingList(group),
                };

                mappings.Add(mapping);
            }

            return mappings;
        }

        private static ICollection<OpcMappingEntry> GetMappingList(IGrouping<string, RawMappingEntry> group)
        {
            var entries = group.Select(x =>
                new OpcMappingEntry
                {
                    ElementID = x.ElementID,
                    ElementLabel = x.ElementLabel,
                    Enabled = x.Enabled,
                    OpcTag = x.OpcTag
                }).ToList();
            return entries;
        }

        private List<RawMappingEntry> GetRawMappings(ISheet sheet)
        {
            var rawMappings = new List<RawMappingEntry>();
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }

                try
                {
                    var entry = MapEntry(row);
                    if (entry == null)
                    {
                        continue;
                    }

                    rawMappings.Add(entry);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Could not read OPC mapping entry in row {0}.", i + 1);
                    this.logger.WriteMessage(OutputLevel.Errors, message);
                    this.logger.WriteException(ex, true);
                }
            }

            return rawMappings;
        }

        private static RawMappingEntry MapEntry(IRow row)
        {
            var fieldName = row.GetCell(0);
            var elementID = row.GetCell(1);
            var label = row.GetCell(2);
            var enabled = row.GetCell(3);
            var tag = row.GetCell(4);
            if (fieldName == null || elementID == null || label == null || enabled == null || tag == null)
            {
                return null;
            }

            var entry = new RawMappingEntry
            {
                FieldName = fieldName.StringCellValue,
                ElementID = (int)elementID.NumericCellValue,
                ElementLabel = label.StringCellValue,
                Enabled = enabled.BooleanCellValue,
                OpcTag = tag.StringCellValue,
            };

            return entry;
        }

        private class RawMappingEntry
        {
            public string FieldName { get; set; }
            public int ElementID { get; set; }
            public string ElementLabel { get; set; }
            public bool Enabled { get; set; }
            public string OpcTag { get; set; }
        }
    }
}
