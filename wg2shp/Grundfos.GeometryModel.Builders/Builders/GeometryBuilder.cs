using System;
using System.Collections.Generic;
using System.Linq;

namespace Grundfos.GeometryModel.Builders
{
    public class GeometryBuilder
    {
        private readonly IDictionary<ObjectTypes, IGeometryBuilder> geometryBuilders;

        public GeometryBuilder(ICollection<IGeometryBuilder> geometryBuilders)
        {
            this.geometryBuilders = geometryBuilders.ToDictionary(x => x.GeometryType, x => x);
        }

        public ICollection<Geometry> BuildGeometry(ICollection<DomainObjectData> models)
        {
            var result = new List<Geometry>();
            foreach (var item in models)
            {
                if (!this.geometryBuilders.TryGetValue(item.ObjectType, out IGeometryBuilder builder))
                {
                    throw new NotSupportedException("Could not find builder for the specified geometry type: " + item.ObjectType.ToString());
                }

                var geometry = builder.Build(item);
                result.Add(geometry);
            }

            return result;
        }
    }
}
