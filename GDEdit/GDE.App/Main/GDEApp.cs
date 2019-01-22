using osu.Framework.Allocation;
using osu.Framework.Graphics;
using GDE.App.Main.Screens;
using System;
using GDE.App.Main.Toasts;
using osuTK;

namespace GDE.App.Main
{
    public class GDEApp : GDEAppBase
    {
        private MainContainer container;
        private ToastNotif notif;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRange(new Drawable[]
            {
                container = new MainContainer(),
                notif = new ToastNotif
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
            notif.text.Text = $"An error has occured, This has been reported to the devs automatically.";
            notif.ToggleVisibility();

            return base.ExceptionHandler(arg);
        }
    }
}
