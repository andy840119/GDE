using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.IO.Stores;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Caching;

namespace GDE.App.Main.UI
{
    public class SpriteIcon : CompositeDrawable
    {
        private Sprite spriteShadow;
        private Sprite spriteMain;

        private Cached layout = new Cached();
        private Container shadowVisibility;

        private FontStore store;

        [BackgroundDependencyLoader]
        private void load(FontStore store)
        {
            this.store = store;

            InternalChildren = new Drawable[]
            {
                shadowVisibility = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Child = spriteShadow = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit,
                        Y = 2,
                        Colour = new Color4(0f, 0f, 0f, 0.2f),
                    },
                    Alpha = shadow ? 1 : 0,
                },
                spriteMain = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit
                },
            };

            updateTexture();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateTexture();
        }

        private FontAwesome loadedIcon;
        private void updateTexture()
        {
            var loadableIcon = icon;

            if (loadableIcon == loadedIcon)
                return;

            var texture = store.Get(((char)loadableIcon).ToString());

            spriteMain.Texture = texture;
            spriteShadow.Texture = texture;

            if (Size == Vector2.Zero)
                Size = new Vector2(texture?.DisplayWidth ?? 0, texture?.DisplayHeight ?? 0);

            loadedIcon = loadableIcon;
        }

        public override bool Invalidate(Invalidation invalidation = Invalidation.All, Drawable source = null, bool shallPropagate = true)
        {
            if ((invalidation & Invalidation.Colour) > 0 && Shadow)
                layout.Invalidate();
            return base.Invalidate(invalidation, source, shallPropagate);
        }

        protected override void Update()
        {
            if (!layout.IsValid)
            {
                //adjust shadow alpha based on highest component intensity to avoid muddy display of darker text.
                //squared result for quadratic fall-off seems to give the best result.
                var avgColour = (Color4)DrawColourInfo.Colour.AverageColour;

                spriteShadow.Alpha = (float)Math.Pow(Math.Max(Math.Max(avgColour.R, avgColour.G), avgColour.B), 2);

                layout.Validate();
            }
        }

        private bool shadow;
        public bool Shadow
        {
            get
            {
                return shadow;
            }
            set
            {
                shadow = value;
                if (shadowVisibility != null)
                    shadowVisibility.Alpha = value ? 1 : 0;
            }
        }

        private FontAwesome icon;

        public FontAwesome Icon
        {
            get
            {
                return icon;
            }
            set
            {
                if (icon == value)
                        return;

                icon = value;
                if (LoadState == LoadState.Loaded)
                    updateTexture();
            }
        }
    }
}