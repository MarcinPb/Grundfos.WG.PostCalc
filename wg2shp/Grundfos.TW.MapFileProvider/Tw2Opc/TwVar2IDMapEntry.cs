namespace Grundfos.TW.DataSourceMap.Tw2Opc
{
    public class TwVar2IDMapEntry
    {
        public string VariableName { get; set; }
        public long VariableID { get; set; }

        public override string ToString()
        {
            return $"{this.VariableName}<->{this.VariableID}";
        }
    }
}
