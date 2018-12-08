using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using System.Drawing;
using osu.Framework;

namespace GDE.App.Main
{
    public class GDEAppBase : Game
    {
        private string mainResourceFile = "GDE.Resources.dll";

        private DependencyContainer dependencies;
        private Storage storage;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public GDEAppBase()
        {
            Name = @"GD Edit";

            storage = new DesktopStorage("GD Edit", Host);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
            dependencies.Cache(storage);

            Resources.AddStore(new DllResourceStore(mainResourceFile));
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var config = new FrameworkConfigManager(storage);

            config.Set(FrameworkSetting.WindowMode, WindowMode.Windowed);
            config.Set(FrameworkSetting.WindowedSize, new Size(1280, 720));

            Window.WindowBorder = WindowBorder.Fixed;
            Window.Title = @"GD Edit";

            Window.SetupWindow(config);
        }
    }
}
