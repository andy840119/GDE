using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Objects.General;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    public abstract class Trigger : GeneralObject
    {
        /// <summary>Contains the <seealso cref="bool"/> values of the trigger. Indices 0, 1, 2 are reserved for Touch Triggered, Spawn Triggered and Multi Trigger respectively.</summary>
        protected BitArray8 TriggerBools = new BitArray8();
        
        /// <summary>The Object ID of the trigger.</summary>
        // IMPORTANT: If we want to change the object IDs of objects through some function, this has to be reworked
        [ObjectStringMappable(ObjectParameter.Duration)]
        public new virtual short ObjectID { get; }
        
        /// <summary>The Touch Triggered property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TouchTriggered)]
        public bool TouchTriggered
        {
            get => TriggerBools[0];
            set
            {
                if (value && SpawnTriggered)
                    SpawnTriggered = false;
                TriggerBools[0] = value;
            }
        }
        /// <summary>The Spawn Triggered property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SpawnTriggered)]
        public bool SpawnTriggered
        {
            get => TriggerBools[1];
            set
            {
                if (value && TouchTriggered)
                    TouchTriggered = false;
                TriggerBools[1] = value;
            }
        }
        /// <summary>The Multi Trigger property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MultiTrigger)]
        public bool MultiTrigger
        {
            get => TriggerBools[2];
            set => TriggerBools[2] = value;
        }

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
