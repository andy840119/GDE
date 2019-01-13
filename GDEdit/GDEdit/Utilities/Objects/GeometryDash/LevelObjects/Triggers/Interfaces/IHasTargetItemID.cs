using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Item ID.</summary>
    public interface IHasTargetItemID
    {
        /// <summary>The target Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ItemID)]
        int TargetItemID { get; set; }
    }
}
