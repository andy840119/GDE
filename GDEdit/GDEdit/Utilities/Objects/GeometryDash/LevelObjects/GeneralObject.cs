using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects
{
    // TODO: Remove LevelObject entirely once completely migrated to this
    /// <summary>Represents a general object.</summary>
    public class GeneralObject
    {
        private static ObjectTypeInfo[] initializableObjectTypes = typeof(GeneralObject).Assembly.GetTypes().Where(t => t.GetCustomAttribute<ObjectIDAttribute>() != null).ToList().ConvertAll(t => new ObjectTypeInfo(t)).ToArray();

        private short[] groupIDs = new short[0];
        private BitArray8 bools = new BitArray8();
        private short objectID, el1, el2, zLayer, zOrder, color1ID, color2ID;
        private float rotation, scaling = 1;
        
        /// <summary>The Object ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public int ObjectID
        {
            get => objectID;
            set => objectID = (short)value;
        }
        /// <summary>The X position of this object.</summary>
        [ObjectStringMappable(ObjectParameter.X)]
        public double X { get; set; }
        /// <summary>The Y position of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Y)]
        public double Y { get; set; }
        /// <summary>Determines whether this object is flipped horizontally or not.</summary>
        [ObjectStringMappable(ObjectParameter.FlippedHorizontally)]
        public bool FlippedHorizontally
        {
            get => bools[0];
            set => bools[0] = value;
        }
        /// <summary>Determines whether this object is flipped vertically or not.</summary>
        [ObjectStringMappable(ObjectParameter.FlippedVertically)]
        public bool FlippedVertically
        {
            get => bools[1];
            set => bools[1] = value;
        }
        /// <summary>The rotation of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Rotation)]
        public double Rotation
        {
            get => rotation;
            set
            {
                rotation = (float)value;
                if (rotation > 360)
                    rotation -= 360;
                else if (rotation < -360)
                    rotation += 360;
            }
        }
        /// <summary>The scaling of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Scaling)]
        public double Scaling
        {
            get => scaling;
            set => scaling = (float)value;
        }
        /// <summary>The Editor Layer 1 of this object.</summary>
        [ObjectStringMappable(ObjectParameter.EL1)]
        public int EL1
        {
            get => el1;
            set => el1 = (short)value;
        }
        /// <summary>The Editor Layer 2 of this object.</summary>
        [ObjectStringMappable(ObjectParameter.EL2)]
        public int EL2
        {
            get => el2;
            set => el2 = (short)value;
        }
        /// <summary>The Z Layer of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ZLayer)]
        public int ZLayer
        {
            get => zLayer;
            set => zLayer = (short)value;
        }
        /// <summary>The Z Order of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ZOrder)]
        public int ZOrder
        {
            get => zOrder;
            set => zOrder = (short)value;
        }
        /// <summary>The Color 1 ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Color1)]
        public int Color1ID
        {
            get => color1ID;
            set => color1ID = (short)value;
        }
        /// <summary>The Color 2 ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Color2)]
        public int Color2ID
        {
            get => color2ID;
            set => color2ID = (short)value;
        }
        /// <summary>The Group IDs of this object.</summary>
        [ObjectStringMappable(ObjectParameter.GroupIDs)]
        public int[] GroupIDs
        {
            get => groupIDs.CastToInt();
            set => groupIDs = value.CastToShort();
        }
        /// <summary>The linked group ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.LinkedGroupID)]
        public int LinkedGroupID { get; set; }
        /// <summary>Determines whether this object is the group parent or not.</summary>
        [ObjectStringMappable(ObjectParameter.GroupParent)]
        public bool GroupParent
        {
            get => bools[2];
            set => bools[2] = value;
        }
        /// <summary>Determines whether this object is for high detail or not.</summary>
        [ObjectStringMappable(ObjectParameter.HighDetail)]
        public bool HighDetail
        {
            get => bools[3];
            set => bools[3] = value;
        }
        /// <summary>Determines whether this object should have an entrance effect or not.</summary>
        [ObjectStringMappable(ObjectParameter.DontEnter)]
        public bool DontEnter
        {
            get => bools[4];
            set => bools[4] = value;
        }
        /// <summary>Determines whether this object should have the fade in and out disabled or not.</summary>
        [ObjectStringMappable(ObjectParameter.DontFade)]
        public bool DontFade
        {
            get => bools[5];
            set => bools[5] = value;
        }
        /// <summary>Determines whether this object should have its glow disabled or not.</summary>
        [ObjectStringMappable(ObjectParameter.DisableGlow)]
        public bool DisableGlow
        {
            get => bools[6];
            set => bools[6] = value;
        }

        /// <summary>Gets or sets a <seealso cref="Point"/> instance with the location of the object.</summary>
        public Point Location
        {
            get => new Point(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>The rotation of this object in degrees according to math.</summary>
        public double MathRotationDegrees
        {
            get => -Rotation;
            set => Rotation = -value;
        }
        /// <summary>The rotation of this object in radians according to math.</summary>
        public double MathRotationRadians
        {
            get => MathRotationDegrees * Math.PI / 180;
            set => MathRotationDegrees = value * 180 / Math.PI;
        }
        
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class with the object ID parameter set to 1.</summary>
        public GeneralObject()
        {
            ObjectID = 1;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID)
        {
            ObjectID = objectID;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, double x, double y)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="rotation">The rotation of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, double x, double y, double rotation)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
            Rotation = rotation;
        }

        /// <summary>Returns a clone of this <seealso cref="GeneralObject"/>.</summary>
        public virtual GeneralObject Clone() => AddClonedInstanceInformation(new GeneralObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected virtual GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            cloned.ObjectID = ObjectID;
            cloned.X = X;
            cloned.Y = Y;
            cloned.FlippedHorizontally = FlippedHorizontally;
            cloned.FlippedVertically = FlippedVertically;
            cloned.Rotation = Rotation;
            cloned.Scaling = Scaling;
            cloned.EL1 = EL1;
            cloned.EL2 = EL2;
            cloned.ZLayer = ZLayer;
            cloned.ZOrder = ZOrder;
            cloned.Color1ID = Color1ID;
            cloned.Color2ID = Color2ID;
            cloned.GroupIDs = GroupIDs;
            cloned.LinkedGroupID = LinkedGroupID;
            cloned.GroupParent = GroupParent;
            cloned.HighDetail = HighDetail;
            cloned.DontEnter = DontEnter;
            cloned.DontFade = DontFade;
            cloned.DisableGlow = DisableGlow;
            return cloned;
        }

        // Reflection is FUN
        public T GetParameterWithID<T>(int ID)
        {
            var properties = GetType().GetProperties();
            foreach (var p in properties)
                if (((ObjectStringMappableAttribute)p.GetCustomAttributes(typeof(ObjectStringMappableAttribute), false).FirstOrDefault())?.Key == ID)
                    return (T)p.GetValue(this);
            throw new KeyNotFoundException("The requested ID was not found.");
        }
        public void SetParameterWithID<T>(int ID, T newValue)
        {
            var properties = GetType().GetProperties();
            foreach (var p in properties)
                if (((ObjectStringMappableAttribute)p.GetCustomAttributes(typeof(ObjectStringMappableAttribute), false).FirstOrDefault())?.Key == ID)
                {
                    p.SetValue(this, newValue);
                    return;
                }
        }
        
        /// <summary>Determines whether the object's location is within a rectangle.</summary>
        /// <param name="startingX">The starting X position of the rectangle.</param>
        /// <param name="startingY">The starting Y position of the rectangle.</param>
        /// <param name="endingX">The ending X position of the rectangle.</param>
        /// <param name="endingY">The ending Y position of the rectangle.</param>
        public bool IsWithinRange(double startingX, double startingY, double endingX, double endingY) => startingX <= X && endingX >= X && startingY <= Y && endingY >= Y;
        
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(int objectID)
        {
            foreach (var t in initializableObjectTypes)
                if (t.ObjectID == objectID)
                {
                    if (t.NonGeneratableAttribute != null)
                        throw new InvalidOperationException(t.NonGeneratableAttribute.ExceptionMessage);
                    return t.Constructor.Invoke(null) as GeneralObject;
                }

            if (ObjectLists.RotatingObjectList.Contains(objectID))
                return new RotatingObject(objectID);
            if (ObjectLists.AnimatedObjectList.Contains(objectID))
                return new AnimatedObject(objectID);
            if (ObjectLists.PickupItemList.Contains(objectID))
                return new PickupItem(objectID);
            if (ObjectLists.PulsatingObjectList.Contains(objectID))
                return new PulsatingObject(objectID);

            return new GeneralObject(objectID);
        }

        private class ObjectTypeInfo
        {
            public Type ObjectType { get; }

            public int? ObjectID { get; }
            public ConstructorInfo Constructor { get; }
            public NonGeneratableAttribute NonGeneratableAttribute { get; }

            public ObjectTypeInfo(Type objectType)
            {
                ObjectType = objectType;
                ObjectID = objectType.GetCustomAttribute<ObjectIDAttribute>()?.ObjectID;
                Constructor = objectType.GetConstructor(Type.EmptyTypes);
                NonGeneratableAttribute = objectType.GetCustomAttribute<NonGeneratableAttribute>();
            }
        }
    }
}
