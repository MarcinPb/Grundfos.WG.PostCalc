namespace Grundfos.WG2SVG.Configuration
{
    public interface IStrokeWidthSettings
    {
        double DefaultWidth { get; }
        double SymbolWidth { get; }
        double PipeDiameterToWidthFactor { get; }
        string DiameterFieldName { get; }
    }
}
