using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Grundfos.Workbooks
{
    public class ExcelReader
    {
        private const string SettingsSheetName = "ApplicationSettings";

        public ExcelReader(string filePath)
        {
            this.FilePath = filePath;
            this.Workbook = this.ReadFile();
        }

        public string FilePath { get; }
        public XSSFWorkbook Workbook { get; }

        private XSSFWorkbook ReadFile()
        {
            XSSFWorkbook wbk;
            using (FileStream file = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read))
            {
                wbk = new XSSFWorkbook(file);
            }

            return wbk;
        }

        public T ReadSetting<T>(string settingName)
        {
            var sheet = this.Workbook.GetSheet(SettingsSheetName);
            if (sheet == null)
            {
                string message = string.Format("Could not find setting sheet: {0}.", SettingsSheetName);
                throw new Exception(message);
            }

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }

                var settingNameCell = row.GetCell(0);
                if (settingNameCell == null)
                {
                    continue;
                }

                if (!settingNameCell.StringCellValue.Equals(settingName, System.StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var valueCell = row.GetCell(1);
                if (valueCell == null)
                {
                    throw new Exception("The value was empty");
                }

                T value = this.GetCellValue<T>(valueCell);
                return value;
            }

            throw new Exception("Could not find the requested setting.");
        }

        public T ReadCell<T>(string sheetName, string cellName)
        {
            string ascii = "ABCDEFGHIJKLMNOPQRSTUWXYZ";

            string columnName = Regex.Match(cellName, @"[A-Z]").Value;
            string rowName = Regex.Match(cellName, @"\d+").Value;

            int columnNo = ascii.IndexOf(columnName);
            int rowNo = Convert.ToInt32(rowName)-1;

            return ReadCell<T>(sheetName, columnNo, rowNo);
        }

        public T ReadCell<T>(string sheetName, int columnNo, int rowNo)
        {
            var sheet = this.Workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                string message = $"Could not find setting sheet: {SettingsSheetName}.";
                throw new Exception(message);
            }

            IRow row = sheet.GetRow(rowNo);
            ICell valueCell = row.GetCell(columnNo);

            T value = this.GetCellValue<T>(valueCell);
            return value;
        }

        private T GetCellValue<T>(ICell cell)
        {
            Type t = typeof(T);
            if (t == typeof(double) || t == typeof(int))
            {
                return (T)Convert.ChangeType(cell.NumericCellValue, t);
            }

            if (t == typeof(string))
            {
                return (T)Convert.ChangeType(cell.NumericCellValue, t);
            }

            if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(cell.BooleanCellValue, t);
            }

            if (typeof(T) == typeof(DateTime))
            {
                return (T)Convert.ChangeType(cell.DateCellValue, t);
            }

            throw new ArgumentException(string.Format("The requested type is not supported: {0}", t.ToString()), nameof(T));
        }
    }
}
