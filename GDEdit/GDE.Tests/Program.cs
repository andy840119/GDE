using System;
using osu.Framework;
using osu.Framework.Platform;

namespace GDE.Tests
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            using (DesktopGameHost host = Host.GetSuitableHost(@"GDE Tests", true))
            {
                host.Run(new GDETestBrowser());
                return 0;
            }
        }
    }
}
