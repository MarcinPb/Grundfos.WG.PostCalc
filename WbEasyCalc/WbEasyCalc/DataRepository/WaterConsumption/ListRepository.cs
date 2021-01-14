using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DataRepository.WaterConsumption
{
    public class ListRepository : IListRepository
    {

        private List<DataModel.WaterConsumption> _list;
        public List<DataModel.WaterConsumption> GetList()
        {
            using (IDbConnection cnn = new SqlConnection(_cnnString))
            {
                string sql = "dbo.spWaterConsumptionList";
                _list = cnn.Query<DataModel.WaterConsumption>(sql, commandType: CommandType.StoredProcedure).ToList();
                return _list;
            }
        }

        public DataModel.WaterConsumption GetItem(int id)
        {
            if (id != 0)
            {
                var customer = _list.Single(f => f.WaterConsumptionId == id);
                return (DataModel.WaterConsumption)customer?.Clone();
            }
            else
            {
                return new DataModel.WaterConsumption();
            }
        }

        public DataModel.WaterConsumption SaveItem(DataModel.WaterConsumption model)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@ZoneId", model.ZoneId);
                p.Add("@YearNo", model.YearNo);
                p.Add("@MonthNo", model.MonthNo);
                p.Add("@Description", model.Description);
                p.Add("@IsArchive", model.IsArchive);
                p.Add("@IsAccepted", model.IsAccepted);

                connection.Execute("dbo.spWaterConsumptionSave", p, commandType: CommandType.StoredProcedure);

                model.WaterConsumptionId = p.Get<int>("@id");

                return model;
            }
        }

        public bool DeleteItem(int id)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@id", id);
                connection.Execute("dbo.spWaterConsumptionDelete", p, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public bool DeleteItem(List<int> idList)
        {
            throw new NotImplementedException();
        }

        public int Clone(int id)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@id", id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                connection.Execute("dbo.spWaterConsumptionClone", p, commandType: CommandType.StoredProcedure);

                var wbEasyCalcDataId = p.Get<int>("@id");

                return wbEasyCalcDataId;
            }
        }


        private readonly string _cnnString;
        public ListRepository(string cnnString)
        {
            _cnnString = cnnString;
        }
    }
}
