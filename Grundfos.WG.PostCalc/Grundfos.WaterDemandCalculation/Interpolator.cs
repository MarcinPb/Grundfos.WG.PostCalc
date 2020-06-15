using System.Linq;
using Grundfos.WaterDemandCalculation.ExtensionMethods;
using Grundfos.WG.Model;

namespace Grundfos.WaterDemandCalculation
{
    public class Interpolator
    {
        public const int WeekDays = 7;
        public const int DayHours = 24;
        public const int HourMinutes = 60;
        public const int WeekMinutes = WeekDays * DayHours * HourMinutes;
        public const double Tolerance = 0.01d;

        public Interpolator(TimeshiftAdjuster timeshiftAdjuster)
        {
            this.TimeshiftAdjuster = timeshiftAdjuster;
        }

        public TimeshiftAdjuster TimeshiftAdjuster { get; }

        public double GetValueAt(double timeshiftMinutes, WaterDemandPattern pattern)
        {
            double adjustedTime = this.TimeshiftAdjuster.GetAdjustedTime(timeshiftMinutes);
            var previous = pattern.Profile.Last(x => x.TimeshiftMinutes <= adjustedTime);
            if (previous.TimeshiftMinutes.EqualsWithTolerance(timeshiftMinutes, Tolerance))
            {
                return previous.Value;
            }

            var index = pattern.Profile.IndexOf(previous);
            var next = pattern.Profile[(index + 1) % pattern.Profile.Count];
            double interpolatedValue = this.Interpolate(
                previous.TimeshiftMinutes,
                previous.Value,
                next.TimeshiftMinutes,
                next.Value,
                adjustedTime);
            return interpolatedValue;
        }


        private double Interpolate(double x0, double y0, double x1, double y1, double x)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }

            double value = y0 + ((x - x0) * (y1 - y0) / (x1 - x0));
            return value;
        }
    }
}
