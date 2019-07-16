using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public class GDEBreadcrumbNavigation<T> : BreadcrumbNavigation<T>
    {
        public GDEBreadcrumbNavigation()
        {
            AutoSizeAxes = Axes.X;
        }

        protected override FillFlowContainer<Breadcrumb> CreateAndAddFillFlowContainer()
        {
            var fillflow = new FillFlowContainer<Breadcrumb>
            {
                AutoSizeAxes = Axes.X,
                Spacing = new Vector2(3, 0),
                RelativeSizeAxes = Axes.Y,
            };

            AddInternal(fillflow);

            return fillflow;
        }

        protected override Breadcrumb CreateBreadcrumb(T value)
        {
            return new GDEBreadcrumb(value)
            {
                RelativeSizeAxes = Axes.Y,
            };
        }

        private class GDEBreadcrumb : Breadcrumb
        {
            private readonly SpriteText text;
            private readonly Box background;

            public string Text
            {
                get => text.Text;
                set => text.Text = value;
            }

            public GDEBreadcrumb(T value)
                : base(value)
            {
                AutoSizeAxes = Axes.X;
                Current.ValueChanged += args => background.Colour = args.NewValue ? Color4.DarkSlateGray : Color4.Gray;

                AddRangeInternal(new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Gray
                    },
                    text = new SpriteText
                    {
                        Origin = Anchor.CentreLeft,
                        Anchor = Anchor.CentreLeft,
                        Margin = new MarginPadding(3),
                        Text = value.ToString(),
                    }
                });
            }
        }
    }
}
