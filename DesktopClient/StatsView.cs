using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;
using DesktopClient.Annotations;
using Microsoft.AspNet.SignalR.Client;


namespace DesktopClient
{
    public class StatsView : INotifyPropertyChanged
    {
        private int _retries = 3;

        private readonly PerformanceCounter _perfCountCpuLoad = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        private readonly PerformanceCounter _perfCountSysMem = new PerformanceCounter("Memory", "% Committed Bytes In Use");

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private readonly string _serverPath = ConfigurationManager.AppSettings["server"];

        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;

        private bool _connected;
        private Task _connectionTask;

        private int _cpu;
        public int Cpu
        {
            get { return _cpu; }
            set
            {
                _cpu = value;
               OnPropertyChanged();
            }
        }

        private int _ram;
        public int Ram
        {
            get { return _ram; }
            set
            {
                _ram = value;
                OnPropertyChanged();
            }
        }

        private string _specs;
        public string Specs
        {
            get { return _specs; }
            set
            {
                _specs = value;
                OnPropertyChanged();
            }
        }

        public StatsView()
        {
            Specs = $"{Environment.UserName}@{Environment.MachineName}";

            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

            _hubConnection = new HubConnection(_serverPath);
            _hubProxy = _hubConnection.CreateHubProxy("statsReportHub");

            _connectionTask = Connect();
            
        }

        public async Task Connect()
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
            Cpu = (int) _perfCountCpuLoad.NextValue();
            Ram = (int) _perfCountSysMem.NextValue();
            _dispatcherTimer.Start();

            if (_connected)
            {
               _hubProxy.Invoke("reportStats", Specs ,_cpu, _ram);
            }
            else if (_retries-- != 0 && _connectionTask.IsCompleted)
            {
                _connectionTask = Connect();
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
