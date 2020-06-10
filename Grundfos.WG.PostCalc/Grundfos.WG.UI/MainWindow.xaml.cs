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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ReportViewer1.Load += ReportViewer_Load;
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
