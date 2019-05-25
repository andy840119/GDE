using System;
using osu.Framework.Graphics.UserInterface;

namespace GDE.App.Main.UI
{
    public class GDEMenuItem : MenuItem
    {
        public readonly MenuItemType Type;

        public GDEMenuItem(string text, MenuItemType type = MenuItemType.Standard)
            : base (text) { }

        public GDEMenuItem(string text, MenuItemType type, Action action)
            : base(text, action) { }
    }
}
