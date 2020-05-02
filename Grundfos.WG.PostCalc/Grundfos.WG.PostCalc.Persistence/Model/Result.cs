using System;

namespace Grundfos.WG.PostCalc.Persistence.Model
{
    public class Result
    {
        public int ObjectID { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
