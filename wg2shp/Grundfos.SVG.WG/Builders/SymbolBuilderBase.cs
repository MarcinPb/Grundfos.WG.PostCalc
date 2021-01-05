using Grundfos.SVG.Builders;

namespace Grundfos.SVG.WG.Builders
{
    public abstract class SymbolBuilderBase
    {
        public SymbolBuilderBase(ColorServer colorServer, ClosedPathBuilder closedPathBuilder, PathBuilder pathBuilder)
        {
            this.ColorServer = colorServer;
            this.ClosedPathBuilder = closedPathBuilder;
            this.PathBuilder = pathBuilder;
        }

        public ColorServer ColorServer { get; }
        public ClosedPathBuilder ClosedPathBuilder { get; }
        public PathBuilder PathBuilder { get; }
    }
}
