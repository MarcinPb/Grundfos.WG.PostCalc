using Grundfos.GeometryModel;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public interface IStrokeWidthService
    {
        double GetStrokeWidth(DomainObjectData item);
    }
}