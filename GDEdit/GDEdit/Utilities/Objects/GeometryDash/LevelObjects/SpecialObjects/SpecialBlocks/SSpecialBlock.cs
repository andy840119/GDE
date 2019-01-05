using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the S special block.</summary>
    public class SSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the S special block.</summary>
        public override short ObjectID => (short)(int)SpecialBlockType.S;

        /// <summary>Initializes a new instance of the <seealso cref="SSpecialBlock"/> class.</summary>
        public SSpecialBlock() : base() { }
    }
}
