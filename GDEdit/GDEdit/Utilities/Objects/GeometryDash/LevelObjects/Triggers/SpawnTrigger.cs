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
        private short targetGroupID;
        private float delay;

        public override int ObjectID => (int)Enumerations.GeometryDash.TriggerType.Spawn;

        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Delay property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SpawnDelay)]
        public double Delay
        {
            get => delay;
            set => delay = (float)value;
        }
        /// <summary>The Editor Disable property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EditorDisable)]
        public bool EditorDisable
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="SpawnTrigger"/> class.</summary>
        public SpawnTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpawnTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="delay">The Delay property of the trigger.</param>
        /// <param name="editorDisable">The Editor Disable property of the trigger.</param>
        public SpawnTrigger(int targetGroupID, double delay, bool editorDisable = false)
        {
            TargetGroupID = targetGroupID;
            Delay = delay;
            EditorDisable = editorDisable;
        }

        /// <summary>Returns a clone of this <seealso cref="SpawnTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new SpawnTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as SpawnTrigger;
            c.TargetGroupID = TargetGroupID;
            c.Delay = Delay;
            c.EditorDisable = EditorDisable;
            return base.AddClonedInstanceInformation(c);
        }
    }
}
