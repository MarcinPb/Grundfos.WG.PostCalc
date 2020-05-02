using System;

namespace Grundfos.WaterDemandCalculation.ExtensionMethods
{
    public static class DoubleExtensions
    {
        public static bool EqualsWithTolerance(this double d, double value, double tolerance)
        {
            return Math.Abs(d - value) <= tolerance;
        }
    }
}
