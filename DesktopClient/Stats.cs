namespace DesktopClient
{
    public class Stats
    {
        public Stats()
        {

        }

        public Stats(int cpuLoad, int cpuFreq,  int ram)
        {
            CpuLoad = cpuLoad;
            CpuFreq = cpuFreq;
            Ram = ram;
        }

        public int CpuLoad { get; private set; }
        public int CpuFreq { get; private set; }
        public int Ram { get; private set; }

    }
}
