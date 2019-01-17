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
        public bool FlippedHorizontally;
        ///<summary>Weither it is flipped horizontally</summary>
        public bool FlippedVertically;
        ///<summary>Rotation of the object</summary>
        public double Rotation;
        ///<summary>Scale of the object</summary>
        public double Scale;
        ///<summary>Editor layer 1 of the object</summary>
        public int EL1;
        ///<summary>Editor layer 2 of the object</summary>
        public int EL2;
        ///<summary>What and how many groups are in the object</summary>
        public int[] GroupID;
        #endregion

        public ObjectBase()
        {
            lvlObj = new GeneralObject(ID, x, y);

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
            switch(ID)
            {
                case 1:
                    Console.WriteLine(Object.Texture = texStore.Get("Objects/square_01_001.png"));
                    break;
                default:
                    throw new Exception("Rest hasnt been done yet");
            }
        }
    }
}
