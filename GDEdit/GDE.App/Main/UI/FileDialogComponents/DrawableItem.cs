using GDE.App.Main.Colors;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using System;
using System.Collections.Generic;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public class DrawableItem : FillFlowContainer, IHasFilterTerms
    {
        private string itemName = "";

        private BindableBool selected = new BindableBool(false);

        private Color4 color = Color4.White;

        private SpriteText text;
        private SpriteIcon icon;

        private IconUsage itemIcon = FontAwesome.Regular.FileAlt;

        public Action Action;
        public Action Click;

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
            get => color;
            set => text.Colour = icon.Colour = color = value;
        }

        public IEnumerable<string> FilterTerms => new[]
        {
            itemName
        };

        public DrawableItem()
        {
            Spacing = new Vector2(10, 0);
            Direction = FillDirection.Horizontal;
            AutoSizeAxes = Axes.Both;
            Padding = new MarginPadding
            {
                Left = 10
            };

            AddRange(new Drawable[]
            {
                icon = new SpriteIcon
                {
                    Icon = itemIcon,
                    Size = new Vector2(25),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                },
                text = new SpriteText
                {
                    Text = itemName,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                }
            });

            selected.ValueChanged += FadeColour;
        }

        private void FadeColour(ValueChangedEvent<bool> value)
        {
            if (value.NewValue)
            {
                icon.FadeColour(GDEColors.FromHex("66ccff"), 500);
                text.FadeColour(GDEColors.FromHex("66ccff"), 500);
            }
            else
            {
                icon.FadeColour(color, 500);
                text.FadeColour(color, 500);
            }
        }

        protected override bool OnClick(ClickEvent e)
        {
            Click?.Invoke();
            return base.OnClick(e);
        }
        protected override bool OnDoubleClick(DoubleClickEvent e)
        {
            Action?.Invoke();
            return base.OnDoubleClick(e);
        }
    }
}
