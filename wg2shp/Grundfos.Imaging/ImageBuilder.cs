using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Grundfos.Imaging.Drawers;
using Grundfos.Imaging.Shapes;

namespace Grundfos.Imaging
{
    public class ImageBuilder : IDisposable
    {
        private readonly Dictionary<Type, AbstractDrawer> drawers;
        private Bitmap bitmap;

        public ImageBuilder(ICollection<AbstractDrawer> drawers)
        {
            this.drawers = drawers.ToDictionary(x => x.SupportedShapeType, x => x);
        }

        public void InitializeImage(int width, int height)
        {
            if (this.bitmap != null)
            {
                this.bitmap.Dispose();
            }

            this.bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(this.bitmap))
            {
                g.Clear(System.Drawing.Color.Green);
            }
        }

        public void Draw(Shape shape)
        {
            if (!this.drawers.TryGetValue(shape.GetType(), out AbstractDrawer drawer))
            {
                throw new NotSupportedException("Not supported shape type: " + shape.GetType());
            }

            drawer.Draw(this.bitmap, shape);
        }

        public void Save(string fileName, ImageFormats format)
        {
            var target = Convert(format);
            this.bitmap.Save(fileName, target);
        }

        public void Dispose()
        {
            this.bitmap.Dispose();
        }

        private static ImageFormat Convert(ImageFormats source)
        {
            switch (source)
            {
                case ImageFormats.Png:
                    return ImageFormat.Png;
                case ImageFormats.Bmp:
                    return ImageFormat.Bmp;
                default:
                    throw new NotSupportedException("Not supported image format: " + source);
            }
        }
    }
}
