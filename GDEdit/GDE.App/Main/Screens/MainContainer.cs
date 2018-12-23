using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;

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
            Add(screen = new MainScreen());
        }
    }
}
