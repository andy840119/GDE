using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Graphics.Sprites;
using System.ComponentModel;
using GDE.App.Main.Screens.Menu.Components;
using osuTK;
using GDE.App.Main.Levels.Metas;

namespace GDE.Tests.Visual
{
    public class TestCaseLevelCard : TestCase
    {
        private LevelCard card;

        public TestCaseLevelCard()
        {
            Children = new Drawable[]
            {
                card = new LevelCard
                {
                    Size = new Vector2(250, 100),
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre
                }
            };

            AddStep("Change name to \"Test---\"", () => card.Level.Value = new Level
            {
                AuthorName = "TestAuthor",
                Name = "TestLevel",
                Position = 0,
                Verified = false,
                Length = 95000,
                Song = new Song
                {
                    AuthorName = "TestAuthor",
                    Name = "TestName",
                    AuthorNG = "TestAuthorNG",
                    ID = 000000,
                    Link = "TestLink"
                },
            });

            AddStep("Revert name", () => card.Level.Value = new Level
            {
                AuthorName = "Unknown author",
                Name = "Unknown name",
                Position = 0,
                Verified = false,
                Length = 0,
                Song = new Song
                {
                    AuthorName = "Unknown author",
                    Name = "Unknown name",
                    AuthorNG = "Unkown author NG",
                    ID = 000000,
                    Link = "Unkown link"
                },
            });
        }
    }
}
