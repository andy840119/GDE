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
    /// <summary>Represents a wave portal.</summary>
    [ObjectID(PortalType.Wave)]
    public class WavePortal : GamemodePortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the wave portal.</summary>
        public override int ObjectID => (int)PortalType.Wave;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Wave;

        /// <summary>The checked property of the wave portal that determines whether the borders of the player's gamemode will be shown or not.</summary>
        [ObjectStringMappable(ObjectParameter.PortalChecked)]
        public bool Checked
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="WavePortal"/> class.</summary>
        public WavePortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="WavePortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new WavePortal());
    }
}
