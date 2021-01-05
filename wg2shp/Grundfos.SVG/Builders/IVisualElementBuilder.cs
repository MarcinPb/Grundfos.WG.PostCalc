using System;

using Grundfos.GeometryModel;

using Svg;

namespace Grundfos.SVG.Builders
{
    public interface IVisualElementBuilder
    {
        Type GeometryType { get; }
        SvgVisualElement Build(Geometry geometry);
    }
}
