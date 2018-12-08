using osu.Framework.Allocation;
using osu.Framework.Graphics;
using GDE.App.Main.Screens;

namespace GDE.App.Main
{
    public class GDEApp : GDEAppBase
    {
        private MainContainer container;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRange(new Drawable[]
            {
                container = new MainContainer()
            });
        }
    }
}
