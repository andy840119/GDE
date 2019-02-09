using GDE.App.Main.Colours;
using GDE.App.Main.UI;
using GDE.App.Main.Screens.Menu.Components;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace GDE.App.Main.Screens.Menu
{
    public class MainScreen : Screen
    {
        public MainScreen()
        {
            Children = new Drawable[]
            {
                new DrawSizePreservingFillContainer
                {
                    Strategy = DrawSizePreservationStrategy.Average,
                    Children = new Drawable[]
                    {
                        new Toolbar
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1f, 40),
                            // Update to currently selected level
                            LevelName =
                            {
                                Value = "Level name"
                            },
                            SongName =
                            {
                                Value = "Song name"
                            }
                        }
                    }
                }
            };
        }
    }
}
