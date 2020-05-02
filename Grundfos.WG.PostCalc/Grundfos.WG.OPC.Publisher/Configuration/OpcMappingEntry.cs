namespace Grundfos.WG.OPC.Publisher.Configuration
{
    public class OpcMappingEntry
    {
        public int ElementID { get; set; }
        public string ElementLabel { get; set; }
        public bool Enabled { get; set; }
        public string OpcTag { get; set; }
    }
}
