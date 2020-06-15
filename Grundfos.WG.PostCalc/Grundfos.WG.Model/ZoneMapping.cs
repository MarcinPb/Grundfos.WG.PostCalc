namespace Grundfos.WG.Model
{
    public class ZoneMapping
    {
        public string ZoneName { get; set; }
        public string OpcTag { get; set; }

        public override string ToString()
        {
            return $"{ZoneName}: {OpcTag}";
        }
    }
}
