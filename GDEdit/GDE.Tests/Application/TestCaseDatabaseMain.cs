using GDEdit.Application;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using static GDEdit.Application.ApplicationDatabase;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDE.Tests.Application
{
    public class TestCaseDatabaseMain : TestCase
    {
        public TestCaseDatabaseMain()
        {
            Databases.Add(new Database());

            Children = new[]
            {
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    RelativePositionAxes = Axes.X,
                    Direction = FillDirection.Vertical,
                    Children = new[]
                    {
                        new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 40,
                            Text = "Name: " + Databases[0].UserLevels[0].Name
                        },
                        new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 15,
                            Text = "Description: " + Databases[0].UserLevels[0].Description
                        },
                        new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                            Text = "Revision: " + Databases[0].UserLevels[0].Revision
                        },
                        new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                            Text = "Version: " + Databases[0].UserLevels[0].Version
                        },
                        new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                            Text = "Object count: " + Databases[0].UserLevels[0].ObjectCount
                        },
                        new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                            Text = "Length: " + Databases[0].UserLevels[0].Length
                        },
                    }
                },
            };
        }
    }
}
