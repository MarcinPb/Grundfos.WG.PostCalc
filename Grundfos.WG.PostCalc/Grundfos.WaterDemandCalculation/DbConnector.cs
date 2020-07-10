using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.OPC;
using Haestad.Support.OOP.Logging;

namespace Grundfos.WaterDemandCalculation
{
    public class DbConnector
    {
        private class PipeMeterOpcValue
        {
            public int QpId { get; set; }
            public int ModelVarId { get; set; }
            public string ModelVarName { get; set; }
            public double Value { get; set; }
        }

        public static void SavePipeMeterListToDatabase(string conStr, string opcServerAddress)
        {
            try
            {
                //_logger?.WriteMessage(OutputLevel.Info, $"SavePipeMeterListToDatabase ############################");

                using (SqlConnection sqlConn = new SqlConnection(conStr))
                {
                    DataSet dataSet = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("spLoadPipeMeterList", sqlConn)
                        {
                            CommandType = CommandType.StoredProcedure
                        }
                    };
                    adapter.Fill(dataSet);

                    var pipeMeterList = dataSet.Tables[0].AsEnumerable().Select(x => new PipeMeterOpcValue
                    {
                        QpId = x.Field<int>("QpId"),
                        ModelVarId = x.Field<int>("ModelVarId"),
                        ModelVarName = x.Field<string>("ModelVarName"),
                    }).ToList();

                    FillPipeMeterValueList(pipeMeterList, opcServerAddress);

                    int logHeaderId;

                    using (SqlCommand cmd = new SqlCommand("spSaveLogHeader", sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@DateTimeIn", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@DateTimeUtcIn", SqlDbType.DateTime).Value = DateTime.UtcNow;
                        cmd.Parameters.Add("@LogTypeId", SqlDbType.Int).Value = 2;  // Save PipeMeter list
                        cmd.Parameters.Add("@RatioFormula", SqlDbType.NVarChar, 4000).Value = string.Empty;
                        cmd.Parameters.Add("@MinutesFromMonday", SqlDbType.Float).Value = 0;  // Calculated by last Create method.

                        cmd.Parameters.Add("@LogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters["@LogId"].Value = 0;

                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                        logHeaderId = Convert.ToInt32(cmd.Parameters["@LogId"].Value);
                        sqlConn.Close();
                    }

                    foreach (var zone in pipeMeterList)
                    {
                        using (SqlCommand cmd = new SqlCommand("spSaveLogPipeMeter", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@LogHeaderId", SqlDbType.Int).Value = logHeaderId;
                            cmd.Parameters.Add("@PipeMeterId", SqlDbType.Int).Value = zone.QpId;
                            cmd.Parameters.Add("@Value", SqlDbType.Float).Value = zone.Value;

                            sqlConn.Open();
                            cmd.ExecuteNonQuery();
                            sqlConn.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //_logger?.WriteMessage(OutputLevel.Errors, $"Saving PipeMeterList to database.\n{e.Message}");
                throw new Exception($"Saving PipeMeterList to database.\n{e.Message}");
            }
        }

        private static void FillPipeMeterValueList(List<PipeMeterOpcValue> opcTagValueDict, string opcServerAddress)
        {
            using (var opc = new OpcReader(opcServerAddress))
            {
                foreach (var opcTagValue in opcTagValueDict)
                {
                    opcTagValue.Value = opc.GetDouble($"PipeFlow.DEV.{opcTagValue.ModelVarName}_{opcTagValue.ModelVarId}");
                }
            }
        }
    }



}
