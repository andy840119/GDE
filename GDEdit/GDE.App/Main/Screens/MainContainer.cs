using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;

namespace GDE.App.Main.Screens
{
    public class MainContainer : Container
    {
        private MainScreen screen;

        public MainContainer()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new ScreenStack(screen = new MainScreen())
            {
                RelativeSizeAxes = Axes.Both,
            });
        }
    }
}
