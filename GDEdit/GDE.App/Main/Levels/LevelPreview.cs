using GDE.App.Main.Containers;
using GDE.App.Main.Objects;
using GDE.App.Main.Screens.Edit;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using System.Collections.Generic;

namespace GDE.App.Main.Levels
{
    public class LevelPreview : Container<ObjectBase>, IKeyBindingHandler<GlobalAction>
    {
        private EditorScreen editorScreen;

        private readonly int i;

        private Database database;

        private bool modifier;

        public IReadOnlyList<ObjectBase> Objects => Children;
        public bool AllowDrag = true;

        public Level Level => database.UserLevels[i];

        public LevelPreview(EditorScreen editorScreen, int index)
        {
            this.editorScreen = editorScreen;
            i = index;

            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];

            foreach (var o in Level.LevelObjects)
                Add(new ObjectBase(o));
        }

        public override bool HandlePositionalInput => editorScreen.HandlePositionalInput;

        public bool OnPressed(GlobalAction action)
        {
            var val = modifier ? Editor.SmallMovementStep : Editor.NormalMovementStep;

            foreach (var i in Objects)
            {
                if (i.State == SelectionState.Selected)
                    switch (action)
                    {
                        case GlobalAction.ObjMoveRight:
                            i.ObjectX += val;
                            break;
                        case GlobalAction.ObjMoveLeft:
                            i.ObjectX -= val;
                            break;
                        case GlobalAction.ObjMoveUp:
                            i.ObjectY += val;
                            break;
                        case GlobalAction.ObjMoveDown:
                            i.ObjectY -= val;
                            break;
                    }
            }

            switch (action)
            {
                case GlobalAction.ObjMoveModifier:
                    modifier = !modifier;
                    break;
            }

            return true;
        }

        public bool OnReleased(GlobalAction action) => true;
    }
}
