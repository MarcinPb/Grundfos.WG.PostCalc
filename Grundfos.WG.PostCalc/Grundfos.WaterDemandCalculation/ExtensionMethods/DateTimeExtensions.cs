using System;

namespace Grundfos.WaterDemandCalculation.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static double MinutesFromMonday(this DateTime dt)
        {
            var monday = dt.StartOfWeek(DayOfWeek.Monday);
            TimeSpan ts = dt - monday;
            double minutes = ts.TotalMinutes;
            return minutes;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

    }
}
