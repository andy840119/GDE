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
        private Dictionary<int, int> objectCounts;
        private List<Guideline> guidelines;
        private string levelString;
        private string decryptedLevelString;
        private string guidelineString;

        #region Properties
        /// <summary>Returns the name of the level followed by its revision if needed.</summary>
        public string LevelNameWithRevision => $"{Name}{(Revision > 0 ? $" (Rev. {Revision})" : "")}";
        /// <summary>The name of the level.</summary>
        public string Name;
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
        public string Description = "";
        /// <summary>The raw form of the level as found in the gamesave.</summary>
        public string RawLevel;
        /// <summary>The revision of the level.</summary>
        public int Revision;
        /// <summary>The official song ID used in the level.</summary>
        public int OfficialSongID;
        /// <summary>The custom song ID used in the level.</summary>
        public int CustomSongID;
        /// <summary>The level object count.</summary>
        public int ObjectCount => LevelObjects.Count - ObjectCounts.ValueOrDefault((int)TriggerType.StartPos);
        /// <summary>The level trigger count.</summary>
        public int TriggerCount => LevelObjects.TriggerCount;
        /// <summary>The attempts made in the level.</summary>
        public int Attempts;
        /// <summary>The ID of the level.</summary>
        public int LevelID;
        /// <summary>The version of the level.</summary>
        public int Version;
        /// <summary>The length of the level.</summary>
        public int Length;
        /// <summary>The folder of the level.</summary>
        public int Folder;
        /// <summary>The time spent in the editor building the level in seconds.</summary>
        public int BuildTime;
        /// <summary>The time spent in the editor building the level.</summary>
        public TimeSpan TotalBuildTime
        {
            get => new TimeSpan(0, 0, BuildTime);
            set => BuildTime = (int)value.TotalSeconds;
        }
        /// <summary>Determines whether the level has been verified or not.</summary>
        public bool VerifiedStatus;
        /// <summary>Determines whether the level has been uploaded or not.</summary>
        public bool UploadedStatus;
        /// <summary>The level's objects.</summary>
        public LevelObjectCollection LevelObjects { get; set; }
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
        /// <summary>Contains the number of times each object ID has been used in the level.</summary>
        public Dictionary<int, int> ObjectCounts
        {
            get
            {
                if (objectCounts == null)
                {
                    objectCounts = new Dictionary<int, int>();
                    foreach (var l in LevelObjects)
                        objectCounts.IncrementOrAddKeyValue(l.ObjectID);
                }
                return objectCounts;
            }
        }
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

        private void GetInformation(string raw)
        {
            string startKeyString = "<k>k";
            string endKeyString = "</k><";
            // TODO: Shorten variable names
            int parameterIDStartIndex;
            int parameterIDEndIndex;
            int parameterValueTypeStartIndex;
            int parameterValueTypeEndIndex;
            int parameterValueStartIndex;
            int parameterValueEndIndex;
            int parameterID;
            string parameterValueType;
            string parameterValue;
            for (int i = 0; i < raw.Length;)
            {
                parameterIDStartIndex = raw.Find(startKeyString, i, raw.Length) + startKeyString.Length;
                if (parameterIDStartIndex <= startKeyString.Length)
                    break;
                parameterIDEndIndex = raw.Find(endKeyString, parameterIDStartIndex);
                parameterValueTypeStartIndex = parameterIDEndIndex + endKeyString.Length;
                parameterValueTypeEndIndex = raw.Find(">", parameterValueTypeStartIndex, raw.Length);
                parameterValueType = raw.Substring(parameterValueTypeStartIndex, parameterValueTypeEndIndex - parameterValueTypeStartIndex);
                parameterValueStartIndex = parameterValueTypeEndIndex + 1;
                parameterValueEndIndex = parameterValueType[parameterValueType.Length - 1] != '/' ? raw.Find($"</{parameterValueType}>", parameterValueStartIndex, raw.Length) : parameterValueStartIndex;
                parameterValue = raw.Substring(parameterValueStartIndex, parameterValueEndIndex - parameterValueStartIndex);
                string s = raw.Substring(parameterIDStartIndex, parameterIDEndIndex - parameterIDStartIndex);
                if (s != "CEK" && !s.StartsWith("I"))
                {
                    parameterID = ToInt32(s);
                    switch (parameterID)
                    {
                        case 1: // Level ID
                            LevelID = ToInt32(parameterValue);
                            break;
                        case 2: // Level Name
                            Name = parameterValue;
                            break;
                        case 3: // Level Description
                            Description = Encoding.UTF8.GetString(Base64Decrypt(parameterValue));
                            break;
                        case 4: // Level String
                            LevelString = parameterValue;
                            break;
                        case 8: // Official Song ID
                            OfficialSongID = ToInt32(parameterValue);
                            break;
                        case 14: // Level Verified Status
                            VerifiedStatus = parameterValueType == "t /"; // Well that's how it's implemented ¯\_(ツ)_/¯
                            break;
                        case 15: // Level Uploaded Status
                            UploadedStatus = parameterValueType == "t /";
                            break;
                        case 16: // Level Version
                            Version = ToInt32(parameterValue);
                            break;
                        case 18: // Level Attempts
                            Attempts = ToInt32(parameterValue);
                            break;
                        case 23: // Level Length
                            Length = ToInt32(parameterValue);
                            break;
                        case 45: // Custom Song ID
                            CustomSongID = ToInt32(parameterValue);
                            break;
                        case 46: // Level Revision
                            Revision = ToInt32(parameterValue);
                            break;
                        case 80: // Time Spent
                            BuildTime = ToInt32(parameterValue);
                            break;
                        case 84: // Level Folder
                            Folder = ToInt32(parameterValue);
                            break;
                        default: // Not something we care about
                            break;
                    }
                }
                i = parameterValueEndIndex;
            }

            if (DecryptedLevelString != null) // If there is a level string
            {
                LevelObjects = GetObjects(GetObjectString(DecryptedLevelString));
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