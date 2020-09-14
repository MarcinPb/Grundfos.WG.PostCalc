using System;
using System.Collections.Generic;
using Grundfos.WB.EasyCalc.Calculations.Model;

namespace Grundfos.WB.EasyCalc.Calculations
{
    public class EasyCalcDataReaderMoq : IEasyCalcDataReader
    {
        public void PublishSheetData(EasyCalcSheetData data, string zoneName)
        {
        }

        public EasyCalcSheetData ReadSheetData(string zone, DateTime yearMonth)
        {
            var data = new EasyCalcSheetData();
            data.StartSheet = new StartSheet
            {
                PeriodDays_M21 = 30,
            };
            data.SystemInputSheet = new SystemInputSheet
            {
                SystemInputVolumeM3_D6_D70 = new List<double> { 6593339 },
                SystemInputVolumeError_F6_F70 = new List<double> { 0.05 }
            };
            data.BilledConsumptionSheet = new BilledConsumptionSheet
            {
                BilledMeteredConsumptionBulkWaterSupplyExportM3_D6 = 5332026,
                BulledUnmeteredConsumptionBulkWaterSupplyExportM3_H6 = 1000,
            };
            data.UnbilledConsumptionSheet = new UnbilledConsumptionSheet
            {
                MeteredConsumptionBulkWaterSupplyExportM3_D6 = 309349,
            };
            data.UnauthorizedConsumptionSheet = new UnauthorizedConsumptionSheet(data)
            {
                IllegalConnectionsDomesticEstimatedNumber_D6 = 100,
                IllegalConnectionsDomesticPersonsPerHouse_H6 = 3,
                IllegalConnectionsDomesticConsumptionLitersPerPersonPerDay_J6 = 120,
                IllegalConnectionsDomesticErrorMargin_F6 = 0.05,
                IllegalConnectionsOthersErrorMargin_F10 = 0.0,
                MeterTamperingBypassesEtcEstimatedNumber_D14 = 1000,
                MeterTamperingBypassesEtcErrorMargin_F14 = 0.10,
                MeterTamperingBypassesEtcConsumptionLitersPerCustomerPerDay_J14 = 160,
            };
            data.MeterErrorsSheet = new MeterErrorsSheet(data)
            {
                DetailedManualSpec_J6 = false,
                BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8 = 0.03,
                BilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N8 = 0.02,
                MeteredBulkSupplyExportMetereUnderregistration_H32 = 0.03,
                UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34 = 0.03,
                CorruptMeterReadingPracticesMeterUnderregistration_H38 = 0.03,
            };

            data.NetworkSheet = new NetworkSheet(data)
            {
                DistributionAndTransmissionMainsEntries_D7_D26 = new List<double> { 260.0 },
                NumberOfConnectionsOfRegsteredCustomers_H10 = 8000,
                NumberOfInactiveAccountsWServiceConnections_H18 = 1500,
                AvgLenOfServiceConnectionFromBoundaryToMeterM_H32 = 7,
            };
            data.PressureSheet = new PressureSheet()
            {
                ApproximateNumberOfConnections_D7_D24 = new List<double> { 9500, },
                DailyAveragePressureM_F7_F24 = new List<double> { 30 },
            };
            data.IntermittentSupply = new IntermittentSupplySheet();

            data.PiSheet = new PiSheet(data);
            data.WaterBalanceSheet = new WaterBalanceSheet();

            return data;
        }
    }
}
