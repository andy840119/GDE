using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osuTK;
using System;

namespace GDE.App.Main.Objects
{
    // Change this into a Drawable instead of a Container in a later commit
    ///<summary>A drawable <seealso cref="GeneralObject"/>.</summary>
    public class ObjectBase : Container
    {
        private GeneralObject lvlObj;
        private Box obj;
        private TextureStore textureStore;

        #region Level Object Variables
        ///<summary>The ID of the object.</summary>
        public int ObjectID
        {
            get => lvlObj.ObjectID;
            set => UpdateObjectID(lvlObj.ObjectID = value);
        }
        ///<summary>The X position of the object.</summary>
        public double ObjectX
        {
            get => lvlObj.X;
            set => UpdateObjectX(lvlObj.X = value);
        }
        ///<summary>The Y position of the object.</summary>
        public double ObjectY
        {
            get => lvlObj.Y;
            set => UpdateObjectY(lvlObj.Y = value);
        }
        ///<summary>Represents whether the object is flipped horizontally or not.</summary>
        public bool FlippedHorizontally
        {
            get => lvlObj.FlippedHorizontally;
            set => UpdateFlippedHorizontally(lvlObj.FlippedHorizontally = value);
        }
        ///<summary>Represents whether the object is flipped vertically or not.</summary>
        public bool FlippedVertically
        {
            get => lvlObj.FlippedVertically;
            set => UpdateFlippedVertically(lvlObj.FlippedVertically = value);
        }
        ///<summary>The rotation of the object.</summary>
        public double ObjectRotation
        {
            get => lvlObj.Rotation;
            set => UpdateObjectRotation(lvlObj.Rotation = value);
        }
        ///<summary>The scaling of the object.</summary>
        public double ObjectScaling
        {
            get => lvlObj.Scaling;
            set => UpdateObjectScaling(lvlObj.Scaling = value);
        }
        ///<summary>The Editor Layer 1 of the object.</summary>
        public int EL1
        {
            get => lvlObj.EL1;
            set => lvlObj.EL1 = value;
        }
        ///<summary>The Editor Layer 2 of the object.</summary>
        public int EL2
        {
            get => lvlObj.EL2;
            set => lvlObj.EL2 = value;
        }
        #endregion

        /// <summary>Initializes a new instance of the <seealso cref="ObjectBase"/> class.</summary>
        public ObjectBase(GeneralObject o)
        {
            Children = new Drawable[]
            {
                obj = new Box
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both
                }
            };

            Size = new Vector2(30);
            UpdateObject(lvlObj = o);
        }
        
        [BackgroundDependencyLoader]
        private void load(TextureStore ts)
        {
            textureStore = ts;
            UpdateObjectID(lvlObj.ObjectID);
        }
        
        private void UpdateObject(GeneralObject o)
        {
            UpdateObjectID(o.ObjectID);

            UpdateObjectX(o.X);
            UpdateObjectY(o.Y);

            UpdateFlippedHorizontally(o.FlippedHorizontally);
            UpdateFlippedVertically(o.FlippedVertically);

            UpdateObjectRotation(o.Rotation);

            UpdateObjectScaling(o.Scaling);
        }

        private void UpdateObjectID(int value) => obj.Texture = textureStore?.Get($"Objects/{value}.png");

        private void UpdateObjectX(double value) => X = (float)value;
        private void UpdateObjectY(double value) => Y = -(float)value;

        private void UpdateFlippedHorizontally(bool value) => Width = SetSign(Width, !value);
        private void UpdateFlippedVertically(bool value) => Height = SetSign(Height, !value);

        private void UpdateObjectRotation(double value) => Rotation = (float)value;

        private void UpdateObjectScaling(double value) => Scale = new Vector2((float)value);

        private float SetSign(float value, bool sign)
        {
            if (!sign ^ value < 0)
                return -value;
            return value;
        }
    }
}
