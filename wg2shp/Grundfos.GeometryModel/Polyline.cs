using System.Collections.Generic;

namespace Grundfos.GeometryModel
{
    public class Polyline : Geometry
    {
        public IList<Point2D> Path { get; set; }
        public override string ToString()
        {
            return $"{Label} ({ID})";
        }
    }
}
