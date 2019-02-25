using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using System;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDString : LCDDisplay
    {
        private string v = "";

        private LCDCharacter[] chars;

        public string Value
        {
            get => v;
            set
            {
                chars = new LCDCharacter[(v = value).Length];
                for (int i = 0; i < value.Length; i++)
                    chars[i] = new LCDCharacter(value[i]);
            }
        }

        public LCDString(string value)
            : base()
        {
            Value = value;
            Children = chars;
        }
    }
}
