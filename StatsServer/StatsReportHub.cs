using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace StatsServer
{
    [HubName("statsReportHub")]
    public class StatsReportHub:Hub
    {
        [HubMethodName("reportStats")]
        public void ReportStats(int cpuLoad, int cpuFreq, int cpuTemp, int ram)
        {
            GlobalHost.ConnectionManager.GetHubContext<StatsHub>()
                .Clients.All()
                .sendStats(cpuLoad, cpuFreq, cpuTemp, ram);
        }
    }
}
