using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osu.Framework.Graphics;

namespace GDE.App.Main.Screens
{
    public class MainScreen : Screen
    {
        private Box box;
        private TextureStore ts;

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
            ts = texStore;

            Add(new DrawSizePreservingFillContainer
            {
                Strategy = DrawSizePreservationStrategy.Average,
                Children = new Drawable[]
                {
                    box = new Box
                    {
                        Size = new Vector2(190, 190)
                    }
                }
            });
        }
    }
}
