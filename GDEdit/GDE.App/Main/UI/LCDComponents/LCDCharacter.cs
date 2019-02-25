using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using System;
using System.Collections.Generic;
using static GDE.App.Main.UI.LCDComponents.LCDCharacterBar;

namespace GDE.App.Main.UI.LCDComponents
{
    public class LCDCharacter : Container
    {
        public const int Spacing = 2;

        private static readonly Dictionary<char, int[]> Characters = new Dictionary<char, int[]>
        {
            {
                '0',
                new int[]
                {
                       1,
                    1,    1,
                       0,
                    1,    1,
                       1,
                }
            },
            {
                '1',
                new int[]
                {
                       0,
                    0,    1,
                       0,
                    0,    1,
                       0,
                }
            },
            {
                '2',
                new int[]
                {
                       1,
                    0,    1,
                       1,
                    1,    0,
                       1,
                }
            },
            {
                '3',
                new int[]
                {
                       1,
                    0,    1,
                       1,
                    0,    1,
                       1,
                }
            },
            {
                '4',
                new int[]
                {
                       0,
                    1,    1,
                       1,
                    0,    1,
                       0,
                }
            },
            {
                '5',
                new int[]
                {
                       1,
                    1,    0,
                       1,
                    0,    1,
                       1,
                }
            },
            {
                '6',
                new int[]
                {
                       1,
                    1,    0,
                       1,
                    1,    1,
                       1,
                }
            },
            {
                '7',
                new int[]
                {
                       1,
                    1,    1,
                       0,
                    0,    1,
                       0,
                }
            },
            {
                '8',
                new int[]
                {
                       1,
                    1,    1,
                       1,
                    1,    1,
                       1,
                }
            },
            {
                '9',
                new int[]
                {
                       1,
                    1,    1,
                       1,
                    0,    1,
                       1,
                }
            },
            {
                'A',
                new int[]
                {
                       1,
                    1,    1,
                       1,
                    1,    1,
                       0,
                }
            },
            {
                'C',
                new int[]
                {
                       1,
                    1,    0,
                       0,
                    1,    0,
                       1,
                }
            },
            {
                'E',
                new int[]
                {
                       1,
                    1,    0,
                       1,
                    1,    0,
                       1,
                }
            },
            {
                'F',
                new int[]
                {
                       1,
                    1,    0,
                       1,
                    1,    0,
                       0,
                }
            },
            {
                'H',
                new int[]
                {
                       0,
                    1,    1,
                       1,
                    1,    1,
                       0,
                }
            },
            {
                'L',
                new int[]
                {
                       0,
                    1,    0,
                       0,
                    1,    0,
                       1,
                }
            },
            {
                'O',
                new int[]
                {
                       1,
                    1,    1,
                       0,
                    1,    1,
                       1,
                }
            },
            {
                'P',
                new int[]
                {
                       1,
                    1,    1,
                       1,
                    1,    0,
                       0,
                }
            },
            {
                'S',
                new int[]
                {
                       1,
                    1,    0,
                       1,
                    0,    1,
                       1,
                }
            },
            {
                'U',
                new int[]
                {
                       0,
                    1,    1,
                       0,
                    1,    1,
                       1,
                }
            },
            {
                ' ',
                new int[]
                {
                       0,
                    0,    0,
                       0,
                    0,    0,
                       0,
                }
            },
        };

        private char c;

        protected LCDCharacterBar[] Bars = new LCDCharacterBar[]
        {
            new LCDCharacterHorizontalBar
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
            },
            new LCDCharacterVerticalBar
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
                Y = -(DimensionSize * (SizeRatio + 1)) / 2f - Spacing,
            },
            new LCDCharacterVerticalBar
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.Centre,
                Y = -(DimensionSize * (SizeRatio + 1)) / 2f - Spacing,
            },
            new LCDCharacterHorizontalBar
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            },
            new LCDCharacterVerticalBar
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
                Y = (DimensionSize * (SizeRatio + 1)) / 2f + Spacing,
            },
            new LCDCharacterVerticalBar
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.Centre,
                Y = (DimensionSize * (SizeRatio + 1)) / 2f + Spacing,
            },
            new LCDCharacterHorizontalBar
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.Centre,
            },
        };

        public char Character
        {
            get => c;
            set
            {
                for (int i = 0; i < Bars.Length; i++)
                    Bars[i].Enabled = Characters[value][i] == 1;
                c = value;
            }
        }

        public LCDCharacter(char character)
            : base()
        {
            RelativeSizeAxes = Axes.None;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Children = Bars;
            Character = character;
            Size = new Vector2(DimensionSize * (SizeRatio + 1) + Spacing * 2, DimensionSize * (SizeRatio + 1) * 2 + Spacing * 4);
        }
    }
}
