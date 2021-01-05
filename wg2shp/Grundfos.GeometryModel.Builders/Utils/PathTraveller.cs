using System;
using System.Collections.Generic;

namespace Grundfos.GeometryModel.Builders.Utils
{
    public class PathTraveller
    {
        public const double Tolerance = 0.1;

        public Pointer GetPointerAt(IList<Point2D> path, double distance)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                var a = path[i];
                var b = path[i + 1];
                double segment = PathLenghtResolver.GetSegmentLength(a, b);
                distance -= segment;
                if (distance < Tolerance)
                {
                    if (Math.Abs(distance) > Tolerance)
                    {
                        distance += segment;
                        return new Pointer
                        {
                            Point = GetPointBetween(a, b, distance),
                            Direction = UnitVector2D.Build(b.X - a.X, b.Y - a.Y),
                        };
                    }

                    // if last point, take the point and direction of last segment
                    if (i == path.Count - 2)
                    {
                        return new Pointer
                        {
                            Point = b,
                            Direction = UnitVector2D.Build(b.X - a.X, b.Y - a.Y),
                        };
                    }

                    // if intermediate point, take point and summarize unit vectors of surrounding segments
                    var c = path[i + 2];
                    var previousVector = UnitVector2D.Build(b.X - a.X, b.Y - a.Y);
                    var nextVector = UnitVector2D.Build(c.X - b.X, c.Y - b.Y);
                    var derivateVector = UnitVector2D.Build(previousVector.X + nextVector.X, previousVector.Y + nextVector.Y);
                    return new Pointer
                    {
                        Point = b,
                        Direction = derivateVector,
                    };

                }
            }

            throw new ArgumentException("The specified distance exceeds the total length of the path", nameof(distance));
        }

        public static Point2D GetPointBetween(Point2D a, Point2D b, double distance)
        {
            double hypotenuse = PathLenghtResolver.GetSegmentLength(a, b);
            if (hypotenuse < distance)
            {
                string message = string.Format(
                    "The distance between the points {0} and {1} is less than the specified distance {2}.",
                    a.ToString(),
                    b.ToString(),
                    distance);
                throw new ArgumentException(message, nameof(distance));
            }

            double ratio = distance / hypotenuse;
            double x = a.X + ((b.X - a.X) * ratio);
            double y = a.Y + ((b.Y - a.Y) * ratio);
            return new Point2D(x, y);
        }
    }
}
