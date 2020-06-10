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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private Microsoft.Reporting.WinForms.ReportDataSource _reportDataSource1;
        private string conStr = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.Setting"].ConnectionString;
        public ReportWindow()
        {
            InitializeComponent();
            ReportViewer1.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {

            _reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource
            {
                Name = "DataSet1",
                Value = FillDataTable(conStr)
            };
            ReportViewer1.LocalReport.ReportPath = "Data/Rdlc/ReportWg01.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            //ReloadData();
            ReportViewer1.RefreshReport();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReloadData();
        }
        private void ReloadData()
        {
            DataTable dt = FillDataTable(conStr);

            // Create ReportDataSource
            //Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            //reportDataSource1.Name = "DataSet1";
            //reportDataSource1.Value = dt;

            //ReportViewer
            _reportDataSource1.Value = dt;
            ReportViewer1.RefreshReport();
        }
        public DataTable FillDataTable(string conStr)
        {
            string query = "SELECT * FROM dbo.ZoneFlowComparison ORDER BY D_TIME DESC";

            using (SqlConnection sqlConn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
    }
}
