using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colours;
using osu.Framework.Input.Events;
using System;

namespace GDE.App.Main.UI.Tabs
{
    public class LevelTab : TabbableContainer
    {
        public string Text;
        public Action clickAction;

        private ColourInfo colours;
        private Box tab;
        private Box hoverLayer;

        public LevelTab()
        {
            RelativeSizeAxes = Axes.Both;

            colours = new ColourInfo
            {
                TopLeft = GDEColours.FromHex(@"353535"),
                TopRight = GDEColours.FromHex(@"353535"),
                BottomLeft = GDEColours.FromHex(@"272727"),
                BottomRight = GDEColours.FromHex(@"272727")
            };

            Children = new Drawable[]
            {
                tab = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Colour = colours
                },
                hoverLayer = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Alpha = 0
                },
                new SpriteText
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Text = Text,
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            hoverLayer.FadeTo(0.2f, 800, Easing.OutExpo);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);
            hoverLayer.FadeOut(800, Easing.OutExpo);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            hoverLayer.FadeTo(0.3f, 1200, Easing.InOutExpo);
            return base.OnMouseDown(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            hoverLayer.FadeTo(0.5f, 100, Easing.OutExpo).Delay(100).FadeOut(100);
            clickAction?.Invoke();
            return base.OnClick(e);
        }
    }
}
