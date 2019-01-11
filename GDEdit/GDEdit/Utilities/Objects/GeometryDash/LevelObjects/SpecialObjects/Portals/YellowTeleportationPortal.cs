using GDEdit.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    // TODO: Create an attribute to mark this and other potential unusable objects
    /// <summary>Represents a yellow teleportation portal. Should not be available to be created through the editor.</summary>
    public class YellowTeleportationPortal : Portal
    {
        // This is static to avoid getting the exact same value more than once
        private static readonly PropertyInfo[] properties = typeof(GeneralObject).GetProperties();

        /// <summary>The blue teleportation portal to which this belongs.</summary>
        public readonly BlueTeleportationPortal LinkedTeleportationPortal;

        /// <summary>The object ID of the yellow teleportation portal.</summary>
        public override int ObjectID => (int)PortalType.YellowTeleportation;

        /// <summary>Initializes a new instance of the <seealso cref="YellowTeleportationPortal"/> class.</summary>
        public YellowTeleportationPortal(BlueTeleportationPortal p)
            : base()
        {
            LinkedTeleportationPortal = p;
            SetProperties(p);
        }

        /// <summary>Sets the properties of this <seealso cref="YellowTeleportationPortal"/> according to the linked <seealso cref="BlueTeleportationPortal"/>.</summary>
        private void SetProperties(BlueTeleportationPortal a)
        {
            foreach (var p in properties)
                p.SetValue(this, p.GetValue(a));
            Y = a.Y + a.YellowTeleportationPortalDistance;
            Rotation = a.Rotation;
        }
    }
}
