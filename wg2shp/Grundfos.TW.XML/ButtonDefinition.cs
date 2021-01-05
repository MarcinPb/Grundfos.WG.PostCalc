using System.Collections.Generic;

namespace Grundfos.TW.XML
{
    public class ButtonDefinition
    {
        public ButtonDefinition()
        {
            this.AttributeReplacements = new Dictionary<string, string>();
        }

        public string Label { get; set; }
        public int ID { get; set; }
        public Dictionary<string, string> AttributeReplacements { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string TemplatePath { get; set; }

        public override string ToString()
        {
            return $"{this.ID} ({this.Label}) ({nameof(AttributeReplacements)}:{this.AttributeReplacements.Count})";
        }
    }
}
