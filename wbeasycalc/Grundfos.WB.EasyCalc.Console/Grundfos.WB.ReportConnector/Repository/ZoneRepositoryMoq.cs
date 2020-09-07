using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Grundfos.WB.ReportConnector.Repository
{
    public class ZoneRepositoryMoq : IRepository<DataRow>
    {
        public ICollection<DataRow> GetAll()
        {
            var dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn { ColumnName = "id_strefy", DataType = typeof(int) },
                new DataColumn { ColumnName = "nazwa_strefy", DataType = typeof(string) },
                new DataColumn { ColumnName = "dlugosc_sieci", DataType =  typeof(double) },
                new DataColumn { ColumnName = "ilosc_przylaczy", DataType = typeof(int) },
                new DataColumn { ColumnName = "sprzedaz_w_strefie", DataType = typeof(double) },
            });

            var rowI = dataTable.NewRow();
            rowI.ItemArray = new object[] { 1, "I", 1500.3900, 1500, 955435 };
            var rowII = dataTable.NewRow();
            rowII.ItemArray = new object[] { 2, "II", 2500.3900, 1500, 955435 };
            var rowIII = dataTable.NewRow();
            rowIII.ItemArray = new object[] { 3, "III", 3500.3900, 1500, 955435 };
            var rowIV = dataTable.NewRow();
            rowIV.ItemArray = new object[] { 4, "IV", 4500.3900, 1500, 955435 };
            var rowV = dataTable.NewRow();
            rowV.ItemArray = new object[] { 5, "V", 5500.3900, 1500, 955435 };

            dataTable.Rows.Add(rowI);
            dataTable.Rows.Add(rowII);
            dataTable.Rows.Add(rowIII);
            dataTable.Rows.Add(rowIV);
            dataTable.Rows.Add(rowV);

            return dataTable.Rows.Cast<DataRow>().ToList();
        }
    }
}
