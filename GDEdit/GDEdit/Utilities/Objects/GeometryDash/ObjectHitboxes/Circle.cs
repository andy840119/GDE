using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a circular hitbox.</summary>
    public class Circle : HitboxType, IHasRadius
    {
        /// <summary>The radius of the circular hitbox.</summary>
        public double Radius { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="Circle"/> class.</summary>
        /// <param name="radius">The radius of the circular hitbox.</param>
        public Circle(double radius)
            : base()
        {
            Radius = radius;
        }
    }
}
