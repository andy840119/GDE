using System;
using GDEdit.Utilities.Objects.GeometryDash;

namespace GDEdit.Utilities.Attributes
{
    /// <summary>Marks a <seealso cref="LevelObject"/> that may not be explicitly generated in the editor.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NonGeneratableAttribute : Attribute
    {
    }
}