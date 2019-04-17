using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics;
using osuTK.Graphics;
using osuTK;

namespace GDE.App.Main.Levels
{
    public class Grid : Container
    {
        private double zoom;
        private Vector2 position;

        private Container gridContainer;
        private Drawable[] gridLineDrawables;

        public Vector2 GridPosition
        {
            get => position;
            set
            {
                UpdateGridLines();
                position = value;
            }
        }
        public double Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
            }
        }

        public Grid()
        {
            Children = new Drawable[]
            {
                gridContainer = new Container
                {
                    BorderColour = Color4.Black.Opacity(0.7f),
                    BorderThickness = 2,
                    Masking = true,
                    RelativeSizeAxes = Axes.Both,
                    Children = gridLineDrawables = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(128, 128, 128, 128),
                            AlwaysPresent = true,
                        }
                    }
                }
            };
        }

        private void UpdateGridLines()
        {
            // TODO: Figure out a way to retrieve the number of grid lines to render each time
            // This is currently a small test
            var gridLines = new Drawable[200];
            for (int i = 0; i < 100; i++)
                gridLines[i] = new HorizontalGridLine
                {
                    Y = 30 * i
                };
            for (int i = 0; i < 100; i++)
                gridLines[100 + i] = new VerticalGridLine
                {
                    X = 30 * i
                };

            gridContainer.Children = gridLineDrawables = gridLines;
        }
    }
}
