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
        public double AveragePressureMBestEstimate_F11 { get => data.PressureSheet.AveragePressureBestEstimate_F33; }
        public double CaplBestEstimate_F17 { get; internal set; }
        public double MaplBestEstimate_F19 { get; internal set; }
        public double IliBestEstimate_F25 { get; set; }
    }
}
