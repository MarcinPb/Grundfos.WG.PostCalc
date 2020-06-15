using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.WG;
using Grundfos.WG.Model;
using Grundfos.Workbooks;
using NPOI.SS.UserModel;

namespace Grundfos.WaterDemandCalculation
{
    public class DemandPatternExcelReader
    {
        private readonly ExcelReader excelReader;

        public DemandPatternExcelReader(ExcelReader excelReader)
        {
            this.excelReader = excelReader;
        }

        public Dictionary<string, WaterDemandPattern> ReadDemands()
        {
            var rawData = new List<RawDemandEntry>();
            ISheet sheet = this.excelReader.Workbook.GetSheet("DemandPatterns");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null) //null is when the row only contains empty cells 
                {
                    var patternNameCell = row.GetCell(0);
                    if (patternNameCell == null)
                    {
                        continue;
                    }

                    string patternName = patternNameCell.CellType == CellType.String ?
                        patternNameCell.StringCellValue : patternNameCell.NumericCellValue.ToString();
                    var entry = new RawDemandEntry
                    {
                        PatternName = patternName,
                        TimeshiftMinutes = row.GetCell(1).NumericCellValue,
                        Value = row.GetCell(2).NumericCellValue,
                    };
                    rawData.Add(entry);
                }
            }

            var result = new Dictionary<string, WaterDemandPattern>(StringComparer.OrdinalIgnoreCase);
            var grouped = rawData.GroupBy(x => x.PatternName, StringComparer.OrdinalIgnoreCase);
            foreach (var group in grouped)
            {
                var pattern = new WaterDemandPattern
                {
                    Name = group.Key,
                    Profile = group
                        .OrderBy(x => x.TimeshiftMinutes)
                        .Select(x => new WaterDemandPatternEntry
                        {
                            TimeshiftMinutes = x.TimeshiftMinutes,
                            Value = x.Value
                        }).ToList(),
                };
                result[pattern.Name] = pattern;
            }

            return result;
        }

        public List<int> ReadExcludedObjects()
        {
            var items = new List<int>();
            ISheet sheet = this.excelReader.Workbook.GetSheet("ExcludedItems");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                ICell cell;
                if (sheet.GetRow(i) != null && (cell = sheet.GetRow(i).GetCell(0)) != null) //null is when the row only contains empty cells 
                {
                    int item = (int)cell.NumericCellValue;
                    items.Add(item);
                }
            }

            return items;
        }

        public List<string> ReadExcludedPatterns()
        {
            var patterns = new List<string>();
            ISheet sheet = this.excelReader.Workbook.GetSheet("ExcludedItems");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                ICell cell;
                if (sheet.GetRow(i) != null && (cell = sheet.GetRow(i).GetCell(1)) != null) //null is when the row only contains empty cells 
                {
                    string pattern = cell.StringCellValue;
                    patterns.Add(pattern);
                }
            }

            return patterns;
        }

        public ICollection<ZoneMapping> ReadZones()
        {
            var zones = new List<ZoneMapping>();
            ISheet sheet = this.excelReader.Workbook.GetSheet("Zones");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }

                var zoneNameCell = row.GetCell(0);
                var opcNameCell = row.GetCell(1);
                if (zoneNameCell == null || opcNameCell == null)
                {
                    continue;
                }

                zones.Add(new ZoneMapping { ZoneName = zoneNameCell.StringCellValue, OpcTag = opcNameCell.StringCellValue });
            }

            return zones;
        }

        private class RawDemandEntry
        {
            public string PatternName { get; set; }
            public double TimeshiftMinutes { get; set; }
            public double Value { get; set; }
        }
    }
}
