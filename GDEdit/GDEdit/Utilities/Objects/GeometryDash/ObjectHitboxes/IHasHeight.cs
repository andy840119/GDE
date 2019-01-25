using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a hitbox that has a height property.</summary>
    public interface IHasHeight
    {
        /// <summary>The height of the hitbox.</summary>
        double Height { get; set; }
    }
}
