using System;
using System.Threading.Tasks;
using System.Windows;
using DataRepository;

namespace WpfAppUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.InitializeSettings();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private async void DownloadBusStopListToFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                DownloadBusStopListToFile.IsEnabled = false;

                //Methods.DownloadBusStopListToFile();
                await Methods.DownloadBusStopListToFileAsync();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                ResultsWindow.Text += $"Total execution time: { elapsedMs }.\n";
                DownloadBusStopListToFile.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DownloadBusStopListToFile.IsEnabled = true;
            }
        }



        private async void GetZtmArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetZtmArea.IsEnabled = false;

                string msg = await Task.Run(() => Methods.GetZtmArea());

                ResultsWindow.Text += $"Area size based on ZTM file: \"{msg}\".\n";
                GetZtmArea.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GetZtmArea.IsEnabled = true;
            }
        }




        private async void CreateOsmZtmFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                CreateOsmZtmFile.IsEnabled = false;

                //Methods.CreateZtmOsmFile();
                await Task.Run(() => Methods.CreateZtmOsmFile());

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                ResultsWindow.Text += $"Total execution time: { elapsedMs }.\n";
                CreateOsmZtmFile.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CreateOsmZtmFile.IsEnabled = true;
            }
        }

        private async void CreateReportFile01_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                CreateReportFile01.IsEnabled = false;

                await Task.Run(() => Methods.CreateReportFile01());

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                ResultsWindow.Text += $"Total execution time: { elapsedMs }.\n";
                CreateReportFile01.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CreateReportFile01.IsEnabled = true;
            }
        }
        private void ShowMap_Click(object sender, RoutedEventArgs e)
        {
            var mapWindow = new MapWindow();
            mapWindow.ShowDialog();
        }
    }
}
