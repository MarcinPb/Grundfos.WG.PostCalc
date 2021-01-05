using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.GeometryModel;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public class SymbolStrokeWidthService : IStrokeWidthService
    {
        private readonly double symbolStrokeWidth;

        public SymbolStrokeWidthService(double symbolStrokeWidth)
        {
            this.symbolStrokeWidth = symbolStrokeWidth;
        }

        public double GetStrokeWidth(DomainObjectData item)
        {
            return this.symbolStrokeWidth;
        }
    }
}
