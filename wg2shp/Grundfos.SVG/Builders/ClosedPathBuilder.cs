using Grundfos.GeometryModel;

using Svg.Pathing;

namespace Grundfos.SVG.Builders
{
    public class ClosedPathBuilder : PathBuilder, IVisualElementBuilder
    {
        public ClosedPathBuilder(ColorServer colorServer) : base(colorServer)
        {
        }

        protected override SvgPathSegmentList BuildPathSegmentList(Polyline item)
        {
            var path = base.BuildPathSegmentList(item);
            path.Add(new SvgClosePathSegment());
            return path;
        }
    }
}
