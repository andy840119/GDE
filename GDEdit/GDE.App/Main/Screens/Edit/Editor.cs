using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colours;
using GDE.App.Main.UI;
using GDE.App.Main.UI.Toolbar;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash;
using System.Collections.Generic;

namespace GDE.App.Main.Screens.Edit
{
    public class Editor : Screen
    {
        private Level lvl;
        private List<LevelObject> objs;

        private ToolbarMenu toolbar;
        //private List<ToolbarMenuItem> items;

        public Editor()
        {
            //for (int i = 0; i < 10; i++)
            //    items.Add(new ToolbarMenuItem($"Item {i}", MenuItemType.Standard));

            //Children = new Drawable[]
            //{
            //    toolbar = new ToolbarMenu(Direction.Horizontal)
            //    {
            //        Items = items
            //    }
            //};
        }
    }
}
