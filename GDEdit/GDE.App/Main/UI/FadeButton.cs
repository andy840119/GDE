using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
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

        private void EnabledChanged(ValueChangedEvent<bool> v) => this.TransformTo(nameof(BackgroundColour), GetAppropriateColor(v.NewValue), 200);

        private Color4 GetAppropriateColor(bool enabled) => EnabledColor.Darken(enabled ? 0 : 0.5f);
    }
}
