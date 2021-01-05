using Grundfos.GeometryModel;
using Svg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Grundfos.SVG.Builders
{
    public class SvgBuilder
    {
        private readonly IDictionary<Type, IVisualElementBuilder> builders;
        private readonly SvgDocument document;
        private readonly Transformations transformations;

        public SvgBuilder(ICollection<IVisualElementBuilder> builders, Transformations transformations)
        {
            this.builders = builders.ToDictionary(x => x.GeometryType, x => x);
            this.document = new TwSvgDocument();
            this.transformations = transformations;
        }

        public void Draw(ICollection<Geometry> items)
        {
            foreach (var item in items)
            {
                var geometry = this.BuildSvgGeometry(item);
                if (geometry != null)
                {
                    document.Children.Add(geometry);
                }
            }
        }

        private SvgVisualElement BuildSvgGeometry(Geometry item)
        {
            var builder = this.builders[item.GetType()];
            var visualElement = builder.Build(item);
            return visualElement;
        }


        public void Scale(float x, float y)
        {
            this.transformations.Scale(this.document, x, y);
        }

        public void RotateBy(float angleRadians)
        {
            this.transformations.RotateBy(this.document, angleRadians);
        }

        public void MoveBy(float x, float y)
        {
            this.transformations.MoveBy(this.document, x, y);
        }

        public void SetCanvasSize(float width, float height)
        {
            this.transformations.SetCanvasSize(this.document, width, height);
        }

        public void ResizeCanvas()
        {
            this.transformations.ResizeCanvasToContent(this.document);
        }

        public void Save(string filePath)
        {
            using (var file = new FileStream(filePath, FileMode.Create))
            {
                this.document.Write(file, false);
            }
        }
    }
}
