using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Logging;
using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Input;
using osu.Framework.Audio.Sample;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;

namespace GDE.App.Main.Screens
{
    public class MainScreen : Screen
    {
        private Box box;
        private TextureStore texStore;

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
            this.texStore = texStore;

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
