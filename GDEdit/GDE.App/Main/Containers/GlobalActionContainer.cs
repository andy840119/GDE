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
            //Object Manipulation
            new KeyBinding(InputKey.D, GlobalAction.objMoveRight),
            new KeyBinding(InputKey.A, GlobalAction.objMoveLeft),
            new KeyBinding(InputKey.W, GlobalAction.objMoveUp),
            new KeyBinding(InputKey.S, GlobalAction.objMoveDown),

            new KeyBinding(new[] { InputKey.Shift, InputKey.D }, GlobalAction.objMoveRightSmall),
            new KeyBinding(new[] { InputKey.Shift, InputKey.A }, GlobalAction.objMoveLeftSmall),
            new KeyBinding(new[] { InputKey.Shift, InputKey.W }, GlobalAction.objMoveRightSmall),
            new KeyBinding(new[] { InputKey.Shift, InputKey.S }, GlobalAction.objMoveRightSmall),

            //Others
            new KeyBinding(new[] { InputKey.Alt, InputKey.Control, InputKey.F2 }, GlobalAction.LordsKeys),
        };

        public GlobalActionContainer(KeyCombinationMatchingMode keyCombinationMatchingMode = KeyCombinationMatchingMode.Any, SimultaneousBindingMode simultaneousBindingMode = SimultaneousBindingMode.All)
            : base(simultaneousBindingMode, keyCombinationMatchingMode)
        {
        }
    }

    public enum GlobalAction
    {
        //Object Manipulation
        [Description("Manipulates the objects")]
        objMoveRight,
        objMoveLeft,
        objMoveUp,
        objMoveDown,
        objMoveRightSmall,
        objMoveLeftSmall,
        objMoveUpSmall,
        objMoveDownSmall,

        //Others
        [Description("Toggles the lords screen")]
        LordsKeys
    }
}
