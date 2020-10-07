using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WpfApp1
{
    public class SystemDataRepo
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.Setting"].ConnectionString;
        public SystemDataRepo()
        {

        }

        public DataTable GetSystemData(int year, int month, int zoneId)
        {
            try
            {
                string sqlQueryString = $"SELECT * FROM [dbo].[vw_zuzycia_stref_2] WHERE [year] = {year} AND [month] = {month} AND ZoneId = {zoneId}";
                    //@"
                    //SELECT 
                    //  [id_strefy]
                    // ,[nazwa_strefy]
                    // ,CAST(REPLACE([dlugosc_sieci], ',','.') AS FLOAT) AS dlugosc_sieci
                    // ,[ilosc_przylaczy]
                    // ,[sprzedaz_w_strefie]
                    // ,[year]
                    // ,[month]
                    //FROM 
                    // [dbo].[vw_zuzycia_stref]
                    //WHERE 
                    // [year] >= 2020
                    //";
                return FillDataTable(sqlQueryString);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private DataTable FillDataTable(string sqlQueryString)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQueryString, sqlConn))
                    {
                        sqlConn.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw new Exception($"FillDataTable error: {e.Message}");
            }
        }
    }
}
