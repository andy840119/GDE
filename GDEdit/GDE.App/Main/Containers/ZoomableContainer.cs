using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace GDE.App.Main.Containers
{
    public class ZoomableContainer : Container
    {
        private readonly Container zoomedContent;

        protected override Container<Drawable> Content => zoomedContent;

        public const float MaxZoom = 40;
        public const float MinZoom = 0.05f;
        public float CurrentZoom = 1;

        public ZoomableContainer()
        {
            base.Content.Add(zoomedContent = new Container
            {
                RelativeSizeAxes = Axes.Both
            });
        }
    }
}
