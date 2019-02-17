using GDE.App.Main.Colors;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using osuTK;

namespace GDE.Tests.Visual
{
    public class TestCaseTheme : TestCase
    {
        private Bindable<string> hexValue = new Bindable<string>("151515");
        private TextBox hexBox;
        private Box themedBox;

        public TestCaseTheme()
        {
            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both,
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        hexBox = new TextBox
                        {
                            Text = "151515",
                            Size = new Vector2(80, 20),
                        },
                        new Button
                        {
                            Text = "Dark theme",
                            Size = new Vector2(80, 20),
                            Action = () => 
                            {
                                hexValue.Value = "151515";
                            },
                            BackgroundColour = GDEColors.FromHex("1c1c1c")
                        },
                        new Button
                        {
                            Text = "Light theme",
                            Size = new Vector2(80, 20),
                            Action = () =>
                            {
                                hexValue.Value = "e5e5e5";
                            },
                            BackgroundColour = GDEColors.FromHex("1c1c1c")
                        }
                    }
                },
                themedBox = new Box
                {
                    Colour = GDEColors.FromHex(hexValue),
                    Size = new Vector2(190),
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight
                }
            };

            hexValue.ValueChanged += HexChanged;
            hexBox.Current.ValueChanged += HexBoxChanged;
        }

        private void HexBoxChanged(string obj)
        {
            hexValue.Value = obj;
        }

        private void HexChanged(string obj)
        {
            themedBox.FadeColour(GDEColors.FromHex(obj), 500);
        }
    }
}
