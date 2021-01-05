using System;
using System.Drawing;

namespace Grundfos.Imaging.Drawers
{
    public abstract class AbstractDrawer
    {
        public abstract Type SupportedShapeType { get; }

        public abstract void Draw(Bitmap bitmap, Shapes.Shape shape);
    }
}
