using GDE.App.Main.Colors;
using osu.Framework.Graphics.UserInterface;
using osuTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDE.App.Main.UI
{
    public class GDEButton : Button
    {
        public GDEButton()
            : base()
        {
            CornerRadius = 5;
            Colour = Color4.White;
            BackgroundColour = GDEColors.FromHex("303030");
        }
    }
}
