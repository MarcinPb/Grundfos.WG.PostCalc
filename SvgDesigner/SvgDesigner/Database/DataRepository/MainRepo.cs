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


        public static List<Dot> GetCustomerNodeList()
        {
            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            //List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            //stream.Close();

            //Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
            //    .GroupBy(x => x.ObjectType)
            //    .ToDictionary(x => x.Key, x => x.ToList());

            var dotList = domainGrouppedObjects[ObjectTypes.CustomerNode]
                .Select(x => new Dot()
                {
                    ID = x.ID,
                    Label = x.Label,
                    Center= x.Geometry[0],
                })
                .ToList();
            return dotList;
        }
        public static List<DomainObjectData> GetCustomerNodeList2()
        {
            return domainGrouppedObjects[ObjectTypes.CustomerNode];
        }


        public static List<Dot> GetJunctionList()
        {
            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            //List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            //stream.Close();

            //Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
            //    .GroupBy(x => x.ObjectType)
            //    .ToDictionary(x => x.Key, x => x.ToList());

            var dotList = domainGrouppedObjects[ObjectTypes.Junction]
                .Select(x => new Dot()
                {
                    ID = x.ID,
                    Label = x.Label,
                    Center= x.Geometry[0],
                })
                .ToList();
            return dotList;
        }
        public static List<DomainObjectData> GetJunctionList2()
        {
            return domainGrouppedObjects[ObjectTypes.Junction];
        }

        public static Point2D GetPointTopLeft()
        {
            var junctionList = GetJunctionList();
            var xMin = junctionList.Min(x => x.Center.X);
            var yMin = junctionList.Min(x => x.Center.Y);
            return new Point2D(xMin, yMin);
        }
        public static Point2D GetPointBottomRight()
        {
            var junctionList = GetJunctionList();
            var xMax = junctionList.Max(x => x.Center.X);
            var yMax = junctionList.Max(x => x.Center.Y);
            return new Point2D(xMax, yMax);
        }

        //public static List<Pipe> GetPipeList()
        //{
        //    //IFormatter formatter = new BinaryFormatter();
        //    //Stream stream = new FileStream(".\\Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    //List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
        //    //stream.Close();

        //    //Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
        //    //    .GroupBy(x => x.ObjectType)
        //    //    .ToDictionary(x => x.Key, x => x.ToList());

        //    var dotList = domainGrouppedObjects[ObjectTypes.Pipe]
        //        .Select(x => new Pipe()
        //        {
        //            ID = x.ID,
        //            Label = x.Label,
        //            Path = x.Geometry,
        //        })
        //        .ToList();
        //    return dotList;
        //}
        public static List<DomainObjectData> GetPipeList2()
        {
            return domainGrouppedObjects[ObjectTypes.Pipe];
        }
    }
}
