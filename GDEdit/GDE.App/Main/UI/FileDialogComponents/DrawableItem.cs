using GDE.App.Main.Colors;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using System;
using System.Collections.Generic;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public class DrawableItem : Container, IHasFilterTerms
    {
        private string itemName = "";

        private BindableBool selected = new BindableBool(false);

        private Color4 selectedForegroundColor = GDEColors.FromHex("66ccff");
        private Color4 deselectedForegroundColor = Color4.White;
        private Color4 selectedBackgroundColor = GDEColors.FromHex("606060");
        private Color4 hoveredBackgroundColor = GDEColors.FromHex("404040");
        private Color4 deselectedBackgroundColor = Color4.Transparent;

        private Box background;
        private SpriteText text;
        private SpriteIcon icon;

        private IconUsage itemIcon = FontAwesome.Regular.FileAlt;

        public Action Action;
        public Action OnClicked;

        public IconUsage ItemIcon
        {
            get => itemIcon;
            set => icon.Icon = itemIcon = value;
        }

        public string ItemName
        {
            get => itemName;
            set => text.Text = itemName = value;
        }

        public bool Selected
        {
            get => selected.Value;
            set => selected.Value = value;
        }

        public Color4 Color
        {
            get => selectedForegroundColor;
            set => text.Colour = icon.Colour = selectedForegroundColor = value;
        }

        public IEnumerable<string> FilterTerms => new[]
        {
            itemName
        };

        public DrawableItem()
        {
            RelativeSizeAxes = Axes.X;
            Height = 30;
            CornerRadius = 5;
            Masking = true;

            AddRange(new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = deselectedBackgroundColor,
                },
                new FillFlowContainer
                {
                    Spacing = new Vector2(5, 0),
                    Direction = FillDirection.Horizontal,
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Padding = new MarginPadding { Left = 5 },
                    Children = new Drawable[]
                    {
                        icon = new SpriteIcon
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(25),
                            Icon = itemIcon,
                        },
                        text = new SpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Text = itemName,
                        }
                    }
                },
            });

            selected.ValueChanged += FadeColour;
        }

        public void ToggleSelection() => selected.Toggle();

        private void FadeColour(ValueChangedEvent<bool> value)
        {
            var newColor = value.NewValue ? selectedForegroundColor : deselectedForegroundColor;
            icon.FadeColour(newColor, 200);
            text.FadeColour(newColor, 200);
            background.FadeColour(value.NewValue ? selectedBackgroundColor : deselectedBackgroundColor, 200);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!Selected)
                background.FadeColour(hoveredBackgroundColor, 200);
            return base.OnHover(e);
        }
        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!Selected)
                background.FadeColour(deselectedBackgroundColor, 200);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            ToggleSelection();
            OnClicked?.Invoke();
            return base.OnClick(e);
        }
        protected override bool OnDoubleClick(DoubleClickEvent e)
        {
            Action?.Invoke();
            return base.OnDoubleClick(e);
        }
    }
}
