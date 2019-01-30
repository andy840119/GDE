using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colors;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Functions.GeometryDash;
using static GDEdit.Application.ApplicationDatabase;

namespace GDE.App.Main.Screens
{
    public class LevelCreation : Screen
    {
        private TextBox name;
        private TextBox desc;

        public LevelCreation()
        {
            Databases.Add(new Database());

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = GDEColors.FromHex("111")
                },
                name = new TextBox
                {
                    PlaceholderText = "Level name",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(0.8f, 50),
                    Margin = new MarginPadding
                    {
                        Horizontal = 10,
                        Vertical = 10
                    }
                },
                desc = new TextBox
                {
                    PlaceholderText = "Description [Optional]",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(0.8f, 50),
                    Position = new Vector2(1, 60),
                    Margin = new MarginPadding
                    {
                        Horizontal = 10,
                        Vertical = 10
                    },
                },
                new Button
                {
                    Size = new Vector2(100, 20),
                    Origin = Anchor.BottomRight,
                    Anchor = Anchor.BottomRight,
                    Margin = new MarginPadding
                    {
                        Horizontal = 5,
                        Vertical = 5
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
                            Text = "Create",
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre
                        }
                    },
                    Action = () =>
                    {
                        Databases[0].CreateLevel(name.Text, desc.Text);
                    }
                },
            };
        }
    }
}
