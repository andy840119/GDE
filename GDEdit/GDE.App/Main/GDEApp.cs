using GDE.App.Main.Containers;
using GDE.App.Main.Screens.Menu;
using GDE.App.Main.Toasts;
using GDE.App.Main.Tools;
using GDE.App.Updater;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osuTK;
using System;

namespace GDE.App.Main
{
    public class GDEApp : GDEAppBase
    {
        private ToastNotification notification;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new ScreenStack(new MainScreen())
                {
                    RelativeSizeAxes = Axes.Both
                },
                notification = new ToastNotification
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(600, 30),
                    Margin = new MarginPadding
                    {
                        Bottom = 5
                    }
                },
                new GlobalActionContainer()
            };

            new RavenLogger(this);
        }

        protected override void LoadComplete()
        {
            if (RuntimeInfo.OS == RuntimeInfo.Platform.Windows)
                Add(new SquirrelUpdateManager());

            base.LoadComplete();
        }

        protected override bool ExceptionHandler(Exception arg)
        {
            notification.text.Text = $"An error has occurred, Please report this to the devs. (Err: {arg.Message})";
            notification.ToggleVisibility();

            return base.ExceptionHandler(arg);
        }
    }
}
