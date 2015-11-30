using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using Microsoft.AspNet.SignalR.Client;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly StatsView _statsView;

        private readonly PerformanceCounter _perfCountCpuLoad = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        private readonly PerformanceCounter _perfCountCpuTemp = new PerformanceCounter("Thermal Zone Information", "Temperature", @"\_TZ.THM");
        private readonly PerformanceCounter _perfCountSysMem = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        private readonly PerformanceCounter _perfCountFreq = new PerformanceCounter("Processor Information", "Processor Frequency", "_Total");

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private const string ServerPath = "http://localhost:63637/signalr";

        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;

        private bool _connected;

        public MainWindow()
        {
            InitializeComponent();
            _statsView = (StatsView)DataContext;
            Left = SystemParameters.PrimaryScreenWidth - Width;

            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

            _hubConnection=new HubConnection(ServerPath);
            _hubProxy =_hubConnection.CreateHubProxy("statsReportHub");
            Connect();
        }


        public async void Connect()
        {
            try
            {
                await _hubConnection.Start();
                _connected = true;
            }
            catch
            {
                //ignore
            }
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var stats = new Stats((int)_perfCountCpuLoad.NextValue(), (int)_perfCountFreq.NextValue(),(int)_perfCountCpuTemp.NextValue()-273, (int)_perfCountSysMem.NextValue());     
            _statsView.CurrentStats = stats;
            _dispatcherTimer.Start();
            if (_connected)
            {
                _hubProxy.Invoke("reportStats", stats.CpuLoad, stats.CpuFreq, stats.CpuTemp, stats.Ram);
            }
        }
    }
}
