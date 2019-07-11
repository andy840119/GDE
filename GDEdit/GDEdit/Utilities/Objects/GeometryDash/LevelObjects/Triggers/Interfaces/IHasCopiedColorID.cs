using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a copied Color ID.</summary>
    public interface IHasCopiedColorID
    {
        /// <summary>The copied Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID)]
        int CopiedColorID { get; set; }
    }
}
