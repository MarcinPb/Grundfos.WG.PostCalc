using System;

namespace Grundfos.TW.SQL.Model
{
    public class SignalValue
    {
        public long ID { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public override string ToString()
        {
            return $"{this.ID.ToString()}:{this.Value}";
        }
    }
}
