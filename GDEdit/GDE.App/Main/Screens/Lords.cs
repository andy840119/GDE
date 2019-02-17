using GDE.App.Main.Colors;
using GDE.App.Main.UI;
using GDE.App.Main.Screens.Menu.Components;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using static GDEdit.Application.ApplicationDatabase;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using System.Collections.Generic;
using osu.Framework.Configuration;
using GDE.App.Main.Levels.Metas;
using GDE.App.Main.Overlays;
using osu.Framework.Graphics.Textures;

namespace GDE.App.Main.Screens
{
    public class Lords : Screen
    {
        private Box Alfas;
        private Box Alten;
        private FillFlowContainer Container;
        private Button ExitButton;

        public Lords()
        {
            Children = new Drawable[]
            {
                Container = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(100, 0),
                    Alpha = 0f,
                    Children = new Drawable[]
                    {
                        Alfas = new Box
                        {
                            Size = new Vector2(390),
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre
                        },
                        Alten = new Box
                        {
                            Size = new Vector2(390),
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre
                        }
                    }
                },
                ExitButton = new Button
                {
                    Text = "Exit",
                    Action = () => Exit(),
                    Size = new Vector2(100, 50),
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    BackgroundColour = GDEColors.FromHex("191919"),
                    Alpha = 0,
                }
            };
        }
        
        [BackgroundDependencyLoader]
        private void load(TextureStore TexStore)
        {
            Alfas.Texture = TexStore.Get("Alfas.png");
            Alten.Texture = TexStore.Get("Alten.png");
        }

        protected override void OnEntering(Screen last)
        {
            SpriteText welc;
            SpriteText praise;
            Alpha = 0;

            this.Delay(1000).FadeInFromZero(2000);
            Add(welc = new SpriteText
            {
                Text = "Welcome...",
                TextSize = 70,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });

            Add(praise = new SpriteText
            {
                Text = "Now come and praise us",
                TextSize = 70,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Alpha = 0f
            });

            welc.Delay(4000).FadeOutFromOne(1000);
            praise.Delay(4000).FadeInFromZero(1000);
            praise.Delay(6000).FadeOutFromOne(1000);
            Container.Delay(7000).FadeInFromZero(2000);
            ExitButton.Delay(7000).FadeInFromZero(2000);
            base.OnEntering(last);
        }
    }
}
