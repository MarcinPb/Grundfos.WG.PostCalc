namespace Grundfos.TW.DataSourceMap
{
    public class DataSourceMapEntry
    {
        public int WgObjectID { get; set; }
        public string WgObjectLabel { get; set; }
        public int WgAttributeID { get; set; }
        public long TwVariableID { get; set; }
        public string TwVariableName { get; set; }

        public override string ToString()
        {
            return $"WG([{WgObjectID}] [{WgObjectLabel}] [{WgAttributeID}]) TW([{TwVariableID}] [{TwVariableName}])";
        }
    }
}
