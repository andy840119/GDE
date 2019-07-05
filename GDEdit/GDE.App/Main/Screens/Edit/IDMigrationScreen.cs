using DiscordRPC;
using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.Screens.Edit.Components;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDEdit.Application;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDE.App.Main.Screens.Edit
{
    public class IDMigrationScreen : Screen
    {
        private NumberTextBox sourceFrom;
        private NumberTextBox sourceTo;
        private NumberTextBox targetFrom;
        private NumberTextBox targetTo;
        private Button performAction;

        private IDMigrationStepList stepList;
        private Bindable<SourceTargetRange> range = new Bindable<SourceTargetRange>();
        private List<IDMigrationStepCard> cards = new List<IDMigrationStepCard>();

        private Editor editor;

        public IDMigrationScreen(Editor e)
        {
            editor = e;

            //Size = new Vector2(0.4f);

            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Y,
                    Width = 520,
                    Margin = new MarginPadding
                    {
                        Top = 10,
                    },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = GDEColors.FromHex("111111"),
                        },
                        stepList = new IDMigrationStepList(editor)
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 500,
                            Padding = new MarginPadding
                            {
                                Top = 10,
                                Left = 10,
                                Right = 10
                            }
                        },
                    }
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Spacing = new Vector2(5),
                    Margin = new MarginPadding { Left = 15, Right = 15 },
                    RelativeSizeAxes = Axes.Y,
                    Width = 150,
                    Children = new Drawable[]
                    {
                        GetNewSpriteText("Source From"),
                        sourceFrom = GetNewNumberTextBox(),
                        GetNewSpriteText("Source To"),
                        sourceTo = GetNewNumberTextBox(),
                        GetNewSpriteText("Target From"),
                        targetFrom = GetNewNumberTextBox(),
                        GetNewSpriteText("Target To"),
                        targetTo = GetNewNumberTextBox(),
                        performAction = new Button
                        {
                            Origin = Anchor.Centre,
                            Size = new Vector2(220, 32),
                            Text = "Perform Action",
                            BackgroundColour = GDEColors.FromHex("242424"),
                            Action = stepList.PerformMigration,
                        },
                    },
                }
            });

            stepList.StepSelected = s =>
            {
                range.Value = s.StepRange;
            };

            sourceFrom.NumberChanged += n => range.Value.SourceFrom = n;
            sourceTo.NumberChanged += n => range.Value.SourceTo = n;
            targetFrom.NumberChanged += n => range.Value.TargetFrom = n;
            targetTo.NumberChanged += n => range.Value.TargetTo = n;
        }

        private static NumberTextBox GetNewNumberTextBox() => new NumberTextBox
        {
            RelativeSizeAxes = Axes.X,
            Height = 30,
            Margin = new MarginPadding
            {
                Top = 5,
                Left = 5,
            },
        };
        private static SpriteText GetNewSpriteText(string text) => new SpriteText
        {
            Margin = new MarginPadding { Left = 5 },
            Text = text,
        };
    }
}