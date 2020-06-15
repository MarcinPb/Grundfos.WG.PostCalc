using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.WG.Model;
using NPOI.HSSF.Util;
using NPOI.OpenXmlFormats;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Grundfos.Workbooks
{
    public class ExcelWriter
    {
        private string _sqliteDbFile = @"C:\WG2TW\WGModel\testOPC.wtg.sqlite";

        public static void Write(
            string excelFileName, 
            IList<IdahoPatternPatternCurve> idahoPatternPatternCurveList,
            IList<WaterDemandData> waterDemandDataList
            )
        {
            using (var fs = new FileStream(excelFileName, FileMode.Create, FileAccess.Write))
            {
                int rowIndex;
                IRow row;

                IWorkbook workbook = new XSSFWorkbook();

                ISheet sheet1 = workbook.CreateSheet("DemandPatterns");
                rowIndex = 0;
                row = sheet1.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue("Pattern Name");
                row.CreateCell(1).SetCellValue("Start (min)");
                row.CreateCell(2).SetCellValue("Value");
                foreach (var item in idahoPatternPatternCurveList)
                {
                    rowIndex++;
                    row = sheet1.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(item.SupportElementId);
                    row.CreateCell(1).SetCellValue(item.PatternCurveTimeFromStart/60);
                    row.CreateCell(2).SetCellValue(item.PatternCurveMultiplier);
                }

                ISheet sheet2 = workbook.CreateSheet("ObjectData");
                rowIndex = 0;
                row = sheet2.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue("ObjectID");
                row.CreateCell(1).SetCellValue("ObjectTypeID");
                row.CreateCell(2).SetCellValue("DemandPatternName");
                row.CreateCell(3).SetCellValue("BaseDemandValue");
                row.CreateCell(4).SetCellValue("ZoneName");
                row.CreateCell(5).SetCellValue("IsActive");
                foreach (var item in waterDemandDataList)
                {
                    rowIndex++;
                    row = sheet2.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(item.ObjectID);
                    row.CreateCell(1).SetCellValue(item.ObjectTypeID);
                    row.CreateCell(2).SetCellValue(item.DemandPatternName);
                    row.CreateCell(3).SetCellValue(item.BaseDemandValue);
                    row.CreateCell(4).SetCellValue(item.ZoneName);
                    row.CreateCell(5).SetCellValue(item.IsActive);
                }

                workbook.Write(fs);
            }
        }

        public static void Write1()
        {
            var newFile = @"C:\WG2TW\Grundfos.WG.PostCalc\newbook.core.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();

                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                var rowIndex = 0;
                IRow row = sheet1.CreateRow(rowIndex);
                row.Height = 30 * 80;
                row.CreateCell(0).SetCellValue("this is content");
                sheet1.AutoSizeColumn(0);
                rowIndex++;

                var sheet2 = workbook.CreateSheet("Sheet2");
                var style1 = workbook.CreateCellStyle();
                style1.FillForegroundColor = HSSFColor.Blue.Index2;
                style1.FillPattern = FillPattern.SolidForeground;

                var style2 = workbook.CreateCellStyle();
                style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                style2.FillPattern = FillPattern.SolidForeground;

                var cell2 = sheet2.CreateRow(0).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(0);

                cell2 = sheet2.CreateRow(1).CreateCell(0);
                cell2.CellStyle = style2;
                cell2.SetCellValue(1);

                workbook.Write(fs);
            }
        }

    }
}
