using osu.Framework;
using osu.Framework.Platform;
using GDE.App.Main;

namespace GDE.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Game app = new GDEApp();
            DesktopGameHost host = Host.GetSuitableHost("GDE");

            host.Run(app);
        }
    }
}