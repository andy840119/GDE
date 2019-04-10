using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;

namespace GDE.App.Main.Containers
{
    public class GlobalActionContainer : KeyBindingContainer<GlobalAction>
    {
        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.Alt, InputKey.Control, InputKey.F2 }, GlobalAction.LordsKeys)
        };

        public GlobalActionContainer(KeyCombinationMatchingMode keyCombinationMatchingMode = KeyCombinationMatchingMode.Any, SimultaneousBindingMode simultaneousBindingMode = SimultaneousBindingMode.All)
            : base(simultaneousBindingMode, keyCombinationMatchingMode)
        {
        }
    }

    public enum GlobalAction
    {
        //Others
        [Description("Toggles the lords screen")]
        LordsKeys
    }
}
