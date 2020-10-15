﻿using System;
using System.Configuration;
using DataModel;
using DataRepository;
using WbEasyCalc;

namespace GlobalRepository
{
    public static class GlobalConfig
    {
        // ex: GlobalConfig.Opc.RunOpcPublish(zoneRomanNo, easyCalcDataInput, easyCalcDataOutput); 
        public static IOpcServer OpcServer { get; set; }

        public static IWbEasyCalcExcel WbEasyCalcExcel { get; set; }

        public static IDataRepository DataRepository { get; private set; }


        public static void InitializeConnection(DatabaseType db)
        {
            OpcServer = new OpcServer(ConfigurationManager.AppSettings["opcAddress"]);
            WbEasyCalcExcel = new WbEasyCalcExcel(ConfigurationManager.AppSettings["ExcelTemplateFileName"]);

            if (db == DatabaseType.Sql)
            {
                DataRepository = new DataRepository.DataRepository(CnnString("TWDB"));
            }
            else if (db == DatabaseType.TextFile)
            {
                // todo - Set up the text Connector properly
                //TextConnector text = new TextConnector();
                DataRepository = null;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
