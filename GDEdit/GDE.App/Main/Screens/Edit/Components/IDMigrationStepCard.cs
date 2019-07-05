using System;
using System.Collections.Generic;
using System.Linq;
using GDE.App.Main.Colors;
using GDEdit.Application;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using static System.Convert;

namespace GDE.App.Main.Screens.Edit.Components
{
    public class IDMigrationStepCard : ClickableContainer, IFilterable
    {
        private bool matchingFilter, filteringActive;

        private Box selectionBar;
        private Box stepIndexContainer;
        private Box hoverBox;
        private DraggableCardContainer stepDragContainer;
        private SpriteText rightArrow, sourceText, targetText;

        public Bindable<bool> Selected = new Bindable<bool>(false);
        public int Index;

        public readonly SourceTargetRange StepRange;

        public Action<DragEvent> CardDragged;

        public bool MatchingFilter
        {
            set => Alpha = ToInt32(!filteringActive || (matchingFilter = value));
        }
        public bool FilteringActive
        {
            set => Alpha = ToInt32(!(filteringActive = value) || matchingFilter);
        }

        public IEnumerable<string> FilterTerms => new List<string>
        {
            StepRange.ToString(),
            StepRange.SourceToString(),
            StepRange.TargetToString(),
            StepRange.SourceFrom.ToString(),
            StepRange.SourceTo.ToString(),
            StepRange.TargetFrom.ToString(),
            StepRange.TargetTo.ToString()
        };
        

        public IDMigrationStepCard(SourceTargetRange range)
        {
            StepRange = range;

            CornerRadius = 5;
            Masking = true;

            Children = new Drawable[]
            {
                hoverBox = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = GDEColors.FromHex("161616")
                },
                selectionBar = new Box
                {
                    RelativeSizeAxes = Axes.Y,
                    Size = new Vector2(5, 1),
                    Colour = GDEColors.FromHex("202020")
                },
                selectionBar = new Box
                {
                    RelativeSizeAxes = Axes.Y,
                    Size = new Vector2(20, 1),
                    Colour = GDEColors.FromHex("404040")
                },
                rightArrow = new SpriteText
                {
                    Text = ">",
                    Font = new FontUsage(size: 14),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0.5f,
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Both,
                    // TODO: Migrate to a class called IDMigrationStepText
                    Children = new Drawable[]
                    {
                        sourceText = new SpriteText
                        {
                            Text = range.SourceToString(),
                            Font = new FontUsage(size: 14)
                        },
                        targetText = new SpriteText
                        {
                            Text = range.TargetToString(),
                            Font = new FontUsage(size: 14)
                        },
                    }
                },
                stepDragContainer = new DraggableCardContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Y,
                            Size = new Vector2(25, 1),
                            Colour = GDEColors.FromHex("404040"),
                            
                        },
                        new SpriteIcon
                        {
                            Icon = FontAwesome.Solid.Bars,
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both
                        }
                    },
                    CardDragged = e =>
                    {
                        Position += e.Delta;
                        CardDragged?.Invoke(e);
                    },
                    Size = new Vector2(25, 1),
                    Colour = GDEColors.FromHex("404040")
                },
            };

            Selected.ValueChanged += OnSelected;

        }

        /// <summary>Initializes the right arrow's animation. This function should be only called once.</summary>
        public void InitializeArrowAnimation()
        {
            rightArrow
                .MoveToOffset(new Vector2(20, 0), 125, Easing.InElastic).FadeTo(0)
                .Then(125)
                .MoveToOffset(new Vector2(-40, 0)) // Reset label position
                .Then()
                .MoveToOffset(new Vector2(20, 0), 125, Easing.InElastic).FadeTo(0.5f)
                .Loop(3000);
        }

        private void OnSelected(ValueChangedEvent<bool> value) => selectionBar.FadeColour(GDEColors.FromHex(value.OldValue ? "202020" : "00bc5c"), 200);

        protected override bool OnHover(HoverEvent e)
        {
            hoverBox.FadeColour(GDEColors.FromHex("323232"), 500);
            return base.OnHover(e);
        }
        protected override void OnHoverLost(HoverLostEvent e)
        {
            hoverBox.FadeColour(GDEColors.FromHex("161616"), 500);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Selected.Value = !Selected.Value;
            return base.OnClick(e);
        }

        private class DraggableCardContainer : Container
        {
            public Action<DragEvent> CardDragged;

            protected override bool OnDrag(DragEvent e)
            {
                CardDragged?.Invoke(e);
                return base.OnDrag(e);
            }
        }
    }
}
