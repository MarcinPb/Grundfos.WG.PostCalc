using System.Collections.Generic;
using System.Drawing;
using Svg;

namespace Grundfos.SVG
{
    public class ColorServer
    {
        private readonly Dictionary<Color, SvgColourServer> colorServers;

        public ColorServer()
        {
            this.colorServers = new Dictionary<Color, SvgColourServer>();
        }

        public SvgColourServer ToSvgColourServer(Color color)
        {
            if (this.colorServers.TryGetValue(color, out SvgColourServer result))
            {
                return result;
            }

            result = new SvgColourServer(Color.FromArgb(color.R, color.G, color.B));
            return result;
        }
    }
}
