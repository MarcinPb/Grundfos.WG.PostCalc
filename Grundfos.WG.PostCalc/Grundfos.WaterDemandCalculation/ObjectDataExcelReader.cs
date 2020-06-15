
using System;
using System.Collections.Generic;
using Grundfos.WG.Model;
using Grundfos.Workbooks;
using NPOI.SS.UserModel;

namespace Grundfos.WaterDemandCalculation
{
    public class ObjectDataExcelReader
    {
        private readonly ExcelReader excelReader;

        public ObjectDataExcelReader(ExcelReader excelReader)
        {
            this.excelReader = excelReader;
        }

        public ICollection<WaterDemandData> ReadObjects()
        {
            var objects = new List<WaterDemandData>();
            ISheet sheet = this.excelReader.Workbook.GetSheet("ObjectData");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null) //null is when the row only contains empty cells 
                {
                    var idCell = row.GetCell(0);
                    if (idCell == null)
                    {
                        continue;
                    }

                    var entry = new WaterDemandData
                    {
                        ObjectID = GetInt(idCell),
                        ObjectTypeID = GetInt(row.GetCell(1)),
                        DemandPatternName = GetString(row.GetCell(2)),
                        BaseDemandValue = GetDouble(row.GetCell(3)),
                        ZoneName = GetString(row.GetCell(4)),
                    };

                    objects.Add(entry);
                }
            }

            return objects;
        }

        private static int GetInt(ICell cell)
        {
            int value = cell.CellType == CellType.Numeric ? Convert.ToInt32(cell.NumericCellValue) : int.Parse(cell.StringCellValue);
            return value;
        }

        private static string GetString(ICell cell)
        {
            string value = cell.CellType == CellType.Numeric ? cell.NumericCellValue.ToString() : cell.StringCellValue;
            return value;
        }

        private static double GetDouble(ICell cell)
        {
            double value = cell.CellType == CellType.Numeric ? cell.NumericCellValue : double.Parse(cell.StringCellValue);
            return value;
        }
    }
}
