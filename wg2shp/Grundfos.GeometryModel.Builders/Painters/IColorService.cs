using System.Drawing;

namespace Grundfos.GeometryModel.Builders.Painters
{
    public interface IColorService
    {
        Color GetColor(DomainObjectData item);
    }
}
