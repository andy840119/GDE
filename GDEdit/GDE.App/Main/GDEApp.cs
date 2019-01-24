using GDE.App.Main.Screens;
using GDE.App.Main.Toasts;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using System;

namespace GDE.App.Main
{
    public class GDEApp : GDEAppBase
    {
        private MainContainer container;
        private ToastNotification notif;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRange(new Drawable[]
            {
                container = new MainContainer(),
                notif = new ToastNotification
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(600, 30),
                    Margin = new MarginPadding
                    {
                        Bottom = 5
                    }
                }
            });
        }

        protected override bool ExceptionHandler(Exception arg)
        {
            notif.text.Text = $"An error has occurred, Please report this to the devs. (Err: {arg.Message})";
            notif.ToggleVisibility();

            return base.ExceptionHandler(arg);
        }
    }
}
