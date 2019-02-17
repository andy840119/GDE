using GDE.App.Main.Colors;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;

namespace GDE.App.Main.Screens
{
    public class Lords : Screen
    {
        private SpriteText Alfas;
        private SpriteText Alten;
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
                        Alfas = new SpriteText
                        {
                            Text = "Alfas",
                            TextSize = 170,
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre
                        },
                        Alten = new SpriteText
                        {
                            Text = "Alten",
                            TextSize = 170,
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
