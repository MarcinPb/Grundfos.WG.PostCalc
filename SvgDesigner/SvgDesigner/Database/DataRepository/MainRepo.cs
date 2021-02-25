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

        public static List<Dot> GetJunctionList()
        {

            // Horizontal
            //Point2D topLeft = new Point2D() { X = 5570422, Y = 5676000 };
            //Point2D bottomRight = new Point2D() { X = 5590485, Y = 5674000 };

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            var dotList = domainGrouppedObjects[ObjectTypes.Junction]
                //.Where(y => y.Geometry[0].X >= topLeft.X && y.Geometry[0].X <= bottomRight.X && y.Geometry[0].Y <= topLeft.Y && y.Geometry[0].Y >= bottomRight.Y)
                .Select(x => new Dot()
                {
                    ID = x.ID,
                    Label = x.Label,
                    //Center = XyToLonLat(x.Geometry[0]),
                    Center= x.Geometry[0],
                })
                .ToList();
            return dotList;
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

        public static List<Pipe> GetPipeList()
        {

            // Vertical2
            //Point2D topLeft = new Point2D() { X = 5570422, Y = 5679728 };
            //Point2D bottomRight = new Point2D() { X = 5590485, Y = 5670339 };

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjects
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            var dotList = domainGrouppedObjects[ObjectTypes.Pipe]
                //.Where(y => y.Geometry[0].X >= topLeft.X && y.Geometry[0].X <= bottomRight.X && y.Geometry[0].Y <= topLeft.Y && y.Geometry[0].Y >= bottomRight.Y )
                //.Where(y => y.Label.StartsWith("p-3-5"))   //p-3-534            
                .Select(x => new Pipe()
                {
                    ID = x.ID,
                    Label = x.Label,
                    //Path = x.Geometry.Select(p => XyToLonLat(p)).ToList(),
                    Path = x.Geometry,
                })
                //.Take(300)
                .ToList();
            return dotList;
        }



        public static List<Dot> GetJunctionRecalcList(double width, double height)
        {
            var pointTopLeft = GetPointTopLeft();
            var pointBottomRight = GetPointBottomRight();
            var xFactor = width / (pointBottomRight.X - pointTopLeft.X);
            var yFactor = height / (pointBottomRight.Y - pointTopLeft.Y);

            var junctionList = GetJunctionList();
            //junctionList.ForEach(p => p.Center = new Point2D((p.Center.X - pointTopLeft.X) * xFactor, ((pointBottomRight.Y - pointTopLeft.Y) - (p.Center.Y - pointTopLeft.Y)) * yFactor));
            junctionList.ForEach(p => p.Center = new Point2D((p.Center.X - pointTopLeft.X) * xFactor, (pointBottomRight.Y - p.Center.Y) * yFactor));
            return junctionList;
        }


        //public static List<Pipe> GetPipeRecalcList(double width, double height, double margin)
        //{
        //    var pointTopLeft = GetPointTopLeft();
        //    var pointBottomRight = GetPointBottomRight();
        //    var xFactor = width / (pointBottomRight.X - pointTopLeft.X);
        //    var yFactor = height / (pointBottomRight.Y - pointTopLeft.Y);

        //    var list = GetPipeList();
        //    list.ForEach(t => t.Path.ToList().ForEach(p => { p.X = (p.X - pointTopLeft.X) * xFactor; p.Y = (pointBottomRight.Y - p.Y) * yFactor; }));
        //    return list;
        //}






        //private static Point2D XyToLonLat(Point2D point2D)
        //{
        //    //var xDiv = 345026.4027225150;
        //    //var yDiv = 110831.2189204590;

        //    var moveXx = -63.4400885695583;
        //    var multiXx = 0.0000142625456859728;

        //    var moveYy = 0.562678511340267;
        //    var multiYy = 0.00000892357598435176;

        //    return new Point2D() { X = moveXx + point2D.X * multiXx, Y = moveYy + point2D.Y * multiYy };
        //}

    }
    }
