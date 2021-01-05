namespace Grundfos.GeometryModel.Builders
{
    public interface IGeometryBuilder
    {
        ObjectTypes GeometryType { get; }

        /// <summary>
        /// Builds a <see cref="Geometry"/> based on the specified <paramref name="data"/>.
        /// </summary>
        /// <param name="data">The data of item to build.</param>
        /// <returns></returns>
        Geometry Build(DomainObjectData data);
    }
}
