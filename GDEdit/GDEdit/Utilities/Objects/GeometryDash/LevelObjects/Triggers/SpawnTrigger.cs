using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Spawn trigger.</summary>
    public class SpawnTrigger : Trigger, IHasTargetGroupID
    {
        public override int ObjectID => (int)Enumerations.GeometryDash.Trigger.Spawn;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID { get; set; }
        /// <summary>The Delay property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SpawnDelay)]
        public float Delay { get; set; }
        /// <summary>The Editor Disable property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EditorDisable)]
        public bool EditorDisable { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="SpawnTrigger"/> class.</summary>
        public SpawnTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpawnTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="delay">The Delay property of the trigger.</param>
        /// <param name="editorDisable">The Editor Disable property of the trigger.</param>
        public SpawnTrigger(int targetGroupID, float delay, bool editorDisable = false)
        {
            TargetGroupID = targetGroupID;
            Delay = delay;
            EditorDisable = editorDisable;
        }

        // TODO: Add cloning method
    }
}