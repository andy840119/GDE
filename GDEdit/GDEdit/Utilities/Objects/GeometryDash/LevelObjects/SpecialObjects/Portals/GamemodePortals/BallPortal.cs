using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a ball portal.</summary>
    [ObjectID(PortalType.Ball)]
    public class BallPortal : GamemodePortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the ball portal.</summary>
        public override int ObjectID => (int)PortalType.Ball;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Ball;

        /// <summary>The checked property of the ball portal that determines whether the borders of the player's gamemode will be shown or not.</summary>
        [ObjectStringMappable(ObjectParameter.PortalChecked)]
        public bool Checked
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="BallPortal"/> class.</summary>
        public BallPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BallPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BallPortal());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as BallPortal;
            c.Checked = Checked;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
