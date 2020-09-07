using System;
using Grundfos.WB.EasyCalc.Calculations;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace Grundfos.WB.Service.Jobs
{
    public class WbCalculationJob
    {
        private readonly IEasyCalcDataReader dataReader;

        public WbCalculationJob(IEasyCalcDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public WbCalculationJob() : this(new EasyCalcDataReaderMoq())
        {
        }

        [AutomaticRetry(Attempts = 5)]
        public void ExecuteCalculation(PerformContext context, DateTime yearMonth, string zoneIdentifier)
        {
            try
            {
                context.WriteLine("Reading input data for zone {0}, month: {1}-{2}.", zoneIdentifier, yearMonth.Year, yearMonth.Month);
                var data = this.dataReader.ReadSheetData(zoneIdentifier, yearMonth);

                context.WriteLine("Calculating water balance for zone {0}, month: {1}-{2}.", zoneIdentifier, yearMonth.Year, yearMonth.Month);
                EasyCalcRefactored.GetWaterLosses(data);
                EasyCalcRefactored.GetWaterLossesErrorMargin(data);
                EasyCalcRefactored.GetInfrastructureLeakageIndex(data);

                context.WriteLine("Publishing results for zone {0}, month: {1}-{2}.", zoneIdentifier, yearMonth.Year, yearMonth.Month);
                this.dataReader.PublishSheetData(data, zoneIdentifier);
            }
            catch (Exception ex)
            {
                context.WriteLine(ConsoleTextColor.Red, "Exception occurred when calculating data for zone {0}, month: {1}-{2}:", zoneIdentifier, yearMonth.Year, yearMonth.Month);
                context.WriteLine(ConsoleTextColor.Red, "\t{0}", ex.ToString());
                throw;
            }
        }
    }
}