using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    public class Level
    {
        #region Properties
        /// <summary>Returns the name of the level followed by its revision if needed.</summary>
        public string LevelNameWithRevision => $"{LevelName}{(LevelRevision > 0 ? $" (Rev. {LevelRevision})" : "")}";
        /// <summary>The name of the level.</summary>
        public string LevelName;
        /// <summary>The level string.</summary>
        public string LevelString;
        /// <summary>The decrypted form of the level string.</summary>
        public string DecryptedLevelString;
        /// <summary>The guideline string of the level.</summary>
        public string LevelGuidelinesString
        {
            get => levelGuidelinesString;
            set
            {
                levelGuidelines = null; // Reset and only analyze if requested
                levelGuidelinesString = value;
            }
        }
        /// <summary>The description of the level.</summary>
        public string LevelDescription = "";
        /// <summary>The raw form of the level as found in the gamesave.</summary>
        public string RawLevel;
        /// <summary>The revision of the level.</summary>
        public int LevelRevision;
        /// <summary>The official song ID used in the level.</summary>
        public int LevelOfficialSongID;
        /// <summary>The custom song ID used in the level.</summary>
        public int LevelCustomSongID;
        /// <summary>The level object count.</summary>
        public int LevelObjectCount
        {
            get
            {
                if (levelObjectCount == -1)
                {
                    levelObjectCount = 0;
                    if (ObjectCounts != null)
                    {
                        foreach (var kvp in ObjectCounts)
                            levelObjectCount += kvp.Value;
                        levelObjectCount -= ObjectCounts.ValueOrDefault((int)Trigger.StartPos);
                    }
                }
                return levelObjectCount;
            }
        }
        /// <summary>The level trigger count.</summary>
        public int LevelTriggerCount
        {
            get
            {
                if (levelTriggerCount == -1)
                {
                    levelTriggerCount = 0;
                    if (ObjectCounts != null)
                    {
                        foreach (var kvp in ObjectCounts)
                            if (Enum.GetValues(typeof(Trigger)).Cast<int>().Contains(kvp.Key))
                                levelTriggerCount += kvp.Value;
                        levelTriggerCount -= ObjectCounts.ValueOrDefault((int)Trigger.StartPos);
                    }
                }
                return levelTriggerCount;
            }
        }
        /// <summary>The attempts made in the level.</summary>
        public int LevelAttempts;
        /// <summary>The ID of the level.</summary>
        public int LevelID;
        /// <summary>The version of the level.</summary>
        public int LevelVersion;
        /// <summary>The length of the level.</summary>
        public int LevelLength;
        /// <summary>The folder of the level.</summary>
        public int LevelFolder;
        /// <summary>The time spent in the editor building the level in seconds.</summary>
        public int BuildTime;
        /// <summary>The time spent in the editor building the level.</summary>
        public TimeSpan TotalBuildTime
        {
            get => new TimeSpan(0, 0, BuildTime);
            set => BuildTime = (int)value.TotalSeconds;
        }
        /// <summary>Determines whether the level has been verified or not.</summary>
        public bool LevelVerifiedStatus;
        /// <summary>Determines whether the level has been uploaded or not.</summary>
        public bool LevelUploadedStatus;
        // Changing this to List<GeneralObject> caused errors, fix in another PR
        /// <summary>The level's objects.</summary>
        public List<GeneralObject> LevelObjects
        {
            get => levelObjects;
            set
            {
                levelObjects = value;
                levelObjectCount = -1;
                levelTriggerCount = -1;
                colorTriggerCount = -1;
            }
        }
        /// <summary>The level's guidelines.</summary>
        public List<Guideline> LevelGuidelines
        {
            get
            {
                if (levelGuidelines == null)
                    levelGuidelines = Gamesave.GetGuidelines(LevelGuidelinesString);
                return levelGuidelines;
            }
            set
            {
                levelGuidelines = value;
                LevelGuidelinesString = GetGuidelineString(levelGuidelines);
            }
        }
        /// <summary>The different object IDs that have been used in the level.</summary>
        public int[] LevelDifferentObjectIDs = new int[0];
        /// <summary>The group IDs that have been used in the level.</summary>
        public int[] LevelUsedGroupIDs = new int[0];
        /// <summary>Contains the number of times each object ID has been used in the level.</summary>
        public Dictionary<int, int> ObjectCounts;
        #region Trigger info
        public int MoveTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Move);
        public int StopTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Stop);
        public int PulseTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Pulse);
        public int AlphaTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Alpha);
        public int ToggleTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Toggle);
        public int SpawnTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Spawn);
        public int CountTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Count);
        public int InstantCountTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.InstantCount);
        public int PickupTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Pickup);
        public int FollowTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Follow);
        public int FollowPlayerYTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.FollowPlayerY);
        public int TouchTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Touch);
        public int AnimateTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Animate);
        public int RotateTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Rotate);
        public int ShakeTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Shake);
        public int CollisionTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.Collision);
        public int OnDeathTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.OnDeath);
        public int ColorTriggersCount
        {
            get
            {
                if (colorTriggerCount == -1)
                {
                    colorTriggerCount = ObjectCounts.ValueOrDefault((int)Trigger.Color);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.BG);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.GRND);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.GRND2);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.Line);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.Obj);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.ThreeDL);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.Color1);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.Color2);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.Color3);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)Trigger.Color4);
                }
                return colorTriggerCount;
            }
        }
        public int HidePlayerTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.HidePlayer);
        public int ShowPlayerTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.ShowPlayer);
        public int DisableTrailTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.DisableTrail);
        public int EnableTrailTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.EnableTrail);
        public int BGEffectOnTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.BGEffectOn);
        public int BGEffectOffTriggersCount => ObjectCounts.ValueOrDefault((int)Trigger.BGEffectOff);
        #endregion
        private int levelObjectCount = -1;
        private int levelTriggerCount = -1;
        private int colorTriggerCount = -1;
        private List<GeneralObject> levelObjects;
        private List<Guideline> levelGuidelines;
        private string levelGuidelinesString;
        #endregion

        #region Constructors
        /// <summary>Creates a new empty instance of the <see cref="Level"/> class.</summary>
        public Level() { }
        /// <summary>Creates a new instance of the <see cref="Level"/> class from a raw string containing a level without getting its info.</summary>
        /// <param name="level">The raw string containing the level.</param>
        public Level(string level)
        {
            RawLevel = level;
        }
        #endregion

        #region Functions
        public static string GetGuidelineString(List<Guideline> guidelines)
        {
            StringBuilder result = new StringBuilder();
            foreach (var g in guidelines)
                result.Append(g.ToString() + "~");
            return result.ToString();
        }
        #endregion
    }
}