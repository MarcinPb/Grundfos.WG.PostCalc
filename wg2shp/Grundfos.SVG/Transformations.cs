using System;
using System.Linq;
using Grundfos.SVG.Model;
using Svg;

namespace Grundfos.SVG
{
    public class Transformations
    {
        public const string TransformationGroupID = "TransformationGroup";
        private readonly GroupTransformations groupTransformations;

        public Transformations(GroupTransformations groupTransformations)
        {
            this.groupTransformations = groupTransformations;
        }

        public void Scale(SvgDocument document, float x, float y)
        {
            var transformationGroup = GetOrCreateTransformationGroup(document);
            transformationGroup.Transforms.Insert(0, new Svg.Transforms.SvgScale(x, y));
        }

        public void SetCanvasSize(SvgDocument document, float width, float height)
        {
            document.Width = width;
            document.Height = height;
        }

        public void ResizeCanvasToContent(SvgDocument document)
        {
            var boundary = GetBoundaries(document);
            MoveBy(document, -boundary.MinX, -boundary.MinY);
            document.Width = boundary.MaxX - boundary.MinX;
            document.Height = boundary.MaxY - boundary.MinY;
        }

        public void MoveBy(SvgDocument document, float x, float y)
        {
            var transformationGroup = GetOrCreateTransformationGroup(document);
            transformationGroup.Transforms.Insert(0, new Svg.Transforms.SvgTranslate(x, y));
        }

        public void RotateBy(SvgDocument document, float angleRadians)
        {
            var transformationGroup = GetOrCreateTransformationGroup(document);
            float degrees = (float)((angleRadians * 180) / Math.PI);
            transformationGroup.Transforms.Insert(0, new Svg.Transforms.SvgRotate(degrees));
        }

        private static DocumentBoundaryInfo GetBoundaries(SvgDocument document)
        {
            return new DocumentBoundaryInfo
            {
                MinX = document.Bounds.Left,
                MinY = document.Bounds.Top,
                MaxX = document.Bounds.Right,
                MaxY = document.Bounds.Bottom,
            };
        }

        private static SvgGroup GetOrCreateTransformationGroup(SvgDocument document)
        {
            var transformationGroup = document.Children.FirstOrDefault(x => x.ID == TransformationGroupID && x is SvgGroup) as SvgGroup;
            if (transformationGroup == null)
            {
                transformationGroup = new SvgGroup { ID = TransformationGroupID, };
                for (int i = 0; i < document.Children.Count; i++)
                {
                    transformationGroup.Children.Add(document.Children[i]);
                }

                document.Children.Clear();
                document.Children.Add(transformationGroup);
            }

            return transformationGroup;
        }
    }
}
