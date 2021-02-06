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

        /*
        */
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


        public double Pis_F9 { get; set; }
        public double Pis_H9 { get; set; }
        public double Pis_J9 { get; set; }
        public double Pis_L9 { get; set; }
        public double Pis_F11 { get; set; }
        public double Pis_H11 { get; set; }
        public double Pis_J11 { get; set; }
        public double Pis_L11 { get; set; }
        public double Pis_F17 { get; set; }
        public double Pis_H17 { get; set; }
        public double Pis_J17 { get; set; }
        public double Pis_L17 { get; set; }
        public double Pis_F19 { get; set; }
        public double Pis_H19 { get; set; }
        public double Pis_J19 { get; set; }
        public double Pis_L19 { get; set; }
        public double Pis_F25 { get; set; }
        public double Pis_H25 { get; set; }
        public double Pis_J25 { get; set; }
        public double Pis_L25 { get; set; }
        public double Pis_F27 { get; set; }
        public double Pis_H27 { get; set; }
        public double Pis_J27 { get; set; }
        public double Pis_L27 { get; set; }
        public double Pis_F29 { get; set; }
        public double Pis_H29 { get; set; }
        public double Pis_J29 { get; set; }
        public double Pis_L29 { get; set; }
        public double Pis_F31 { get; set; }
        public double Pis_H31 { get; set; }
        public double Pis_J31 { get; set; }
        public double Pis_L31 { get; set; }
        public double Pis_F37 { get; set; }
        public double Pis_H37 { get; set; }
        public double Pis_J37 { get; set; }
        public double Pis_L37 { get; set; }
        public double Pis_F39 { get; set; }
        public double Pis_H39 { get; set; }
        public double Pis_J39 { get; set; }
        public double Pis_L39 { get; set; }
        public double Pis_F41 { get; set; }
        public double Pis_H41 { get; set; }
        public double Pis_J41 { get; set; }
        public double Pis_L41 { get; set; }
        public double Pis_F47 { get; set; }
        public double Pis_H47 { get; set; }
        public double Pis_J47 { get; set; }
        public double Pis_L47 { get; set; }
        public double Pis_F49 { get; set; }
        public double Pis_H49 { get; set; }
        public double Pis_J49 { get; set; }
        public double Pis_L49 { get; set; }
        public double Pis_F51 { get; set; }
        public double Pis_H51 { get; set; }
        public double Pis_J51 { get; set; }
        public double Pis_L51 { get; set; }

    }
}
