﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ShapeModel
{
    public class CnShp : Shp
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
