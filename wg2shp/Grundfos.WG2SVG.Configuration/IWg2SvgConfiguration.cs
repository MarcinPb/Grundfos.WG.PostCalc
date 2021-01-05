namespace Grundfos.WG2SVG.Configuration
{
    public interface IWg2SvgConfiguration
    {
        DataSource DataSource { get; }
        TargetCollection Targets { get; }
    }
}
