using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colours;
using GDE.App.Main.UI;

namespace GDE.App.Main.Screens
{
    public class MainScreen : Screen
    {
        private Box bg;

        public MainScreen()
        {
            Children = new Drawable[]
            {
                new DrawSizePreservingFillContainer
                {
                    Strategy = DrawSizePreservationStrategy.Average,
                    Children = new Drawable[]
                    {
                        bg = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = GDEColours.FromHex("111")
                        },
                        new FillFlowContainer
                        {
                            Margin = new MarginPadding
                            {
                                Top = 20
                            },
                            Padding = new MarginPadding
                            {
                                Bottom = 20
                            },
                            RelativeSizeAxes = Axes.Both,
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Children = new Drawable[]
                            {
                                new Button
                                {
                                    Size = new Vector2(190),
                                    Origin = Anchor.Centre,
                                    Anchor = Anchor.Centre,
                                    Margin = new MarginPadding
                                    {
                                        Horizontal = 5
                                    },
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            AlwaysPresent = true,
                                            Colour = Color4.White.Opacity(0.2f),
                                        },
                                        new SpriteText
                                        {
                                            Origin = Anchor.Centre,
                                            Anchor = Anchor.Centre,
                                            Position = new Vector2(0, 60),
                                            Text = "New level",
                                            Font = @"OpenSans"
                                        },
                                        new SpriteIcon
                                        {
                                            Icon = FontAwesome.fa_file_text_o,
                                            Size = new Vector2(50),
                                            Origin = Anchor.Centre,
                                            Anchor = Anchor.Centre,
                                        }
                                    },
                                    Action = () =>
                                    {
                                        Push(new LevelCreation());
                                    }
                                },
                                new Button
                                {
                                    Size = new Vector2(190),
                                    Origin = Anchor.Centre,
                                    Anchor = Anchor.Centre,
                                    Margin = new MarginPadding
                                    {
                                        Horizontal = 5
                                    },
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            AlwaysPresent = true,
                                            Colour = Color4.White.Opacity(0.2f),
                                        },
                                        new SpriteText
                                        {
                                            Origin = Anchor.Centre,
                                            Anchor = Anchor.Centre,
                                            Position = new Vector2(0, 60),
                                            Text = "Edit existing level",
                                            Font = @"OpenSans"
                                        },
                                        new SpriteIcon
                                        {
                                            Icon = FontAwesome.fa_pencil_square_o,
                                            Size = new Vector2(50),
                                            Origin = Anchor.Centre,
                                            Anchor = Anchor.Centre,
                                        }
                                    },
                                    Action = () =>
                                    {
                                        Push(new Edit.Editor());
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
