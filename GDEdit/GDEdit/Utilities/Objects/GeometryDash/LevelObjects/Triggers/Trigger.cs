using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    public abstract class Trigger : GeneralObject
    {
        private bool touchTriggered, spawnTriggered;
        
        /// <summary>The Object ID of the trigger.</summary>
        // IMPORTANT: If we want to change the object IDs of objects through some function, this has to be reworked
        [ObjectStringMappable(ObjectParameter.ID)]
        public new virtual int ObjectID { get; }
        
        /// <summary>The Touch Triggered property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TouchTriggered)]
        public bool TouchTriggered
        {
            get => touchTriggered;
            set
            {
                if (value && spawnTriggered)
                    spawnTriggered = false;
                touchTriggered = value;
            }
        }
        /// <summary>The Spawn Triggered property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SpawnTriggered)]
        public bool SpawnTriggered
        {
            get => spawnTriggered;
            set
            {
                if (value && touchTriggered)
                    touchTriggered = false;
                spawnTriggered = value;
            }
        }
        /// <summary>The Multi Trigger property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MultiTrigger)]
        public bool MultiTrigger { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="Trigger"/> class.</summary>
        public Trigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="Trigger"/> class.</summary>
        /// <param name="touchTriggered">The Touch Triggered property of the trigger.</param>
        public Trigger(bool touchTriggered)
        {
            TouchTriggered = touchTriggered;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Trigger"/> class.</summary>
        /// <param name="spawnTriggered">The Spawn Triggered property of the trigger.</param>
        /// <param name="multiTrigger">The Multi Trigger property of the trigger.</param>
        public Trigger(bool spawnTriggered, bool multiTrigger)
        {
            SpawnTriggered = spawnTriggered;
            MultiTrigger = multiTrigger;
        }
    }
}
