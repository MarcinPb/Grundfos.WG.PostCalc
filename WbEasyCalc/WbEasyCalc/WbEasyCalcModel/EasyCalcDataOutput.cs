using System;
using WbEasyCalcModel.WbEasyCalc;

namespace WbEasyCalcModel
{
    public class EasyCalcDataOutput : ICloneable
    {
        public double SystemInputVolume_B19{ get; set; }
        public double SystemInputVolumeErrorMargin_B21{ get; set; } 

        public double AuthorizedConsumption_K12{ get; set; } 
        public double AuthorizedConsumptionErrorMargin_K15{ get; set; }
        public double WaterLosses_K29{ get; set; } 
        public double WaterLossesErrorMargin_K31{ get; set; }

        public double BilledAuthorizedConsumption_T8{ get; set; } 
        public double UnbilledAuthorizedConsumption_T16{ get; set; } 
        public double UnbilledAuthorizedConsumptionErrorMargin_T20{ get; set; }
        public double CommercialLosses_T26{ get; set; } 
        public double CommercialLossesErrorMargin_T29{ get; set; } 
        public double PhysicalLossesM3_T34{ get; set; } 
        public double PhyscialLossesErrorMargin_AH35{ get; set; } 

        public double BilledMeteredConsumption_AC4{ get; set; }
        public double BilledUnmeteredConsumption_AC9{ get; set; }
        public double UnbilledMeteredConsumption_AC14{ get; set; }

        public double UnbilledUnmeteredConsumption_AC19{ get; set; }
        public double UnbilledUnmeteredConsumptionErrorMargin_AO20{ get; set; }
        public double UnauthorizedConsumption_AC24{ get; set; }
        public double UnauthorizedConsumptionErrorMargin_AO25{ get; set; }
        public double CustomerMeterInaccuraciesAndErrorsM3_AC29{ get; set; }
        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30{ get; set; }

        public double RevenueWaterM3_AY8{ get; set; }
        public double NonRevenueWaterM3_AY24{ get; set; }
        public double NonRevenueWaterErrorMargin_AY26{ get; set; } // 6593339 


        public double WbDay_SystemInputVolume_B19 { get; set; } // 6593339 


        public WaterBalanceModel WaterBalanceDay { get; set; }  
        public WaterBalanceModel WaterBalancePeriod { get; set; } 
        public WaterBalanceModel WaterBalanceYear { get; set; }  






        public double AverageSupplyTimeHPerDayBestEstimate_F9 { get; set; } // 24.0 
        public double AveragePressureMBestEstimate_F11 { get; set; } // 30.0 


        public double Prs_Min_F29 { get; set; }  
        public double Prs_Max_F31 { get; set; }
        public double Prs_BestEstimate_F33 { get; set; } // 30.0 

        public double SysInput_ErrorMarg_F72 { get; set; }
        public double SysInput_Min_D75 { get; set; }
        public double SysInput_Max_D77 { get; set; }
        public double SysInput_BestEstimate_D79 { get; set; }

        public double BilledCons_Sum_D28 { get; set; }
        public double BilledCons_Sum_H28 { get; set; }

        public double UnbilledCons_Sum_D32 { get; set; }
        public double UnbilledCons_ErrorMarg_J25 { get; set; }
        public double UnbilledCons_Min_H28 { get; set; }
        public double UnbilledCons_Max_H30 { get; set; }
        public double UnbilledCons_BestEstimate_H32 { get; set; }

        public double UnauthCons_Total_L6 { get; set; }
        public double UnauthCons_Total_L10 { get; set; }
        public double UnauthCons_Total_L14 { get; set; }
        public double UnauthCons_ErrorMarg_F24 { get; set; }
        public double UnauthCons_Min_L27 { get; set; }
        public double UnauthCons_Max_L29 { get; set; }
        public double UnauthCons_BestEstimate_L31 { get; set; }

        public double MetErrors_Total_F8 { get; set; }
        public double MetErrors_Total_F32 { get; set; }
        public double MetErrors_Total_F34 { get; set; }
        public double MetErrors_Total_F38 { get; set; }
        public double MetErrors_Total_L8 { get; set; }
        public double MetErrors_Total_L32 { get; set; }
        public double MetErrors_Total_L34 { get; set; }
        public double MetErrors_Total_L38 { get; set; }
        public double MetErrors_ErrorMarg_N42 { get; set; }
        public double MetErrors_Min_L45 { get; set; }
        public double MetErrors_Max_L47 { get; set; }
        public double MetErrors_BestEstimate_L49 { get; set; }

        public double MetErrors_Total_L12 { get; set; }
        public double MetErrors_Total_L13 { get; set; }
        public double MetErrors_Total_L14 { get; set; }
        public double MetErrors_Total_L15 { get; set; }

