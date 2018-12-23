using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;

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

            Gamesave.DecryptGamesave();

            Add(new DrawSizePreservingFillContainer
            {
                Strategy = DrawSizePreservationStrategy.Average,
                Children = new Drawable[]
                {
                    box = new Box
                    {
                        Size = new Vector2(50, 50),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre
                    },
                    new SpriteText
                    {
                        Text = ObjectLists.OrbList[0].ToString(),
                        RelativeSizeAxes = Axes.Both,
                    }
                }
            });
        }
    }
}
