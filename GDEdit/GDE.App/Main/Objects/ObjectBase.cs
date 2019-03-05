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
            set => UpdateObjectID(value);
        }
        ///<summary>The X position of the object.</summary>
        public double ObjectX
        {
            get => lvlObj.X;
            set => UpdateObjectX(value);
        }
        ///<summary>The Y position of the object.</summary>
        public double ObjectY
        {
            get => lvlObj.Y;
            set => UpdateObjectY(value);
        }
        ///<summary>Represents whether the object is flipped horizontally or not.</summary>
        public bool FlippedHorizontally
        {
            get => lvlObj.FlippedHorizontally;
            set => UpdateFlippedHorizontally(value);
        }
        ///<summary>Represents whether the object is flipped vertically or not.</summary>
        public bool FlippedVertically
        {
            get => lvlObj.FlippedVertically;
            set => UpdateFlippedVertically(value);
        }
        ///<summary>The rotation of the object.</summary>
        public double ObjectRotation
        {
            get => lvlObj.Rotation;
            set => UpdateObjectRotation(value);
        }
        ///<summary>The scaling of the object.</summary>
        public double ObjectScaling
        {
            get => lvlObj.Scaling;
            set => UpdateObjectScaling(value);
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
        public ObjectBase(int objectID = 1)
        {
            lvlObj = new GeneralObject(objectID);

            Children = new Drawable[]
            {
                obj = new Box
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both
                }
            };
        }
        
        [BackgroundDependencyLoader]
        private void load(TextureStore ts)
        {
            textureStore = ts;
            UpdateObjectID();
        }

        private void UpdateObjectID() => obj.Texture = textureStore.Get($"Objects/{lvlObj.ObjectID}.png");
        private void UpdateObjectID(int value) => obj.Texture = textureStore.Get($"Objects/{lvlObj.ObjectID = value}.png");
        private void UpdateObjectX(double value) => obj.X = (float)(lvlObj.X = value);
        private void UpdateObjectY(double value) => obj.Y = (float)(lvlObj.Y = value);
        private void UpdateFlippedHorizontally(bool value) => obj.Width = SetSign(obj.Width, lvlObj.FlippedHorizontally = value);
        private void UpdateFlippedVertically(bool value) => obj.Height = SetSign(obj.Height, lvlObj.FlippedVertically = value);
        private void UpdateObjectRotation(double value) => obj.Rotation = (float)(lvlObj.Rotation = value);
        private void UpdateObjectScaling(double value)
        {
            obj.Size *= (float)(value / lvlObj.Scaling);
            lvlObj.Scaling = value;
        }

        private float SetSign(float value, bool sign)
        {
            if (!sign ^ value < 0)
                return -value;
            return value;
        }
    }
}
