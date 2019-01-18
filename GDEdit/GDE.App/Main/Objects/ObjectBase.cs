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
        private Box Object;

        #region Level Object Variables
        ///<summary>The ID of the object.</summary>
        public int ObjectID
        {
            get => lvlObj.ObjectID;
            set
            {
                lvlObj.ObjectID = value;
                // TODO: Change the texture of the object
            }
        }
        ///<summary>The X position of the object.</summary>
        public double ObjectX
        {
            get => lvlObj.X;
            set => lvlObj.X = value;
        }
        ///<summary>The Y position of the object.</summary>
        public double ObjectY
        {
            get => lvlObj.Y;
            set => lvlObj.Y = value;
        }
        ///<summary>Represents whether the object is flipped horizontally or not.</summary>
        public bool FlippedHorizontally
        {
            get => lvlObj.FlippedHorizontally;
            set => lvlObj.FlippedHorizontally = value;
        }
        ///<summary>Represents whether the object is flipped vertically or not.</summary>
        public bool FlippedVertically
        {
            get => lvlObj.FlippedVertically;
            set => lvlObj.FlippedVertically = value;
        }
        ///<summary>The rotation of the object.</summary>
        public double ObjectRotation
        {
            get => lvlObj.Rotation;
            set => lvlObj.Rotation = value;
        }
        ///<summary>The scaling of the object.</summary>
        public double ObjectScaling
        {
            get => lvlObj.Scaling;
            set => lvlObj.Scaling = value;
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
        ///<summary>The Group IDs of the object.</summary>
        public int[] GroupIDs
        {
            get => lvlObj.GroupIDs;
            set => lvlObj.GroupIDs = value;
        }
        #endregion

        /// <summary>Initializes a new instance of the <seealso cref="ObjectBase"/> class.</summary>
        public ObjectBase()
        {
            lvlObj = new GeneralObject(ObjectID);

            Children = new Drawable[]
            {
                Object = new Box
                {
                    Size = new Vector2(100),
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
            Object.Texture = texStore.Get($"Objects/{ObjectID}.png");
        }
    }
}
