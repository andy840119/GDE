using System;
using System.Collections.Generic;
using GDE.App.Main.Screens.Edit;
using GDE.App.Main.Screens.Edit.Components;
using GDEdit.Application.Editor;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Testing;

namespace GDE.Tests.Visual.TestCaseEditor
{
    public class TestCaseIDMigration : TestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new Type[] { typeof(IDMigrationScreen), typeof(IDMigrationStepCard), typeof(IDMigrationStepList) };

        public TestCaseIDMigration()
        {
            Add(new ScreenStack(new IDMigrationScreen(new Editor(new Level())))
            {
                RelativeSizeAxes = Axes.Both
            });
        }
    }
}
