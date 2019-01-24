using GDE.App.Main.Colours;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace GDE.App.Main.Panels
{
    public class Panel : FocusedOverlayContainer
    {
        private SpriteText text;
        private PinButton pin;

        public bool AllowDrag = true;
        public string Text
        {
            get => text.Text;
            set => text.Text = value;
        }

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
                            Action = Hide
                        },
                        pin = new PinButton
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
                                if (AllowDrag)
                                    pin.Rotation = 45;
                                else
                                    pin.Rotation = 0;
                                AllowDrag = !AllowDrag;
                            }
                        },
                    }
                },
                text = new SpriteText
                {
                    Margin = new MarginPadding
                    {
                        Horizontal = 5,
                        Vertical = 2
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            Scale = new Vector2(1, 0);
            base.LoadComplete();
        }

        protected override void PopIn()
        {
            this.ScaleTo(new Vector2(1, 1), 500, Easing.OutExpo);
            base.PopIn();
        }

        protected override void PopOut()
        {
            this.ScaleTo(new Vector2(1, 0), 500, Easing.OutExpo);
            base.PopIn();
        }

        protected override bool OnDrag(DragEvent e)
        {
            if (!AllowDrag)
                return false;

            Position += e.Delta;
            return true;
        }

        protected override bool OnDragEnd(DragEndEvent e) => true;

        protected override bool OnDragStart(DragStartEvent e) => AllowDrag;
    }
}
