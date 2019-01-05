using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the J special block.</summary>
    public class JSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the J special block.</summary>
        public override short ObjectID => (short)(int)SpecialBlockType.J;

        /// <summary>Initializes a new instance of the <seealso cref="JSpecialBlock"/> class.</summary>
        public JSpecialBlock() : base() { }
    }
}
