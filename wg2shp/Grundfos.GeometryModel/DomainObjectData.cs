using System.Collections.Generic;

namespace Grundfos.GeometryModel
{
    public class DomainObjectData
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string Zone { get; set; }
        public bool IsActive { get; set; }
        public IList<Point2D> Geometry { get; set; }
        public IDictionary<string, object> Fields { get; set; }
        public ObjectTypes ObjectType { get; set; }

        public override string ToString()
        {
            return $"{Label} ({ID})";
        }
    }
}
