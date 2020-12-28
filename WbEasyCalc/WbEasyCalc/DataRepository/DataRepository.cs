using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModel;
using DataRepository.WbEasyCalcData;

namespace DataRepository
{
    public class DataRepository : IDataRepository
    {
        private readonly string _cnnString;

        public IWbEasyCalcDataListRepository WbEasyCalcDataListRepository { get; private set; }


        public DataRepository(string cnnString)
        {
            _cnnString = cnnString;
            WbEasyCalcDataListRepository = new WbEasyCalcDataListRepository(_cnnString);
        }

        private List<ZoneItem> _zoneList;
        public List<ZoneItem> ZoneList
        {
            get
            {
                if (_zoneList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(_cnnString))
                    {
                        _zoneList = cnn.Query<ZoneItem>("dbo.spZoneList", commandType: CommandType.StoredProcedure).ToList();
                        return _zoneList;
                    }
                }
                return _zoneList;
            }
            //set => throw new System.NotImplementedException();
        }

        private List<IdNamePair> _yearList;
        public List<IdNamePair> YearList
        {
            get
            {
                if (_yearList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(_cnnString))
                    {
                        _yearList = cnn.Query<IdNamePair>("dbo.spYearList", commandType: CommandType.StoredProcedure).ToList();
                        return _yearList;
                    }
                }
                return _yearList;
            }
            set => throw new System.NotImplementedException();
        }

        private List<IdNamePair> _montList;
        public List<IdNamePair> MonthList
        {
            get
            {
                if (_montList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(_cnnString))
                    {
                        _montList = cnn.Query<IdNamePair>("dbo.spMonthList", commandType: CommandType.StoredProcedure).ToList();
                        return _montList;
                    }
                }
                return _montList;
            }
            set => throw new System.NotImplementedException();
        }

        public DataModel.WbEasyCalcData GetAutomaticData(int yearNo, int monthNo, int zoneId)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();

                p.Add("@YearNo", yearNo);
                p.Add("@MonthNo", monthNo);
                p.Add("@ZoneId", zoneId);

                // input
                p.Add("@Start_PeriodDays_M21", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@SysInput_SystemInputVolumeM3_D6", dbType: DbType.Double, direction: ParameterDirection.Output);                     // @SystemInputVolume
                p.Add("@SysInput_SystemInputVolumeError_F6", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@BilledCons_BilledMetConsBulkWatSupExpM3_D6", dbType: DbType.Double, direction: ParameterDirection.Output);          // @ZoneSale
                p.Add("@BilledCons_BilledUnmetConsBulkWatSupExpM3_H6", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnbilledCons_MetConsBulkWatSupExpM3_D6", dbType: DbType.Double, direction: ParameterDirection.Output);

                p.Add("@UnbilledCons_UnbUnmetConsM3_H6", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnbilledCons_UnbUnmetConsError_J6", dbType: DbType.Double, direction: ParameterDirection.Output);

                p.Add("@UnauthCons_IllegalConnDomEstNo_D6", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_IllegalConnDomPersPerHouse_H6", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_IllegalConnDomConsLitPerPersDay_J6", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_IllegalConnDomErrorMargin_F6", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_IllegalConnOthersErrorMargin_F10", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_MeterTampBypEtcEstNo_D14", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_MeterTampBypEtcErrorMargin_F14", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@MetErrors_DetailedManualSpec_J6", dbType: DbType.Double, direction: ParameterDirection.Output);                      //
                p.Add("@MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@MetErrors_BilledMetConsWoBulkSupErrorMargin_N8", dbType: DbType.Double, direction: ParameterDirection.Output);

                p.Add("@MeteredBulkSupplyExportErrorMargin_N32", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@CorruptMeterReadingPracticessErrorMargin_N38", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@DataHandlingErrorsOffice_L40", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@DataHandlingErrorsOfficeErrorMargin_N40", dbType: DbType.Double, direction: ParameterDirection.Output);

                p.Add("@MetErrors_MetBulkSupExpMetUnderreg_H32", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@MetErrors_CorruptMetReadPractMetUndrreg_H38", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@Network_DistributionAndTransmissionMains_D7", dbType: DbType.Double, direction: ParameterDirection.Output);         // @NetworkLength
                p.Add("@Network_NoOfConnOfRegCustomers_H10", dbType: DbType.Double, direction: ParameterDirection.Output);                  // @CustomersQuantity
                p.Add("@Network_NoOfInactAccountsWSvcConns_H18", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@Prs_ApproxNoOfConn_D7", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@Prs_DailyAvgPrsM_F7", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@PIs_IliBestEstimate_F25", dbType: DbType.Double, direction: ParameterDirection.Output);

                connection.Execute("dbo.spGisModelScadaData", p, commandType: CommandType.StoredProcedure);

                return new DataModel.WbEasyCalcData()
                {
                    Start_PeriodDays_M21 = p.Get<int>("@Start_PeriodDays_M21"),                                                             // //
                    SysInput_SystemInputVolumeM3_D6 = p.Get<double>("@SysInput_SystemInputVolumeM3_D6"),                                    // @SystemInputVolume
                    SysInput_SystemInputVolumeError_F6 = p.Get<double>("@SysInput_SystemInputVolumeError_F6"),                              // 
                    BilledCons_BilledMetConsBulkWatSupExpM3_D6 = p.Get<double>("@BilledCons_BilledMetConsBulkWatSupExpM3_D6"),              // @ZoneSale
                    BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = p.Get<double>("@BilledCons_BilledUnmetConsBulkWatSupExpM3_H6"),          // 
                    UnbilledCons_MetConsBulkWatSupExpM3_D6 = p.Get<double>("@UnbilledCons_MetConsBulkWatSupExpM3_D6"),                      // 

                    UnbilledCons_UnbMetConsM3_D8 = p.Get<double>("@UnbilledCons_UnbMetConsM3_D8"),
                    UnbilledCons_UnbUnmetConsM3_H6 = p.Get<double>("@UnbilledCons_UnbUnmetConsM3_H6"),
                    UnbilledCons_UnbUnmetConsError_J6 = p.Get<double>("@UnbilledCons_UnbUnmetConsError_J6"),

                    UnauthCons_IllegalConnDomEstNo_D6 = p.Get<int>("@UnauthCons_IllegalConnDomEstNo_D6"),                                   // //
                    UnauthCons_IllegalConnDomPersPerHouse_H6 = p.Get<double>("@UnauthCons_IllegalConnDomPersPerHouse_H6"),                  // 
                    UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = p.Get<double>("@UnauthCons_IllegalConnDomConsLitPerPersDay_J6"),        // 
                    UnauthCons_IllegalConnDomErrorMargin_F6 = p.Get<double>("@UnauthCons_IllegalConnDomErrorMargin_F6"),                    // 
                    UnauthCons_IllegalConnOthersErrorMargin_F10 = p.Get<double>("@UnauthCons_IllegalConnOthersErrorMargin_F10"),            // 
                    UnauthCons_MeterTampBypEtcEstNo_D14 = p.Get<double>("@UnauthCons_MeterTampBypEtcEstNo_D14"),                            // 
                    UnauthCons_MeterTampBypEtcErrorMargin_F14 = p.Get<double>("@UnauthCons_MeterTampBypEtcErrorMargin_F14"),                // 
                    UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = p.Get<double>("@UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14"),    // 
                    MetErrors_DetailedManualSpec_J6 = p.Get<int>("@MetErrors_DetailedManualSpec_J6"),                                    // //
                    MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = p.Get<double>("@MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8"),        // 
                    MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = p.Get<double>("@MetErrors_BilledMetConsWoBulkSupErrorMargin_N8"),      // 

                    MeteredBulkSupplyExportErrorMargin_N32 = p.Get<double>("@MeteredBulkSupplyExportErrorMargin_N32"),                                              // 
                    UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = p.Get<double>("@UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34"),      // 
                    CorruptMeterReadingPracticessErrorMargin_N38 = p.Get<double>("@CorruptMeterReadingPracticessErrorMargin_N38"),                                  // 
                    DataHandlingErrorsOffice_L40 = p.Get<double>("@DataHandlingErrorsOffice_L40"),                                                                  // 
                    DataHandlingErrorsOfficeErrorMargin_N40 = p.Get<double>("@DataHandlingErrorsOfficeErrorMargin_N40"),                                            // 

                    MetErrors_MetBulkSupExpMetUnderreg_H32 = p.Get<double>("@MetErrors_MetBulkSupExpMetUnderreg_H32"),                      // 
                    MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = p.Get<double>("@MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34"),  // 
                    MetErrors_CorruptMetReadPractMetUndrreg_H38 = p.Get<double>("@MetErrors_CorruptMetReadPractMetUndrreg_H38"),            // 
                    Network_DistributionAndTransmissionMains_D7 = p.Get<double>("@Network_DistributionAndTransmissionMains_D7"),            // @NetworkLength
                    Network_NoOfConnOfRegCustomers_H10 = p.Get<double>("@Network_NoOfConnOfRegCustomers_H10"),                              // @CustomersQuantity 
                    Network_NoOfInactAccountsWSvcConns_H18 = p.Get<double>("@Network_NoOfInactAccountsWSvcConns_H18"),                      // 
                    Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = p.Get<double>("@Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32"),    // 
                    Prs_ApproxNoOfConn_D7 = p.Get<double>("@Prs_ApproxNoOfConn_D7"),                                                        // 
                    Prs_DailyAvgPrsM_F7 = p.Get<double>("@Prs_DailyAvgPrsM_F7"),                                                            // 
                    PIs_IliBestEstimate_F25 = p.Get<double>("@PIs_IliBestEstimate_F25"),                                                    // 
                };
            }
        }

    }
}
