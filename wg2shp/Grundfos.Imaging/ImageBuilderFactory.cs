using System.Collections.Generic;
using Grundfos.Imaging.Drawers;

namespace Grundfos.Imaging
{
    public class ImageBuilderFactory
    {
        public ImageBuilder Build()
        {
            var builders = new List<AbstractDrawer>
            {
                new RectangleDrawer(),
                new TextDrawer(),
            };
            var legendDrawer = new LegendBoxDrawer(builders);
            builders.Add(legendDrawer);
            return new ImageBuilder(builders);
        }
    }
}
