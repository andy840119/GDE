using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents an object hitbox.</summary>
    public class Hitbox
    {
        private Point position;

        /// <summary>The position of the hitbox.</summary>
        public Point Position
        {
            get => position;
            set => position = value;
        }
        /// <summary>The X position of the hitbox.</summary>
        public double X
        {
            get => position.X;
            set => position.X = value;
        }
        /// <summary>The Y position of the hitbox.</summary>
        public double Y
        {
            get => position.Y;
            set => position.Y = value;
        }

        /// <summary>The hitbox type of the hitbox.</summary>
        public HitboxType HitboxType { get; }
        /// <summary>The behavior of the hitbox.</summary>
        public HitboxBehavior Behavior { get; }

        /// <summary>Initializes a new instance of the <seealso cref="Hitbox"/> class.</summary>
        /// <param name="position">The position of the hitbox.</param>
        /// <param name="type">The hitbox type of the hitbox.</param>
        /// <param name="behavior">The behavior of the hitbox.</param>
        public Hitbox(Point position, HitboxType type, HitboxBehavior behavior = HitboxBehavior.Platform)
        {
            Position = position;
            HitboxType = type;
            Behavior = behavior;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Hitbox"/> class.</summary>
        /// <param name="x">The X position of the hitbox.</param>
        /// <param name="y">The Y position of the hitbox.</param>
        /// <param name="type">The hitbox type of the hitbox.</param>
        /// <param name="behavior">The behavior of the hitbox.</param>
        public Hitbox(double x, double y, HitboxType type, HitboxBehavior behavior = HitboxBehavior.Platform) : this(new Point(x, y), type, behavior) { }
    }

    /// <summary>Represents a hitbox behavior.</summary>
    public enum HitboxBehavior
    {
        /// <summary>Represents a hitbox that behaves as a platform, allowing the player to land on it.</summary>
        Platform,
        /// <summary>Represents a hitbox that behaves as a hazard, killing the player right as soon as they come in contact.</summary>
        Hazard,
        /// <summary>Represents a hitbox that triggers an effect as soon as the player contacts it.</summary>
        Trigger,
    }
}
