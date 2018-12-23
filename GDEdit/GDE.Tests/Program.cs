using System;
using osu.Framework;
using osu.Framework.Platform;

namespace GDE.Tests
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (DesktopGameHost host = Host.GetSuitableHost(@"GDE Tests", true))
            {
                host.Run(new GDETestBrowser());
            }
        }
    }
}
