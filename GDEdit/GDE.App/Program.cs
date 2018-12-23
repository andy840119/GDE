using osu.Framework;
using osu.Framework.Platform;
using GDE.App.Main;

namespace GDE.App
{
    public static class Program
    {
        public static void Main()
        {
            Game app = new GDEApp();
            DesktopGameHost host = Host.GetSuitableHost("GDE");

            host.Run(app);
        }
    }
}