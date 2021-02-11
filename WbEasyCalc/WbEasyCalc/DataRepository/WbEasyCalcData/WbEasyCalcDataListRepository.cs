﻿using System;
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
                var list = cnn.Query<WbEasyCalcDb>(sql, commandType: CommandType.StoredProcedure).ToList();
                _list = list.Select(x => x.GetWbEasyCalcData()).ToList();
                return _list;
            }
        }

        public DataModel.WbEasyCalcData GetItem(int id)
        {
            if (id != 0)
            {
                var item = _list.Single(f => f.WbEasyCalcDataId == id);
                return (DataModel.WbEasyCalcData)item?.Clone();
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

                p.Add("@SysInput_Desc_B6", model.SysInput_Desc_B6);
                p.Add("@SysInput_Desc_B7", model.SysInput_Desc_B7);
                p.Add("@SysInput_Desc_B8", model.SysInput_Desc_B8);
                p.Add("@SysInput_Desc_B9", model.SysInput_Desc_B9);
                p.Add("@SysInput_SystemInputVolumeM3_D6", model.SysInput_SystemInputVolumeM3_D6);
                p.Add("@SysInput_SystemInputVolumeError_F6", model.SysInput_SystemInputVolumeError_F6);
                p.Add("@SysInput_SystemInputVolumeM3_D7", model.SysInput_SystemInputVolumeM3_D7);
                p.Add("@SysInput_SystemInputVolumeError_F7", model.SysInput_SystemInputVolumeError_F7);
                p.Add("@SysInput_SystemInputVolumeM3_D8", model.SysInput_SystemInputVolumeM3_D8);
                p.Add("@SysInput_SystemInputVolumeError_F8", model.SysInput_SystemInputVolumeError_F8);
                p.Add("@SysInput_SystemInputVolumeM3_D9", model.SysInput_SystemInputVolumeM3_D9);
                p.Add("@SysInput_SystemInputVolumeError_F9", model.SysInput_SystemInputVolumeError_F9);

                p.Add("@BilledCons_Desc_B8", model.BilledCons_Desc_B8   );
                p.Add("@BilledCons_Desc_B9", model.BilledCons_Desc_B9   );
                p.Add("@BilledCons_Desc_B10", model.BilledCons_Desc_B10  );
                p.Add("@BilledCons_Desc_B11", model.BilledCons_Desc_B11  );
                p.Add("@BilledCons_Desc_F8", model.BilledCons_Desc_F8   );
                p.Add("@BilledCons_Desc_F9", model.BilledCons_Desc_F9   );
                p.Add("@BilledCons_Desc_F10", model.BilledCons_Desc_F10  );
                p.Add("@BilledCons_Desc_F11", model.BilledCons_Desc_F11  );
                p.Add("@UnbilledCons_Desc_D8", model.UnbilledCons_Desc_D8 );
                p.Add("@UnbilledCons_Desc_D9", model.UnbilledCons_Desc_D9 );
                p.Add("@UnbilledCons_Desc_D10", model.UnbilledCons_Desc_D10);
                p.Add("@UnbilledCons_Desc_D11", model.UnbilledCons_Desc_D11);
                p.Add("@UnbilledCons_Desc_F6", model.UnbilledCons_Desc_F6 );
                p.Add("@UnbilledCons_Desc_F7", model.UnbilledCons_Desc_F7 );
                p.Add("@UnbilledCons_Desc_F8", model.UnbilledCons_Desc_F8 );
                p.Add("@UnbilledCons_Desc_F9", model.UnbilledCons_Desc_F9 );
                p.Add("@UnbilledCons_Desc_F10", model.UnbilledCons_Desc_F10);
                p.Add("@UnbilledCons_Desc_F11", model.UnbilledCons_Desc_F11);
                p.Add("@UnauthCons_Desc_B18", model.UnauthCons_Desc_B18  );
                p.Add("@UnauthCons_Desc_B19", model.UnauthCons_Desc_B19  );
                p.Add("@UnauthCons_Desc_B20", model.UnauthCons_Desc_B20  );
                p.Add("@UnauthCons_Desc_B21", model.UnauthCons_Desc_B21  );
                p.Add("@MetErrors_Desc_D12", model.MetErrors_Desc_D12   );
                p.Add("@MetErrors_Desc_D13", model.MetErrors_Desc_D13   );
                p.Add("@MetErrors_Desc_D14", model.MetErrors_Desc_D14   );
                p.Add("@MetErrors_Desc_D15", model.MetErrors_Desc_D15   );
                p.Add("@Network_Desc_B7", model.Network_Desc_B7      );
                p.Add("@Network_Desc_B8", model.Network_Desc_B8      );
                p.Add("@Network_Desc_B9", model.Network_Desc_B9      );
                p.Add("@Network_Desc_B10", model.Network_Desc_B10     );
                p.Add("@Interm_Area_B7", model.Interm_Area_B7       );
                p.Add("@Interm_Area_B8", model.Interm_Area_B8       );
                p.Add("@Interm_Area_B9", model.Interm_Area_B9       );
                p.Add("@Interm_Area_B10", model.Interm_Area_B10      );

                p.Add("@BilledCons_BilledMetConsBulkWatSupExpM3_D6", model.BilledCons_BilledMetConsBulkWatSupExpM3_D6);
                p.Add("@BilledCons_BilledUnmetConsBulkWatSupExpM3_H6", model.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6);

                p.Add("@BilledCons_UnbMetConsM3_D8", model.BilledCons_UnbMetConsM3_D8);
                p.Add("@BilledCons_UnbMetConsM3_D9", model.BilledCons_UnbMetConsM3_D9);
                p.Add("@BilledCons_UnbMetConsM3_D10", model.BilledCons_UnbMetConsM3_D10);
                p.Add("@BilledCons_UnbMetConsM3_D11", model.BilledCons_UnbMetConsM3_D11);
                p.Add("@BilledCons_UnbUnmetConsM3_H8", model.BilledCons_UnbUnmetConsM3_H8);
                p.Add("@BilledCons_UnbUnmetConsM3_H9", model.BilledCons_UnbUnmetConsM3_H9);
                p.Add("@BilledCons_UnbUnmetConsM3_H10", model.BilledCons_UnbUnmetConsM3_H10);
                p.Add("@BilledCons_UnbUnmetConsM3_H11", model.BilledCons_UnbUnmetConsM3_H11);

                p.Add("@UnbilledCons_MetConsBulkWatSupExpM3_D6", model.UnbilledCons_MetConsBulkWatSupExpM3_D6);

                p.Add("@UnbilledCons_UnbMetConsM3_D8", model.UnbilledCons_UnbMetConsM3_D8);
                p.Add("@UnbilledCons_UnbMetConsM3_D9", model.UnbilledCons_UnbMetConsM3_D9);
                p.Add("@UnbilledCons_UnbMetConsM3_D10", model.UnbilledCons_UnbMetConsM3_D10);
                p.Add("@UnbilledCons_UnbMetConsM3_D11", model.UnbilledCons_UnbMetConsM3_D11);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H6", model.UnbilledCons_UnbUnmetConsM3_H6);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H7", model.UnbilledCons_UnbUnmetConsM3_H7);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H8", model.UnbilledCons_UnbUnmetConsM3_H8);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H9", model.UnbilledCons_UnbUnmetConsM3_H9);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H10", model.UnbilledCons_UnbUnmetConsM3_H10);
                p.Add("@UnbilledCons_UnbUnmetConsM3_H11", model.UnbilledCons_UnbUnmetConsM3_H11);
                p.Add("@UnbilledCons_UnbUnmetConsError_J6", model.UnbilledCons_UnbUnmetConsError_J6);
                p.Add("@UnbilledCons_UnbUnmetConsError_J7", model.UnbilledCons_UnbUnmetConsError_J7);
                p.Add("@UnbilledCons_UnbUnmetConsError_J8", model.UnbilledCons_UnbUnmetConsError_J8);
                p.Add("@UnbilledCons_UnbUnmetConsError_J9", model.UnbilledCons_UnbUnmetConsError_J9);
                p.Add("@UnbilledCons_UnbUnmetConsError_J10", model.UnbilledCons_UnbUnmetConsError_J10);
                p.Add("@UnbilledCons_UnbUnmetConsError_J11", model.UnbilledCons_UnbUnmetConsError_J11);

                p.Add("@UnauthCons_OthersErrorMargin_F18", model.UnauthCons_OthersErrorMargin_F18);
                p.Add("@UnauthCons_OthersErrorMargin_F19", model.UnauthCons_OthersErrorMargin_F19);
                p.Add("@UnauthCons_OthersErrorMargin_F20", model.UnauthCons_OthersErrorMargin_F20);
                p.Add("@UnauthCons_OthersErrorMargin_F21", model.UnauthCons_OthersErrorMargin_F21);
                p.Add("@UnauthCons_OthersM3PerDay_J18", model.UnauthCons_OthersM3PerDay_J18);
                p.Add("@UnauthCons_OthersM3PerDay_J19", model.UnauthCons_OthersM3PerDay_J19);
                p.Add("@UnauthCons_OthersM3PerDay_J20", model.UnauthCons_OthersM3PerDay_J20);
                p.Add("@UnauthCons_OthersM3PerDay_J21", model.UnauthCons_OthersM3PerDay_J21);

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

                p.Add("@MetErrors_Total_F12", model.MetErrors_Total_F12);
                p.Add("@MetErrors_Total_F13", model.MetErrors_Total_F13);
                p.Add("@MetErrors_Total_F14", model.MetErrors_Total_F14);
                p.Add("@MetErrors_Total_F15", model.MetErrors_Total_F15);
                p.Add("@MetErrors_Meter_H12", model.MetErrors_Meter_H12);
                p.Add("@MetErrors_Meter_H13", model.MetErrors_Meter_H13);
                p.Add("@MetErrors_Meter_H14", model.MetErrors_Meter_H14);
                p.Add("@MetErrors_Meter_H15", model.MetErrors_Meter_H15);
                p.Add("@MetErrors_Error_N12", model.MetErrors_Error_N12);
                p.Add("@MetErrors_Error_N13", model.MetErrors_Error_N13);
                p.Add("@MetErrors_Error_N14", model.MetErrors_Error_N14);
                p.Add("@MetErrors_Error_N15", model.MetErrors_Error_N15);

                p.Add("@MeteredBulkSupplyExportErrorMargin_N32", model.MeteredBulkSupplyExportErrorMargin_N32);
                p.Add("@UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34", model.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34);
                p.Add("@CorruptMeterReadingPracticessErrorMargin_N38", model.CorruptMeterReadingPracticessErrorMargin_N38);
                p.Add("@DataHandlingErrorsOffice_L40", model.DataHandlingErrorsOffice_L40);
                p.Add("@DataHandlingErrorsOfficeErrorMargin_N40", model.DataHandlingErrorsOfficeErrorMargin_N40);

                p.Add("@MetErrors_MetBulkSupExpMetUnderreg_H32", model.MetErrors_MetBulkSupExpMetUnderreg_H32);
                p.Add("@MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34", model.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34);
                p.Add("@MetErrors_CorruptMetReadPractMetUndrreg_H38", model.MetErrors_CorruptMetReadPractMetUndrreg_H38);
                p.Add("@Network_DistributionAndTransmissionMains_D7", model.Network_DistributionAndTransmissionMains_D7);
                p.Add("@Network_DistributionAndTransmissionMains_D8", model.Network_DistributionAndTransmissionMains_D8);
                p.Add("@Network_DistributionAndTransmissionMains_D9", model.Network_DistributionAndTransmissionMains_D9);
                p.Add("@Network_DistributionAndTransmissionMains_D10", model.Network_DistributionAndTransmissionMains_D10);
                p.Add("@Network_NoOfConnOfRegCustomers_H10", model.Network_NoOfConnOfRegCustomers_H10);
                p.Add("@Network_NoOfInactAccountsWSvcConns_H18", model.Network_NoOfInactAccountsWSvcConns_H18);
                p.Add("@Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32", model.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32);

                p.Add("@Network_PossibleUnd_D30", model.Network_PossibleUnd_D30);
                p.Add("@Network_NoCustomers_H7", model.Network_NoCustomers_H7);  
                p.Add("@Network_ErrorMargin_J7", model.Network_ErrorMargin_J7);  
                p.Add("@Network_ErrorMargin_J10", model.Network_ErrorMargin_J10);
                p.Add("@Network_ErrorMargin_J18", model.Network_ErrorMargin_J18);
                p.Add("@Network_ErrorMargin_J32", model.Network_ErrorMargin_J32);
                p.Add("@Network_ErrorMargin_D35", model.Network_ErrorMargin_D35);

                p.Add("@Prs_Area_B7", model.Prs_Area_B7);
                p.Add("@Prs_Area_B8", model.Prs_Area_B8);
                p.Add("@Prs_Area_B9", model.Prs_Area_B9);
                p.Add("@Prs_Area_B10", model.Prs_Area_B10);
                p.Add("@Prs_ApproxNoOfConn_D7", model.Prs_ApproxNoOfConn_D7);
                p.Add("@Prs_DailyAvgPrsM_F7", model.Prs_DailyAvgPrsM_F7);
                p.Add("@Prs_ApproxNoOfConn_D8", model.Prs_ApproxNoOfConn_D8);
                p.Add("@Prs_DailyAvgPrsM_F8", model.Prs_DailyAvgPrsM_F8);
                p.Add("@Prs_ApproxNoOfConn_D9", model.Prs_ApproxNoOfConn_D9);
                p.Add("@Prs_DailyAvgPrsM_F9", model.Prs_DailyAvgPrsM_F9);
                p.Add("@Prs_ApproxNoOfConn_D10", model.Prs_ApproxNoOfConn_D10);
                p.Add("@Prs_DailyAvgPrsM_F10", model.Prs_DailyAvgPrsM_F10);
                p.Add("@Prs_ErrorMarg_F26", model.Prs_ErrorMarg_F26);

                p.Add("@Interm_Conn_D7", model.Interm_Conn_D7);
                p.Add("@Interm_Conn_D8", model.Interm_Conn_D8 );
                p.Add("@Interm_Conn_D9", model.Interm_Conn_D9 );
                p.Add("@Interm_Conn_D10", model.Interm_Conn_D10);
                p.Add("@Interm_Days_F7", model.Interm_Days_F7 );
                p.Add("@Interm_Days_F8", model.Interm_Days_F8 );
                p.Add("@Interm_Days_F9", model.Interm_Days_F9 );
                p.Add("@Interm_Days_F10", model.Interm_Days_F10);
                p.Add("@Interm_Hour_H7", model.Interm_Hour_H7 );
                p.Add("@Interm_Hour_H8", model.Interm_Hour_H8 );
                p.Add("@Interm_Hour_H9", model.Interm_Hour_H9 );
                p.Add("@Interm_Hour_H10", model.Interm_Hour_H10);
                p.Add("@Interm_ErrorMarg_H26", model.Interm_ErrorMarg_H26);

                p.Add("@FinancData_G6", model.FinancData_G6);
                p.Add("@FinancData_K6", model.FinancData_K6);
                p.Add("@FinancData_G8", model.FinancData_G8);
                p.Add("@FinancData_D26", model.FinancData_D26);
                p.Add("@FinancData_G35", model.FinancData_G35);

                // output
                p.Add("@SystemInputVolume_B19", model.WaterBalancePeriod.SystemInputVolume_B19);
                p.Add("@SystemInputVolumeErrorMargin_B21", model.WaterBalancePeriod.SystemInputVolumeErrorMargin_B21);
                p.Add("@AuthorizedConsumption_K12", model.WaterBalancePeriod.AuthorizedConsumption_K12);
                p.Add("@AuthorizedConsumptionErrorMargin_K15", model.WaterBalancePeriod.AuthorizedConsumptionErrorMargin_K15);
                p.Add("@WaterLosses_K29", model.WaterBalancePeriod.WaterLosses_K29);
                p.Add("@WaterLossesErrorMargin_K31", model.WaterBalancePeriod.WaterLossesErrorMargin_K31);
                p.Add("@BilledAuthorizedConsumption_T8", model.WaterBalancePeriod.BilledAuthorizedConsumption_T8);
                p.Add("@UnbilledAuthorizedConsumption_T16", model.WaterBalancePeriod.UnbilledAuthorizedConsumption_T16);
                p.Add("@UnbilledAuthorizedConsumptionErrorMargin_T20", model.WaterBalancePeriod.UnbilledAuthorizedConsumptionErrorMargin_T20);
                p.Add("@CommercialLosses_T26", model.WaterBalancePeriod.CommercialLosses_T26);
                p.Add("@CommercialLossesErrorMargin_T29", model.WaterBalancePeriod.CommercialLossesErrorMargin_T29);
                p.Add("@PhysicalLossesM3_T34", model.WaterBalancePeriod.PhysicalLossesM3_T34);
                p.Add("@PhyscialLossesErrorMargin_AH35", model.WaterBalancePeriod.PhyscialLossesErrorMargin_AH35);
                p.Add("@BilledMeteredConsumption_AC4", model.WaterBalancePeriod.BilledMeteredConsumption_AC4);
                p.Add("@BilledUnmeteredConsumption_AC9", model.WaterBalancePeriod.BilledUnmeteredConsumption_AC9);
                p.Add("@UnbilledMeteredConsumption_AC14", model.WaterBalancePeriod.UnbilledMeteredConsumption_AC14);
                p.Add("@UnbilledUnmeteredConsumption_AC19", model.WaterBalancePeriod.UnbilledUnmeteredConsumption_AC19);
                p.Add("@UnbilledUnmeteredConsumptionErrorMargin_AO20", model.WaterBalancePeriod.UnbilledUnmeteredConsumptionErrorMargin_AO20);
                p.Add("@UnauthorizedConsumption_AC24", model.WaterBalancePeriod.UnauthorizedConsumption_AC24);
                p.Add("@UnauthorizedConsumptionErrorMargin_AO25", model.WaterBalancePeriod.UnauthorizedConsumptionErrorMargin_AO25);
                p.Add("@CustomerMeterInaccuraciesAndErrorsM3_AC29", model.WaterBalancePeriod.CustomerMeterInaccuraciesAndErrorsM3_AC29);
                p.Add("@CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", model.WaterBalancePeriod.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30);
                p.Add("@RevenueWaterM3_AY8", model.WaterBalancePeriod.RevenueWaterM3_AY8);
                p.Add("@NonRevenueWaterM3_AY24", model.WaterBalancePeriod.NonRevenueWaterM3_AY24);
                p.Add("@NonRevenueWaterErrorMargin_AY26", model.WaterBalancePeriod.NonRevenueWaterErrorMargin_AY26);

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

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@id", id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                p.Add("@UserName", userName);

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
