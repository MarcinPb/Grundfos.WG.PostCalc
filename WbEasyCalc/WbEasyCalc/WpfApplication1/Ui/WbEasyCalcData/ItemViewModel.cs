using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DataRepository;
using GlobalRepository;
using WbEasyCalc;
using WbEasyCalcRepository;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.WbEasyCalcData
{

    public class ItemViewModel : ViewModelBase
    {
        public DataModel.WbEasyCalcData Model => new DataModel.WbEasyCalcData()
        {
            WbEasyCalcDataId = this.Id,

            CreateLogin = CreateLogin,
            CreateDate = CreateDate,
            ModifyLogin = ModifyLogin,
            ModifyDate = ModifyDate,

            YearNo = YearNo,
            MonthNo = MonthNo,
            ZoneId = ZoneId,
            Description = Description,
            IsArchive = IsArchive,
            IsAccepted = IsAccepted,

            //EasyCalcDataInput = MapEasyCalcDataInput(),
            Start_PeriodDays_M21 = Start_PeriodDays_M21,
            SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
            SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
            BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
            BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,

            BilledCons_UnbMetConsM3_D8 = BilledCons_UnbMetConsM3_D8,
            BilledCons_UnbUnmetConsM3_H8 = BilledCons_UnbUnmetConsM3_H8,

            UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,

            UnbilledCons_UnbMetConsM3_D8 = UnbilledCons_UnbMetConsM3_D8,
            UnbilledCons_UnbUnmetConsM3_H6 = UnbilledCons_UnbUnmetConsM3_H6,
            UnbilledCons_UnbUnmetConsError_J6 = UnbilledCons_UnbUnmetConsError_J6,

            UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
            UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
            UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
            UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
            UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,

            IllegalConnectionsOthersEstimatedNumber_D10 = IllegalConnectionsOthersEstimatedNumber_D10,
            IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10,

            UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
            UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
            UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
            MetErrors_DetailedManualSpec_J6 = MetErrors_DetailedManualSpec_J6,
            MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
            MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,

            MeteredBulkSupplyExportErrorMargin_N32 = MeteredBulkSupplyExportErrorMargin_N32,
            UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34,
            CorruptMeterReadingPracticessErrorMargin_N38 = CorruptMeterReadingPracticessErrorMargin_N38,
            DataHandlingErrorsOffice_L40 = DataHandlingErrorsOffice_L40,
            DataHandlingErrorsOfficeErrorMargin_N40 = DataHandlingErrorsOfficeErrorMargin_N40,

            MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
            MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
            MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
            Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
            Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
            Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
            Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
            Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
            Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7,
            //PIs_IliBestEstimate_F25 = PIs_IliBestEstimate_F25,

            // output
            SystemInputVolume_B19 = SystemInputVolume_B19,
            SystemInputVolumeErrorMargin_B21 = SystemInputVolumeErrorMargin_B21,
            AuthorizedConsumption_K12 = AuthorizedConsumption_K12,
            AuthorizedConsumptionErrorMargin_K15 = AuthorizedConsumptionErrorMargin_K15,
            WaterLosses_K29 = WaterLosses_K29,
            WaterLossesErrorMargin_K31 = WaterLossesErrorMargin_K31,
            BilledAuthorizedConsumption_T8 = BilledAuthorizedConsumption_T8,
            UnbilledAuthorizedConsumption_T16 = UnbilledAuthorizedConsumption_T16,
            UnbilledAuthorizedConsumptionErrorMargin_T20 = UnbilledAuthorizedConsumptionErrorMargin_T20,
            CommercialLosses_T26 = CommercialLosses_T26,
            CommercialLossesErrorMargin_T29 = CommercialLossesErrorMargin_T29,
            PhysicalLossesM3_T34 = PhysicalLossesM3_T34,
            PhyscialLossesErrorMargin_AH35 = PhyscialLossesErrorMargin_AH35,
            BilledMeteredConsumption_AC4 = BilledMeteredConsumption_AC4,
            BilledUnmeteredConsumption_AC9 = BilledUnmeteredConsumption_AC9,
            UnbilledMeteredConsumption_AC14 = UnbilledMeteredConsumption_AC14,
            UnbilledUnmeteredConsumption_AC19 = UnbilledUnmeteredConsumption_AC19,
            UnbilledUnmeteredConsumptionErrorMargin_AO20 = UnbilledUnmeteredConsumptionErrorMargin_AO20,
            UnauthorizedConsumption_AC24 = UnauthorizedConsumption_AC24,
            UnauthorizedConsumptionErrorMargin_AO25 = UnauthorizedConsumptionErrorMargin_AO25,
            CustomerMeterInaccuraciesAndErrorsM3_AC29 = CustomerMeterInaccuraciesAndErrorsM3_AC29,
            CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30,
            RevenueWaterM3_AY8 = RevenueWaterM3_AY8,
            NonRevenueWaterM3_AY24 = NonRevenueWaterM3_AY24,
            NonRevenueWaterErrorMargin_AY26 = NonRevenueWaterErrorMargin_AY26,

            AverageSupplyTimeHPerDayBestEstimate_F9 = AverageSupplyTimeHPerDayBestEstimate_F9,
            AveragePressureMBestEstimate_F11 = AveragePressureMBestEstimate_F11,
        };

        #region Props ViewModel: Id, ZoneId,...

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; RaisePropertyChanged(nameof(Id)); }
        }

        private string _createLogin;
        public string CreateLogin
        {
            get => _createLogin;
            set { _createLogin = value; RaisePropertyChanged(nameof(CreateLogin)); }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        { 
            get => _createDate;
            set { _createDate = value; RaisePropertyChanged(nameof(CreateDate)); }
        }

        private string _modifyLogin;
        public string ModifyLogin
        {
            get => _modifyLogin;
            set { _modifyLogin = value; RaisePropertyChanged(nameof(ModifyLogin)); }
        }

        private DateTime _modifyDate;
        public DateTime ModifyDate
        { 
            get => _modifyDate;
            set { _modifyDate = value; RaisePropertyChanged(nameof(ModifyDate)); }
        }


        private int _yearNo;
        public int YearNo
        {
            get
            {
                return _yearNo;
            }
            set
            {
                _yearNo = value;
                RaisePropertyChanged("YearNo");
                CalculateDaysNumber();
            }
        }
        public string YearName => GlobalConfig.DataRepository.YearList.FirstOrDefault(x => x.Id == YearNo)?.Name;

        private int _monthNo;
        public int MonthNo
        {
            get
            {
                return _monthNo;
            }
            set
            {
                _monthNo = value;
                RaisePropertyChanged("MonthNo");
                CalculateDaysNumber();
            }
        }

        private int _zoneId;
        public int ZoneId
        {
            get
            {
                return _zoneId;
            }
            set
            {
                _zoneId = value;
                RaisePropertyChanged("ZoneId");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private bool _isArchive;
        public bool IsArchive
        {
            get
            {
                return _isArchive;
            }
            set
            {
                _isArchive = value;
                RaisePropertyChanged();
                //if (IsArchive == false)
                //{
                //    IsAccepted = false;
                //}
            }
        }

        private bool _isAccepted;
        public bool IsAccepted
        {
            get
            {
                return _isAccepted;
            }
            set
            {
                _isAccepted = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Props input 

        private int _startPeriodDaysM21;
        private double _sysInputSystemInputVolumeM3D6;
        private double _sysInputSystemInputVolumeErrorF6;
        private double _billedConsBilledMetConsBulkWatSupExpM3D6;
        private double _billedConsBilledUnmetConsBulkWatSupExpM3H6;
        private double _unbilledConsMetConsBulkWatSupExpM3D6;
        private int _unauthConsIllegalConnDomEstNoD6;
        private double _unauthConsIllegalConnDomPersPerHouseH6;
        private double _unauthConsIllegalConnDomConsLitPerPersDayJ6;
        private double _unauthConsIllegalConnDomErrorMarginF6;
        private double _unauthConsIllegalConnOthersErrorMarginF10;
        private double _unauthConsMeterTampBypEtcEstNoD14;
        private double _unauthConsMeterTampBypEtcErrorMarginF14;
        private double _unauthConsMeterTampBypEtcConsLitPerCustDayJ14;
        private int _metErrorsDetailedManualSpecJ6;
        private double _metErrorsBilledMetConsWoBulkSupMetUndrregH8;
        private double _metErrorsBilledMetConsWoBulkSupErrorMarginN8;
        private double _metErrorsMetBulkSupExpMetUnderregH32;
        private double _metErrorsUnbillMetConsWoBulkSupplMetUndrregH34;
        private double _metErrorsCorruptMetReadPractMetUndrregH38;
        private double _networkDistributionAndTransmissionMainsD7;
        private double _networkNoOfConnOfRegCustomersH10;
        private double _networkNoOfInactAccountsWSvcConnsH18;
        private double _networkAvgLenOfSvcConnFromBoundaryToMeterMH32;
        private double _prsApproxNoOfConnD7;
        private double _prsDailyAvgPrsMF7;


        public int Start_PeriodDays_M21
        {
            get => _startPeriodDaysM21;
            set { _startPeriodDaysM21 = value; RaisePropertyChanged(nameof(Start_PeriodDays_M21)); CalculateExcel(); }
        }

        public double SysInput_SystemInputVolumeM3_D6
        {
            get => _sysInputSystemInputVolumeM3D6;
            set { _sysInputSystemInputVolumeM3D6 = value; RaisePropertyChanged(nameof(SysInput_SystemInputVolumeM3_D6)); CalculateExcel(); }
        }

        public double SysInput_SystemInputVolumeError_F6
        {
            get => _sysInputSystemInputVolumeErrorF6;
            set { _sysInputSystemInputVolumeErrorF6 = value; RaisePropertyChanged(nameof(SysInput_SystemInputVolumeError_F6)); CalculateExcel(); }
        }

        public double BilledCons_BilledMetConsBulkWatSupExpM3_D6
        {
            get => _billedConsBilledMetConsBulkWatSupExpM3D6;
            set { _billedConsBilledMetConsBulkWatSupExpM3D6 = value; RaisePropertyChanged(nameof(BilledCons_BilledMetConsBulkWatSupExpM3_D6)); CalculateExcel(); }
        }

        public double BilledCons_BilledUnmetConsBulkWatSupExpM3_H6
        {
            get => _billedConsBilledUnmetConsBulkWatSupExpM3H6;
            set { _billedConsBilledUnmetConsBulkWatSupExpM3H6 = value; RaisePropertyChanged(nameof(BilledCons_BilledUnmetConsBulkWatSupExpM3_H6)); CalculateExcel(); }
        }


        private double _billedCons_UnbMetConsM3_D8;
        public double BilledCons_UnbMetConsM3_D8
        {
            get => _billedCons_UnbMetConsM3_D8;
            set { _billedCons_UnbMetConsM3_D8 = value; RaisePropertyChanged(nameof(BilledCons_UnbMetConsM3_D8)); CalculateExcel(); }
        }
        private double _billedCons_UnbUnmetConsM3_H8;
        public double BilledCons_UnbUnmetConsM3_H8
        {
            get => _billedCons_UnbUnmetConsM3_H8;
            set { _billedCons_UnbUnmetConsM3_H8 = value; RaisePropertyChanged(nameof(BilledCons_UnbUnmetConsM3_H8)); CalculateExcel(); }
        }


        public double UnbilledCons_MetConsBulkWatSupExpM3_D6
        {
            get => _unbilledConsMetConsBulkWatSupExpM3D6;
            set { _unbilledConsMetConsBulkWatSupExpM3D6 = value; RaisePropertyChanged(nameof(UnbilledCons_MetConsBulkWatSupExpM3_D6)); CalculateExcel(); }
        }


        private double _unbilledCons_UnbMetConsM3_D8;
        public double UnbilledCons_UnbMetConsM3_D8
        {
            get => _unbilledCons_UnbMetConsM3_D8;
            set { _unbilledCons_UnbMetConsM3_D8 = value; RaisePropertyChanged(nameof(UnbilledCons_UnbMetConsM3_D8)); CalculateExcel(); }
        }
        private double _unbilledCons_UnbUnmetConsM3_H6;
        public double UnbilledCons_UnbUnmetConsM3_H6
        {
            get => _unbilledCons_UnbUnmetConsM3_H6;
            set { _unbilledCons_UnbUnmetConsM3_H6 = value; RaisePropertyChanged(nameof(UnbilledCons_UnbUnmetConsM3_H6)); CalculateExcel(); }
        }
        private double _unbilledCons_UnbUnmetConsError_J6;
        public double UnbilledCons_UnbUnmetConsError_J6
        {
            get => _unbilledCons_UnbUnmetConsError_J6;
            set { _unbilledCons_UnbUnmetConsError_J6 = value; RaisePropertyChanged(nameof(UnbilledCons_UnbUnmetConsError_J6)); CalculateExcel(); }
        }


        public int UnauthCons_IllegalConnDomEstNo_D6
        {
            get => _unauthConsIllegalConnDomEstNoD6;
            set { _unauthConsIllegalConnDomEstNoD6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomEstNo_D6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnDomPersPerHouse_H6
        {
            get => _unauthConsIllegalConnDomPersPerHouseH6;
            set { _unauthConsIllegalConnDomPersPerHouseH6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomPersPerHouse_H6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnDomConsLitPerPersDay_J6
        {
            get => _unauthConsIllegalConnDomConsLitPerPersDayJ6;
            set { _unauthConsIllegalConnDomConsLitPerPersDayJ6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomConsLitPerPersDay_J6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnDomErrorMargin_F6
        {
            get => _unauthConsIllegalConnDomErrorMarginF6;
            set { _unauthConsIllegalConnDomErrorMarginF6 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnDomErrorMargin_F6)); CalculateExcel(); }
        }

        public double UnauthCons_IllegalConnOthersErrorMargin_F10
        {
            get => _unauthConsIllegalConnOthersErrorMarginF10;
            set { _unauthConsIllegalConnOthersErrorMarginF10 = value; RaisePropertyChanged(nameof(UnauthCons_IllegalConnOthersErrorMargin_F10)); CalculateExcel(); }
        }



        private double _illegalConnectionsOthersEstimatedNumber_D10;
        public double IllegalConnectionsOthersEstimatedNumber_D10
        {
            get => _illegalConnectionsOthersEstimatedNumber_D10;
            set { _illegalConnectionsOthersEstimatedNumber_D10 = value; RaisePropertyChanged(nameof(IllegalConnectionsOthersEstimatedNumber_D10)); CalculateExcel(); }
        }

        private double _illegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10;
        public double IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10
        {
            get => _illegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10;
            set { _illegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = value; RaisePropertyChanged(nameof(IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10)); CalculateExcel(); }
        }




        public double UnauthCons_MeterTampBypEtcEstNo_D14
        {
            get => _unauthConsMeterTampBypEtcEstNoD14;
            set { _unauthConsMeterTampBypEtcEstNoD14 = value; RaisePropertyChanged(nameof(UnauthCons_MeterTampBypEtcEstNo_D14)); CalculateExcel(); }
        }

        public double UnauthCons_MeterTampBypEtcErrorMargin_F14
        {
            get => _unauthConsMeterTampBypEtcErrorMarginF14;
            set { _unauthConsMeterTampBypEtcErrorMarginF14 = value; RaisePropertyChanged(nameof(UnauthCons_MeterTampBypEtcErrorMargin_F14)); CalculateExcel(); }
        }

        public double UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14
        {
            get => _unauthConsMeterTampBypEtcConsLitPerCustDayJ14;
            set { _unauthConsMeterTampBypEtcConsLitPerCustDayJ14 = value; RaisePropertyChanged(nameof(UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14)); CalculateExcel(); }
        }

        public int MetErrors_DetailedManualSpec_J6
        {
            get => _metErrorsDetailedManualSpecJ6;
            set { _metErrorsDetailedManualSpecJ6 = value; RaisePropertyChanged(nameof(MetErrors_DetailedManualSpec_J6)); CalculateExcel(); }
        }

        public double MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8
        {
            get => _metErrorsBilledMetConsWoBulkSupMetUndrregH8;
            set { _metErrorsBilledMetConsWoBulkSupMetUndrregH8 = value; RaisePropertyChanged(nameof(MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8)); CalculateExcel(); }
        }

        public double MetErrors_BilledMetConsWoBulkSupErrorMargin_N8
        {
            get => _metErrorsBilledMetConsWoBulkSupErrorMarginN8;
            set { _metErrorsBilledMetConsWoBulkSupErrorMarginN8 = value; RaisePropertyChanged(nameof(MetErrors_BilledMetConsWoBulkSupErrorMargin_N8)); CalculateExcel(); }
        }


        private double _meteredBulkSupplyExportErrorMargin_N32;
        public double MeteredBulkSupplyExportErrorMargin_N32
        {
            get => _meteredBulkSupplyExportErrorMargin_N32;
            set { _meteredBulkSupplyExportErrorMargin_N32 = value; RaisePropertyChanged(nameof(MeteredBulkSupplyExportErrorMargin_N32)); CalculateExcel(); }
        }
        private double _unbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34;
        public double UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34
        {
            get => _unbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34;
            set { _unbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = value; RaisePropertyChanged(nameof(UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34)); CalculateExcel(); }
        }
        private double _corruptMeterReadingPracticessErrorMargin_N38;
        public double CorruptMeterReadingPracticessErrorMargin_N38
        {
            get => _corruptMeterReadingPracticessErrorMargin_N38;
            set { _corruptMeterReadingPracticessErrorMargin_N38 = value; RaisePropertyChanged(nameof(CorruptMeterReadingPracticessErrorMargin_N38)); CalculateExcel(); }
        }
        private double _dataHandlingErrorsOffice_L40;
        public double DataHandlingErrorsOffice_L40
        {
            get => _dataHandlingErrorsOffice_L40;
            set { _dataHandlingErrorsOffice_L40 = value; RaisePropertyChanged(nameof(DataHandlingErrorsOffice_L40)); CalculateExcel(); }
        }
        private double _dataHandlingErrorsOfficeErrorMargin_N40;
        public double DataHandlingErrorsOfficeErrorMargin_N40
        {
            get => _dataHandlingErrorsOfficeErrorMargin_N40;
            set { _dataHandlingErrorsOfficeErrorMargin_N40 = value; RaisePropertyChanged(nameof(DataHandlingErrorsOfficeErrorMargin_N40)); CalculateExcel(); }
        }





        public double MetErrors_MetBulkSupExpMetUnderreg_H32
        {
            get => _metErrorsMetBulkSupExpMetUnderregH32;
            set { _metErrorsMetBulkSupExpMetUnderregH32 = value; RaisePropertyChanged(nameof(MetErrors_MetBulkSupExpMetUnderreg_H32)); CalculateExcel(); }
        }

        public double MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34
        {
            get => _metErrorsUnbillMetConsWoBulkSupplMetUndrregH34;
            set { _metErrorsUnbillMetConsWoBulkSupplMetUndrregH34 = value; RaisePropertyChanged(nameof(MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34)); CalculateExcel(); }
        }

        public double MetErrors_CorruptMetReadPractMetUndrreg_H38
        {
            get => _metErrorsCorruptMetReadPractMetUndrregH38;
            set { _metErrorsCorruptMetReadPractMetUndrregH38 = value; RaisePropertyChanged(nameof(MetErrors_CorruptMetReadPractMetUndrreg_H38)); CalculateExcel(); }
        }

        public double Network_DistributionAndTransmissionMains_D7
        {
            get => _networkDistributionAndTransmissionMainsD7;
            set { _networkDistributionAndTransmissionMainsD7 = value; RaisePropertyChanged(nameof(Network_DistributionAndTransmissionMains_D7)); CalculateExcel(); }
        }

        public double Network_NoOfConnOfRegCustomers_H10
        {
            get => _networkNoOfConnOfRegCustomersH10;
            set { _networkNoOfConnOfRegCustomersH10 = value; RaisePropertyChanged(nameof(Network_NoOfConnOfRegCustomers_H10)); CalculateExcel(); }
        }

        public double Network_NoOfInactAccountsWSvcConns_H18
        {
            get => _networkNoOfInactAccountsWSvcConnsH18;
            set { _networkNoOfInactAccountsWSvcConnsH18 = value; RaisePropertyChanged(nameof(Network_NoOfInactAccountsWSvcConns_H18)); CalculateExcel(); }
        }

        public double Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32
        {
            get => _networkAvgLenOfSvcConnFromBoundaryToMeterMH32;
            set { _networkAvgLenOfSvcConnFromBoundaryToMeterMH32 = value; RaisePropertyChanged(nameof(Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32)); CalculateExcel(); }
        }

        public double Prs_ApproxNoOfConn_D7
        {
            get => _prsApproxNoOfConnD7;
            set { _prsApproxNoOfConnD7 = value; RaisePropertyChanged(nameof(Prs_ApproxNoOfConn_D7)); CalculateExcel(); }
        }

        public double Prs_DailyAvgPrsM_F7
        {
            get => _prsDailyAvgPrsMF7;
            set { _prsDailyAvgPrsMF7 = value; RaisePropertyChanged(nameof(Prs_DailyAvgPrsM_F7)); CalculateExcel(); }
        }

        #endregion Props input

        #region Props output

        private double _systemInputVolume_B19;
        private double _systemInputVolumeErrorMargin_B21;
        private double _authorizedConsumption_K12;
        private double _authorizedConsumptionErrorMargin_K15;
        private double _waterLosses_K29;
        private double _waterLossesErrorMargin_K31;
        private double _billedAuthorizedConsumption_T8;
        private double _unbilledAuthorizedConsumption_T16;
        private double _unbilledAuthorizedConsumptionErrorMargin_T20;
        private double _commercialLosses_T26;
        private double _commercialLossesErrorMargin_T29;
        private double _physicalLossesM3T34;
        private double _physcialLossesErrorMarginAh35;
        private double _billedMeteredConsumptionAc4;
        private double _billedUnmeteredConsumptionAc9;
        private double _unbilledMeteredConsumptionAc14;
        private double _unbilledUnmeteredConsumptionAc19;
        private double _unbilledUnmeteredConsumptionErrorMarginAo20;
        private double _unauthorizedConsumptionAc24;
        private double _unauthorizedConsumptionErrorMarginAo25;
        private double _customerMeterInaccuraciesAndErrorsM3Ac29;
        private double _customerMeterInaccuraciesAndErrorsErrorMarginAo30;
        private double _revenueWaterM3Ay8;
        private double _nonRevenueWaterM3Ay24;
        private double _nonRevenueWaterErrorMarginAy26;
        private double _averageSupplyTimeHPerDayBestEstimate_F9;
        private double _averagePressureMBestEstimate_F11;

        public double SystemInputVolume_B19
        {
            get { return _systemInputVolume_B19; }
            set { _systemInputVolume_B19 = value; RaisePropertyChanged(nameof(SystemInputVolume_B19)); }
        }

        public double SystemInputVolumeErrorMargin_B21
        {
            get { return _systemInputVolumeErrorMargin_B21; }
            set { _systemInputVolumeErrorMargin_B21 = value; RaisePropertyChanged(nameof(SystemInputVolumeErrorMargin_B21)); }
        }

        public double AuthorizedConsumption_K12
        {
            get => _authorizedConsumption_K12;
            set { _authorizedConsumption_K12 = value; RaisePropertyChanged(nameof(AuthorizedConsumption_K12)); }
        }
        public double AuthorizedConsumptionErrorMargin_K15
        {
            get => _authorizedConsumptionErrorMargin_K15;
            set { _authorizedConsumptionErrorMargin_K15 = value; RaisePropertyChanged(nameof(AuthorizedConsumptionErrorMargin_K15)); }
        }

        public double WaterLosses_K29
        {
            get => _waterLosses_K29;
            set { _waterLosses_K29 = value; RaisePropertyChanged(nameof(WaterLosses_K29)); }
        }

        public double WaterLossesErrorMargin_K31
        {
            get => _waterLossesErrorMargin_K31;
            set { _waterLossesErrorMargin_K31 = value; RaisePropertyChanged(nameof(WaterLossesErrorMargin_K31)); }
        }

        public double BilledAuthorizedConsumption_T8
        {
            get => _billedAuthorizedConsumption_T8;
            set { _billedAuthorizedConsumption_T8 = value; RaisePropertyChanged(nameof(BilledAuthorizedConsumption_T8)); }
        }

        public double UnbilledAuthorizedConsumption_T16
        {
            get => _unbilledAuthorizedConsumption_T16;
            set { _unbilledAuthorizedConsumption_T16 = value; RaisePropertyChanged(nameof(UnbilledAuthorizedConsumption_T16)); }
        }

        public double UnbilledAuthorizedConsumptionErrorMargin_T20
        {
            get => _unbilledAuthorizedConsumptionErrorMargin_T20;
            set { _unbilledAuthorizedConsumptionErrorMargin_T20 = value; RaisePropertyChanged(nameof(UnbilledAuthorizedConsumptionErrorMargin_T20)); }
        }

        public double CommercialLosses_T26
        {
            get => _commercialLosses_T26;
            set { _commercialLosses_T26 = value; RaisePropertyChanged(nameof(CommercialLosses_T26)); }
        }

        public double CommercialLossesErrorMargin_T29
        {
            get => _commercialLossesErrorMargin_T29;
            set { _commercialLossesErrorMargin_T29 = value; RaisePropertyChanged(nameof(CommercialLossesErrorMargin_T29)); }
        }

        public double PhysicalLossesM3_T34
        {
            get => _physicalLossesM3T34;
            set { _physicalLossesM3T34 = value; RaisePropertyChanged(nameof(PhysicalLossesM3_T34)); }
        }

        public double PhyscialLossesErrorMargin_AH35
        {
            get => _physcialLossesErrorMarginAh35;
            set { _physcialLossesErrorMarginAh35 = value; RaisePropertyChanged(nameof(PhyscialLossesErrorMargin_AH35)); }
        }

        public double BilledMeteredConsumption_AC4
        {
            get => _billedMeteredConsumptionAc4;
            set { _billedMeteredConsumptionAc4 = value; RaisePropertyChanged(nameof(BilledMeteredConsumption_AC4)); }
        }

        public double BilledUnmeteredConsumption_AC9
        {
            get => _billedUnmeteredConsumptionAc9;
            set { _billedUnmeteredConsumptionAc9 = value; RaisePropertyChanged(nameof(BilledUnmeteredConsumption_AC9)); }
        }

        public double UnbilledMeteredConsumption_AC14
        {
            get => _unbilledMeteredConsumptionAc14;
            set { _unbilledMeteredConsumptionAc14 = value; RaisePropertyChanged(nameof(UnbilledMeteredConsumption_AC14)); }
        }

        public double UnbilledUnmeteredConsumption_AC19
        {
            get => _unbilledUnmeteredConsumptionAc19;
            set { _unbilledUnmeteredConsumptionAc19 = value; RaisePropertyChanged(nameof(UnbilledUnmeteredConsumption_AC19)); }
        }

        public double UnbilledUnmeteredConsumptionErrorMargin_AO20
        {
            get => _unbilledUnmeteredConsumptionErrorMarginAo20;
            set { _unbilledUnmeteredConsumptionErrorMarginAo20 = value; RaisePropertyChanged(nameof(UnbilledUnmeteredConsumptionErrorMargin_AO20)); }
        }

        public double UnauthorizedConsumption_AC24
        {
            get => _unauthorizedConsumptionAc24;
            set { _unauthorizedConsumptionAc24 = value; RaisePropertyChanged(nameof(UnauthorizedConsumption_AC24)); }
        }

        public double UnauthorizedConsumptionErrorMargin_AO25
        {
            get => _unauthorizedConsumptionErrorMarginAo25;
            set { _unauthorizedConsumptionErrorMarginAo25 = value; RaisePropertyChanged(nameof(UnauthorizedConsumptionErrorMargin_AO25)); }
        }

        public double CustomerMeterInaccuraciesAndErrorsM3_AC29
        {
            get => _customerMeterInaccuraciesAndErrorsM3Ac29;
            set { _customerMeterInaccuraciesAndErrorsM3Ac29 = value; RaisePropertyChanged(nameof(CustomerMeterInaccuraciesAndErrorsM3_AC29)); }
        }

        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30
        {
            get => _customerMeterInaccuraciesAndErrorsErrorMarginAo30;
            set { _customerMeterInaccuraciesAndErrorsErrorMarginAo30 = value; RaisePropertyChanged(nameof(CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30)); }
        }

        public double RevenueWaterM3_AY8
        {
            get => _revenueWaterM3Ay8;
            set { _revenueWaterM3Ay8 = value; RaisePropertyChanged(nameof(RevenueWaterM3_AY8)); }
        }

        public double NonRevenueWaterM3_AY24
        {
            get => _nonRevenueWaterM3Ay24;
            set { _nonRevenueWaterM3Ay24 = value; RaisePropertyChanged(nameof(NonRevenueWaterM3_AY24)); }
        }

        public double NonRevenueWaterErrorMargin_AY26
        {
            get => _nonRevenueWaterErrorMarginAy26;
            set { _nonRevenueWaterErrorMarginAy26 = value; RaisePropertyChanged(nameof(NonRevenueWaterErrorMargin_AY26)); }
        }

        public double AverageSupplyTimeHPerDayBestEstimate_F9 
        { 
            get => _averageSupplyTimeHPerDayBestEstimate_F9;
            set { _averageSupplyTimeHPerDayBestEstimate_F9 = value; RaisePropertyChanged(nameof(AverageSupplyTimeHPerDayBestEstimate_F9)); }
        }
        public double AveragePressureMBestEstimate_F11 
        { 
            get => _averagePressureMBestEstimate_F11;
            set { _averagePressureMBestEstimate_F11 = value; RaisePropertyChanged(nameof(AveragePressureMBestEstimate_F11)); }
        }



        #endregion

        public ItemViewModel(DataModel.WbEasyCalcData model)
        {
            Id = model.WbEasyCalcDataId;

            if (model.WbEasyCalcDataId != 0)
            {



                ZoneId = model.ZoneId;
                YearNo = model.YearNo;
                MonthNo = model.MonthNo;
                Start_PeriodDays_M21 = model.Start_PeriodDays_M21;
            }
            else
            {
                ZoneId = GlobalConfig.DataRepository.ZoneList.First().ZoneId;
                YearNo = DateTime.Now.Year;
                MonthNo = DateTime.Now.Month;
                //Start_PeriodDays_M21 = model.Start_PeriodDays_M21;
                CalculateDaysNumber();
            }

            CreateLogin = model.CreateLogin;
            CreateDate = model.CreateDate;
            ModifyLogin = model.ModifyLogin;
            ModifyDate = model.ModifyDate;

            Description = model.Description;
            IsArchive = model.IsArchive;
            IsAccepted = model.IsAccepted;


            SysInput_SystemInputVolumeM3_D6 = model.SysInput_SystemInputVolumeM3_D6;
            SysInput_SystemInputVolumeError_F6 = model.SysInput_SystemInputVolumeError_F6;
            BilledCons_BilledMetConsBulkWatSupExpM3_D6 = model.BilledCons_BilledMetConsBulkWatSupExpM3_D6;
            BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = model.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6;

            BilledCons_UnbMetConsM3_D8 = model.BilledCons_UnbMetConsM3_D8;
            BilledCons_UnbUnmetConsM3_H8 = model.BilledCons_UnbUnmetConsM3_H8;

            UnbilledCons_MetConsBulkWatSupExpM3_D6 = model.UnbilledCons_MetConsBulkWatSupExpM3_D6;

            UnbilledCons_UnbMetConsM3_D8 = model.UnbilledCons_UnbMetConsM3_D8;
            UnbilledCons_UnbUnmetConsM3_H6 = model.UnbilledCons_UnbUnmetConsM3_H6;
            UnbilledCons_UnbUnmetConsError_J6 = model.UnbilledCons_UnbUnmetConsError_J6;

            UnauthCons_IllegalConnDomEstNo_D6 = model.UnauthCons_IllegalConnDomEstNo_D6;
            UnauthCons_IllegalConnDomPersPerHouse_H6 = model.UnauthCons_IllegalConnDomPersPerHouse_H6;
            UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = model.UnauthCons_IllegalConnDomConsLitPerPersDay_J6;
            UnauthCons_IllegalConnDomErrorMargin_F6 = model.UnauthCons_IllegalConnDomErrorMargin_F6;
            UnauthCons_IllegalConnOthersErrorMargin_F10 = model.UnauthCons_IllegalConnOthersErrorMargin_F10;

            IllegalConnectionsOthersEstimatedNumber_D10 = model.IllegalConnectionsOthersEstimatedNumber_D10;
            IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = model.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10;

            UnauthCons_MeterTampBypEtcEstNo_D14 = model.UnauthCons_MeterTampBypEtcEstNo_D14;
            UnauthCons_MeterTampBypEtcErrorMargin_F14 = model.UnauthCons_MeterTampBypEtcErrorMargin_F14;
            UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = model.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14;
            MetErrors_DetailedManualSpec_J6 = model.MetErrors_DetailedManualSpec_J6;
            MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = model.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8;
            MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = model.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8;

            MeteredBulkSupplyExportErrorMargin_N32 = model.MeteredBulkSupplyExportErrorMargin_N32;
            UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = model.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34;
            CorruptMeterReadingPracticessErrorMargin_N38 = model.CorruptMeterReadingPracticessErrorMargin_N38;
            DataHandlingErrorsOffice_L40 = model.DataHandlingErrorsOffice_L40;
            DataHandlingErrorsOfficeErrorMargin_N40 = model.DataHandlingErrorsOfficeErrorMargin_N40;

            MetErrors_MetBulkSupExpMetUnderreg_H32 = model.MetErrors_MetBulkSupExpMetUnderreg_H32;
            MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = model.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34;
            MetErrors_CorruptMetReadPractMetUndrreg_H38 = model.MetErrors_CorruptMetReadPractMetUndrreg_H38;
            Network_DistributionAndTransmissionMains_D7 = model.Network_DistributionAndTransmissionMains_D7;
            Network_NoOfConnOfRegCustomers_H10 = model.Network_NoOfConnOfRegCustomers_H10;
            Network_NoOfInactAccountsWSvcConns_H18 = model.Network_NoOfInactAccountsWSvcConns_H18;
            Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = model.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32;
            Prs_ApproxNoOfConn_D7 = model.Prs_ApproxNoOfConn_D7;
            Prs_DailyAvgPrsM_F7 = model.Prs_DailyAvgPrsM_F7;
            //PIs_IliBestEstimate_F25 = model.PIs_IliBestEstimate_F25;

            // output
            SystemInputVolume_B19 = model.SystemInputVolume_B19;
            SystemInputVolumeErrorMargin_B21 = model.SystemInputVolumeErrorMargin_B21;
            AuthorizedConsumption_K12 = model.AuthorizedConsumption_K12;
            AuthorizedConsumptionErrorMargin_K15 = model.AuthorizedConsumptionErrorMargin_K15;
            WaterLosses_K29 = model.WaterLosses_K29;
            WaterLossesErrorMargin_K31 = model.WaterLossesErrorMargin_K31;
            BilledAuthorizedConsumption_T8 = model.BilledAuthorizedConsumption_T8;
            UnbilledAuthorizedConsumption_T16 = model.UnbilledAuthorizedConsumption_T16;
            UnbilledAuthorizedConsumptionErrorMargin_T20 = model.UnbilledAuthorizedConsumptionErrorMargin_T20;
            CommercialLosses_T26 = model.CommercialLosses_T26;
            CommercialLossesErrorMargin_T29 = model.CommercialLossesErrorMargin_T29;
            PhysicalLossesM3_T34 = model.PhysicalLossesM3_T34;
            PhyscialLossesErrorMargin_AH35 = model.PhyscialLossesErrorMargin_AH35;
            BilledMeteredConsumption_AC4 = model.BilledMeteredConsumption_AC4;
            BilledUnmeteredConsumption_AC9 = model.BilledUnmeteredConsumption_AC9;
            UnbilledMeteredConsumption_AC14 = model.UnbilledMeteredConsumption_AC14;
            UnbilledUnmeteredConsumption_AC19 = model.UnbilledUnmeteredConsumption_AC19;
            UnbilledUnmeteredConsumptionErrorMargin_AO20 = model.UnbilledUnmeteredConsumptionErrorMargin_AO20;
            UnauthorizedConsumption_AC24 = model.UnauthorizedConsumption_AC24;
            UnauthorizedConsumptionErrorMargin_AO25 = model.UnauthorizedConsumptionErrorMargin_AO25;
            CustomerMeterInaccuraciesAndErrorsM3_AC29 = model.CustomerMeterInaccuraciesAndErrorsM3_AC29;
            CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = model.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30;
            RevenueWaterM3_AY8 = model.RevenueWaterM3_AY8;
            NonRevenueWaterM3_AY24 = model.NonRevenueWaterM3_AY24;
            NonRevenueWaterErrorMargin_AY26 = model.NonRevenueWaterErrorMargin_AY26;

            AverageSupplyTimeHPerDayBestEstimate_F9 = model.AverageSupplyTimeHPerDayBestEstimate_F9;
            AveragePressureMBestEstimate_F11 = model.AveragePressureMBestEstimate_F11;
        }

        public void CalculateExcel()
        {
            try
            {
                EasyCalcDataInput easyCalcDataInput = Model.EasyCalcDataInput;
                IWbEasyCalc wbEasyCalc = new WbEasyCalcRepository.WbEasyCalc();
                EasyCalcDataOutput easyCalcDataOutput = wbEasyCalc.Calculate(easyCalcDataInput);
                MapEasyCalcDataOutput(easyCalcDataOutput);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculateDaysNumber()
        {
            if (MonthNo == 0 || YearNo == 0)
            {
                return;
            }
            Start_PeriodDays_M21 = MonthNo == 13 ? new DateTime(YearNo, 12, 31).DayOfYear : DateTime.DaysInMonth(YearNo, MonthNo);
        }

        private void MapEasyCalcDataOutput(EasyCalcDataOutput easyCalcDataOutput)
        {
            SystemInputVolume_B19 = easyCalcDataOutput.SystemInputVolume_B19;
            SystemInputVolumeErrorMargin_B21 = easyCalcDataOutput.SystemInputVolumeErrorMargin_B21;

            AuthorizedConsumption_K12 = easyCalcDataOutput.AuthorizedConsumption_K12;
            AuthorizedConsumptionErrorMargin_K15 = easyCalcDataOutput.AuthorizedConsumptionErrorMargin_K15;
            WaterLosses_K29 = easyCalcDataOutput.WaterLosses_K29;
            WaterLossesErrorMargin_K31 = easyCalcDataOutput.WaterLossesErrorMargin_K31;

            BilledAuthorizedConsumption_T8 = easyCalcDataOutput.BilledAuthorizedConsumption_T8;
            UnbilledAuthorizedConsumption_T16 = easyCalcDataOutput.UnbilledAuthorizedConsumption_T16;
            UnbilledAuthorizedConsumptionErrorMargin_T20 = easyCalcDataOutput.UnbilledAuthorizedConsumptionErrorMargin_T20;
            CommercialLosses_T26 = easyCalcDataOutput.CommercialLosses_T26;
            CommercialLossesErrorMargin_T29 = easyCalcDataOutput.CommercialLossesErrorMargin_T29;
            PhysicalLossesM3_T34 = easyCalcDataOutput.PhysicalLossesM3_T34;
            PhyscialLossesErrorMargin_AH35 = easyCalcDataOutput.PhyscialLossesErrorMargin_AH35;

            BilledMeteredConsumption_AC4 = easyCalcDataOutput.BilledMeteredConsumption_AC4;
            BilledUnmeteredConsumption_AC9 = easyCalcDataOutput.BilledUnmeteredConsumption_AC9;
            UnbilledMeteredConsumption_AC14 = easyCalcDataOutput.UnbilledMeteredConsumption_AC14;

            UnbilledUnmeteredConsumption_AC19 = easyCalcDataOutput.UnbilledUnmeteredConsumption_AC19;
            UnbilledUnmeteredConsumptionErrorMargin_AO20 = easyCalcDataOutput.UnbilledUnmeteredConsumptionErrorMargin_AO20;
            UnauthorizedConsumption_AC24 = easyCalcDataOutput.UnauthorizedConsumption_AC24;
            UnauthorizedConsumptionErrorMargin_AO25 = easyCalcDataOutput.UnauthorizedConsumptionErrorMargin_AO25;
            CustomerMeterInaccuraciesAndErrorsM3_AC29 = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsM3_AC29;
            CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30;

            RevenueWaterM3_AY8 = easyCalcDataOutput.RevenueWaterM3_AY8;
            NonRevenueWaterM3_AY24 = easyCalcDataOutput.NonRevenueWaterM3_AY24;
            NonRevenueWaterErrorMargin_AY26 = easyCalcDataOutput.NonRevenueWaterErrorMargin_AY26;

            AverageSupplyTimeHPerDayBestEstimate_F9 = easyCalcDataOutput.AverageSupplyTimeHPerDayBestEstimate_F9;
            AveragePressureMBestEstimate_F11 = easyCalcDataOutput.AveragePressureMBestEstimate_F11;
        }


        #region Waste


        //public EasyCalcDataInput MapEasyCalcDataInput()
        //{
        //    EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
        //    {
        //        Start_PeriodDays_M21 = Start_PeriodDays_M21,
        //        SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
        //        SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
        //        BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
        //        BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
        //        UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,
        //        UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
        //        UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
        //        UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
        //        UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
        //        UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,
        //        UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
        //        UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
        //        UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
        //        MetErrors_DetailedManualSpec_J6 = Math.Abs(MetErrors_DetailedManualSpec_J6 - 2) < 0.1,
        //        MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
        //        MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,
        //        MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
        //        MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
        //        MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
        //        Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
        //        Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
        //        Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
        //        Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
        //        Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
        //        Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7
        //    };
        //    return easyCalcDataInput;
        //}


        //private EasyCalcDataOutput MapEasyCalcDataOutput()
        //{
        //    EasyCalcDataOutput easyCalcDataOutput = new EasyCalcDataOutput()
        //    {
        //        SystemInputVolume_B19 = SystemInputVolume_B19,
        //        SystemInputVolumeErrorMargin_B21 = SystemInputVolumeErrorMargin_B21,

        //        AuthorizedConsumption_K12 = AuthorizedConsumption_K12,
        //        AuthorizedConsumptionErrorMargin_K15 = AuthorizedConsumptionErrorMargin_K15,
        //        WaterLosses_K29 = WaterLosses_K29,
        //        WaterLossesErrorMargin_K31 = WaterLossesErrorMargin_K31,

        //        BilledAuthorizedConsumption_T8 = BilledAuthorizedConsumption_T8,
        //        UnbilledAuthorizedConsumption_T16 = UnbilledAuthorizedConsumption_T16,
        //        UnbilledAuthorizedConsumptionErrorMargin_T20 =
        //        UnbilledAuthorizedConsumptionErrorMargin_T20,
        //        CommercialLosses_T26 = CommercialLosses_T26,
        //        CommercialLossesErrorMargin_T29 = CommercialLossesErrorMargin_T29,
        //        PhysicalLossesM3_T34 = PhysicalLossesM3_T34,
        //        PhyscialLossesErrorMargin_AH35 = PhyscialLossesErrorMargin_AH35,

        //        BilledMeteredConsumption_AC4 = BilledMeteredConsumption_AC4,
        //        BilledUnmeteredConsumption_AC9 = BilledUnmeteredConsumption_AC9,
        //        UnbilledMeteredConsumption_AC14 = UnbilledMeteredConsumption_AC14,

        //        UnbilledUnmeteredConsumption_AC19 = UnbilledUnmeteredConsumption_AC19,
        //        UnbilledUnmeteredConsumptionErrorMargin_AO20 =
        //        UnbilledUnmeteredConsumptionErrorMargin_AO20,
        //        UnauthorizedConsumption_AC24 = UnauthorizedConsumption_AC24,
        //        UnauthorizedConsumptionErrorMargin_AO25 = UnauthorizedConsumptionErrorMargin_AO25,
        //        CustomerMeterInaccuraciesAndErrorsM3_AC29 =
        //        CustomerMeterInaccuraciesAndErrorsM3_AC29,
        //        CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 =
        //        CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30,

        //        RevenueWaterM3_AY8 = RevenueWaterM3_AY8,
        //        NonRevenueWaterM3_AY24 = NonRevenueWaterM3_AY24,
        //        NonRevenueWaterErrorMargin_AY26 = NonRevenueWaterErrorMargin_AY26,
        //    };
        //    return easyCalcDataOutput;
        //}
        #endregion
    }
}
