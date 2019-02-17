using GDE.App.Main.Colors;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osuTK;
using System;
using osuTK.Graphics;

namespace GDE.App.Main.Overlays
{
    public class OverlayPopup : FocusedOverlayContainer
    {
        private string  text;

        public string HeaderText
        {
            get => text;
            set
            {
                if (text == value)
                    return;
                text = value;

                header.Text = value;
            }
        }

        public string BodyText
        {
            set => body.Text = value;
        }

        public string Button1Text
        {
            set => Button1.Text = value;
        }

        public string Button2Text
        {
            set => Button2.Text = value;
        }

        public Color4 ConfirmButtonCol
        {
            set
            {
                Button2.BackgroundColour = value;
            }
        }

        private SpriteText header;
        private SpriteText body;
        private Button Button1;
        private Button Button2;

        public Action confirmAction;

        public OverlayPopup()
        {
            Padding = new MarginPadding(10);

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColors.FromHex("191919")
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Padding = new MarginPadding
                    {
                        Top = 40
                    },
                    Children = new Drawable[]
                    {
                        header = new SpriteText
                        {
                            TextSize = 70,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        },
                        body = new SpriteText
                        {
                            TextSize = 40,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        }
                    }
                },
                Button1 = new Button
                {
                    BackgroundColour = GDEColors.FromHex("1E1E1E"),
                    Size = new Vector2(100, 50),
                    Action = () =>ToggleVisibility(),
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Margin = new MarginPadding(20)
                },
                Button2 = new Button
                {
                    BackgroundColour = GDEColors.FromHex("1E1E1E"),
                    Size = new Vector2(100, 50),
                    Action = () => confirmAction?.Invoke(),
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Margin = new MarginPadding(20)
                }
            };

            Rotation = -25;
            Alpha = 0;
            Scale = new Vector2(0.5f);
        }

        protected override void PopIn()
        {
            this.RotateTo(0, 300, Easing.OutExpo);
            this.FadeInFromZero(300, Easing.OutExpo);
            this.ScaleTo(0.8f, 300, Easing.OutExpo);
            base.PopIn();
        }

        protected override void PopOut()
        {
            this.RotateTo(-25, 300, Easing.InExpo);
            this.FadeOutFromOne(300, Easing.InExpo);
            this.ScaleTo(0.5f, 300, Easing.InExpo);
            base.PopOut();
        }
    }
}
