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
    ///<summary>Displays an object of any <see cref="ObjectLists"/></summary>
    public class ObjectBase : Container
    {
        private GeneralObject lvlObj;
        private Box Object;

        #region Level Object Variables
        ///<summary>Tells what Object ID it is. you can switch this id for other object</summary>
        public int ID;
        ///<summary>Position X of the object</summary>
        public double X;
        ///<summary>Position Y of the object</summary>
        public double Y;
        ///<summary>Weither it is flipped horizontally</summary>
        public bool FlippedHorizontally
        {
            get => lvlObj.FlippedHorizontally;
            set => lvlObj.FlippedHorizontally = value;
        }
        ///<summary>Weither it is flipped horizontally</summary>
        public bool FlippedVertically
        {
            get => lvlObj.FlippedVertically;
            set => lvlObj.FlippedVertically = value;
        }
        ///<summary>Rotation of the object</summary>
        public double Rotation
        {
            get => lvlObj.Rotation;
            set => lvlObj.Rotation = value;
        }
        ///<summary>Scale of the object</summary>
        public double Scale
        {
            get => lvlObj.Scaling;
            set => lvlObj.Scaling = value;
        }
        ///<summary>Editor layer 1 of the object</summary>
        public int EL1
        {
            get => lvlObj.EL1;
            set => lvlObj.EL1 = value;
        }
        ///<summary>Editor layer 2 of the object</summary>
        public int EL2
        {
            get => lvlObj.EL2;
            set => lvlObj.EL2 = value;
        }
        ///<summary>What and how many groups are in the object</summary>
        public int[] GroupID
        {
            get => lvlObj.GroupIDs;
            set => lvlObj.GroupIDs = value;
        }
        #endregion

        public ObjectBase()
        {
            lvlObj = new GeneralObject(ID);

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
            Object.Texture = texStore.Get($"Objects/{ID}.png");
        }
    }
}
