using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Grundfos.GeometryModel.Builders.Painters
{
    public class AttributeColorService : IColorService
    {
        private readonly Dictionary<int, IColorSelectorService> colorSelectors;

        public AttributeColorService(ICollection<IColorSelectorService> services)
        {
            this.colorSelectors = services.ToDictionary(x => x.ObjectTypeID, x => x);
        }

        public Color GetColor(DomainObjectData item)
        {
            if (this.colorSelectors.TryGetValue((int)item.ObjectType, out IColorSelectorService colorSelector))
            {
                return colorSelector.GetColor(item);
            }

            return Color.FromArgb(0, 0, 0);
        }
    }
}
