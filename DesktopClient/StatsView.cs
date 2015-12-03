using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using DesktopClient.Annotations;
using Microsoft.AspNet.SignalR.Client;

namespace DesktopClient
{
    public class StatsView : INotifyPropertyChanged
    {
        private readonly PerformanceCounter _perfCountCpuLoad = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        private readonly PerformanceCounter _perfCountCpuTemp = new PerformanceCounter("Thermal Zone Information", "Temperature", @"\_TZ.THM");
        private readonly PerformanceCounter _perfCountSysMem = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        private readonly PerformanceCounter _perfCountFreq = new PerformanceCounter("Processor Information", "Processor Frequency", "_Total");

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private const string ServerPath = "http://localhost:63637/signalr";

        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;

        private bool _connected;


        private Stats _stats = new Stats();

        public Stats CurrentStats
        {
            get { return _stats; }
            set
            {
                _stats = value;
               OnPropertyChanged();
            }
        }

        public StatsView()
        {
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

            _hubConnection = new HubConnection(ServerPath);
            _hubProxy = _hubConnection.CreateHubProxy("statsReportHub");
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
            var stats = new Stats((int)_perfCountCpuLoad.NextValue(), (int)_perfCountFreq.NextValue(), (int)_perfCountCpuTemp.NextValue() - 273, (int)_perfCountSysMem.NextValue());
            CurrentStats = stats;
            _dispatcherTimer.Start();
            if (_connected)
            {
                _hubProxy.Invoke("reportStats", stats.CpuLoad, stats.CpuFreq, stats.CpuTemp, stats.Ram);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