        public double Network_Total_D28 { get; set; }
        public double Network_Total_D32 { get; set; }
        public double Network_Min_D39 { get; set; }
        public double Network_Max_D41 { get; set; }
        public double Network_BestEstimate_D43 { get; set; }
        public double Network_Number_H21 { get; set; }
        public double Network_ErrorMarg_J21 { get; set; }
        public double Network_ErrorMarg_J24 { get; set; }
        public double Network_Min_H26 { get; set; }
        public double Network_Max_H28 { get; set; }
        public double Network_BestEstimate_H30 { get; set; }
        public double Network_Number_H39 { get; set; }
        public double Network_ErrorMarg_J39 { get; set; }



        public double UnauthCons_Total_L18 { get; set; }
        public double UnauthCons_Total_L19 { get; set; }
        public double UnauthCons_Total_L20 { get; set; }
        public double UnauthCons_Total_L21 { get; set; }

        public double Interm_Min_H29 { get; set; }
        public double Interm_Max_H31 { get; set; }
        public double Interm_BestEstimate_H33 { get; set; }

        public double FinancData_G13 { get; set; }
        public double FinancData_G15 { get; set; }
        public double FinancData_G17 { get; set; }
        public double FinancData_G19 { get; set; }
        public double FinancData_G20 { get; set; }
        public double FinancData_G22 { get; set; }
        public double FinancData_D24 { get; set; }
        public double FinancData_G31 { get; set; }
        public string FinancData_K8 { get; set; }
        public string FinancData_K13 { get; set; }
        public string FinancData_K15 { get; set; }
        public string FinancData_K17 { get; set; }
        public string FinancData_K19 { get; set; }
        public string FinancData_K20 { get; set; }
        public string FinancData_K22 { get; set; }
        public string FinancData_K31 { get; set; }
        public string FinancData_K35 { get; set; }

        public double Pis_AverageSupplyTime_F9 { get; set; }
        public double Pis_AverageSupplyTime_H9 { get; set; }
        public double Pis_AverageSupplyTime_J9 { get; set; }
        public double Pis_AverageSupplyTime_L9 { get; set; }


