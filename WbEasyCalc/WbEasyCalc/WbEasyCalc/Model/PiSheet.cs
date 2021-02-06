using System;

namespace WbEasyCalcRepository.Model
{
    public class PiSheet
    {
        private readonly EasyCalcSheetData data;

        public PiSheet(EasyCalcSheetData data)
        {
            this.data = data;
        }

        public double AverageSupplyTimeHPerDayBestEstimate_F9 { get => data.IntermittentSupply.SupplyTimeBestEstimate_H33; }
        public double AverageSupplyTimeHPerDayBestEstimate_H9 { get => data.IntermittentSupply.ErrorMargin_H26; }
        public double AverageSupplyTimeHPerDayBestEstimate_J9 { get => AverageSupplyTimeHPerDayBestEstimate_F9*(1 - AverageSupplyTimeHPerDayBestEstimate_H9); }
        public double AverageSupplyTimeHPerDayBestEstimate_L9 { get => GetAverageSupplyTimeHPerDayBestEstimate_L9(); }
        private double GetAverageSupplyTimeHPerDayBestEstimate_L9()
        {
            // =IF(H$33=24;24;IF(H$33*(1+H$26)>24;24;(H$33*(1+H$26))))
            if (data.IntermittentSupply.SupplyTimeBestEstimate_H33 == 24)
            {
                return 24;
            }
            else
            {
                if (data.IntermittentSupply.SupplyTimeBestEstimate_H33 * (1 + data.IntermittentSupply.ErrorMargin_H26) > 24)
                {
                    return 24;
                }
                else
                {
                    return data.IntermittentSupply.SupplyTimeBestEstimate_H33 * (1 + data.IntermittentSupply.ErrorMargin_H26);
                }
            }
        }

        public double AveragePressureMBestEstimate_F11 { get => data.PressureSheet.AveragePressureBestEstimate_F33; }
        public double CaplBestEstimate_F17 { get; internal set; }
        public double MaplBestEstimate_F19 { get; internal set; }
        public double IliBestEstimate_F25 { get; set; }


    }
}
