using System;
using System.Collections.Generic;
using System.Linq;

namespace WbEasyCalcRepository.Model
{
    public class MeterErrorsSheet
    {
        private readonly EasyCalcSheetData data;

        public MeterErrorsSheet(EasyCalcSheetData data)
        {
            this.data = data;
            this.BilledMeteredConsumptionManuallyEnteredM3_F12_F28 = new List<double>();
            this.BilledMeteredConsumptionManuallyEnteredMeterUnderregistration_H12_H28 = new List<double>();
            this.BilledMeteredConsumptionManuallyEnteredErrorMargin_N12_N28 = new List<double>();
        }

        public double MeteredBulkSupplyExportMetereUnderregistration_H32 { get; set; }
        public double UnbilledMeteredConsumptionWithoutBulkSupplyM3_F34 { get; set; }
        public double UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34 { get; set; }
        public double CorruptMeterReadingPracticesMeterUnderregistration_H38 { get; set; }
        public double BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8 { get; set; }
        public List<double> BilledMeteredConsumptionManuallyEnteredM3_F12_F28 { get; set; }
        public List<double> BilledMeteredConsumptionManuallyEnteredMeterUnderregistration_H12_H28 { get; set; }
        public List<double> BilledMeteredConsumptionManuallyEnteredErrorMargin_N12_N28 { get; set; }
        public List<double> BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28
        {
            get => this.GetBilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28();
        }
        public double DataHandlingErrorsOffice_L40 { get; set; }
        public bool DetailedManualSpec_J6 { get; set; }
        public double BestEstimateTotalM3_L49 { get => this.GetBestEstimateTotalM3_L49(); }
        public double BilledMeteredConsumptionWithoutBulkSupplyTotalM3_F8 { get; private set; }
        public double BilledMeteredConsumptionWithoutBulkSupplyTotalM3_L8 { get; private set; }
        public double BilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N8 { get; set; }
        public double MeteredBulkSupplyExportErrorMargin_N32 { get; set; }
        public double UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 { get; set; }
        public double CorruptMeterReadingPracticessErrorMargin_N38 { get; set; }
        public double DataHandlingErrorsOfficeErrorMargin_N40 { get; set; }
        public double ErrorMarginTotal_N42 { get => this.GetErrorMarginTotal_N42(); }
        public double MeteredBulkSupplyExportTotalM3_F32
        {
            get =>
                this.data.BilledConsumptionSheet.BilledMeteredConsumptionBulkWaterSupplyExportM3_D6
                + this.data.UnbilledConsumptionSheet.MeteredConsumptionBulkWaterSupplyExportM3_D6;
        }
        public double MeteredBulkSupplyExportTotalWithMeterUnderregistrationM3_L32 { get; private set; }
        public double UnbilledMeteredConsumptionWithoutBulkSupplyWithMeterUnderregistration_L34 { get; private set; }
        public double CorruptMeterReadingPracticesTotalM3_F38 { get; private set; }
        public double CorruptMeterReadingPracticesWithMeterUnderregistrationM3_L38 { get; private set; }

        private double GetErrorMarginTotal_N42()
        {
            double s8 =
                Math.Pow(
                this.BilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N8
                * this.BilledMeteredConsumptionWithoutBulkSupplyTotalM3_L8
                / Constants.StandardDistributionFactor,
                2);
            double s12_s28 = GetBilledMeteredConsumptionManuallyEnteredFactorized_S12_S28().Sum();
            double s32 = Math.Pow(
                this.MeteredBulkSupplyExportTotalWithMeterUnderregistrationM3_L32
                * this.MeteredBulkSupplyExportErrorMargin_N32
                / Constants.StandardDistributionFactor,
                2);
            double s34 = Math.Pow(
                this.UnbilledMeteredConsumptionWithoutBulkSupplyWithMeterUnderregistration_L34
                * this.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34
                / Constants.StandardDistributionFactor,
                2);
            double s38 = Math.Pow(
                this.CorruptMeterReadingPracticessErrorMargin_N38
                * this.CorruptMeterReadingPracticesWithMeterUnderregistrationM3_L38
                / Constants.StandardDistributionFactor
                ,
                2);
            double s40 = Math.Pow(
                this.DataHandlingErrorsOfficeErrorMargin_N40
                * this.DataHandlingErrorsOffice_L40
                / Constants.StandardDistributionFactor,
                2);

            double s42 = s12_s28 + s32 + s34 + s38 + s40;
            double r42 = Math.Sqrt(s42);
            double n42 = this.BestEstimateTotalM3_L49 == 0 ? 0 : r42 * Constants.StandardDistributionFactor / this.BestEstimateTotalM3_L49;
            return n42;
        }

