using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using GDE.App.Main.Containers;
using GDE.App.Main.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using System;

namespace GDE.App.Main.Objects
{
    // Change this into a Drawable instead of a Container in a later commit
    ///<summary>A drawable <seealso cref="GeneralObject"/>.</summary>
    public class ObjectBase : Container, IKeyBindingHandler<GlobalAction>
    {
        private Box obj;
        private TextureStore textureStore;

        public GeneralObject LevelObject;
        public readonly ObjectBase Object;
        public SelectionState State;
        public Action Selected;

        #region Level Object Variables
        ///<summary>The ID of the object.</summary>
        public int ObjectID
        {
            get => LevelObject.ObjectID;
            set => UpdateObjectID(LevelObject.ObjectID = value);
        }
        ///<summary>The X position of the object.</summary>
        public double ObjectX
        {
            get => LevelObject.X;
            set => UpdateObjectX(LevelObject.X = value);
        }
        ///<summary>The Y position of the object.</summary>
        public double ObjectY
        {
            get => LevelObject.Y;
            set => UpdateObjectY(LevelObject.Y = value);
        }
        ///<summary>Represents whether the object is flipped horizontally or not.</summary>
        public bool FlippedHorizontally
        {
            get => LevelObject.FlippedHorizontally;
            set => UpdateFlippedHorizontally(LevelObject.FlippedHorizontally = value);
        }
        ///<summary>Represents whether the object is flipped vertically or not.</summary>
        public bool FlippedVertically
        {
            get => LevelObject.FlippedVertically;
            set => UpdateFlippedVertically(LevelObject.FlippedVertically = value);
        }
        ///<summary>The rotation of the object.</summary>
        public double ObjectRotation
        {
            get => LevelObject.Rotation;
            set => UpdateObjectRotation(LevelObject.Rotation = value);
        }
        ///<summary>The scaling of the object.</summary>
        public double ObjectScaling
        {
            get => LevelObject.Scaling;
            set => UpdateObjectScaling(LevelObject.Scaling = value);
        }
        ///<summary>The Editor Layer 1 of the object.</summary>
        public int EL1
        {
            get => LevelObject.EL1;
            set => LevelObject.EL1 = value;
        }
        ///<summary>The Editor Layer 2 of the object.</summary>
        public int EL2
        {
            get => LevelObject.EL2;
            set => LevelObject.EL2 = value;
        }
        #endregion

        /// <summary>Initializes a new instance of the <seealso cref="ObjectBase"/> class.</summary>
        public ObjectBase(GeneralObject o)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

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
            UpdateObject(LevelObject = o);
        }
        
        [BackgroundDependencyLoader]
        private void load(TextureStore ts)
        {
            textureStore = ts;
            UpdateObjectID(LevelObject.ObjectID);
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

        protected override bool OnClick(ClickEvent e)
        {
            if (State == SelectionState.Selected)
            {
                this.FadeColour(Color4.White, 200);
                State = SelectionState.NotSelected;
            }
            else
            {
                this.FadeColour(Color4.LightGreen, 200);
                State = SelectionState.Selected;
            }

            Selected?.Invoke();

            return base.OnClick(e);
        }

        public bool OnPressed(GlobalAction action)
        {
            Console.WriteLine(action);

            if (State == SelectionState.Selected)
                switch (action)
                {
                    case GlobalAction.objMoveRight:
                        ObjectX += 10;
                        break;
                    case GlobalAction.objMoveLeft:
                        ObjectX -= 10;
                        break;
                    case GlobalAction.objMoveUp:
                        ObjectY += 10;
                        break;
                    case GlobalAction.objMoveDown:
                        ObjectY -= 10;
                        break;
                    case GlobalAction.objMoveRightSmall:
                        ObjectX += 5;
                        break;
                    case GlobalAction.objMoveLeftSmall:
                        ObjectX -= 5;
                        break;
                    case GlobalAction.objMoveUpSmall:
                        ObjectY += 5;
                        break;
                    case GlobalAction.objMoveDownSmall:
                        ObjectY -= 5;
                        break;
                }

            return true;
        }

        public bool OnReleased(GlobalAction action) => true;
    }
}
