using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient
{
    public class Stats
    {
        public Stats()
        {

        }

        public Stats(int cpuLoad, int cpuFreq, int cpuTemp, int ram)
        {
            CpuLoad = cpuLoad;
            CpuFreq = cpuFreq;
            CpuTemp = cpuTemp;
            Ram = ram;
        }

        public int CpuLoad { get; private set; }
        public int CpuFreq { get; private set; }
        public int CpuTemp { get; private set; }
        public int Ram { get; private set; }

    }
}
