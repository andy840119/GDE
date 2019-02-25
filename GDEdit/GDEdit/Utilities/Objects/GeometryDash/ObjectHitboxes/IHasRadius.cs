using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a hitbox that has a radius property.</summary>
    public interface IHasRadius
    {
        /// <summary>The radius of the hitbox.</summary>
        double Radius { get; set; }
    }
}
