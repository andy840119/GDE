using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash;
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
        private LevelObject lvlObj;
        private Box Object;

        #region Level Object Variables
        ///<summary>Tells what Object ID it is. you can switch this id for other object</summary>
        public int ID;
        ///<summary>Position X of the object</summary>
        public double x;
        ///<summary>Position Y of the object</summary>
        public double y;
        ///<summary>Weither it is flipped horizontally</summary>
        public bool flippedHorizontally;
        ///<summary>Weither it is flipped horizontally</summary>
        public bool flippedVertically;
        ///<summary>Rotation of the object</summary>
        public double rotation;
        ///<summary>Scale of the object</summary>
        public double scale;
        ///<summary>Editor layer 1 of the object</summary>
        public int EL1;
        ///<summary>Editor layer 2 of the object</summary>
        public int EL2;
        ///<summary>What and how many groups are in the object</summary>
        public int[] groupID;
        #endregion

        public ObjectBase()
        {
            lvlObj = new LevelObject(ID, x, y);

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
