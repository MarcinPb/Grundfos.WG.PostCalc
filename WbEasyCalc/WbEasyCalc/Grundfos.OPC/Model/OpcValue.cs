namespace Grundfos.OPC.Model
{
    public class OpcValue
    {
        public string Tag { get; set; }
        public object Value { get; set; }
        public override string ToString()
        {
            return $"{this.Tag}: {this.Value}";
        }
    }
}
