using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using System;
using static GDE.App.Main.UI.LCDComponents.LCDDigitBar;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDDigit : LCDCharacter
    {
        private static readonly int[,] Numbers = new int[,]
        {
            // 0
            {
                   1,
                1,    1,
                   0,
                1,    1,
                   1,
            },
            // 1
            {
                   0,
                0,    1,
                   0,
                0,    1,
                   0,
            },
            // 2
            {
                   1,
                0,    1,
                   1,
                1,    0,
                   1,
            },
            // 3
            {
                   1,
                0,    1,
                   1,
                0,    1,
                   1,
            },
            // 4
            {
                   0,
                1,    1,
                   1,
                0,    1,
                   0,
            },
            // 5
            {
                   1,
                1,    0,
                   1,
                0,    1,
                   1,
            },
            // 6
            {
                   1,
                1,    0,
                   1,
                1,    1,
                   1,
            },
            // 7
            {
                   1,
                1,    1,
                   0,
                0,    1,
                   0,
            },
            // 8
            {
                   1,
                1,    1,
                   1,
                1,    1,
                   1,
            },
            // 9
            {
                   1,
                1,    1,
                   1,
                0,    1,
                   1,
            },
        };

        private int v;
        private bool active;

        public int Value
        {
            get => v;
            set
            {
                if (value > 9 || value < 0)
                    throw new InvalidOperationException("Cannot set the value of the LCD digit to a number outside the range [0, 9].");
                Character = (char)((v = value) + '0');
            }
        }
        public bool Active
        {
            get => active;
            set
            {
                for (int i = 0; i < Bars.Length; i++)
                    Bars[i].Active = active = value;
            }
        }

        public LCDDigit(int value = 0, bool active = true)
            : base((char)(value + '0'))
        {
            Value = value;
            Active = active;
        }
    }
}
