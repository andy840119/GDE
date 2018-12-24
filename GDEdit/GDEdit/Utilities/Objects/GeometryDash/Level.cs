using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    public class Level
    {
        private List<Guideline> levelGuidelines;
        private string levelGuidelinesString;

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
        public int LevelObjectCount => LevelObjects.Count - ObjectCounts.ValueOrDefault((int)TriggerType.StartPos);
        /// <summary>The level trigger count.</summary>
        public int LevelTriggerCount => LevelObjects.TriggerCount;
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
        /// <summary>The level's objects.</summary>
        public LevelObjectCollection LevelObjects { get; set; }
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
        /// <summary>Contains the number of times each object ID has been used in the level.</summary>
        public Dictionary<int, int> ObjectCounts;
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