using DiscordRPC;
using GDE.App.Main.Colors;
using GDE.App.Main.Levels;
using GDE.App.Main.Objects;
using GDE.App.Main.Screens.Edit.Components;
using GDE.App.Main.Screens.Edit.Components.Menu;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using System.Collections.Generic;

namespace GDE.App.Main.Screens.Edit
{
    public class EditorScreen : Screen
    {
        private TextureStore texStore;
        private Box background;
        private int i;

        private Database database;
        private LevelPreview preview;
        private Level level => database.UserLevels[i];

        private Editor editor;
        private Grid grid;
        private Camera camera;
        private EditorTools tools;

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases, TextureStore ts)
        {
            database = databases[0];

            texStore = ts;
            background.Texture = texStore.Get("Backgrounds/game_bg_01_001-uhd.png");

            EditorMenuBar menuBar;

            var fileMenuItems = new List<MenuItem>();

            fileMenuItems.Add(new EditorMenuItem("Save", Save, MenuItemType.Highlighted));
            fileMenuItems.Add(new EditorMenuItem("Save & Exit", SaveAndExit, MenuItemType.Standard));
            fileMenuItems.Add(new EditorMenuItemSpacer());
            fileMenuItems.Add(new EditorMenuItem("Exit", this.Exit, MenuItemType.Destructive));

            AddInternal(new Container
            {
                Name = "Top bar",
                RelativeSizeAxes = Axes.X,
                Height = 40,
                Child = menuBar = new EditorMenuBar
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Items = new[]
                    {
                        new MenuItem("File")
                        {
                            Items = fileMenuItems
                        }
                    }
                }
            });
        }

        public EditorScreen(int index, Level level)
        {
            RelativeSizeAxes = Axes.Both;

            editor = new Editor(level);
            // TODO: Inject editor into dependencies to work with the other things

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
                camera = new Camera(editor)
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        grid = new Grid
                        {
                            Size = new Vector2(2), //Doubles the size
                            Position = new Vector2(-30, 0) //Offsets to remove visible repetition
                        },
                        preview = new LevelPreview(this, index)
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        },
                    }
                },
                tools = new EditorTools(preview, camera)
                {
                    Size = new Vector2(150, 300),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                }
            });
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (tools.CurrentSelectedObjectID < 1)
                return false;
            var cloned = camera.GetClonedGhostObjectLevelObject();
            editor.AddObject(cloned);
            preview.Add(new ObjectBase(cloned));
            return base.OnClick(e);
        }

        protected override bool OnDrag(DragEvent e)
        {
            foreach (var child in camera.Children)
            {
                child.Position += e.Delta;
            }

            return base.OnDrag(e);
        }

        private void SaveAndExit()
        {
            Save();
            this.Exit();
        }
        private void Save() => editor.Save(database, i);
    }
}
