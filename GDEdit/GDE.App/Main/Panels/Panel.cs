using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;
using GDE.App.Main.Colours;
using osu.Framework.Input.Events;
using System;

namespace GDE.App.Main.Panels
{
    public class Panel : Container
    {
        public bool AllowDrag = true;
        private PinButton Pin;

        public Panel()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColours.FromHex("151515") 
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Direction = FillDirection.Horizontal,
                    Position = new Vector2(-17, 17),
                    Children = new Drawable[]
                    {
                        new CloseButton
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.TopRight,
                            Size = new Vector2(20),
                            Colour = GDEColours.FromHex("424242"),
                            Margin = new MarginPadding
                            {
                                Horizontal = 5,
                                Vertical = 7
                            },
                            Action = () => Hide()
                        },
                        Pin = new PinButton
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.TopRight,
                            Size = new Vector2(20),
                            Colour = GDEColours.FromHex("424242"),
                            Margin = new MarginPadding
                            {
                                Horizontal = 5,
                                Vertical = 7
                            },
                            Action = () => 
                            {
                                if(AllowDrag)
                                {
                                    AllowDrag = false;
                                    Pin.Rotation = 45;
                                }
                                else
                                {
                                    AllowDrag = true;
                                    Pin.Rotation = 0;
                                }
                            }
                        },
                    }
                }
            };
        }

        protected override bool OnDrag(DragEvent e)
        {
            if (!AllowDrag) return false;

            Position += e.Delta;
            return true;
        }

        protected override bool OnDragEnd(DragEndEvent e)
        {
            return true;
        }

        protected override bool OnDragStart(DragStartEvent e) => AllowDrag;
    }
}
