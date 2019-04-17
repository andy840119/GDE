using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDE.App.Main.Levels
{
    public class GridLine : Box
    {
        public GridLine()
            : base()
        {
            Size = new Vector2(1);
            Colour = new Color4(128, 128, 128, 128);
            //AlwaysPresent = true;
        }
    }
}
