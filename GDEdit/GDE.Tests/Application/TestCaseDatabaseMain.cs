using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using System.Threading.Tasks;
using static GDEdit.Application.ApplicationDatabase;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDE.Tests.Application
{
    public class TestCaseDatabaseMain : TestCase
    {
        private Database database;
        private LevelCollection levels;
        private SpriteText name, description, revision, version, objectCount, length;
        private bool finishedLoading;

        public TestCaseDatabaseMain()
        {
            Databases.Add(database = new Database());

            Children = new[]
            {
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    RelativePositionAxes = Axes.X,
                    Direction = FillDirection.Vertical,
                    Children = new[]
                    {
                        name = new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 40,
                        },
                        description = new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 15,
                        },
                        revision = new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                        },
                        version = new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                        },
                        objectCount = new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20,
                        },
                        length = new SpriteText
                        {
                            RelativeSizeAxes = Axes.X,
                            TextSize = 20
                        },
                    }
                },
            };
        }

        protected override void Update()
        {
            if (!finishedLoading && (finishedLoading = database.DecryptLevelDataStatus >= TaskStatus.RanToCompletion))
            {
                if ((levels = database.UserLevels).Count > 0)
                {
                    name.Text = $"Name: {levels[0].Name}";
                    description.Text = $"Name: {levels[0].Description}";
                    revision.Text = $"Name: {levels[0].Revision}";
                    version.Text = $"Name: {levels[0].Version}";
                    objectCount.Text = $"Name: {levels[0].ObjectCount}";
                    length.Text = $"Name: {levels[0].Length}";
                }
                else
                    name.Text = "No levels";
            }
            if (!finishedLoading)
                name.Text = "Loading";
            base.Update();
        }
    }
}
