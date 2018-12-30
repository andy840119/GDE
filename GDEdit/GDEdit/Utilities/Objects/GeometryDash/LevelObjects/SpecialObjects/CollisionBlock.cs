using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a collision block.</summary>
    public class CollisionBlock : SpecialObject, IHasPrimaryBlockID
    {
        /// <summary>The object ID of the collision block.</summary>
        public new int ObjectID => (int)Enumerations.GeometryDash.SpecialObjectType.CollisionBlock;

        /// <summary>The Block ID of the collision block.</summary>
        public int PrimaryBlockID { get; set; }
        /// <summary>The Block ID of the collision block.</summary>
        public int BlockID
        {
            get => PrimaryBlockID;
            set => PrimaryBlockID = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="CollisionBlock"/> class.</summary>
        public CollisionBlock() : base() { }
    }
}
