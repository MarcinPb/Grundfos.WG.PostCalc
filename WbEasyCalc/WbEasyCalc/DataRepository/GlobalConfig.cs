using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataModel;
using DataRepository.WbEasyCalcData;

namespace DataRepository
{
    public static class GlobalConfig
    {
        public static IWbEasyCalcDataListRepository WbEasyCalcDataRepo { get; private set; }


        private static List<ZoneItem> _zoneList;
        public static List<ZoneItem> ZoneList
        {
            get
            {
                if (_zoneList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(GlobalConfig.CnnString("TWDB")))
                    {
                        _zoneList = cnn.Query<ZoneItem>("dbo.spZoneList", commandType: CommandType.StoredProcedure).ToList();
                        return _zoneList;
                    }
                }
                return _zoneList;
            }
            set => throw new System.NotImplementedException();
        }

        private static List<IdNamePair> _yearList;
        public static List<IdNamePair> YearList
        {
            get
            {
                if (_yearList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(GlobalConfig.CnnString("TWDB")))
                    {
                        _yearList = cnn.Query<IdNamePair>("dbo.spYearList", commandType: CommandType.StoredProcedure).ToList();
                        return _yearList;
                    }
                }
                return _yearList;
            }
            set => throw new System.NotImplementedException();
        }

        private static List<IdNamePair> _montList;
        public static List<IdNamePair> MonthList
        {
            get
            {
                if (_montList == null)
                {
                    using (IDbConnection cnn = new SqlConnection(GlobalConfig.CnnString("TWDB")))
                    {
                        _montList = cnn.Query<IdNamePair>("dbo.spMonthList", commandType: CommandType.StoredProcedure).ToList();
                        return _montList;
                    }
                }
                return _montList;
            }
            set => throw new System.NotImplementedException();
        }




        public static void InitializeConnection(DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                WbEasyCalcDataRepo = new WbEasyCalcDataListRepository();
            }
            else if (db== DatabaseType.TextFile)
            {
                // todo - Set up the text Connector properly
                //TextConnector text = new TextConnector();
                WbEasyCalcDataRepo= null;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
