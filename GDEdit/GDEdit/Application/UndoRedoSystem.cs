using System;
using System.Collections.Generic;
using static System.Math;

namespace GDEdit.Application
{
    /// <summary>A system that allows handling undoing and redoing undoable actions.</summary>
    public class UndoRedoSystem
    {
        // I know, the naming is terrible on this one (hence the documentation), we need to find a better name
        /// <summary>Determines whether multiple actions will be logged in the undo stack.</summary>
        public bool MultipleActionToggle;

        /// <summary>The temporarily stored undoable action that is to be registered upon completing an operation.</summary>
        public UndoableAction TemporaryUndoableAction = new UndoableAction();
        /// <summary>The stack that contains all actions that are to be undone.</summary>
        public readonly Stack<UndoableAction> UndoStack = new Stack<UndoableAction>();
        /// <summary>The stack that contains all actions that are to be redone.</summary>
        public readonly Stack<UndoableAction> RedoStack = new Stack<UndoableAction>();

        /// <summary>Registers all the actions.</summary>
        /// <param name="description"></param>
        public void RegisterActions(string description)
        {
            TemporaryUndoableAction.Description = description;
            UndoStack.Push(TemporaryUndoableAction);
            TemporaryUndoableAction = new UndoableAction();
            RedoStack.Clear();
        }
        /// <summary>Adds a temporary action to the temporary action object and registers the undoable action if the multiple action toggle is <see langword="false"/>.</summary>
        /// <param name="description">The description of the actions.</param>
        /// <param name="action">The action. It must only perform the changes the action performs without invoking the respective events.</param>
        /// <param name="undo">The inverse action. It must only perform the changes the inverse action performs without invoking the respective events.</param>
        public void AddTemporaryAction(string description, Action action, Action undo)
        {
            TemporaryUndoableAction.Add(action, undo);
            if (!MultipleActionToggle)
                RegisterActions(description);
        }

        /// <summary>Undoes a number of actions. If the specified count is greater than the available actions to undo, all actions are undone.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void Undo(int count = 1)
        {
            int actions = Min(count, UndoStack.Count);
            for (int i = 0; i < actions; i++)
            {
                var action = UndoStack.Pop();
                action.Undo();
                RedoStack.Push(action);
            }
        }
        /// <summary>Redoes a number of actions. If the specified count is greater than the available actions to redo, all actions are redone.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void Redo(int count = 1)
        {
            int actions = Min(count, RedoStack.Count);
            for (int i = 0; i < actions; i++)
            {
                var action = RedoStack.Pop();
                action.Redo();
                UndoStack.Push(action);
            }
        }
    }
}
