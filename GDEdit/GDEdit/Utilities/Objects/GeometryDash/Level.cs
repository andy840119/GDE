using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
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
        #region Properties
        // Metadata
        /// <summary>Returns the name of the level followed by its revision if needed.</summary>
        public string LevelNameWithRevision => $"{Name}{(Revision > 0 ? $" (Rev. {Revision})" : "")}";
        /// <summary>The name of the level.</summary>
        public string Name { get; set; }
        /// <summary>The description of the level.</summary>
        public string Description { get; set; }
        /// <summary>The name of the creator.</summary>
        public string CreatorName { get; set; }
        /// <summary>The revision of the level.</summary>
        [CommonMergedProperty]
        public int Revision { get; set; }
        /// <summary>The attempts made in the level.</summary>
        public int Attempts { get; set; }
        /// <summary>The ID of the level.</summary>
        [CommonMergedProperty]
        public int ID { get; set; }
        /// <summary>The version of the level.</summary>
        [CommonMergedProperty]
        public int Version { get; set; }
        /// <summary>The folder of the level.</summary>
        [CommonMergedProperty]
        public int Folder { get; set; }
        /// <summary>The password of the level.</summary>
        [CommonMergedProperty]
        public int Password { get; set; }
        /// <summary>The binary version of the game the level was created on.</summary>
        public int BinaryVersion { get; set; }
        /// <summary>The time spent in the editor building the level in seconds.</summary>
        public int BuildTime { get; set; }
        /// <summary>The time spent in the editor building the level.</summary>
        public TimeSpan TotalBuildTime
        {
            get => new TimeSpan(0, 0, BuildTime);
            set => BuildTime = (int)value.TotalSeconds;
        }
        /// <summary>Determines whether the level has been verified or not.</summary>
        [CommonMergedProperty]
        public bool VerifiedStatus { get; set; }
        /// <summary>Determines whether the level has been uploaded or not.</summary>
        [CommonMergedProperty]
        public bool UploadedStatus { get; set; }
        /// <summary>Determines whether the level is unlisted or not.</summary>
        [CommonMergedProperty]
        public bool Unlisted { get; set; }
        /// <summary>The length of the level.</summary>
        public LevelLength Length { get; set; }

        // Level properties
        /// <summary>The official song ID used in the level.</summary>
        [CommonMergedProperty]
        public int OfficialSongID { get; set; }
        /// <summary>The custom song ID used in the level.</summary>
        [CommonMergedProperty]
        public int CustomSongID { get; set; }
        /// <summary>The song offset of the level.</summary>
        [CommonMergedProperty]
        public int SongOffset { get; set; }
        /// <summary>The fade in property of the song of the level.</summary>
        [CommonMergedProperty]
        public bool FadeIn { get; set; }
        /// <summary>The fade out property of the song of the level.</summary>
        [CommonMergedProperty]
        public bool FadeOut { get; set; }

        /// <summary>The starting speed of the player when the level begins.</summary>
        [CommonMergedProperty]
        public Speed StartingSpeed { get; set; }
        /// <summary>The starting gamemode of the player when the level begins.</summary>
        [CommonMergedProperty]
        public Gamemode StartingGamemode { get; set; }
        /// <summary>The starting size of the player when the level begins.</summary>
        [CommonMergedProperty]
        public PlayerSize StartingSize { get; set; }
        /// <summary>The Dual Mode property of the level.</summary>
        [CommonMergedProperty]
        public bool DualMode { get; set; }
        /// <summary>The 2-Player Mode property of the level.</summary>
        [CommonMergedProperty]
        public bool TwoPlayerMode { get; set; }
        /// <summary>The inversed gravity property of the level (there is no ability to set that from the game itself without hacking the gamesave).</summary>
        [CommonMergedProperty]
        public bool InversedGravity { get; set; }
        /// <summary>The background texture property of the level.</summary>
        [CommonMergedProperty]
        public int BackgroundTexture { get; set; }
        /// <summary>The ground texture property of the level.</summary>
        [CommonMergedProperty]
        public int GroundTexture { get; set; }
        /// <summary>The ground line property of the level.</summary>
        [CommonMergedProperty]
        public int GroundLine { get; set; }
        /// <summary>The font property of the level.</summary>
        [CommonMergedProperty]
        public int Font { get; set; }
        /// <summary>The level's guidelines.</summary>
        public GuidelineCollection Guidelines { get; set; }
        /// <summary>The level's objects.</summary>
        public LevelObjectCollection LevelObjects { get; set; }
        /// <summary>The color channels of the level.</summary>
        public LevelColorChannels ColorChannels { get; private set; }

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

        // Editor stuff
        /// <summary>The X position of the camera.</summary>
        [CommonMergedProperty]
        public double CameraX { get; set; }
        /// <summary>The Y position of the camera.</summary>
        [CommonMergedProperty]
        public double CameraY { get; set; }
        /// <summary>The zoom of the camera.</summary>
        [CommonMergedProperty]
        public double CameraZoom { get; set; }

        // Strings
        /// <summary>The level string in its decrypted form.</summary>
        public string LevelString
        {
            get => GetLevelString();
            set
            {
                TryDecryptLevelString(value, out var decryptedLevelString);
                GetLevelStringInformation(decryptedLevelString);
            }
        }
        /// <summary>The raw form of the level as found in the gamesave.</summary>
        public string RawLevel
        {
            get => GetRawLevel();
            set => GetRawInformation(value);
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
            RawLevel = $"<k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(description.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(description))}</s>" : "")}<k>k46</k><i>{revision}</i><k>k5</k><s>{creatorName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>0</i><k>k50</k><i>35</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r>";
        }
        #endregion

        #region Functions
        /// <summary>Clones this level and returns the cloned result.</summary>
        public Level Clone() => new Level(RawLevel.Substring(0));
        /// <summary>Returns the level string of this <seealso cref="Level"/>.</summary>
        public string GetLevelString() => $"kS38,{ColorChannels},kA13,{SongOffset},kA15,{(FadeIn ? "1" : "0")},kA16,{(FadeOut ? "1" : "0")},kA14,{Guidelines},kA6,{BackgroundTexture},kA7,{GroundTexture},kA17,{GroundLine},kA18,{Font},kS39,0,kA2,{StartingGamemode},kA3,{StartingSize},kA8,{(DualMode ? "1" : "0")},kA4,{StartingSpeed},kA9,0,kA10,{(TwoPlayerMode ? "1" : "0")},kA11,{(InversedGravity ? "1" : "0")};{LevelObjects}";
        /// <summary>Returns the raw level string of this <seealso cref="Level"/>.</summary>
        public string GetRawLevel() => $"<k>kCEK</k><i>4</i><k>k1</k><i>{ID}</i><k>k2</k><s>{Name}</s><k>k4</k><s>{LevelString}</s>{(Description.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(Description))}</s>" : "")}<k>k46</k><i>{Revision}</i><k>k5</k><s>{CreatorName}</s><k>k13</k><t />{GetBoolPropertyString("k14", VerifiedStatus)}{GetBoolPropertyString("k15", UploadedStatus)}{GetBoolPropertyString("k79", Unlisted)}<k>k21</k><i>2</i><k>k16</k><i>{Version}</i><k>k8</k><i>{OfficialSongID}</i><k>k45</k><i>{CustomSongID}</i><k>k80</k><i>{BuildTime}</i><k>k50</k><i>{BinaryVersion}</i><k>k47</k><t /><k>k84</k><i>{Folder}</i><k>kI1</k><r>{CameraX}</r><k>kI2</k><r>{CameraY}</r><k>kI3</k><r>{CameraZoom}</r>";
        #endregion

        #region Static Functions
        /// <summary>Merges a number of levels together. All their objects are concatenated, colors are kept the same if equal at respective IDs, otherwise reset, and the rest of the properties (excluding some metadata) are kept the same if respectively equal, otherwise reset.</summary>
        /// <param name="levels">The levels to merge.</param>
        public static Level MergeLevels(params Level[] levels)
        {
            if (levels.Length == 1)
                return levels[0];
            if (levels.Length == 0)
                throw new Exception("Cannot merge 0 levels.");
            Level result = levels[0].Clone();
            for (int i = 1; i < levels.Length; i++)
            {
                result.LevelObjects.AddRange(levels[i].LevelObjects);
                result.Guidelines.AddRange(levels[i].Guidelines);
                result.BuildTime += levels[i].BuildTime;
                if (levels[i].BinaryVersion > result.BinaryVersion)
                    result.BinaryVersion = levels[i].BinaryVersion;
            }
            result.ColorChannels = LevelColorChannels.GetCommonColors(levels);
            result.Guidelines.RemoveDuplicatedGuidelines();
            var properties = typeof(Level).GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttributes(typeof(CommonMergedPropertyAttribute), false).Length > 0)
                {
                    bool common = true;
                    for (int i = 1; i < levels.Length && common; i++)
                        if (!(common = p.GetValue(result) != p.GetValue(levels[i])))
                            p.SetValue(result, default);
                }
            }
            return result;
        }
        #endregion

        /// <summary>Returns a <see langword="string"/> that represents the current object.</summary>
        public override string ToString() => RawLevel;

        #region Private stuff
        private void GetLevelStringInformation(string levelString)
        {
            string infoString = levelString.Substring(0, levelString.IndexOf(';'));
            string[] split = infoString.Split(',');
            for (int i = 0; i < split.Length; i += 2)
                GetLevelStringParameterInformation(split[i], split[i + 1]);
            LevelObjects = GetObjects(GetObjectString(levelString));
        }
        private void GetRawInformation(string raw)
        {
            string startKeyString = "<k>";
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
                GetRawParameterInformation(s, value, valueType);
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
                    Guidelines = GuidelineCollection.Parse(value);
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
                case "kA9": // Level/Start Pos
                case "kS39": // Color Page
                    // We don't care
                    break;
                default: // We need to know more about that suspicious new thing so we keep a log of it
                    Directory.CreateDirectory("ulsk");
                    File.WriteAllText($@"ulsk\{key}.key", key);
                    break;
            }
        }
        private void GetRawParameterInformation(string key, string value, string valueType)
        {
            switch (key)
            {
                case "k1": // Level ID
                    ID = ToInt32(value);
                    break;
                case "k2": // Level Name
                    Name = value;
                    break;
                case "k3": // Level Description
                    Description = Encoding.UTF8.GetString(Base64Decrypt(value));
                    break;
                case "k4": // Level String
                    LevelString = value;
                    break;
                case "k5": // Creator Name
                    CreatorName = value;
                    break;
                case "k8": // Official Song ID
                    OfficialSongID = ToInt32(value);
                    break;
                case "k14": // Level Verified Status
                    VerifiedStatus = valueType == "t /"; // Well that's how it's implemented ¯\_(ツ)_/¯
                    break;
                case "k15": // Level Uploaded Status
                    UploadedStatus = valueType == "t /";
                    break;
                case "k16": // Level Version
                    Version = ToInt32(value);
                    break;
                case "k18": // Level Attempts
                    Attempts = ToInt32(value);
                    break;
                case "k23": // Level Length
                    Length = (LevelLength)ToInt32(value);
                    break;
                case "k41": // Level Password
                    Password = ToInt32(value) % 1000000; // If it exists, the password is in the form $"1{password}" (example: 1000023 = 0023)
                    break;
                case "k45": // Custom Song ID
                    CustomSongID = ToInt32(value);
                    break;
                case "k46": // Level Revision
                    Revision = ToInt32(value);
                    break;
                case "k50": // Binary Version
                    BinaryVersion = ToInt32(value);
                    break;
                case "k79": // Time Spent
                    BuildTime = ToInt32(value);
                    break;
                case "k80": // Time Spent
                    BuildTime = ToInt32(value);
                    break;
                case "k84": // Level Folder
                    Folder = ToInt32(value);
                    break;
                case "kI1": // Camera X
                    CameraX = ToDouble(value);
                    break;
                case "kI2": // Camera Y
                    CameraY = ToDouble(value);
                    break;
                case "kI3": // Camera Zoom
                    CameraZoom = ToDouble(value);
                    break;
                default: // Not something we care about
                    break;
            }
        }
        private string GetBoolPropertyString(string key, bool value) => value ? $"<k>{key}</k><t />" : "";
        #endregion
    }
}