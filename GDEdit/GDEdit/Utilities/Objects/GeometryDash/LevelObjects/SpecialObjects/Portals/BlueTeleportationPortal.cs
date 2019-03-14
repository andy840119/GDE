using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a blue teleportation portal.</summary>
    [ObjectID(PortalType.BlueTeleportation)]
    public class BlueTeleportationPortal : Portal
    {
        private YellowTeleportationPortal linkedYellowTeleportationPortal;

        /// <summary>The <seealso cref="YellowTeleportationPortal"/> that this <seealso cref="BlueTeleportationPortal"/> is linked to.</summary>
        public YellowTeleportationPortal LinkedYellowTeleportationPortal
        {
            get
            {
                if (linkedYellowTeleportationPortal == null)
                    linkedYellowTeleportationPortal = new YellowTeleportationPortal(this);
                return linkedYellowTeleportationPortal;
            }
            private set => linkedYellowTeleportationPortal = value;
        }

        /// <summary>The object ID of the blue teleportation portal.</summary>
        public override int ObjectID => (int)PortalType.BlueTeleportation;

        /// <summary>The distance of the Y location between the yellow and this teleportation portals.</summary>
        [ObjectStringMappable(ObjectParameter.YellowTeleportationPortalDistance)]
        public double YellowTeleportationPortalDistance { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="BlueTeleportationPortal"/> class.</summary>
        public BlueTeleportationPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueTeleportationPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueTeleportationPortal());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as BlueTeleportationPortal;
            c.LinkedYellowTeleportationPortal = LinkedYellowTeleportationPortal;
            c.YellowTeleportationPortalDistance = YellowTeleportationPortalDistance;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
