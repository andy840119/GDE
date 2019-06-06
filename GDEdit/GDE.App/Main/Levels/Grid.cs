using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics;
using osuTK.Graphics;
using osuTK;
using GDE.App.Main.Screens.Edit.Components;
using GDEdit.Utilities.Objects.GeometryDash;

namespace GDE.App.Main.Levels
{
    public class Grid : Container, IDraggable
    {
        private double zoom;
        private Vector2 position;

        private Container gridContainer;
        private Drawable[] gridLineDrawables;

        public bool Draggable => true;

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
            RelativeSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                gridContainer = new Container
                {
                    BorderThickness = 2,
                    Masking = true,
                    RelativeSizeAxes = Axes.Both,
                }
            };

            UpdateGridLines();
        }

        private void UpdateGridLines()
        {
            var gridLines = new Drawable[200];

            for (int i = 0; i < 100; i++)
                gridLines[i] = new HorizontalGridLine(30 * i)
                {
                    Colour = Color4.Black.Opacity(0.5f)
                };
            for (int i = 0; i < 100; i++)
                gridLines[100 + i] = new VerticalGridLine(30 * i)
                {
                    Colour = Color4.Black.Opacity(0.5f)
                };

            gridContainer.Children = gridLineDrawables = gridLines;
        }
    }
}