        public object Clone()
        {
            return new EasyCalcDataOutput()
            {
                SystemInputVolume_B19  = SystemInputVolume_B19,
                SystemInputVolumeErrorMargin_B21  = SystemInputVolumeErrorMargin_B21,
                AuthorizedConsumption_K12  = AuthorizedConsumption_K12,
                AuthorizedConsumptionErrorMargin_K15  = AuthorizedConsumptionErrorMargin_K15,
                WaterLosses_K29  = WaterLosses_K29,
                WaterLossesErrorMargin_K31  = WaterLossesErrorMargin_K31,
                BilledAuthorizedConsumption_T8  = BilledAuthorizedConsumption_T8,
                UnbilledAuthorizedConsumption_T16  = UnbilledAuthorizedConsumption_T16,
                UnbilledAuthorizedConsumptionErrorMargin_T20  = UnbilledAuthorizedConsumptionErrorMargin_T20,
                CommercialLosses_T26  = CommercialLosses_T26,
                CommercialLossesErrorMargin_T29  = CommercialLossesErrorMargin_T29,
                PhysicalLossesM3_T34  = PhysicalLossesM3_T34,
                PhyscialLossesErrorMargin_AH35  = PhyscialLossesErrorMargin_AH35,
                BilledMeteredConsumption_AC4  = BilledMeteredConsumption_AC4,
                BilledUnmeteredConsumption_AC9  = BilledUnmeteredConsumption_AC9,
                UnbilledMeteredConsumption_AC14  = UnbilledMeteredConsumption_AC14,
                UnbilledUnmeteredConsumption_AC19  = UnbilledUnmeteredConsumption_AC19,
                UnbilledUnmeteredConsumptionErrorMargin_AO20  = UnbilledUnmeteredConsumptionErrorMargin_AO20,
                UnauthorizedConsumption_AC24  = UnauthorizedConsumption_AC24,
                UnauthorizedConsumptionErrorMargin_AO25  = UnauthorizedConsumptionErrorMargin_AO25,
                CustomerMeterInaccuraciesAndErrorsM3_AC29  = CustomerMeterInaccuraciesAndErrorsM3_AC29,
                CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30  = CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30,
                RevenueWaterM3_AY8  = RevenueWaterM3_AY8,
                NonRevenueWaterM3_AY24  = NonRevenueWaterM3_AY24,
                NonRevenueWaterErrorMargin_AY26  = NonRevenueWaterErrorMargin_AY26,

                WbDay_SystemInputVolume_B19  = WbDay_SystemInputVolume_B19,

                WaterBalanceDay = (WaterBalanceModel)WaterBalanceDay.Clone(),
                WaterBalancePeriod = (WaterBalanceModel)WaterBalancePeriod.Clone(),
                WaterBalanceYear = (WaterBalanceModel)WaterBalanceYear.Clone(),

                AverageSupplyTimeHPerDayBestEstimate_F9 = AverageSupplyTimeHPerDayBestEstimate_F9,
                AveragePressureMBestEstimate_F11 = AveragePressureMBestEstimate_F11,


                Prs_BestEstimate_F33 = Prs_BestEstimate_F33,
                Prs_Min_F29 = Prs_Min_F29,
                Prs_Max_F31 = Prs_Max_F31,

                SysInput_ErrorMarg_F72 = SysInput_ErrorMarg_F72,
                SysInput_Min_D75 = SysInput_Min_D75,
                SysInput_Max_D77 = SysInput_Max_D77,
                SysInput_BestEstimate_D79 = SysInput_BestEstimate_D79,

                BilledCons_Sum_D28 = BilledCons_Sum_D28,
                BilledCons_Sum_H28 = BilledCons_Sum_H28,
                UnbilledCons_Sum_D32 = UnbilledCons_Sum_D32,
                UnbilledCons_ErrorMarg_J25 = UnbilledCons_ErrorMarg_J25,
                UnbilledCons_Min_H28 = UnbilledCons_Min_H28,
                UnbilledCons_Max_H30 = UnbilledCons_Max_H30,
                UnbilledCons_BestEstimate_H32 = UnbilledCons_BestEstimate_H32,
                UnauthCons_Total_L6 = UnauthCons_Total_L6,
                UnauthCons_Total_L10 = UnauthCons_Total_L10,
                UnauthCons_Total_L14 = UnauthCons_Total_L14,
                UnauthCons_ErrorMarg_F24 = UnauthCons_ErrorMarg_F24,
                UnauthCons_Min_L27 = UnauthCons_Min_L27,
                UnauthCons_Max_L29 = UnauthCons_Max_L29,
                UnauthCons_BestEstimate_L31 = UnauthCons_BestEstimate_L31,

                MetErrors_Total_F8 = MetErrors_Total_F8,
                MetErrors_Total_F32 = MetErrors_Total_F32,
                MetErrors_Total_F34 = MetErrors_Total_F34,
                MetErrors_Total_F38 = MetErrors_Total_F38,
                MetErrors_Total_L8 = MetErrors_Total_L8,
                MetErrors_Total_L32 = MetErrors_Total_L32,
                MetErrors_Total_L34 = MetErrors_Total_L34,
                MetErrors_Total_L38 = MetErrors_Total_L38,
                MetErrors_ErrorMarg_N42 = MetErrors_ErrorMarg_N42,
                MetErrors_Min_L45 = MetErrors_Min_L45,
                MetErrors_Max_L47 = MetErrors_Max_L47,
                MetErrors_BestEstimate_L49 = MetErrors_BestEstimate_L49,

                MetErrors_Total_L12 = MetErrors_Total_L12,
                MetErrors_Total_L13 = MetErrors_Total_L13,
                MetErrors_Total_L14 = MetErrors_Total_L14,
                MetErrors_Total_L15 = MetErrors_Total_L15,

                Network_Total_D28 = Network_Total_D28,
                Network_Total_D32 = Network_Total_D32,
                Network_Min_D39 = Network_Min_D39,
                Network_Max_D41 = Network_Max_D41,
                Network_BestEstimate_D43 = Network_BestEstimate_D43,
                Network_Number_H21 = Network_Number_H21,
                Network_ErrorMarg_J21 = Network_ErrorMarg_J21,
                Network_ErrorMarg_J24 = Network_ErrorMarg_J24,
                Network_Min_H26 = Network_Min_H26,
                Network_Max_H28 = Network_Max_H28,
                Network_BestEstimate_H30 = Network_BestEstimate_H30,
                Network_Number_H39 = Network_Number_H39,
                Network_ErrorMarg_J39 = Network_ErrorMarg_J39,

                UnauthCons_Total_L18 = UnauthCons_Total_L18,
                UnauthCons_Total_L19 = UnauthCons_Total_L19,
                UnauthCons_Total_L20 = UnauthCons_Total_L20,
                UnauthCons_Total_L21 = UnauthCons_Total_L21,

                Interm_Min_H29 = Interm_Min_H29,
                Interm_Max_H31 = Interm_Max_H31,
                Interm_BestEstimate_H33 = Interm_BestEstimate_H33,

                FinancData_G13 = FinancData_G13,
                FinancData_G15 = FinancData_G15,
                FinancData_G17 = FinancData_G17,
                FinancData_G19 = FinancData_G19,
                FinancData_G20 = FinancData_G20,
                FinancData_G22 = FinancData_G22,
                FinancData_D24 = FinancData_D24,
                FinancData_G31 = FinancData_G31,
                FinancData_K8 = FinancData_K8,
                FinancData_K13 = FinancData_K13,
                FinancData_K15 = FinancData_K15,
                FinancData_K17 = FinancData_K17,
                FinancData_K19 = FinancData_K19,
                FinancData_K20 = FinancData_K20,
                FinancData_K22 = FinancData_K22,
                FinancData_K31 = FinancData_K31,
                FinancData_K35 = FinancData_K35,

                Pis_AverageSupplyTime_F9 = Pis_AverageSupplyTime_F9,
                Pis_AverageSupplyTime_H9 = Pis_AverageSupplyTime_H9,
                Pis_AverageSupplyTime_J9 = Pis_AverageSupplyTime_J9,
                Pis_AverageSupplyTime_L9 = Pis_AverageSupplyTime_L9,
            };
        }
    }
}
