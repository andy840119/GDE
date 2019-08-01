using System;
using System.Collections.Generic;
using System.Text;

namespace GDEdit.Application
{
    /// <summary>Contains information about an action that can be undone.</summary>
    public class UndoableAction
    {
        private readonly List<Action> undos = new List<Action>();
        private readonly List<Action> actions = new List<Action>();

        /// <summary>The description of the undoable action.</summary>
        public string Description { get; set; }

        public UndoableAction() { }
        public UndoableAction(string description) => Description = description;

        /// <summary>Adds an undoable action to the action list.</summary>
        /// <param name="action">The action to add to the list.</param>
        /// <param name="undo">The undo action to add to the list.</param>
        public void Add(Action action, Action undo)
        {
            actions.Add(action);
            undos.Add(undo);
        }

        /// <summary>Undoes all the actions in the list.</summary>
        public void Undo()
        {
            foreach (var u in undos)
                u.Invoke();
        }
        /// <summary>Redoes all the actions in the list.</summary>
        public void Redo()
        {
            foreach (var a in actions)
                a.Invoke();
        }
        /// <summary>Clears the action list.</summary>
        public void Clear() => actions.Clear();
    }
}
