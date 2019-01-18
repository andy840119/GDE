using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents a collection of level objects.</summary>
    public class LevelObjectCollection : IEnumerable<GeneralObject>
    {
        private int triggerCount = -1;
        private int colorTriggerCount = -1;

        private List<GeneralObject> objects;

        /// <summary>The count of the level objects in the collection.</summary>
        public int Count => objects.Count;

        /// <summary>The list of objects in the collection.</summary>
        public List<GeneralObject> Objects
        {
            get => objects;
            set
            {
                ResetCounters();
                objects = value;

            }
        }

        /// <summary>The count of all the triggers in the collection (excludes Start Pos).</summary>
        public int TriggerCount
        {
            get
            {
                if (triggerCount == -1)
                {
                    triggerCount = 0;
                    foreach (var kvp in ObjectCounts)
                        if (ObjectLists.TriggerList.Contains(kvp.Key))
                            triggerCount += kvp.Value;
                }
                return triggerCount;
            }
        }
        /// <summary>The count of all the color triggers in the collection.</summary>
        public int ColorTriggerCount
        {
            get
            {
                // TODO: Simplify this like the TriggerCount property
                if (colorTriggerCount == -1)
                {
                    colorTriggerCount = ObjectCounts.ValueOrDefault((int)TriggerType.Color);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.BG);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.GRND);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.GRND2);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Line);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Obj);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.ThreeDL);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color1);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color2);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color3);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color4);
                }
                return colorTriggerCount;
            }
        }
        /// <summary>Contains the count of objects per object ID in the collection.</summary>
        public Dictionary<int, int> ObjectCounts { get; private set; }
        /// <summary>Contains the count of groups per object ID in the collection.</summary>
        public Dictionary<int, int> GroupCounts { get; private set; }
        /// <summary>The different object IDs in the collection.</summary>
        public int DifferentObjectIDCount => ObjectCounts.Keys.Count;
        /// <summary>The different object IDs in the collection.</summary>
        public int[] DifferentObjectIDs => ObjectCounts.Keys.ToArray();
        /// <summary>The group IDs in the collection.</summary>
        public int[] UsedGroupIDs => GroupCounts.Keys.ToArray();
        #region Trigger info
        public int MoveTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Move);
        public int StopTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Stop);
        public int PulseTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Pulse);
        public int AlphaTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Alpha);
        public int ToggleTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Toggle);
        public int SpawnTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Spawn);
        public int CountTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Count);
        public int InstantCountTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.InstantCount);
        public int PickupTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Pickup);
        public int FollowTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Follow);
        public int FollowPlayerYTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.FollowPlayerY);
        public int TouchTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Touch);
        public int AnimateTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Animate);
        public int RotateTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Rotate);
        public int ShakeTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Shake);
        public int CollisionTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Collision);
        public int OnDeathTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.OnDeath);
        public int HidePlayerTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.HidePlayer);
        public int ShowPlayerTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.ShowPlayer);
        public int DisableTrailTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.DisableTrail);
        public int EnableTrailTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.EnableTrail);
        public int BGEffectOnTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.BGEffectOn);
        public int BGEffectOffTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.BGEffectOff);
        #endregion

        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectCollection"/> class.</summary>
        public LevelObjectCollection()
        {
            Objects = new List<GeneralObject>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectCollection"/> class.</summary>
        /// <param name="objects">The list of objects to use.</param>
        public LevelObjectCollection(List<GeneralObject> objects)
        {
            Objects = objects;
        }

        /// <summary>Adds an object to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="o">The object to add.</param>
        public LevelObjectCollection Add(GeneralObject o)
        {
            AddToCounters(o);
            objects.Add(o);
            return this;
        }
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to add.</param>
        public LevelObjectCollection AddRange(List<GeneralObject> objects)
        {
            foreach (var o in objects)
                AddToCounters(o);
            objects.AddRange(objects);
            return this;
        }
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to add.</param>
        public LevelObjectCollection AddRange(LevelObjectCollection objects) => AddRange(objects.Objects);
        /// <summary>Inserts an object to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index to insert the object at.</param>
        /// <param name="o">The object to insert.</param>
        public LevelObjectCollection Insert(int index, GeneralObject o)
        {
            AddToCounters(o);
            objects.Insert(index, o);
            return this;
        }
        /// <summary>Removes an object from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="o">The object to remove.</param>
        public LevelObjectCollection Remove(GeneralObject o)
        {
            RemoveFromCounters(o);
            objects.Remove(o);
            return this;
        }
        /// <summary>Removes an object from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the object to remove.</param>
        public LevelObjectCollection RemoveAt(int index)
        {
            RemoveFromCounters(objects[index]);
            objects.RemoveAt(index);
            return this;
        }
        /// <summary>Removes a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to remove.</param>
        public LevelObjectCollection RemoveRange(List<GeneralObject> objects)
        {
            foreach (var o in objects)
            {
                RemoveFromCounters(o);
                objects.Remove(o);
            }
            return this;
        }
        /// <summary>Removes a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to remove.</param>
        public LevelObjectCollection RemoveRange(LevelObjectCollection objects) => RemoveRange(objects.Objects);
        /// <summary>Clears the <seealso cref="LevelObjectCollection"/>.</summary>
        public LevelObjectCollection Clear()
        {
            ObjectCounts.Clear();
            GroupCounts.Clear();
            objects.Clear();
            return this;
        }
        /// <summary>Clones the <seealso cref="LevelObjectCollection"/> and returns the cloned instance.</summary>
        public LevelObjectCollection Clone()
        {
            var result = new LevelObjectCollection();
            result.ObjectCounts = ObjectCounts.Clone();
            result.GroupCounts = GroupCounts.Clone();
            result.objects = objects.Clone();
            return result;
        }

        /// <summary>Gets or sets the level object at the specified index.</summary>
        /// <param name="index">The index of the level object.</param>
        public GeneralObject this[int index]
        {
            get => objects[index];
            set => objects[index] = value;
        }

        public IEnumerator<GeneralObject> GetEnumerator()
        {
            foreach (var o in objects)
                yield return o;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void AddToCounters(GeneralObject o)
        {
            AdjustCounters(o, 1);
            ObjectCounts.IncrementOrAddKeyValue(o.ObjectID);
            foreach (var g in o.GroupIDs)
                GroupCounts.IncrementOrAddKeyValue(g);
        }
        private void RemoveFromCounters(GeneralObject o)
        {
            AdjustCounters(o, -1);
            ObjectCounts[o.ObjectID]--;
            foreach (var g in o.GroupIDs)
                GroupCounts[g]--;
        }
        private void AdjustCounters(GeneralObject o, int adjustment)
        {
            switch (o)
            {
                case ColorTrigger _:
                    if (colorTriggerCount > -1)
                        colorTriggerCount += adjustment;
                    break;
                case Trigger _:
                    if (triggerCount > -1)
                        triggerCount += adjustment;
                    break;
            }
        }
        private void ResetCounters()
        {
            colorTriggerCount = -1;
            triggerCount = -1;
        }
    }
}
