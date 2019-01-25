using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a hitbox that has a width property.</summary>
    public interface IHasWidth
    {
        /// <summary>The width of the hitbox.</summary>
        double Width { get; set; }
    }
}
