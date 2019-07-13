using GDE.App.Main.Colors;
using GDE.App.Main.UI;
using GDEdit.Application.Editor;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using System;
using BaseMenu = osu.Framework.Graphics.UserInterface.Menu;

namespace GDE.App.Main.Screens.Edit.Components.IDMigration
{
    public class IDMigrationTabControl : Container
    {
        private IDMigrationTabItem[] tabItems = new IDMigrationTabItem[4];
        private IDMigrationTabItem currentTab;

        private FillFlowContainer itemsContainer;

        public event Action<IDMigrationMode> TabSelected;

        public IDMigrationTabControl()
            : base()
        {
            for (int i = 0; i < 4; i++)
            {
                tabItems[i] = new IDMigrationTabItem((IDMigrationMode)i);
                tabItems[i].TabSelected += HandleTabSelected;
            }
            currentTab = this[IDMigrationMode.Groups];

            RelativeSizeAxes = Axes.X;
            Height = 32;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = GDEColors.FromHex("111")
                },
                itemsContainer = new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Left = 10 },
                    Children = tabItems,
                }
            };
        }

        private IDMigrationTabItem this[IDMigrationMode mode] => tabItems[(int)mode];

        private void HandleTabSelected(IDMigrationMode newMode)
        {
            currentTab.MoveToOffset(new Vector2(0, -10), 500, Easing.OutQuint);
            (currentTab = this[newMode]).MoveToOffset(new Vector2(0, 10), 500, Easing.InQuint);
            TabSelected?.Invoke(newMode);
        }
    }
}
