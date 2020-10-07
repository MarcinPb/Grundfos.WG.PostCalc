using System.Configuration;
using DataRepository.WbEasyCalcData;

namespace DataRepository
{
    public static class GlobalConfig
    {
        public static IWbEasyCalcDataListRepository WbEasyCalcDataRepo { get; private set; } 

        public static void InitializeConnection(DatabaseType db)
        {
            //switch (db)
            //{
            //    case DatabaseType.Sql:
            //        WbEasyCalcDataRepo = new SqlConnector();
            //        break;
            //    case DatabaseType.TextFile:
            //        WbEasyCalcDataRepo = new TextConnector();
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(db), db, null);
            //}

            if (db == DatabaseType.Sql)
            {
                // todo - Set up the SQL Connector properly
                WbEasyCalcDataListRepository sql = new WbEasyCalcDataListRepository();
                WbEasyCalcDataRepo = sql;
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
