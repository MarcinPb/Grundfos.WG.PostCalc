using WpfApplication1.Utility;

namespace WpfApplication1.ShapeModel
{
    public class Shp
    {
        public int? Id { get; set; }
        public uint TypeId { get; set; }

        public string Name { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
 
        public override string ToString()
        {
            return $"{Id} - '{Name}'";
        }
    }
}
