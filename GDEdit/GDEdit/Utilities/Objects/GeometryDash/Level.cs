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
using static System.Convert;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a level in the game.</summary>
    public class Level
    {
        private List<Guideline> guidelines;
        private LevelObjectCollection levelObjects;
        private string levelString;
        private string decryptedLevelString;
        private string guidelineString;
        private string rawLevel;

        #region Properties
        /// <summary>Returns the name of the level followed by its revision if needed.</summary>
        public string LevelNameWithRevision => $"{Name}{(Revision > 0 ? $" (Rev. {Revision})" : "")}";
        /// <summary>The name of the level.</summary>
        public string Name { get; set; }
        /// <summary>The level string.</summary>
        public string LevelString
        {
            get
            {
                if (levelString == null)
                    levelString = GetLevelString(RawLevel);
                return levelString;
            }
            set
            {
                RawLevel = RawLevel.Replace($"<k>k4</k><s>{levelString}</s>", $"<k>k4</k><s>{value}</s>");
                levelString = value;
            }
        }
        /// <summary>The decrypted form of the level string.</summary>
        public string DecryptedLevelString
        {
            get
            {
                if (decryptedLevelString == null)
                    TryDecryptLevelString(LevelString, out decryptedLevelString);
                return decryptedLevelString;
            }
            set => LevelString = decryptedLevelString = value;
        }
        /// <summary>The guideline string of the level.</summary>
        public string GuidelineString
        {
            get
            {
                if (guidelineString == null)
                    guidelineString = GetGuidelineString(DecryptedLevelString);
                return guidelineString;
            }
            set
            {
                guidelines = null; // Reset and only analyze if requested
                int gsStartIndex = GetGuidelineStringStartIndex(DecryptedLevelString);
                if (guidelineString.Length == 0)
                    DecryptedLevelString = DecryptedLevelString.Insert(gsStartIndex, value);
                else
                    DecryptedLevelString = DecryptedLevelString.Replace(guidelineString, value);
                guidelineString = value;
            }
        }
        /// <summary>The description of the level.</summary>
        public string Description { get; set; }
        /// <summary>The raw form of the level as found in the gamesave.</summary>
        public string RawLevel
        {
            get => rawLevel;
            set
            {
                rawLevel = value;
                GetInformation(rawLevel);
            }
        }
        /// <summary>The revision of the level.</summary>
        public int Revision { get; set; }
        /// <summary>The official song ID used in the level.</summary>
        public int OfficialSongID { get; set; }
        /// <summary>The custom song ID used in the level.</summary>
        public int CustomSongID { get; set; }
        /// <summary>The level object count.</summary>
        public int ObjectCount => LevelObjects.Count - ObjectCounts.ValueOrDefault((int)TriggerType.StartPos);
        /// <summary>The level trigger count.</summary>
        public int TriggerCount => LevelObjects.TriggerCount;
        /// <summary>The attempts made in the level.</summary>
        public int Attempts { get; set; }
        /// <summary>The ID of the level.</summary>
        public int LevelID { get; set; }
        /// <summary>The version of the level.</summary>
        public int Version { get; set; }
        /// <summary>The length of the level.</summary>
        public int Length { get; set; }
        /// <summary>The folder of the level.</summary>
        public int Folder { get; set; }
        /// <summary>The time spent in the editor building the level in seconds.</summary>
        public int BuildTime { get; set; }
        /// <summary>The time spent in the editor building the level.</summary>
        public TimeSpan TotalBuildTime
        {
            get => new TimeSpan(0, 0, BuildTime);
            set => BuildTime = (int)value.TotalSeconds;
        }
        /// <summary>Determines whether the level has been verified or not.</summary>
        public bool VerifiedStatus { get; set; }
        /// <summary>Determines whether the level has been uploaded or not.</summary>
        public bool UploadedStatus { get; set; }
        /// <summary>The level's objects.</summary>
        public LevelObjectCollection LevelObjects
        {
            get
            {
                if (levelObjects == null)
                    levelObjects = GetObjects(GetObjectString(DecryptedLevelString));
                return levelObjects;
            }
            set => rawLevel = $"{rawLevel.Substring(0, rawLevel.IndexOf(';') + 1)}{levelObjects = value}";
        }

        /// <summary>The level's guidelines.</summary>
        public List<Guideline> Guidelines
        {
            get
            {
                if (guidelines == null)
                    guidelines = GetGuidelines(GuidelineString);
                return guidelines;
            }
            set => GuidelineString = (guidelines = value).GetGuidelineString();
        }
        /// <summary>Contains the count of objects per object ID in the collection.</summary>
        public Dictionary<int, int> ObjectCounts => LevelObjects.ObjectCounts;
        /// <summary>Contains the count of groups per object ID in the collection.</summary>
        public Dictionary<int, int> GroupCounts => LevelObjects.GroupCounts;
        /// <summary>The different object IDs in the collection.</summary>
        public int DifferentObjectIDCount => ObjectCounts.Keys.Count;
        /// <summary>The different object IDs in the collection.</summary>
        public int[] DifferentObjectIDs => ObjectCounts.Keys.ToArray();
        /// <summary>The group IDs in the collection.</summary>
        public int[] UsedGroupIDs => GroupCounts.Keys.ToArray();
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
        #endregion

        public override string ToString() => RawLevel;

        private void GetInformation(string raw)
        {
            string startKeyString = "<k>k";
            string endKeyString = "</k><";
            int IDStart;
            int IDEnd;
            int valueTypeStart;
            int valueTypeEnd;
            int valueStart;
            int valueEnd;
            string valueType;
            string value;
            for (int i = 0; i < raw.Length;)
            {
                IDStart = raw.Find(startKeyString, i, raw.Length) + startKeyString.Length;
                if (IDStart <= startKeyString.Length)
                    break;
                IDEnd = raw.Find(endKeyString, IDStart);
                valueTypeStart = IDEnd + endKeyString.Length;
                valueTypeEnd = raw.Find(">", valueTypeStart, raw.Length);
                valueType = raw.Substring(valueTypeStart, valueTypeEnd - valueTypeStart);
                valueStart = valueTypeEnd + 1;
                valueEnd = valueType[valueType.Length - 1] != '/' ? raw.Find($"</{valueType}>", valueStart, raw.Length) : valueStart;
                value = raw.Substring(valueStart, valueEnd - valueStart);
                string s = raw.Substring(IDStart, IDEnd - IDStart);
                if (s != "CEK" && !s.StartsWith("I"))
                    GetParameterInformation(ToInt32(s), value, valueType);
                i = valueEnd;
            }
        }

        private void GetParameterInformation(int parameterID, string value, string valueType)
        {
            switch (parameterID)
            {
                case 1: // Level ID
                    LevelID = ToInt32(value);
                    break;
                case 2: // Level Name
                    Name = value;
                    break;
                case 3: // Level Description
                    Description = Encoding.UTF8.GetString(Base64Decrypt(value));
                    break;
                case 4: // Level String
                    LevelString = value;
                    break;
                case 8: // Official Song ID
                    OfficialSongID = ToInt32(value);
                    break;
                case 14: // Level Verified Status
                    VerifiedStatus = valueType == "t /"; // Well that's how it's implemented ¯\_(ツ)_/¯
                    break;
                case 15: // Level Uploaded Status
                    UploadedStatus = valueType == "t /";
                    break;
                case 16: // Level Version
                    Version = ToInt32(value);
                    break;
                case 18: // Level Attempts
                    Attempts = ToInt32(value);
                    break;
                case 23: // Level Length
                    Length = ToInt32(value);
                    break;
                case 45: // Custom Song ID
                    CustomSongID = ToInt32(value);
                    break;
                case 46: // Level Revision
                    Revision = ToInt32(value);
                    break;
                case 80: // Time Spent
                    BuildTime = ToInt32(value);
                    break;
                case 84: // Level Folder
                    Folder = ToInt32(value);
                    break;
                default: // Not something we care about
                    break;
            }
        }
    }

    public static class GuidelineFunctions
    {
        /// <summary>Returns the guideline string of a list of guidelines.</summary>
        /// <param name="guidelines">The list of guidelines to get the guideline string of.</param>
        public static string GetGuidelineString(this List<Guideline> guidelines)
        {
            StringBuilder result = new StringBuilder();
            foreach (var g in guidelines)
                result.Append($"{g}~");
            return result.ToString();
        }
    }
}