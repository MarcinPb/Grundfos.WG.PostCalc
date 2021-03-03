using GeometryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataRepository
{
    public class MainRepo
    {
        private static Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = GetWgObjectTypeList();

        public static Dictionary<ObjectTypes, List<DomainObjectData>> GetWgObjectTypeList()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            return domainGrouppedObjects;
        }

        public static List<DomainObjectData> GetJunctionList()
        {
            return domainGrouppedObjects[ObjectTypes.Junction];
        }

        public static Point2D GetPointTopLeft()
        {
            var junctionList = GetJunctionList();
            var xMin = junctionList.Min(x => x.Geometry[0].X);
            var yMin = junctionList.Min(x => x.Geometry[0].Y);
            return new Point2D(xMin, yMin);
        }
        public static Point2D GetPointBottomRight()
        {
            var junctionList = GetJunctionList();
            var xMax = junctionList.Max(x => x.Geometry[0].X);
            var yMax = junctionList.Max(x => x.Geometry[0].Y);
            return new Point2D(xMax, yMax);
        }

        public static List<DomainObjectData> GetCustomerNodeList()
        {
            return domainGrouppedObjects[ObjectTypes.CustomerNode];
        }

        public static List<DomainObjectData> GetPipeList()
        {
            return domainGrouppedObjects[ObjectTypes.Pipe];
        }
    }
}
