using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModel;
using DataRepository.WbEasyCalcData;

namespace DataRepository
{
    public class DataRepository : IDataRepository
    {
        private readonly string _cnnString;

        public IWbEasyCalcDataListRepository WbEasyCalcDataListRepository { get; private set; }


        public DataRepository(string cnnString)
        {
            _cnnString = cnnString;
            WbEasyCalcDataListRepository = new WbEasyCalcDataListRepository(_cnnString);
        }

        private List<ZoneItem> _zoneList;
        public List<ZoneItem> ZoneList
        {
            get
            {
                if (_zoneList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(_cnnString))
                    {
                        _zoneList = cnn.Query<ZoneItem>("dbo.spZoneList", commandType: CommandType.StoredProcedure).ToList();
                        return _zoneList;
                    }
                }
                return _zoneList;
            }
            //set => throw new System.NotImplementedException();
        }

        private List<IdNamePair> _yearList;
        public List<IdNamePair> YearList
        {
            get
            {
                if (_yearList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(_cnnString))
                    {
                        _yearList = cnn.Query<IdNamePair>("dbo.spYearList", commandType: CommandType.StoredProcedure).ToList();
                        return _yearList;
                    }
                }
                return _yearList;
            }
            set => throw new System.NotImplementedException();
        }

        private List<IdNamePair> _montList;
        public List<IdNamePair> MonthList
        {
            get
            {
                if (_montList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(_cnnString))
                    {
                        _montList = cnn.Query<IdNamePair>("dbo.spMonthList", commandType: CommandType.StoredProcedure).ToList();
                        return _montList;
                    }
                }
                return _montList;
            }
            set => throw new System.NotImplementedException();
        }

        public GisModelScadaData GetGisModelScadaData(int yearNo, int monthNo, int zoneId)
        {
            using (IDbConnection connection = new SqlConnection(_cnnString))
            {
                var p = new DynamicParameters();
                p.Add("@YearNo", yearNo);
                p.Add("@MonthNo", monthNo);
                p.Add("@ZoneId", zoneId);
                p.Add("@ZoneSale", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@NetworkLength", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@CustomersQuantity", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@SystemInputVolume", dbType: DbType.Double, direction: ParameterDirection.Output);

                connection.Execute("dbo.spGisModelScadaData", p, commandType: CommandType.StoredProcedure);

                return new GisModelScadaData()
                {
                    ZoneSale = p.Get<double>("@ZoneSale"),
                    NetworkLength = p.Get<double>("@NetworkLength"),
                    CustomersQuantity = p.Get<double>("@CustomersQuantity"),
                    SystemInputVolume = p.Get<double>("@SystemInputVolume"),
                };
            }
        }

    }
}
