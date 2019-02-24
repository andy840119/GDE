using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a shape that has a height property.</summary>
    public interface IHasHeight
    {
        /// <summary>The shape of the hitbox.</summary>
        double Height { get; set; }
    }
}
