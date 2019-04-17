﻿using osu.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDE.App.Main.Levels
{
    public class HorizontalGridLine : GridLine
    {
        public HorizontalGridLine() : base() => RelativeSizeAxes = Axes.X;
    }
}
