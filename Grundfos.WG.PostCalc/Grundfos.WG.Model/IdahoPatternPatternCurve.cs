using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WG.Model
{
    public class IdahoPatternPatternCurve
    {
        public int SupportElementId { get; set; }
        public double PatternCurveTimeFromStart { get; set; }
        public double PatternCurveMultiplier { get; set; }
    }
}
