using System;
using System.Configuration;
using System.Reflection;

namespace DataRepository
{
    public class Settings
    {
        public static string ZtmBusStopListFileName { get; private set; }
        public static string OsmBusStopListFileName { get; private set; }
        public static string ResultFileName { get; private set; }
        public static string Report01FileName { get; private set; }
        public static string OsmUrl { get; private set; }
        public static string OsmContentType { get; private set; }
        public static string OsmFilter { get; private set; }
        public static double MaxMeterDistance { get; private set; }
        public static string WorkArea { get; private set; }
        public static string ResultFileName2 { get; set; }

        public static void InitializeSettings()
        {
            //ZtmBusStopListFileName = ConfigurationManager.AppSettings["ZtmBusStopListFileName"];
            //OsmBusStopListFileName = ConfigurationManager.AppSettings["OsmBusStopListFileName"];
            //ResultFileName = ConfigurationManager.AppSettings["ResultFileName"];
            //Report01FileName = ConfigurationManager.AppSettings["Report01FileName"];
            //OsmUrl = ConfigurationManager.AppSettings["OsmUrl"];
            //OsmContentType = ConfigurationManager.AppSettings["OsmContentType"];
            //OsmFilter = ConfigurationManager.AppSettings["OsmFilter"];
            //MaxMeterDistance = Convert.ToDouble(ConfigurationManager.AppSettings["MaxMeterDistance"]);


            var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            ZtmBusStopListFileName = appConfig.AppSettings.Settings["ZtmBusStopListFileName"].Value;
            OsmBusStopListFileName = appConfig.AppSettings.Settings["OsmBusStopListFileName"].Value;
            ResultFileName = appConfig.AppSettings.Settings["ResultFileName"].Value;
            Report01FileName = appConfig.AppSettings.Settings["Report01FileName"].Value;
            OsmUrl = appConfig.AppSettings.Settings["OsmUrl"].Value;
            OsmContentType = appConfig.AppSettings.Settings["OsmContentType"].Value;
            OsmFilter = appConfig.AppSettings.Settings["OsmFilter"].Value;
            MaxMeterDistance = Convert.ToDouble(appConfig.AppSettings.Settings["MaxMeterDistance"].Value);
            ResultFileName2 = appConfig.AppSettings.Settings["ResultFileName2"].Value;

            if (
                ZtmBusStopListFileName==null ||
                OsmBusStopListFileName == null ||
                ResultFileName == null ||
                Report01FileName == null ||
                OsmUrl == null ||
                OsmContentType == null ||
                OsmFilter == null ||
                ResultFileName2 == null
            )
            {
                throw new Exception("Wrong WpfAppUi.exe.config file. Application will be closed.");
            }
        }
    }
}
