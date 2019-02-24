using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a shape that has a radius property.</summary>
    public interface IHasRadius
    {
        /// <summary>The radius of the shape.</summary>
        double Radius { get; set; }
    }
}
