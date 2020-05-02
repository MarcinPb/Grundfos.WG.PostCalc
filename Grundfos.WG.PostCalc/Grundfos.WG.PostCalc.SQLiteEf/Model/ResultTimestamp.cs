using System;

namespace Grundfos.WG.PostCalc.SQLiteEf.Model
{
    public class ResultTimestamp
    {
        public int ID { get; set; }
        public string Attribute { get; set; }
        public int ProcessID { get; set; }
        public DateTime ProcessStartTime { get; set; }
    }
}
