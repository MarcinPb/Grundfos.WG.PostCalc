using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.Workbooks.Model
{
    public class CellByName<T>
    {
        public string SheetName { get; set; }
        public string CellName { get; set; }
        public T CellValue { get; set; }
        public Type CellValueType { get; set; }

        public CellByName(string sheetName, string cellName, T value)
        {
            SheetName = sheetName;
            CellName = cellName;
            CellValue = value;
            CellValueType = typeof(T);
        }

        public override string ToString()
        {
            return $"{SheetName} - {CellName} - {CellValue}";
        }
    }
}
