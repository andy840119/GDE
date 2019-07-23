using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using static osu.Framework.Input.Bindings.KeyCombinationMatchingMode;
using static osu.Framework.Input.Bindings.SimultaneousBindingMode;

namespace GDE.App.Main.Containers.KeyBindingContainers
{
    public class FileDialogActionContainer : KeyBindingContainer<FileDialogAction>
    {
        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(InputKey.Up, FileDialogAction.NavigateUp),
            new KeyBinding(InputKey.Down, FileDialogAction.NavigateDown),
            new KeyBinding(InputKey.PageUp, FileDialogAction.NavigatePageUp),
            new KeyBinding(InputKey.PageDown, FileDialogAction.NavigatePageDown),
            new KeyBinding(InputKey.Home, FileDialogAction.NavigateToStart),
            new KeyBinding(InputKey.End, FileDialogAction.NavigateToEnd),
            new KeyBinding(InputKey.BackSpace, FileDialogAction.NavigateToPreviousDirectory),
            new KeyBinding(InputKey.Enter, FileDialogAction.PerformAction),
        };

        public FileDialogActionContainer(KeyCombinationMatchingMode keyCombinationMatchingMode = Exact, SimultaneousBindingMode simultaneousBindingMode = All)
            : base(simultaneousBindingMode, keyCombinationMatchingMode) { }
    }

    public enum FileDialogAction
    {
        [Description("Navigates upwards")]
        NavigateUp,
        [Description("Navigates downwards")]
        NavigateDown,
        [Description("Cuts the selected ID migration steps to the clipboard")]
        NavigatePageUp,
        [Description("Copies the selected ID migration steps to the clipboard")]
        NavigatePageDown,
        [Description("Cuts the selected ID migration steps to the clipboard")]
        NavigateToStart,
        [Description("Copies the selected ID migration steps to the clipboard")]
        NavigateToEnd,
        [Description("Pastes the ID migration steps from the clipboard")]
        NavigateToPreviousDirectory,
        [Description("Clones the selected ID migration steps")]
        PerformAction,
    }
}
