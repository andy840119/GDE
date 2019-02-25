using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using System;
using System.Collections.Generic;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDFPSDisplay : LCDText
    {
        private PolyTextIntSegment frames;

        public LCDFPSDisplay(int digits = 3)
            : base()
        {
            PolyText = new PolyText(new List<PolyTextSegment> { (frames = new PolyTextIntSegment(0, digits)), new PolyTextStringSegment(" FPS"), });
        }

        protected override void Update()
        {
            frames.Value = (int)Clock.FramesPerSecond;
        }
    }
}
