using osu.Framework.Allocation;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using GDE.App.Main.Colors;
using GDE.App.Main.Levels;
using GDE.App.Main.Screens.Edit;
using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Input;

namespace GDE.App.Main.Screens.Edit
{
    public class Editor : Screen
    {
        private TextureStore texStore;
        private Box background;
        private int i;

        private Database database;
        private LevelPreview Preview;
        private Level level => database.UserLevels[0];

        private InputManager inputManager;

        public Editor(int index)
        {
            i = index;

            AddRangeInternal(new Drawable[]
            {
                background = new Box
                {
                    Origin = Anchor.BottomLeft,
                    Anchor = Anchor.BottomLeft,
                    Depth = float.MaxValue,
                    Colour = GDEColors.FromHex("4f4f4f"),
                    Size = new Vector2(2048, 2048)
                },
                new Components.Tools()
                {
                    Size = new Vector2(150, 300),
                    //Object = level.LevelObjects[0],
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                },
                Preview = new LevelPreview(index)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
            });

            inputManager = GetContainingInputManager();
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore ts)
        {
            texStore = ts;
            background.Texture = texStore.Get("Backgrounds/game_bg_01_001-uhd.png");
        }
    }
}
