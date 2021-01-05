using System.Collections.Generic;
using System.Linq;
using DotSpatial.Data;
using DotSpatial.Topology;
using Grundfos.GeometryModel;
using Grundfos.SHP.Constants;

namespace Grundfos.SHP
{
    public class ShapefileWriter
    {
        private readonly Dictionary<string, string> mapping;

        public ShapefileWriter(Dictionary<string, string> fieldMapping)
        {
            this.mapping = fieldMapping;
        }

        public void WritePolylines(string filePath, IList<DomainObjectData> items)
        {
            var featureSet = new FeatureSet(FeatureType.Line);
            this.BuildDataTableColumns(featureSet, items);

            foreach (var item in items)
            {
                var coords = item.Geometry.Select(x => new Coordinate(x.X, x.Y)).ToList();
                var lineString = new LineString(coords);
                var feature = featureSet.AddFeature(lineString);
                this.AppendFeatureAttributes(feature, item);
            }

            featureSet.SaveAs(filePath, true);
        }

        public void WritePoints(string filePath, IList<DomainObjectData> items)
        {
            var featureSet = new FeatureSet(FeatureType.Point);
            this.BuildDataTableColumns(featureSet, items);

            foreach (var item in items)
            {
                var coord = item.Geometry.Select(x => new Coordinate(x.X, x.Y)).Single();
                var point = new Point(coord);
                var feature = featureSet.AddFeature(point);
                this.AppendFeatureAttributes(feature, item);
            }

            featureSet.SaveAs(filePath, true);
        }

        private void AppendFeatureAttributes(IFeature feature, DomainObjectData item)
        {
            feature.DataRow.BeginEdit();
            feature.DataRow[ShapeAttributes.ID] = item.ID;
            feature.DataRow[ShapeAttributes.Label] = item.Label;
            feature.DataRow[ShapeAttributes.Zone] = item.Zone;
            feature.DataRow[ShapeAttributes.IsActive] = item.IsActive;
            foreach (var attribute in item.Fields)
            {
                feature.DataRow[this.mapping[attribute.Key]] = attribute.Value;
            }

            feature.DataRow.EndEdit();
        }

        private void BuildDataTableColumns(FeatureSet featureSet, IList<DomainObjectData> items)
        {
            featureSet.DataTable.Columns.Add(ShapeAttributes.ID, typeof(int));
            featureSet.DataTable.Columns.Add(ShapeAttributes.Label, typeof(string));
            featureSet.DataTable.Columns.Add(ShapeAttributes.Zone, typeof(string));
            featureSet.DataTable.Columns.Add(ShapeAttributes.IsActive, typeof(bool));

            foreach (var attribute in items[0].Fields)
            {
                featureSet.DataTable.Columns.Add(this.mapping[attribute.Key], attribute.Value.GetType());
            }
        }
    }
}
