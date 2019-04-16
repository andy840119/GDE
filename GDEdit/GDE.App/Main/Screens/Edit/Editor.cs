using DiscordRPC;
using GDE.App.Main.Colors;
using GDE.App.Main.Levels;
using GDE.App.Main.Screens.Edit.Components;
using GDE.App.Main.Tools;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Screens;
using osuTK;

namespace GDE.App.Main.Screens.Edit
{
    public class EditorScreen : Screen
    {
        private TextureStore texStore;
        private Box background;
        private int i;

        private Database database;
        private LevelPreview Preview;
        private Level level => database.UserLevels[i];

        private Editor editor;

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases, TextureStore ts)
        {
            database = databases[0];

            texStore = ts;
            background.Texture = texStore.Get("Backgrounds/game_bg_01_001-uhd.png");
        }

        public EditorScreen(int index, Level level)
        {
            editor = new Editor(level);

            RPC.UpdatePresence(editor.Level.Name, "Editing a level", new Assets
            {
                LargeImageKey = "gde",
                LargeImageText = "GD Edit"
            });

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
                Preview = new LevelPreview(index, editor)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new EditorTools(Preview, editor)
                {
                    Size = new Vector2(150, 300),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                },
            });
        }
    }
}
