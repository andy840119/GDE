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
using GDE.App.Main.Tools;

namespace GDE.App.Main.Screens.Edit
{
    public class Editor : Screen
    {
        private TextureStore texStore;
        private Box background;
        private int i;

        private Database database;
        private LevelPreview Preview;
        private Level level => database.UserLevels[i];

        //yeah i feel disgusted from this aswell, but sadly theres no other way
        private GDEdit.Application.Editor.Editor editor;

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases, TextureStore ts)
        {
            database = databases[0];

            texStore = ts;
            background.Texture = texStore.Get("Backgrounds/game_bg_01_001-uhd.png");
        }

        public Editor(int index, Level level)
        {
            editor = new GDEdit.Application.Editor.Editor(level);

            try
            {
                RPC.updatePresence(editor.Level.Name, "Editing a level", new DiscordRPC.Assets
                {
                    LargeImageKey = "gde",
                    LargeImageText = "GD Edit"
                });
            }
            catch { }

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
                new Components.Tools(Preview, editor)
                {
                    Size = new Vector2(150, 300),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                },
            });
        }
    }
}
