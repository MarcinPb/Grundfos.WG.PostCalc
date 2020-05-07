namespace Grundfos.WaterDemandCalculation
{
    public class TimeshiftAdjuster
    {
        // ex.: periodMinutes = 7 * 24 * 60
        public TimeshiftAdjuster(double periodMinutes)
        {
            this.PeriodMinutes = periodMinutes;
        }

        public double PeriodMinutes { get; }

        public double GetAdjustedTime(double timeshiftMinutes)
        {
            if (timeshiftMinutes < 0)
            {
                timeshiftMinutes = timeshiftMinutes % -this.PeriodMinutes;
                timeshiftMinutes = this.PeriodMinutes + timeshiftMinutes;
            }

            return timeshiftMinutes % this.PeriodMinutes;
        }

    }
}
