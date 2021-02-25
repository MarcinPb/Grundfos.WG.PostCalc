namespace WpfApplication1.ShapeModel
{
    public class LinkMy : Shp
    {
        public uint LinkId { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }

        public uint TypeId { get; set; }

        public override string ToString()
        {
            return $"{LinkId}";
        }
    }
}
