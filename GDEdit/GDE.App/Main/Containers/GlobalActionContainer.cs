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
            new KeyBinding(InputKey.D, GlobalAction.ObjMoveRight),
            new KeyBinding(InputKey.A, GlobalAction.ObjMoveLeft),
            new KeyBinding(InputKey.W, GlobalAction.ObjMoveUp),
            new KeyBinding(InputKey.S, GlobalAction.ObjMoveDown),

            new KeyBinding(new[] { InputKey.Shift, InputKey.D }, GlobalAction.ObjMoveRightSmall),
            new KeyBinding(new[] { InputKey.Shift, InputKey.A }, GlobalAction.ObjMoveLeftSmall),
            new KeyBinding(new[] { InputKey.Shift, InputKey.W }, GlobalAction.ObjMoveRightSmall),
            new KeyBinding(new[] { InputKey.Shift, InputKey.S }, GlobalAction.ObjMoveRightSmall),

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
        ObjMoveRight,
        ObjMoveLeft,
        ObjMoveUp,
        ObjMoveDown,
        ObjMoveRightSmall,
        ObjMoveLeftSmall,
        ObjMoveUpSmall,
        ObjMoveDownSmall,

        //Others
        [Description("Toggles the lords screen")]
        LordsKeys
    }
}
