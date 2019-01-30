using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace GDE.App.Main.Containers
{
    public class ZoomableContainer : Container
    {
        public float currentZoom = 1;
        public float maxZoom = 40;
        public float minZoom = 0.05f;

        private readonly Container zoomedContent;
        protected override Container<Drawable> Content => zoomedContent;

        public ZoomableContainer()
        {
            base.Content.Add(zoomedContent = new Container
            {
                RelativeSizeAxes = Axes.Both
            });
        }
    }
}
