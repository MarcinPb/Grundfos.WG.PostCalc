/*
MVVM Passing EventArgs As Command Parameter
https://stackoverflow.com/questions/6205472/mvvm-passing-eventargs-as-command-parameter

*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DataRepository;
using Microsoft.Maps.MapControl.WPF;
using NLog;
using WpfApplication1.Repository;
using WpfApplication1.Utility;
using WpfApplication1.ViewModel;

namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private MainRepo _mainRepo;
        private Location _startLocation;


        private Location _center;
        public Location Center
        {
            get => _center;
            set { _center = value; RaisePropertyChanged(); }
        }

        private string _log;
        public string Log
        {
            get => _log;
            set { _log = value; RaisePropertyChanged(); }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            get => _zoomLevel;
            set { _zoomLevel = value; RaisePropertyChanged(); }
        }

        private double _mapOpacity;
        public double MapOpacity
        {
            get => _mapOpacity;
            set { _mapOpacity = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<IMapItem> MapItemList { get; set; }

        public RelayCommand DownloadBusStopListToFileCmd { get; }

        private async void DownloadBusStopListToFileExecute()
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Log += $"Downloading bus stop list to file has started.\n";
                //DownloadBusStopListToFile.IsEnabled = false;

                await Methods.DownloadBusStopListToFileAsync();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                //ResultsWindow.Text += $"Total execution time: { elapsedMs }.\n";
                //MessageBox.Show($"Total execution time: { elapsedMs }.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                Log += $"Downloading bus stop list to file has finished. Total execution time: { elapsedMs }.\n";
                //DownloadBusStopListToFile.IsEnabled = true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //DownloadBusStopListToFile.IsEnabled = true;
            }
        }



        public RelayCommand AddPushpinCmd { get; }
        public RelayCommand AddPushpinDotCmd { get; }
        public RelayCommand ChangePushpinNameCmd { get; }
        public RelayCommand AddLineCmd { get; }

        public RelayCommand ExportCmd { get; }
        private void ExportExecute()
        {
            if (MapOpacity == 1)
            {
                MapOpacity = 0.7;
                MapItemList.Clear();
            }
            else
            {
                MapOpacity = 1;
            }
        }
        public RelayCommand SaveCmd { get; }

        public RelayCommand<object> MouseLeftButtonDownCmd { get; }
        private void MouseLeftButtonDown(object obj)
        {
            if (obj is string name)
            {
                _mainRepo.DeleteMapItem(name);
                Log += $"Connection {name} was removed.\n";
            }
            else if (obj is List<Location> locationList)
            {
                if (locationList.Count==2)
                {
                    _startLocation = locationList[0];
                }
                else
                {
                    Log = $"Mouse location: {locationList[0].Latitude}  {locationList[0].Longitude}\n" + Log;
                }
            }
        }

        public RelayCommand<object> MouseLeftButtonUpCmd { get; }
        private void MouseLeftButtonUp(object obj)
        {
            if (_startLocation != null)
            {
                if (obj is Location endLocation)
                {
                    if (MapOpacity == 1)
                    {
                        var stopConnection = _mainRepo.AddMapItem(_startLocation, endLocation);
                        Log += $"A new connection between '{stopConnection.StartStop.Name}' and '{stopConnection.EndStop.Name}' was added.\n";
                    }
                    else
                    {
                        _mainRepo.LoadMapItemList(_startLocation, endLocation);
                        MapOpacity = 1;
                    }
                }
                _startLocation = null;
            }
        }

        public RelayCommand<object> MouseMoveCmd { get; } = new RelayCommand<object>(null);

        public RelayCommand WindowLoadedCmd { get; } 


        public MainWindowViewModel()
        {
            Logger.Info("App started");
            DownloadBusStopListToFileCmd = new RelayCommand(DownloadBusStopListToFileExecute);

            WindowLoadedCmd = new RelayCommand(WindowLoaded);

            AddPushpinCmd = new RelayCommand(() =>_mainRepo.AddPushpin());
            AddPushpinCmd = new RelayCommand(() =>_mainRepo.AddPushpin());
            ChangePushpinNameCmd = new RelayCommand(() => _mainRepo.ChangePushpinName());
            AddLineCmd = new RelayCommand(() => _mainRepo.AddLine());
            ExportCmd = new RelayCommand(ExportExecute);
            SaveCmd = new RelayCommand(() => _mainRepo.Save());

            AddPushpinDotCmd = new RelayCommand(() =>_mainRepo.AddPushpinDot());

            MapOpacity = 1;
            _mainRepo = new MainRepo();
            Center = _mainRepo.GetCenterLocation();
            ZoomLevel = _mainRepo.GetZoomLevel();
            MapItemList = _mainRepo.GetMapItemList();

            MouseLeftButtonDownCmd = new RelayCommand<object>(MouseLeftButtonDown);
            MouseLeftButtonUpCmd = new RelayCommand<object>(MouseLeftButtonUp);
        }

        private void WindowLoaded()
        {
            try
            {

                Settings.InitializeSettings();

                //throw new Exception("BlaBla");
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //this.Close();
            }
        }
    }
}
