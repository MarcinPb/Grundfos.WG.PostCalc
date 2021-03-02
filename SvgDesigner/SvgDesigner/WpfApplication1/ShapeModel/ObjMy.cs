namespace WpfApplication1.ShapeModel
{
    public class ObjMy : Shp
    {
        //public uint ObjId { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        //public uint TypeId { get; set; }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
