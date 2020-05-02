using System;

namespace Grundfos.WG.PostCalc.Persistence.Model
{
    public class ResultTimestamp
    {
        public string Attribute { get; set; }
        public int ProcessID { get; set; }
        public DateTime ProcessStartTime { get; set; }
    }
}
