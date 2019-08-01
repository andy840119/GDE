using System;
using System.Collections.Generic;
using System.Text;

namespace GDEdit.Application
{
    /// <summary>Contains information about an action that can be undone.</summary>
    public class UndoableAction
    {
        private readonly List<UndoableLinkedAction> links = new List<UndoableLinkedAction>();

        /// <summary>The description of the undoable action.</summary>
        public string Description { get; set; }

        /// <summary>Gets the count of actions that are registered in this undoable action.</summary>
        public int Count => links.Count;

        /// <summary>Initializes a new instance of the <seealso cref="UndoableAction"/> class.</summary>
        /// <param name="description">The description to set for this action. Defaults to <see langword="null"/>.</param>
        public UndoableAction(string description = null) => Description = description;

        /// <summary>Adds an undoable action to the action list.</summary>
        /// <param name="action">The action to add to the list.</param>
        /// <param name="undo">The undo action to add to the list.</param>
        public void Add(Action action, Action undo) => links.Add(new UndoableLinkedAction(action, undo));

        /// <summary>Undoes all the actions in the list.</summary>
        public void Undo()
        {
            for (int i = links.Count - 1; i >= 0; i--)
                links[i].Undo.Invoke();
        }
        /// <summary>Redoes all the actions in the list.</summary>
        public void Redo()
        {
            for (int i = 0; i < links.Count - 1; i++)
                links[i].Action.Invoke();
        }
        /// <summary>Clears the action list.</summary>
        public void Clear() => links.Clear();
    }
}
