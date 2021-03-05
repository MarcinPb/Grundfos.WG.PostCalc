using Dapper;
using Database.DataModel;
using GeometryModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        private static List<DomainObjectData> domainObjectDataList = GetDomainObjectDataList();
        private static Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = GetWgObjectTypeList();

        public static List<DomainObjectData> GetDomainObjectDataList()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Files\\Wg\\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<DomainObjectData> domainObjects = (List<DomainObjectData>)formatter.Deserialize(stream);
            stream.Close();

            return domainObjects;
        }
        public static Dictionary<ObjectTypes, List<DomainObjectData>> GetWgObjectTypeList()
        {
            Dictionary<ObjectTypes, List<DomainObjectData>> domainGrouppedObjects = domainObjectDataList
                .GroupBy(x => x.ObjectType)
                .ToDictionary(x => x.Key, x => x.ToList());

            return domainGrouppedObjects;
        }

        public static DomainObjectData GetItem(int id)
        {
            return domainObjectDataList.FirstOrDefault(x => x.ID==id);
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


        public static void SaveInfra()
        {
            var list = GetPipeList().Where(x => (int)x.Fields["HMITopologyStartNodeID"] == (int)x.Fields["HMITopologyStopNodeID"]).ToList();

            //var list = GetCustomerNodeList().Select(x => new InfraNode() 
            var infraObjectList = GetDomainObjectDataList().Select(x => new InfraNode() 
            { 
                NodeId = x.ID,
                NodeTypeId = (int)x.ObjectType,
                Name = x.Label,
                Description = "",
                IsActive = x.IsActive,
                Xx = x.Geometry[0].X,
                Yy = x.Geometry[0].Y,
            })
            //.Take(10)
            .ToList();

            var infraConnectionCustomerNodeList = GetCustomerNodeList()
                .Select(x => new  
                    {
                        ParentNodeId = GetJunctionList().FirstOrDefault(j => j.Label==(string)x.Fields["Demand_AssociatedElement"])?.ID,
                        ChildNodeId = x.ID,
                    })
                .Where(y => y.ParentNodeId!=null)
                .Select(z => new InfraConnection 
                    {
                        ParentNodeId = (int)z.ParentNodeId,
                        ChildNodeId = z.ChildNodeId
                    })
                //.ToList()
                ;

            var infraConnectionPipeStartList = GetPipeList()
                .Select(x => new  
                    {
                        ParentNodeId = (int?)x.Fields["HMITopologyStartNodeID"],
                        ChildNodeId = x.ID,
                    })
                .Where(y => y.ParentNodeId!=null)
                .Select(z => new InfraConnection 
                    {
                        ParentNodeId = (int)z.ParentNodeId,
                        ChildNodeId = z.ChildNodeId,
                        TypeId = 1,
                    })
                //.ToList()
                ;

            var infraConnectionPipeStopList = GetPipeList()
                .Select(x => new  
                    {
                        ParentNodeId = (int?)x.Fields["HMITopologyStopNodeID"],
                        ChildNodeId = x.ID,
                    })
                .Where(y => y.ParentNodeId!=null)
                .Select(z => new InfraConnection 
                    {
                        ParentNodeId = (int)z.ParentNodeId,
                        ChildNodeId = z.ChildNodeId,
                        TypeId = 2,
                    })
                .ToList()
                ;

            var infraConnectionList = infraConnectionCustomerNodeList                
                .Union(infraConnectionPipeStartList)
                .Union(infraConnectionPipeStopList)
                .Where(x => domainObjectDataList.Any(y => x.ChildNodeId==y.ID) && domainObjectDataList.Any(y => x.ParentNodeId==y.ID))
                .ToList();

            List<InfraNodeField2> infraFieldList = MainRepo.GetInfraFieldList();

            var infraObjectFieldList = infraObjectList
                .Join(
                    domainObjectDataList,
                    l => l.NodeId,
                    r => r.ID,
                    (l, r) => new { l.NodeId, l.NodeTypeId, r.Fields }
                )
                .SelectMany(x => x.Fields, (x, lst) => new InfraNodeField { 
                    NodeId = x.NodeId, 
                    NodeTypeId = x.NodeTypeId, 
                    FieldName = lst.Key, 
                    FieldValue = lst.Value 
                })
                .Join(
                    infraFieldList,
                    l => l.NodeTypeId.ToString() + l.FieldName,
                    r => r.NodeTypeId.ToString() + r.Name,
                    (l, r) => new InfraNodeFieldValue { 
                        NodeFieldId = r.NodeFieldId, 
                        NodeId = l.NodeId, 
                        FloatValue = r.DataTypeId==1 ? (double?)Convert.ToDouble(l.FieldValue) : null,
                        StringValue = r.DataTypeId==2 ? (string)l.FieldValue : null,
                    }
                )
                .ToList();

            //var infraObjectFieldValueList = new List<InfraNodeFieldValue>();

            WriteSet(infraObjectList, infraConnectionList, infraObjectFieldList);

            /*
            int resourceId;
            */

        }






        private static void WriteSet(List<InfraNode> infraObjectList, List<InfraConnection> infraConnectionList, List<InfraNodeFieldValue> infraObjectFieldValueList)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql;

                sql = $@"
                    INSERT INTO dbo.tbInfraNode (
                        NodeId,  NodeTypeId,  Name,  Description,  IsActive,  Xx,  Yy
                    ) VALUES (
                        @NodeId, @NodeTypeId, @Name, @Description, @IsActive, @Xx, @Yy
                    )
                ";
                cnn.Execute(sql, infraObjectList);

                sql = $@"
                    INSERT INTO dbo.tbInfraConnection (
                        ParentNodeId,  ChildNodeId, TypeId
                    ) VALUES (
                        @ParentNodeId, @ChildNodeId, @TypeId
                    )
                ";
                cnn.Execute(sql, infraConnectionList);

                sql = $@"
                    INSERT INTO dbo.tbInfraNodeFielddValue (
                        NodeFieldId,  NodeId, FloatValue, StringValue
                    ) VALUES (
                        @NodeFieldId, @NodeId, @FloatValue, @StringValue
                    )
                ";
                cnn.Execute(sql, infraObjectFieldValueList);

            }
        }
        public static List<InfraNodeField2> GetInfraFieldList()
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql;

                sql = $@"SELECT * FROM dbo.tbInfraNodeField;";
                var list = cnn.Query<InfraNodeField2>(sql);
                return list.ToList();
            }
        }

        private static string GetConnectionString(string name = "DapperDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
