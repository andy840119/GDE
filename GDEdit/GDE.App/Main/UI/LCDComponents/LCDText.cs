using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using System;
using System.Collections.Generic;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDText : LCDDisplay
    {
        private PolyText polyText;

        public PolyText PolyText
        {
            get => polyText;
            protected set
            {
                foreach (var s in (polyText = value).Segments)
                    s.ValueChanged += UpdateView;
                UpdateView();
            }
        }

        public LCDText()
            : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Direction = FillDirection.Horizontal;
            Spacing = new Vector2(20);
        }

        private void UpdateView()
        {
            Drawable[] d = new Drawable[PolyText.Segments.Count];
            for (int i = 0; i < d.Length; i++)
                d[i] = PolyText.Segments[i].CreateLCDComponent();
            Children = d;
        }
    }

    public class PolyText
    {
        public List<PolyTextSegment> Segments { get; set; }

        public PolyText(List<PolyTextSegment> segments)
        {
            Segments = segments;
        }

        public PolyTextSegment this[int index]
        {
            get => Segments[index];
            set => Segments[index] = value;
        }
    }
    public abstract class PolyTextSegment
    {
        private object v;

        public object Value
        {
            get => v;
            set
            {
                if (v == value)
                    return;
                v = value;
                ValueChanged?.Invoke();
            }
        }

        public event Action ValueChanged;

        public abstract LCDDisplay CreateLCDComponent();
    }
    public class PolyTextSegment<T> : PolyTextSegment
    {
        public PolyTextSegment(T value)
        {
            Value = value;
        }

        public override LCDDisplay CreateLCDComponent()
        {
            throw new Exception("Cannot create an LCD component of this polytext segment.");
        }
    }
    public class PolyTextStringSegment : PolyTextSegment<string>
    {
        public PolyTextStringSegment(string value) : base(value) { }

        public override LCDDisplay CreateLCDComponent() => new LCDString((string)Value);
    }
    public class PolyTextIntSegment : PolyTextSegment<int>
    {
        public int Digits { get; }

        public PolyTextIntSegment(int value, int digits = 3) : base(value)
        {
            Digits = digits;
        }

        public override LCDDisplay CreateLCDComponent() => new LCDNumber((int)Value, Digits);
    }
}
