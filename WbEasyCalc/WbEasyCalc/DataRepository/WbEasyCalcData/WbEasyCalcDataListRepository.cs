using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DataRepository.WbEasyCalcData
{
    public class WbEasyCalcDataListRepository : IWbEasyCalcDataListRepository
    {

        private List<DataModel.WbEasyCalcData> _list;  
        public List<DataModel.WbEasyCalcData> GetList()
        {
            using (IDbConnection cnn = new SqlConnection(_cnnString))
            {
                string sql = "dbo.spWbEasyCalcDataList";
                _list = cnn.Query<DataModel.WbEasyCalcData>(sql, commandType: CommandType.StoredProcedure).ToList();
                return _list;
            }
        }

        public DataModel.WbEasyCalcData GetItem(int id)
        {
            if (id != 0)
            {
                var customer = _list.Single(f => f.WbEasyCalcDataId == id);
                return (DataModel.WbEasyCalcData)customer?.Clone();
            }
            else
            {
                return new DataModel.WbEasyCalcData();
            }
        }

        public DataModel.WbEasyCalcData SaveItem(DataModel.WbEasyCalcData model)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();

                p.Add("@id", model.WbEasyCalcDataId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                p.Add("@UserName", userName);
                p.Add("@ZoneId", model.ZoneId);
                p.Add("@YearNo", model.YearNo);
                p.Add("@MonthNo", model.MonthNo);
                p.Add("@Description", model.Description);
                p.Add("@IsArchive", model.IsArchive);
                p.Add("@IsAccepted", model.IsAccepted);
                // input
                p.Add("@Start_PeriodDays_M21", model.Start_PeriodDays_M21);
                p.Add("@SysInput_SystemInputVolumeM3_D6", model.SysInput_SystemInputVolumeM3_D6);
                p.Add("@SysInput_SystemInputVolumeError_F6", model.SysInput_SystemInputVolumeError_F6);
                p.Add("@BilledCons_BilledMetConsBulkWatSupExpM3_D6", model.BilledCons_BilledMetConsBulkWatSupExpM3_D6);
                p.Add("@BilledCons_BilledUnmetConsBulkWatSupExpM3_H6", model.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6);

                p.Add("@BilledCons_UnbMetConsM3_D8", model.BilledCons_UnbMetConsM3_D8);
                p.Add("@BilledCons_UnbUnmetConsM3_H8", model.BilledCons_UnbUnmetConsM3_H8);

                p.Add("@UnbilledCons_MetConsBulkWatSupExpM3_D6", model.UnbilledCons_MetConsBulkWatSupExpM3_D6);

                p.Add("@UnbilledCons_UnbMetConsM3_D8", model.UnbilledCons_UnbMetConsM3_D8);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H6", model.UnbilledCons_UnbUnmetConsM3_H6);
                p.Add("@UnbilledCons_UnbUnmetConsError_J6", model.UnbilledCons_UnbUnmetConsError_J6);

                p.Add("@UnauthCons_IllegalConnDomEstNo_D6", model.UnauthCons_IllegalConnDomEstNo_D6);
                p.Add("@UnauthCons_IllegalConnDomPersPerHouse_H6", model.UnauthCons_IllegalConnDomPersPerHouse_H6);
                p.Add("@UnauthCons_IllegalConnDomConsLitPerPersDay_J6", model.UnauthCons_IllegalConnDomConsLitPerPersDay_J6);
                p.Add("@UnauthCons_IllegalConnDomErrorMargin_F6", model.UnauthCons_IllegalConnDomErrorMargin_F6);
                p.Add("@UnauthCons_IllegalConnOthersErrorMargin_F10", model.UnauthCons_IllegalConnOthersErrorMargin_F10);

                p.Add("@IllegalConnectionsOthersEstimatedNumber_D10", model.IllegalConnectionsOthersEstimatedNumber_D10);
                p.Add("@IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10", model.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10);

                p.Add("@UnauthCons_MeterTampBypEtcEstNo_D14", model.UnauthCons_MeterTampBypEtcEstNo_D14);
                p.Add("@UnauthCons_MeterTampBypEtcErrorMargin_F14", model.UnauthCons_MeterTampBypEtcErrorMargin_F14);
                p.Add("@UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14", model.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14);
                p.Add("@MetErrors_DetailedManualSpec_J6", model.MetErrors_DetailedManualSpec_J6);
                p.Add("@MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8", model.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8);
                p.Add("@MetErrors_BilledMetConsWoBulkSupErrorMargin_N8", model.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8);

                p.Add("@MeteredBulkSupplyExportErrorMargin_N32", model.MeteredBulkSupplyExportErrorMargin_N32);
                p.Add("@UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34", model.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34);
                p.Add("@CorruptMeterReadingPracticessErrorMargin_N38", model.CorruptMeterReadingPracticessErrorMargin_N38);
                p.Add("@DataHandlingErrorsOffice_L40", model.DataHandlingErrorsOffice_L40);
                p.Add("@DataHandlingErrorsOfficeErrorMargin_N40", model.DataHandlingErrorsOfficeErrorMargin_N40);

                p.Add("@MetErrors_MetBulkSupExpMetUnderreg_H32", model.MetErrors_MetBulkSupExpMetUnderreg_H32);
                p.Add("@MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34", model.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34);
                p.Add("@MetErrors_CorruptMetReadPractMetUndrreg_H38", model.MetErrors_CorruptMetReadPractMetUndrreg_H38);
                p.Add("@Network_DistributionAndTransmissionMains_D7", model.Network_DistributionAndTransmissionMains_D7);
                p.Add("@Network_NoOfConnOfRegCustomers_H10", model.Network_NoOfConnOfRegCustomers_H10);
                p.Add("@Network_NoOfInactAccountsWSvcConns_H18", model.Network_NoOfInactAccountsWSvcConns_H18);
                p.Add("@Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32", model.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32);
                p.Add("@Prs_ApproxNoOfConn_D7", model.Prs_ApproxNoOfConn_D7);
                p.Add("@Prs_DailyAvgPrsM_F7", model.Prs_DailyAvgPrsM_F7);
                p.Add("@Prs_ErrorMarg_F26", model.Prs_ErrorMarg_F26);
                p.Add("@PIs_IliBestEstimate_F25", model.PIs_IliBestEstimate_F25);                
                // output
                p.Add("@SystemInputVolume_B19", model.SystemInputVolume_B19);
                p.Add("@SystemInputVolumeErrorMargin_B21", model.SystemInputVolumeErrorMargin_B21);
                p.Add("@AuthorizedConsumption_K12", model.AuthorizedConsumption_K12);
                p.Add("@AuthorizedConsumptionErrorMargin_K15", model.AuthorizedConsumptionErrorMargin_K15);
                p.Add("@WaterLosses_K29", model.WaterLosses_K29);
                p.Add("@WaterLossesErrorMargin_K31", model.WaterLossesErrorMargin_K31);
                p.Add("@BilledAuthorizedConsumption_T8", model.BilledAuthorizedConsumption_T8);
                p.Add("@UnbilledAuthorizedConsumption_T16", model.UnbilledAuthorizedConsumption_T16);
                p.Add("@UnbilledAuthorizedConsumptionErrorMargin_T20", model.UnbilledAuthorizedConsumptionErrorMargin_T20);
                p.Add("@CommercialLosses_T26", model.CommercialLosses_T26);
                p.Add("@CommercialLossesErrorMargin_T29", model.CommercialLossesErrorMargin_T29);
                p.Add("@PhysicalLossesM3_T34", model.PhysicalLossesM3_T34);
                p.Add("@PhyscialLossesErrorMargin_AH35", model.PhyscialLossesErrorMargin_AH35);
                p.Add("@BilledMeteredConsumption_AC4", model.BilledMeteredConsumption_AC4);
                p.Add("@BilledUnmeteredConsumption_AC9", model.BilledUnmeteredConsumption_AC9);
                p.Add("@UnbilledMeteredConsumption_AC14", model.UnbilledMeteredConsumption_AC14);
                p.Add("@UnbilledUnmeteredConsumption_AC19", model.UnbilledUnmeteredConsumption_AC19);
                p.Add("@UnbilledUnmeteredConsumptionErrorMargin_AO20", model.UnbilledUnmeteredConsumptionErrorMargin_AO20);
                p.Add("@UnauthorizedConsumption_AC24", model.UnauthorizedConsumption_AC24);
                p.Add("@UnauthorizedConsumptionErrorMargin_AO25", model.UnauthorizedConsumptionErrorMargin_AO25);
                p.Add("@CustomerMeterInaccuraciesAndErrorsM3_AC29", model.CustomerMeterInaccuraciesAndErrorsM3_AC29);
                p.Add("@CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", model.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30);
                p.Add("@RevenueWaterM3_AY8", model.RevenueWaterM3_AY8);
                p.Add("@NonRevenueWaterM3_AY24", model.NonRevenueWaterM3_AY24);
                p.Add("@NonRevenueWaterErrorMargin_AY26", model.NonRevenueWaterErrorMargin_AY26);

                connection.Execute("dbo.spWbEasyCalcDataSave", p, commandType: CommandType.StoredProcedure);

                model.WbEasyCalcDataId = p.Get<int>("@id");

                return model;
            }
        }

        public bool DeleteItem(int id)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@id", id);
                connection.Execute("dbo.spWbEasyCalcDataDelete", p, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public bool DeleteItem(List<int> idList)
        {
            throw new NotImplementedException();
        }

        public int Clone(int id)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@id", id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                connection.Execute("dbo.spWbEasyCalcDataClone", p, commandType: CommandType.StoredProcedure);

                var wbEasyCalcDataId = p.Get<int>("@id");

                return wbEasyCalcDataId;
            }
        }

        public int CreateAll()
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@RecordQty", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                connection.Execute("dbo.spWbEasyCalcDataCreateAll", p, commandType: CommandType.StoredProcedure);

                var recordQty = p.Get<int>("@RecordQty");

                return recordQty;
            }
        }

        private readonly string _cnnString;
        public WbEasyCalcDataListRepository(string cnnString)
        {
            _cnnString = cnnString;
        }
    }
}
