using System;
using Svg;
using Svg.Transforms;

namespace Grundfos.SVG
{
    public class GroupTransformations
    {
        internal void Scale(SvgGroup group, float scale)
        {
            var transform = new SvgScale(scale);
            group.Transforms.Insert(0, transform);
        }

        internal void Move(SvgGroup group, float x, float y)
        {
            var transform = new SvgTranslate(x, y);
            group.Transforms.Insert(0, transform);
        }

        internal void RotateBy(SvgGroup group, float angleRadians)
        {
            float degrees = (float)((angleRadians * 180) / Math.PI);
            var transform = new SvgRotate(degrees);
            group.Transforms.Insert(0, transform);
        }
    }
}
