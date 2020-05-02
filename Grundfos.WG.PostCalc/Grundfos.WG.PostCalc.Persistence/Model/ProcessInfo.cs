using System;

namespace Grundfos.WG.PostCalc.Persistence.Model
{
    public class ProcessInfo
    {
        public int ProcessID { get; set; }
        public DateTime ProcessStartTime { get; set; }
        public override string ToString()
        {
            return $"({ProcessID}) {ProcessStartTime}";
        }
    }
}
