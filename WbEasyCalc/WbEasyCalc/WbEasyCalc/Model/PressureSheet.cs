using System.Collections.Generic;
using System.Linq;

namespace WbEasyCalcRepository.Model
{
    public class PressureSheet
    {
        public List<double> ApproximateNumberOfConnections_D7_D24 { get; set; }
        public List<double> DailyAveragePressureM_F7_F24 { get; set; }
        public double AveragePressureBestEstimate_F33 { get => this.GetAveragePressureBestEstimate_F33(); }
        private double GetAveragePressureBestEstimate_F33()
        {
            double result = 0;
            for (int i = 0; i < this.ApproximateNumberOfConnections_D7_D24.Count; i++)
            {
                result += this.ApproximateNumberOfConnections_D7_D24[i] * this.DailyAveragePressureM_F7_F24[i];
            }

            result /= this.ApproximateNumberOfConnections_D7_D24.Sum();

            return result;
        }
    }
}
