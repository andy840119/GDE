using osu.Framework.Bindables;
using osu.Framework.Graphics.Colour;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static GDE.App.Main.Colors.GDEColors;
using osuTK.Graphics;

namespace GDE.App.Main.UI
{
    public class FadeButton : Button
    {
        private Color4 enabledColor;

        public Color4 EnabledColor
        {
            get => enabledColor;
            set
            {
                enabledColor = value;
                BackgroundColour = GetAppropriateColor(Enabled.Value);
            }
        }

        public FadeButton()
            : base()
        {
            Enabled.ValueChanged += EnabledChanged;
        }

        private void EnabledChanged(ValueChangedEvent<bool> v) => BackgroundColour = GetAppropriateColor(v.NewValue);

        private Color4 GetAppropriateColor(bool enabled) => EnabledColor.Darken(enabled ? 0 : 0.5f);
    }
}
