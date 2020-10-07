using System.Collections.Generic;
using System.Linq;

namespace Grundfos.WB.EasyCalc.Calculations.Model
{
    public class NetworkSheet
    {
        private readonly EasyCalcSheetData data;

        public NetworkSheet(EasyCalcSheetData data)
        {
            this.data = data;
            this.DistributionAndTransmissionMainsEntries_D7_D26 = new List<double>();
        }

        public double DistributionAndTransmissionMainsPossibleUnderestimation_D30 { get; set; }
        public List<double> DistributionAndTransmissionMainsEntries_D7_D26 { get; set; }
        public double DistributionAndTransmissionMainsTotalKm_D28 { get => this.DistributionAndTransmissionMainsEntries_D7_D26.Sum(); }
        public double DistributionAndTransmissionMainsMinimum_D33 { get => this.DistributionAndTransmissionMainsTotalKm_D28; }
        public double DistributionAndTransmissionMainsMaximum_D35
        {
            get => this.DistributionAndTransmissionMainsTotalKm_D28
                * (1 + this.DistributionAndTransmissionMainsPossibleUnderestimation_D30);
        }
        public double DistributionAndTransmissionMainsBestEstimate_D37
        {
            get => (this.DistributionAndTransmissionMainsMinimum_D33 + this.DistributionAndTransmissionMainsMaximum_D35) / 2;
        }
        public double NumberOfConnectionsOfRegsteredCustomers_H10 { get; set; }
        public double AvgLenOfServiceConnectionFromBoundaryToMeterM_H32 { get; set; }
        public double EstimatedNumberOfIllegalConnections_H21
        {
            get => this.data.UnauthorizedConsumptionSheet.IllegalConnectionsDomesticEstimatedNumber_D6
                + this.data.UnauthorizedConsumptionSheet.IllegalConnectionsOthersEstimatedNumber_D10;
        }
        public double NumberOfInactiveAccountsWServiceConnections_H18 { get; set; }
        public double ServiceConnectionsBestEstimate_H30
        {
            get => this.NumberOfConnectionsOfRegsteredCustomers_H10
                + this.EstimatedNumberOfIllegalConnections_H21
                + this.NumberOfInactiveAccountsWServiceConnections_H18;
        }
        public double LenOfServConnFromBoundToMeterKm_H39
        {
            get => this.AvgLenOfServiceConnectionFromBoundaryToMeterM_H32
                * this.ServiceConnectionsBestEstimate_H30 / 1000;
        }
    }
}
