﻿using System;

namespace GeometryModel
{
    [Serializable]
    public class Point2D
    {
        public Point2D()
        {
        }

        public Point2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"{this.X};{this.Y}";
        }
    }
}
