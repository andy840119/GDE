using osu.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDE.App.Main.Levels
{
    public class VerticalGridLine : GridLine
    {
        public VerticalGridLine(float x) : base(x, 0) => RelativeSizeAxes = Axes.Y;
    }
}
