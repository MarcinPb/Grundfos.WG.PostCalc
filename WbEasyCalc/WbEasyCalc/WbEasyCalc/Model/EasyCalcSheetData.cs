namespace WbEasyCalcRepository.Model
{
    public class EasyCalcSheetData
    {
        public StartSheet StartSheet { get; set; }
        public UnauthorizedConsumptionSheet UnauthorizedConsumptionSheet { get; set; }
        internal BilledConsumptionSheet BilledConsumptionSheet { get; set; }
        public UnbilledConsumptionSheet UnbilledConsumptionSheet { get; set; }
        public MeterErrorsSheet MeterErrorsSheet { get; set; }
        public SystemInputSheet SystemInputSheet { get; set; }
        public NetworkSheet NetworkSheet { get; set; }
        public FinancialDataSheet FinancialDataSheet { get; set; }
        public PressureSheet PressureSheet { get; set; }
        public IntermittentSupplySheet IntermittentSupply { get; set; }
        public WaterBalanceSheet WaterBalanceSheet { get; set; }
        public PiSheet PiSheet { get; set; }
    }
}