        private List<double> GetBilledMeteredConsumptionManuallyEnteredFactorized_S12_S28()
        {
            var entries = new List<double>();
            for (int i = 0; i < this.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28.Count; i++)
            {
                double entry = Math.Pow(
                    this.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28[i]
                    * this.BilledMeteredConsumptionManuallyEnteredErrorMargin_N12_N28[i]
                    / Constants.StandardDistributionFactor,
                    2);
            }

            return entries;
        }

        private double GetBestEstimateTotalM3_L49()
        {
            this.MeteredBulkSupplyExportTotalWithMeterUnderregistrationM3_L32 =
                (this.MeteredBulkSupplyExportTotalM3_F32 / (1 - this.MeteredBulkSupplyExportMetereUnderregistration_H32))
                - this.MeteredBulkSupplyExportTotalM3_F32;
            this.CorruptMeterReadingPracticesTotalM3_F38 =
                this.data.BilledConsumptionSheet.BilledMeteredConsumption_D28
                + this.data.BilledConsumptionSheet.BilledUnmeteredConsumption_H28;
            this.CorruptMeterReadingPracticesWithMeterUnderregistrationM3_L38 =
                (this.CorruptMeterReadingPracticesTotalM3_F38 / (1 - this.CorruptMeterReadingPracticesMeterUnderregistration_H38))
                - this.CorruptMeterReadingPracticesTotalM3_F38;
            this.UnbilledMeteredConsumptionWithoutBulkSupplyWithMeterUnderregistration_L34 =
                (this.UnbilledMeteredConsumptionWithoutBulkSupplyM3_F34 / (1 - this.UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34))
                - this.UnbilledMeteredConsumptionWithoutBulkSupplyM3_F34;
            if (this.DetailedManualSpec_J6)
            {
                double result = this.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28.Sum()
                    + this.MeteredBulkSupplyExportTotalWithMeterUnderregistrationM3_L32
                    + this.UnbilledMeteredConsumptionWithoutBulkSupplyWithMeterUnderregistration_L34
                    + this.CorruptMeterReadingPracticesWithMeterUnderregistrationM3_L38
                    + this.DataHandlingErrorsOffice_L40;
                return result;
            }
            else
            {
                double result = this.BilledMeteredConsumptionWithoutBulkSupplyTotalM3_L8
                    + this.MeteredBulkSupplyExportTotalWithMeterUnderregistrationM3_L32
                    + this.UnbilledMeteredConsumptionWithoutBulkSupplyWithMeterUnderregistration_L34
                    + this.CorruptMeterReadingPracticesWithMeterUnderregistrationM3_L38
                    + this.DataHandlingErrorsOffice_L40;
                return result;
            }
        }

        private List<double> GetBilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28()
        {
            if (!this.DetailedManualSpec_J6)
            {
                return this.BilledMeteredConsumptionManuallyEnteredM3_F12_F28.Select(x => 0d).ToList();
            }

            var results = new List<double>();
            for (int i = 0; i < this.BilledMeteredConsumptionManuallyEnteredM3_F12_F28.Count; i++)
            {
                double item = (
                    this.BilledMeteredConsumptionManuallyEnteredM3_F12_F28[i]
                    / (1 - this.BilledMeteredConsumptionManuallyEnteredMeterUnderregistration_H12_H28[i])
                ) - this.BilledMeteredConsumptionManuallyEnteredM3_F12_F28[i];
                results.Add(item);
            }

            return results;
        }
    }
}