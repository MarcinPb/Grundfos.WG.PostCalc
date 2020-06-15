using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using Grundfos.WG.ObjectReaders;
using Grundfos.Workbooks;

//using Grundfos.Workbooks;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ReportViewer1.Load += ReportViewer_Load;
        }
        private string _sqliteDbFile = @"C:\WG2TW\WGModel\testOPC.wtg.sqlite";

        private void CreateExcelFile(object sender, RoutedEventArgs e)
        {
            SqliteProxy sqliteProxy = new SqliteProxy(_sqliteDbFile);
            var zoneList = sqliteProxy.GetZoneList();
            var idahoPatternList = sqliteProxy.GetIdahoPatternList();
            var idahoPatternPatternCurveList = sqliteProxy.GetIdahoPatternPatternCurveList();

            var customerMeterList = sqliteProxy.GetCustomerMeterList();

            var objectList = sqliteProxy.GetJunctionList().Union(sqliteProxy.GetHydrantList()).ToList();
            sqliteProxy.FillZoneNamesInWaterDemands(objectList, zoneList);
            sqliteProxy.UpdateCustomerMeterZones(customerMeterList, objectList);
            objectList = objectList.Union(customerMeterList).ToList();
            sqliteProxy.FillPatternNames(objectList, idahoPatternList);

            ExcelWriter.Write(@"C:\WG2TW\Grundfos.WG.PostCalc\newbook.core.xlsx", idahoPatternPatternCurveList, objectList);
            MessageBox.Show("File was created successfully.");
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow{}.Show();
        }
        private void button_Click02(object sender, RoutedEventArgs e)
        {
            new ReportWindow02{}.Show();
        }
        private void button_Click03(object sender, RoutedEventArgs e)
        {
            new ReportWindow03{}.Show();
        }
    }
}
