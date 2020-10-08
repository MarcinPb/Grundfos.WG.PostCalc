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
                    //[year]
                    //[month]
                    //[ZoneId]
                    //[sprzedaz_w_strefie]          // BilledCons_BilledMetConsBulkWatSupExpM3_D6
                    //[dlugosc_sieci]               // Network_DistributionAndTransmissionMains_D7
                    //[ilosc_przylaczy]             // Network_NoOfConnOfRegCustomers_H10
                    //[SystemImputVolume]           // SysInput_SystemInputVolumeM3_D6                  
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
