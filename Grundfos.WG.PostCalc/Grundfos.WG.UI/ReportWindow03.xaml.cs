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
using Xceed.Wpf.Toolkit.Primitives;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow03 : Window
    {
        private Microsoft.Reporting.WinForms.ReportDataSource _reportDataSource1;
        private string _conStr = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.Setting"].ConnectionString;
        public ReportWindow03()
        {
            InitializeComponent();
            ReportViewer1.Load += ReportViewer_Load;
        }

        private const int SelectAllOption = -2;
        private List<Item> _optionList;
        private List<Item> _demandList;
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            TopQtyTextBox.Text = "5";

            DataTable dtZoneList = GetZoneList();
            var optionList1 = new List<Item>() { new Item(){ Value=SelectAllOption, Name="<Select All>", Selected=true}};
            var optionList2 = dtZoneList.AsEnumerable().Select(x => new Item()
            {
                Value= Convert.ToInt32(x["AutoZoneId"]),
                Name = Convert.ToString(x["ZoneName"]),
                Selected = true
            });
            _optionList = optionList1.Union(optionList2).ToList();

            CheckComboBox1.ItemsSource = _optionList;
            CheckComboBox1.DisplayMemberPath = "Name";
            CheckComboBox1.SelectedMemberPath = "Selected";
            CheckComboBox1.ValueMemberPath = "Value";
            CheckComboBox1.ItemSelectionChanged += CheckComboBox1_ItemSelectionChanged;

            DataTable dtDemandList = GetDemandList();
            var demandList1 = new List<Item>() { new Item(){ Value=SelectAllOption, Name="<Select All>", Selected=true}};
            var demandList2 = dtDemandList.AsEnumerable().Select(x => new Item()
            {
                Value= Convert.ToInt32(x["AutoDemandId"]),
                Name = Convert.ToString(x["DemandName"]),
                Selected = true
            });
            _demandList = demandList1.Union(demandList2).ToList();

            CheckComboBox2.ItemsSource = _demandList;
            CheckComboBox2.DisplayMemberPath = "Name";
            CheckComboBox2.SelectedMemberPath = "Selected";
            CheckComboBox2.ValueMemberPath = "Value";
            CheckComboBox2.ItemSelectionChanged += CheckComboBox2_ItemSelectionChanged;

            _reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource
            {
                Name = "DataSet1",
                Value = FillDataTable(_conStr)
            };
            //ReportViewer1.LocalReport.ReportPath = "Data/Rdlc/ReportWg02_02.rdlc";
            ReportViewer1.LocalReport.ReportPath = "Data/Rdlc/WgZoneFlowComparison.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            //ReloadData();
            ReportViewer1.RefreshReport();
        }

        private void CheckComboBox2_ItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            if (((Item)e.Item).Value != SelectAllOption) return;

            var selected = ((Item)e.Item).Selected;
            foreach (Item item in _demandList)
            {
                item.Selected = !selected;
            }

            CheckComboBox2.ItemsSource = null;
            CheckComboBox2.ItemsSource = _demandList;
        }

        private void CheckComboBox1_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            if (((Item) e.Item).Value != SelectAllOption) return;

            var selected = ((Item)e.Item).Selected;
            //throw new NotImplementedException();
            foreach (Item item in _optionList)
            {
                //if (item.Value != 6773)
                //{
                    item.Selected = !selected;
                //}
            }

            CheckComboBox1.ItemsSource = null;
            CheckComboBox1.ItemsSource = _optionList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckComboBox1.ItemsSource = _optionList;
            ReloadData();
        }
        private void ReloadData()
        {
            DataTable dt = FillDataTable(_conStr);

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
            string query = "spReportZoneComparison";

            var optionListStr = _optionList
                .Where(x => x.Value!= SelectAllOption && x.Selected)
                .Select(x => x.Value)
                .Aggregate(string.Empty, (current, next) => current + "," + next);
            var demandListStr = _demandList
                .Where(x => x.Value!= SelectAllOption && x.Selected)
                .Select(x => x.Value)
                .Aggregate(string.Empty, (current, next) => current + "," + next);

            using (SqlConnection sqlConn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@TopQty", SqlDbType.NVarChar).Value = Convert.ToInt32(TopQtyTextBox.Text);
                    //cmd.Parameters.Add("@ZoneList", SqlDbType.NVarChar).Value = optionListStr;
                    //cmd.Parameters.Add("@DemandList", SqlDbType.NVarChar).Value = demandListStr;
                    sqlConn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    sqlConn.Close();
                    return dt;
                }
            }
        }
        public DataTable GetZoneList()
        {
            string query = "SELECT AutoZoneId, ZoneName FROM [dbo].[Wg_Zone] ORDER BY [AutoZoneId]";
            //string query = "SELECT [Name] ,[Value] FROM [XLSX_MATRIX]...[ApplicationSettings$]";

            using (SqlConnection sqlConn = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    sqlConn.Close();
                    return dt;
                }
            }
        }
        public DataTable GetDemandList()
        {
            string query = "SELECT AutoDemandId, DemandName FROM [dbo].[Wg_Demand] ORDER BY [DemandName]";

            using (SqlConnection sqlConn = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    sqlConn.Close();
                    return dt;
                }
            }
        }
    }
}
