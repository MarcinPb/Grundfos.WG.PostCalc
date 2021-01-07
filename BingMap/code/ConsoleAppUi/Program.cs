using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;

namespace ConsoleAppUi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - Create OSM (Files\\Osm\\BusStopList.xml) file");
            Console.WriteLine("2 - Show area size based on ZTM (Files\\Ztm\\stops.txt) file");
            Console.WriteLine("3 - Create OSM-ZTM (Files\\Result.xml) file");
            Console.WriteLine("4 - Create Report (Files\\Reports\\Report01.csv) file");
            Console.WriteLine("9 - Exit");
            Console.WriteLine();
            
            Window_Loaded();

            while (true)
            {
                Console.Write("Enter input:");
                string line = Console.ReadLine();
                switch (line)
                {
                    case "1":
                        DownloadBusStopListToFile_Click();
                        break;
                    case "2":
                        GetZtmArea_Click();
                        break;
                    case "3":
                        CreateOsmZtmFile_Click();
                        break;
                    case "4":
                        CreateReportFile01_Click();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        private static void Window_Loaded()
        {
            try
            {
                Settings.InitializeSettings();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static async void DownloadBusStopListToFile_Click()
        {
            try
            {
                Console.WriteLine("Creating 'Files\\Osm\\BusStopList.xml' file ...");
                await Methods.DownloadBusStopListToFileAsync();
                Console.WriteLine("\n'Files\\Osm\\BusStopList.xml' file was created successfully.");
                Console.Write("Enter input:");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static async void GetZtmArea_Click()
        {
            try
            {
                Console.WriteLine("Getting area size based on ZTM file ...");
                string msg = await Task.Run(() => Methods.GetZtmArea());
                Console.WriteLine($"\nArea size based on ZTM file: \"{msg}\".");
                Console.Write("Enter input:");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static async void CreateOsmZtmFile_Click()
        {
            try
            {
                Console.WriteLine("Creating 'Files\\Result.xml' file ...");
                await Task.Run(() => Methods.CreateZtmOsmFile());
                Console.WriteLine("\n'Files\\Result.xml' file was created successfully.");
                Console.Write("Enter input:");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static async void CreateReportFile01_Click()
        {
            try
            {
                Console.WriteLine("Creating Report01.csv file ...");
                await Task.Run(() => Methods.CreateReportFile01());
                Console.WriteLine("\n'Files\\Reports\\Report01.csv' file was created successfully.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

    }
}
