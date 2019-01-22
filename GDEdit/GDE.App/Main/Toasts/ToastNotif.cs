using GDE.App.Main.Colours;
using GDE.App.Main.UI;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GDE.App.Main.Toasts
{
    public class ToastNotif : FocusedOverlayContainer
    {
        public SpriteText text;
        private SpriteIcon toastIcon;
        private Box toastBody;
        private CircularContainer container;

        public ToastNotif()
        {
            Children = new Drawable[]
            {
                container = new CircularContainer
                {
                    Masking = true,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        toastBody = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = GDEColours.FromHex("1c1c1c")
                        },
                        toastIcon = new SpriteIcon
                        {
                            Origin = Anchor.CentreLeft,
                            Anchor = Anchor.CentreLeft,
                            Icon = FontAwesome.fa_exclamation_circle,
                            Size = new Vector2(20),
                        },
                        text = new SpriteText
                        {
                            Text = "filler text",
                            Origin = Anchor.CentreLeft,
                            Anchor = Anchor.CentreLeft,
                            Margin = new MarginPadding
                            {
                                Left = 25
                            },
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            container.Position = new Vector2(0, 20);
            container.Alpha = 0;

            base.LoadComplete();
        }

        protected override void PopIn()
        {
            container.MoveTo(new Vector2(0, 0), 500, Easing.OutExpo);
            container.FadeIn(500, Easing.OutExpo);

            using (BeginDelayedSequence(5000, true)) // 5 seconds
            {
                PopOut();
            }

            base.PopIn();
        }

        protected override void PopOut()
        {
            container.MoveTo(new Vector2(0, 20), 500, Easing.InExpo);
            container.FadeOut(500, Easing.InExpo);

            base.PopOut();
        }

        /// <summary>The severity of the notification (see "https://goo.gl/11qsYP")</summary>
        public enum SeverityEnum
        {
            emergencies,
            alerts,
            critical,
            errors,
            warnings,
            notifications,
            informational,
            debugging
        }
    }
}
