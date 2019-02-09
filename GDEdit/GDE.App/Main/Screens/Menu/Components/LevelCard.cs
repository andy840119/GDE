using GDE.App.Main.Colours;
using GDE.App.Main.Levels.Metas;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osuTK;
using System;
using osu.Framework.Input.Events;

namespace GDE.App.Main.Screens.Menu.Components
{
    public class LevelCard : ClickableContainer
    {
        public Bindable<Level> Level = new Bindable<Level>(new Level
        {
            AuthorName = "Unknown author",
            Name = "Unknown name",
            Position = 0,
            Verified = false,
            Length = 95000,
            Song =new Song
            {
                AuthorName = "Unknown author",
                Name = "Unknown name",
                AuthorNG = "Unkown author NG",
                ID = 000000,
                Link = "Unkown link"
            },
        });

        public Bindable<bool> Selected = new Bindable<bool>(false);

        private Box selectionBar;
        private Box HoverBox;

        public LevelCard()
        {
            Children = new Drawable[]
            {
                HoverBox = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColours.FromHex("161616")
                },
                selectionBar = new Box
                {
                    RelativeSizeAxes = Axes.Y,
                    Size = new Vector2(5, 1f),
                    Colour = GDEColours.FromHex("202020")
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding
                    {
                        Left = 10
                    },
                    Children = new Drawable[]
                    {
                        new SpriteText
                        {
                            Text = Level.Value.Name,
                            TextSize = 30
                        },
                        new SpriteText
                        {
                            Text = Level.Value.Song.Name,
                            TextSize = 20,
                            Colour = GDEColours.FromHex("aaaaaa")
                        }
                    }
                },
                new SpriteText
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Margin = new MarginPadding(5),
                    Text = TimeSpan.FromMilliseconds(Level.Value.Length).ToString(@"m\:ss"),
                    TextSize = 20,
                    Colour = GDEColours.FromHex("aaaaaa")
                }
            };

            Selected.ValueChanged += OnSelected;
        }

        private void OnSelected(bool obj)
        {
            if (obj == false)
                selectionBar.FadeColour(GDEColours.FromHex("202020"), 200);
            else
                selectionBar.FadeColour(GDEColours.FromHex("00bc5c"), 200);
        }

        protected override bool OnHover(HoverEvent e)
        {
            HoverBox.FadeColour(GDEColours.FromHex("1c1c1c"), 500);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            HoverBox.FadeColour(GDEColours.FromHex("161616"), 500);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Selected.Value = !Selected.Value;
            return base.OnClick(e);
        }
    }
}
