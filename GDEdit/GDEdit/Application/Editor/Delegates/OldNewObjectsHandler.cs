using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDEdit.Application.Editor.Delegates
{
    /// <summary>Represents a function that contains information about changing a field of type <typeparamref name="T"/>, including its current and its previous values.</summary>
    /// <param name="newObjects">The new objects.</param>
    /// <param name="oldObjects">The old objects.</param>
    public delegate void OldNewObjectsHandler(LevelObjectCollection newObjects, LevelObjectCollection oldObjects, bool registerUndoable);
}
