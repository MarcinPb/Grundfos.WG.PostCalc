using System.Collections.Generic;
using System.Linq;
using DotSpatial.Projections;
using Grundfos.GeometryModel;

namespace Grundfos.SHP
{
    public class GisProjections
    {
        /// <summary>
        /// Projects the <paramref name="items"/> from one coordinate system,
        /// defined by <paramref name="sourceProj4String"/>,
        /// to another coordinate system, defined by <paramref name="destinationProj4String"/>.
        /// The <paramref name="sourceProj4String"/> and <paramref name="destinationProj4String"/>
        /// should be specified in the PROJ.4 format (see: <see cref="https://proj4.org/"/>).
        /// </summary>
        /// <param name="items">Geometry to project to reproject.</param>
        /// <param name="sourceProj4String">A definition of a source coordinate system.</param>
        /// <param name="destinationProj4String">A definition of a destination coordinate system.</param>
        public void Reproject(ICollection<DomainObjectData> items, string sourceProj4String, string destinationProj4String)
        {
            var points = items.SelectMany(x => x.Geometry).ToList();
            var xyArray = new double[points.Count * 2];
            int xIndex = 0;
            int yIndex = 1;
            foreach (var point in points)
            {
                xyArray[xIndex] = point.X;
                xyArray[yIndex] = point.Y;
                xIndex += 2;
                yIndex += 2;
            }

            this.Reproject(xyArray, sourceProj4String, destinationProj4String);

            xIndex = 0;
            yIndex = 1;
            foreach (var point in points)
            {
                point.X = xyArray[xIndex];
                point.Y = xyArray[yIndex];
                xIndex += 2;
                yIndex += 2;
            }
        }

        /// <summary>
        /// Projects the <paramref name="xyArray"/> from one coordinate system,
        /// defined by <paramref name="sourceProj4String"/>,
        /// to another coordinate system, defined by <paramref name="destinationProj4String"/>.
        /// The <paramref name="sourceProj4String"/> and <paramref name="destinationProj4String"/>
        /// should be specified in the PROJ.4 format (see: <see cref="https://proj4.org/"/>).
        /// </summary>
        /// <param name="xyArray">Geometry to project to reproject.</param>
        /// <param name="sourceProj4String">A definition of a source coordinate system.</param>
        /// <param name="destinationProj4String">A definition of a destination coordinate system.</param>
        public void Reproject(double[] xyArray, string sourceProj4String, string destinationProj4String)
        {
            var source = ProjectionInfo.FromProj4String(sourceProj4String);
            var destination = ProjectionInfo.FromProj4String(destinationProj4String);
            destination.ParseEsriString(destinationProj4String);

            var zArray = new double[] { 1 };
            DotSpatial.Projections.Reproject.ReprojectPoints(
                xyArray, zArray, source, destination, 0, (xyArray.Length / 2)
            );
        }
    }
}
