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
        public void ReportStats(string specs,int cpuLoad, int cpuFreq,  int ram)
        {
            GlobalHost.ConnectionManager.GetHubContext<StatsHub>()
                .Clients.All
                .updateStats(Context.ConnectionId,specs, cpuLoad, cpuFreq, ram);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            GlobalHost.ConnectionManager.GetHubContext<StatsHub>().Clients.All.removeStats(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
