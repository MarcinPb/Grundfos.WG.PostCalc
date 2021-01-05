using System;
using System.Collections.Generic;

namespace Grundfos.GeometryModel.Builders.Utils
{
    public class PathLenghtResolver
    {
        public double GetLength(IList<Point2D> path)
        {
            double length = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                var a = path[i];
                var b = path[i + 1];
                length += GetSegmentLength(a, b);
            }

            return length;
        }

        public static double GetSegmentLength(Point2D a, Point2D b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
