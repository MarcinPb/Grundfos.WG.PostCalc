using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.GeometryModel;

namespace Grundfos.SVG.Model
{
    public class DocumentBoundaryInfo
    {
        public float MinX { get; set; }
        public float MinY { get; set; }
        public float MaxX { get; set; }
        public float MaxY { get; set; }
        public Point2D LeftmostPoint { get; set; }
        public Point2D RightmostPoint { get; set; }
        public Point2D TopPoint { get; set; }
        public Point2D BottomPoint { get; set; }
    }
}
