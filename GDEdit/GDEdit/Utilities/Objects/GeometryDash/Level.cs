using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.ColorChannels;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using static System.Convert;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a level in the game.</summary>
    public class Level
    {
        // TODO: Remove dependency relationships between level strings and properties; prefer re-calculating the level strings after any changes.

        private List<Guideline> guidelines;
        private LevelObjectCollection levelObjects;
        private string levelString;
        private string decryptedLevelString;
        private string guidelineString;
        private string rawLevel;

        #region Properties
        // Metadata
        /// <summary>Returns the name of the level followed by its revision if needed.</summary>
        public string LevelNameWithRevision => $"{Name}{(Revision > 0 ? $" (Rev. {Revision})" : "")}";
        /// <summary>The name of the level.</summary>
        public string Name { get; set; }
        /// <summary>The description of the level.</summary>
        public string Description { get; set; }
        /// <summary>The revision of the level.</summary>
        public int Revision { get; set; }
        /// <summary>The attempts made in the level.</summary>
        public int Attempts { get; set; }
        /// <summary>The ID of the level.</summary>
        public int LevelID { get; set; }
        /// <summary>The version of the level.</summary>
        public int Version { get; set; }
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
        /// <summary>The length of the level.</summary>
        public LevelLength Length { get; set; }

        // Level properties
        /// <summary>The official song ID used in the level.</summary>
        public int OfficialSongID { get; set; }
        /// <summary>The custom song ID used in the level.</summary>
        public int CustomSongID { get; set; }
        /// <summary>The song offset of the level.</summary>
        public int SongOffset { get; set; }
        /// <summary>The fade in property of the song of the level.</summary>
        public bool FadeIn { get; set; }
        /// <summary>The fade out property of the song of the level.</summary>
        public bool FadeOut { get; set; }

        /// <summary>The starting speed of the player when the level begins.</summary>
        public Speed StartingSpeed { get; set; }
        /// <summary>The starting gamemode of the player when the level begins.</summary>
        public Gamemode StartingGamemode { get; set; }
        /// <summary>The starting size of the player when the level begins.</summary>
        public PlayerSize StartingSize { get; set; }
        /// <summary>The Dual Mode property of the level.</summary>
        public bool DualMode { get; set; }
        /// <summary>The 2-Player Mode property of the level.</summary>
        public bool TwoPlayerMode { get; set; }
        /// <summary>The inversed gravity property of the level (there is no ability to set that from the game itself without hacking the gamesave).</summary>
        public bool InversedGravity { get; set; }
        /// <summary>The background texture property of the level.</summary>
        public int BackgroundTexture { get; set; }
        /// <summary>The ground texture property of the level.</summary>
        public int GroundTexture { get; set; }
        /// <summary>The ground line property of the level.</summary>
        public int GroundLine { get; set; }
        /// <summary>The font property of the level.</summary>
        public int Font { get; set; }
        /// <summary>The color channels of the level.</summary>
        public LevelColorChannels ColorChannels { get; private set; }

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
        /// <summary>The level object count.</summary>
        public int ObjectCount => LevelObjects.Count - ObjectCounts.ValueOrDefault((int)TriggerType.StartPos);
        /// <summary>The level trigger count.</summary>
        public int TriggerCount => LevelObjects.TriggerCount;
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

        // Strings
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
                GetLevelStringInformation(levelString = value);
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
        /// <summary>The raw form of the level as found in the gamesave.</summary>
        public string RawLevel
        {
            get => rawLevel;
            set => GetRawInformation(rawLevel = value);
        }
        #endregion

        #region Constructors
        /// <summary>Creates a new empty instance of the <see cref="Level"/> class.</summary>
        public Level() { }
        /// <summary>Creates a new instance of the <see cref="Level"/> class from a raw string containing a level and gets its info.</summary>
        /// <param name="level">The raw string containing the level.</param>
        public Level(string level)
        {
            RawLevel = level;
        }
        /// <summary>Creates a new instance of the <see cref="Level"/> class from a specified name, description, level string, revision and the creator's name.</summary>
        /// <param name="name">The name of the level.</param>
        /// <param name="description">The description of the level.</param>
        /// <param name="levelString">The level string of the level.</param>
        /// <param name="creatorName">The name of the creator.</param>
        /// <param name="revision">The revision of the level.</param>
        public Level(string name, string description, string levelString, string creatorName, int revision = 0)
        {
            // LONG
            RawLevel = $"<k>k_0</k><d><k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(description.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(description))}</s>" : "")}<k>k46</k><i>{revision}</i><k>k5</k><s>{creatorName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>1</i><k>k50</k><i>33</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r><k>kI6</k><d><k>0</k><s>0</s><k>1</k><s>0</s><k>2</k><s>0</s><k>3</k><s>0</s><k>4</k><s>0</s><k>5</k><s>0</s><k>6</k><s>0</s><k>7</k><s>0</s><k>8</k><s>0</s><k>9</k><s>0</s><k>10</k><s>0</s><k>11</k><s>0</s><k>12</k><s>0</s></d></d>";
        }
        #endregion

        #region Functions
        /// <summary>Clones this level and returns the cloned result.</summary>
        public Level Clone() => new Level(new string(RawLevel.ToCharArray()));
        #endregion

        #region Static Functions
        /// <summary>Merges a number of levels together. All their objects are concatenated, colors are kept the same if equal at respective IDs, otherwise reset, and the rest of the properties are kept the same if respectively equal, otherwise reset.</summary>
        /// <param name="levels">The levels to merge.</param>
        public static Level MergeLevels(params Level[] levels)
        {
            if (levels.Length == 1)
                return levels[0];
            if (levels.Length == 0)
                throw new Exception("Cannot merge 0 levels.");
            Level result = new Level();
            foreach (var l in levels)
            {
                foreach (var o in l.LevelObjects)
                    result.LevelObjects.Add(o);
                // TODO: Care for the rest of the properties
            }
            return result;
        }
        #endregion

        /// <summary>Returns a <see langword="string"/> that represents the current object.</summary>
        public override string ToString() => RawLevel;

        #region Private stuff
        private void GetLevelStringInformation(string levelString)
        {
            string infoString = levelString.Split(';').First();
            string[] split = infoString.Split(',');
            for (int i = 0; i < split.Length; i += 2)
                GetLevelStringParameterInformation(split[i], split[i + 1]);
        }
        private void GetRawInformation(string raw)
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
                IDEnd = raw.Find(endKeyString, IDStart, raw.Length);
                valueTypeStart = IDEnd + endKeyString.Length;
                valueTypeEnd = raw.Find(">", valueTypeStart, raw.Length);
                valueType = raw.Substring(valueTypeStart, valueTypeEnd - valueTypeStart);
                valueStart = valueTypeEnd + 1;
                valueEnd = valueType[valueType.Length - 1] != '/' ? raw.Find($"</{valueType}>", valueStart, raw.Length) : valueStart;
                value = raw.Substring(valueStart, valueEnd - valueStart);
                string s = raw.Substring(IDStart, IDEnd - IDStart);
                if (s != "CEK" && !s.StartsWith("I"))
                    GetRawParameterInformation(ToInt32(s), value, valueType);
                i = valueEnd;
            }
        }
        private void GetLevelStringParameterInformation(string key, string value)
        {
            switch (key)
            {
                case "kA2": // Gamemode
                    StartingGamemode = (Gamemode)ToInt32(value);
                    break;
                case "kA3": // Player Size
                    StartingSize = (PlayerSize)ToInt32(value);
                    break;
                case "kA4": // Speed
                    StartingSpeed = (Speed)ToInt32(value);
                    break;
                case "kA6": // Background
                    BackgroundTexture = ToInt32(value);
                    break;
                case "kA7": // Ground
                    GroundTexture = ToInt32(value);
                    break;
                case "kA8": // Dual Mode
                    DualMode = value == "1"; // Seriously the easiest way to determine whether it's true or not
                    break;
                case "kA9": // Level/Start Pos
                    // We don't care
                    break;
                case "kA10": // 2-Player Mode
                    TwoPlayerMode = value == "1";
                    break;
                case "kA11": // Inversed Gravity
                    InversedGravity = value == "1";
                    break;
                case "kA13": // Song Offset
                    SongOffset = ToInt32(value);
                    break;
                case "kA14": // Guidelines
                    guidelines = GetGuidelines(value);
                    break;
                case "kA15": // Fade In
                    FadeIn = value == "1";
                    break;
                case "kA16": // Fade Out
                    FadeOut = value == "1";
                    break;
                case "kA17": // Ground Line
                    GroundLine = ToInt32(value);
                    break;
                case "kA18": // Font
                    Font = ToInt32(value);
                    break;
                case "kS38": // Color Channel
                    ColorChannels = LevelColorChannels.Parse(value);
                    break;
                case "kS39": // Color Page
                    // We don't care
                    break;
                default: // We need to know more about that suspicious new thing so we keep a log of it
                    Directory.CreateDirectory("ulsk");
                    File.WriteAllText($@"ulsk\{key}.key", key);
                    break;
            }
        }
        private void GetRawParameterInformation(int parameterID, string value, string valueType)
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
                    Length = (LevelLength)ToInt32(value);
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
        #endregion
    }
}