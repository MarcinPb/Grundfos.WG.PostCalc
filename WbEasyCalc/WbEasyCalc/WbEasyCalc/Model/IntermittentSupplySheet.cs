using System.Collections.Generic;

namespace WbEasyCalcRepository.Model
{
    public class IntermittentSupplySheet
    {
        public List<double> Interm_Conn_D7_24_List { get; set; }
        public List<double> Interm_Days_F7_24_List { get; set; }
        public List<double> Interm_Hour_H7_24_List { get; set; }

        public double ErrorMargin_H26 { get => 0; }
        public double SupplyTimeBestEstimate_H33 { get => 24; }
    }
}