using System;
using System.Collections.Generic;
using System.Text;

namespace GDEdit.Application
{
    /// <summary>Represents an action that is linked with its undo version.</summary>
    public class UndoableLinkedAction 
    {
        private (Action Action, Action Undo) linkedAction;

        /// <summary>Gets or sets the original action.</summary>
        public Action Action
        {
            get => linkedAction.Action;
            set => linkedAction.Action = value;
        }
        /// <summary>Gets or sets the undoable action.</summary>
        public Action Undo
        {
            get => linkedAction.Undo;
            set => linkedAction.Undo = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="UndoableLinkedAction"/> class.</summary>
        /// <param name="action">The original action of the <seealso cref="UndoableLinkedAction"/>.</param>
        /// <param name="undo">The undoable action of the <seealso cref="UndoableLinkedAction"/>.</param>
        public UndoableLinkedAction(Action action, Action undo) => linkedAction = (action, undo);
    }
}
