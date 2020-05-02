namespace Grundfos.WG.PostCalc.SQLiteEf.Model
{
    public class Result
    {
        public int ID { get; set; }
        public string Attribute { get; set; }
        public int ObjectID { get; set; }
        public double Value { get; set; }
    }
}
