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
    /// <summary>Represents a Count trigger.</summary>
    public class CountTrigger : Trigger, IHasTargetGroupID, IHasPrimaryItemID
    {
        public override short ObjectID => (short)(int)Enumerations.GeometryDash.TriggerType.Count;
        
        /// <summary>The target Group ID of the trigger.</summary>
        public short TargetGroupID { get; set; }
        /// <summary>The Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ItemID)]
        public short ItemID { get; set; }
        /// <summary>The primary Item ID of the trigger.</summary>
        public short PrimaryItemID
        {
            get => ItemID;
            set => ItemID = value;
        }
        /// <summary>The Target Count property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Count)]
        public int TargetCount { get; set; }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ActivateGroup)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Multi Activate property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MultiActivate)]
        public bool MultiActivate
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="CountTrigger"/> class.</summary>
        public CountTrigger() { }
        /// <summary>Initializes a new instance of the <seealso cref="CountTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="itemID">The Item ID of the trigger.</param>
        /// <param name="count">The Target Count property of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</param>
        /// <param name="multiActivate">The Multi Activate property of the trigger.</param>
        public CountTrigger(short targetGroupID, short itemID, short targetCount, bool activateGroup = false, bool multiActivate = false)
        {
            TargetGroupID = targetGroupID;
            ItemID = itemID;
            TargetCount = targetCount;
            ActivateGroup = activateGroup;
            MultiActivate = multiActivate;
        }

        // TODO: Add cloning method
    }
}
