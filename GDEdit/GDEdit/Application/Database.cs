using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Functions.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash;
using static System.Environment;
using static System.Convert;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDEdit.Application
{
    /// <summary>Contains information about a database for the game.</summary>
    public class Database
    {
        private string decryptedGamesave;
        private string decryptedLevelData;
        private List<CustomLevelObject> customObjects;

        #region Constants
        /// <summary>The default local data folder path of the game.</summary>
        public static readonly string GDLocalData = $@"{GetFolderPath(SpecialFolder.LocalApplicationData)}\GeometryDash";
        /// <summary>The default game manager file path of the game.</summary>
        public static readonly string GDGameManager = $@"{GDLocalData}\CCGameManager.dat";
        /// <summary>The default local levels file path of the game.</summary>
        public static readonly string GDLocalLevels = $@"{GDLocalData}\CCLocalLevels.dat";
        #endregion

        #region Information
        /// <summary>The path for the game manager file.</summary>
        public string GameManagerPath { get; }
        /// <summary>The path for the local levels file.</summary>
        public string LocalLevelsPath { get; }

        // TODO: Split into LevelInfo and GameInfo

        /// <summary>The level key start indices of the database in the local levels file.</summary>
        public List<int> LevelKeyStartIndices { get; private set; }

        /// <summary>The user name of the player as found in the game manager file.</summary>
        public string UserName { get; set; }
        /// <summary>The user name of the player.</summary>
        public List<Level> UserLevels { get; set; }
        /// <summary>The names of the folders.</summary>
        public List<string> FolderNames { get; set; }

        /// <summary>The decrypted form of the game manager.</summary>
        public string DecryptedGamesave
        {
            get
            {
                if (decryptedGamesave == null)
                    TryDecryptGamesave(File.ReadAllText(GameManagerPath), out decryptedGamesave);
                return decryptedGamesave;
            }
            set => decryptedGamesave = value;
        }
        /// <summary>The decrypted form of the level data.</summary>
        public string DecryptedLevelData
        {
            get
            {
                if (decryptedLevelData == null)
                    TryDecryptLevelData(File.ReadAllText(LocalLevelsPath), out decryptedLevelData);
                return decryptedLevelData;
            }
            set => decryptedLevelData = value;
        }
        /// <summary>The custom objects.</summary>
        public List<CustomLevelObject> CustomObjects
        {
            get
            {
                if (customObjects == null)
                    GetCustomObjects();
                return customObjects;
            }
            set => customObjects = value;
        }

        /// <summary>The number of local levels in the level data file.</summary>
        public int UserLevelCount => UserLevels.Count;
        #endregion

        #region Contsructors
        /// <summary>Initializes a new instance of the <seealso cref="Database"/> class from the default database file paths.</summary>
        public Database() : this(GDGameManager, GDLocalLevels) { }
        /// <summary>Initializes a new instance of the <seealso cref="Database"/> class from custom database file paths.</summary>
        /// <param name="gameManagerPath">The file path of the game manager file of the game.</param>
        /// <param name="localLevelsPath">The file path of the local levels file of the game.</param>
        public Database(string gameManagerPath, string localLevelsPath)
        {
            GameManagerPath = gameManagerPath;
            LocalLevelsPath = localLevelsPath;
            GetKeyIndices();
            GetLevels();
        }
        #endregion

        // TODO: Order these appropriately
        #region Functions
        /// <summary>Adds a level string parameter to the raw data of the level at the specified index.</summary>
        /// <param name="newLS">The new level string to add to the level.</param>
        /// <param name="levelIndex">The index of the level in the database to add the level string parameter at.</param>
        public void AddLevelStringParameter(string newLS, int levelIndex)
        {
            string nameKey = $"<k>k2</k><s>{UserLevels[levelIndex].Name}</s>";
            int nameKeyOccurence = 0;
            for (int i = 0; i < levelIndex; i++)
                if (UserLevels[i].Name == UserLevels[levelIndex].Name)
                    nameKeyOccurence++;
            int index = DecryptedLevelData.Find(nameKey, nameKeyOccurence + 1) + nameKey.Length;
            DecryptedLevelData = DecryptedLevelData.Insert(index, $"<k>k4</k><s>{newLS}</s>");
            for (int i = levelIndex + 1; i < UserLevelCount; i++)
                LevelKeyStartIndices[i] += $"<k>k4</k><s>{newLS}</s>".Length;
            UserLevels[levelIndex].LevelString = newLS;
        }
        /// <summary>Clones a level and adds it to the start of the list.</summary>
        /// <param name="index">The index of the level to clone.</param>
        public void CloneLevel(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "The index of the level cannot be a negative number.");
            if (index >= UserLevelCount)
                throw new ArgumentOutOfRangeException("index", "The argument that is parsed is out of range.");
            UserLevels.Insert(0, UserLevels[index]); // TODO: .Clone()
            UpdateLevelData();
        }
        /// <summary>Clones a number of levels and adds them to the start of the level list in their original order.</summary>
        /// <param name="indices">The indices of the levels to clone.</param>
        public void CloneLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] >= 0)
                    if (indices[i] < UserLevelCount)
                        UserLevels.Insert(i, UserLevels[indices[i] + i]); // TODO: .Clone()
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        /// <summary>Creates a new level with the name "Unnamed {n}" and adds it to the start of the level list.</summary>
        public void CreateLevel()
        {
            int n = 0;
            List<int> nums = new List<int>();
            for (int i = 0; i < UserLevelCount; i++) // Add the unnamed numbers to the list
                if (UserLevels[i].Name.Contains("Unnamed ") && UserLevels[i].Name.Split(' ').Length == 2 && int.TryParse(UserLevels[i].Name.Split(' ')[1], out int _))
                    nums.Add(UserLevels[i].Name.GetLastNumber());
            nums.Sort();
            while (n < nums.Count && n == nums[n])
                n++;
            CreateLevel($"Unnamed {n}", "");
        }
        /// <summary>Creates a new level with a specified name and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        public void CreateLevel(string name)
        {
            CreateLevel(name, "");
        }
        /// <summary>Creates a new level with a specified name and description and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="desc">The description of the new level to create.</param>
        public void CreateLevel(string name, string desc)
        {
            int r = 0;
            List<int> levelsWithSameName = new List<int>();
            for (int i = 0; i < UserLevels.Count; i++)
                if (name == UserLevels[i].Name)
                    levelsWithSameName.Add(i);
            List<int> revs = new List<int>(); // The revisions of the levels with the same name
            for (int i = 0; i < levelsWithSameName.Count; i++) // Add the revisions of the levels with the same name in the list
                revs.Add(UserLevels[levelsWithSameName[i]].Revision);
            revs.Sort();
            while (r < revs.Count && r == revs[r])
                r++;
            // TODO: Transfer to separate function
            string level = $"<k>k_0</k><d><k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{DefaultLevelString}</s>{(desc.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(desc))}</s>" : "")}<k>k46</k><i>{r}</i><k>k5</k><s>{UserName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>1</i><k>k50</k><i>33</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r><k>kI6</k><d><k>0</k><s>0</s><k>1</k><s>0</s><k>2</k><s>0</s><k>3</k><s>0</s><k>4</k><s>0</s><k>5</k><s>0</s><k>6</k><s>0</s><k>7</k><s>0</s><k>8</k><s>0</s><k>9</k><s>0</s><k>10</k><s>0</s><k>11</k><s>0</s><k>12</k><s>0</s></d></d>";
            // Insert the cloned level's parameters
            UserLevels.Insert(0, new Level(level));
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        /// <summary>Creates a new level with a specified name, description and level string and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="desc">The description of the new level to create.</param>
        /// <param name="levelString">The level string of the new level to create.</param>
        public void CreateLevel(string name, string desc, string levelString)
        {
            int r = 0;
            List<int> levelsWithSameName = new List<int>();
            for (int i = 0; i < UserLevels.Count; i++)
                if (name == UserLevels[i].Name)
                    levelsWithSameName.Add(i);
            List<int> revs = new List<int>(); // The revisions of the levels with the same name
            for (int i = 0; i < levelsWithSameName.Count; i++) // Add the revisions of the levels with the same name in the list
                revs.Add(UserLevels[levelsWithSameName[i]].Revision);
            revs.Sort();
            while (r < revs.Count && r == revs[r])
                r++;
            // TODO: Transfer to separate function
            string level = $"<k>k_0</k><d><k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(desc.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(desc))}</s>" : "")}<k>k46</k><i>{r}</i><k>k5</k><s>{UserName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>1</i><k>k50</k><i>33</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r><k>kI6</k><d><k>0</k><s>0</s><k>1</k><s>0</s><k>2</k><s>0</s><k>3</k><s>0</s><k>4</k><s>0</s><k>5</k><s>0</s><k>6</k><s>0</s><k>7</k><s>0</s><k>8</k><s>0</s><k>9</k><s>0</s><k>10</k><s>0</s><k>11</k><s>0</s><k>12</k><s>0</s></d></d>";

            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        /// <summary>Creates a number of new levels with the names "Unnamed {n}" and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        public void CreateLevels(int numberOfLevels)
        {
            // TODO: Create
        }
        /// <summary>Creates a number of new levels with specified names and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        /// <param name="name">The names of the new levels to create.</param>
        public void CreateLevels(int numberOfLevels, string[] names)
        {
            // TODO: Create
        }
        /// <summary>Creates a number of new levels with specified names and descriptions and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        /// <param name="name">The names of the new levels to create.</param>
        /// <param name="desc">The descriptions of the new levels to create.</param>
        public void CreateLevels(int numberOfLevels, string[] names, string[] descs)
        {
            // TODO: Create
        }
        /// <summary>Deletes all levels in the database.</summary>
        public void DeleteAllLevels()
        {
            DecryptedLevelData = DefaultLevelData; // Set the level data to the default
            UpdateLevelData(); // Write the new data

            // Delete all the level info from the prorgam's memory
            UserLevels.Clear();
            LevelKeyStartIndices.Clear();
        }
        /// <summary>Deletes the levels at the specified indices in the database.</summary>
        /// <param name="indices">The indices of the levels to delete from the database.</param>
        public void DeleteLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates();
            indices = indices.Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                RemoveAllLevelInfoAt(indices[i]);
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        /// <summary>Exports the level at the specified index in the database to a .dat file in the specified folder.</summary>
        /// <param name="index">The index of the level to export.</param>
        /// <param name="folderPath">The path of the folder to export the level at.</param>
        public void ExportLevel(int index, string folderPath)
        {
            File.WriteAllText($@"{folderPath}\{UserLevels[index].LevelNameWithRevision}.dat", UserLevels[index].ToString());
        }
        /// <summary>Exports the levels at the specified indices in the database to a .dat file in the specified folder.</summary>
        /// <param name="indices">The indices of the levels to export.</param>
        /// <param name="folderPath">The path of the folder to export the level at.</param>
        public void ExportLevels(int[] indices, string folderPath)
        {
            for (int i = 0; i < indices.Length; i++)
                File.WriteAllText($@"{folderPath}\{UserLevels[indices[i]].LevelNameWithRevision}.dat", UserLevels[indices[i]].ToString());
        }
        /// <summary>Sets the level strings for levels whose level strings are empty.</summary>
        public int SetLevelStringsForEmptyLevels()
        {
            int lvls = 0;
            for (int i = 0; i < UserLevelCount; i++)
                if (UserLevels[i].LevelString == "")
                {
                    AddLevelStringParameter(DefaultLevelString, i);
                    lvls++;
                }
            UpdateLevelData();
            return lvls;
        }
        /// <summary>Retrieves the custom song location of the song with the specified ID.</summary>
        /// <param name="ID">The ID of the song.</param>
        public string GetCustomSongLocation(int ID) => $@"{GDLocalData}\{ID}.mp3";
        public string GetLevelKeyEntry(string levelString, string name, string desc)
        {
            int objectCount = -1;
            for (int i = 0; i < levelString.Length; i++)
                if (levelString[i] == ';')
                    objectCount++;
            return $"<d><k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(desc.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(desc))}</s>" : "")}<k>k46</k><i>0</i><k>k48</k><i>{objectCount}</i><k>k5</k><s>{UserName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>1</i><k>k50</k><i>33</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r><k>kI6</k><d><k>0</k><s>0</s><k>1</k><s>0</s><k>2</k><s>0</s><k>3</k><s>0</s><k>4</k><s>0</s><k>5</k><s>0</s><k>6</k><s>0</s><k>7</k><s>0</s><k>8</k><s>0</s><k>9</k><s>0</s><k>10</k><s>0</s><k>11</k><s>0</s><k>12</k><s>0</s></d></d>";
        }
        public int GetLevelCount()
        {
            return DecryptedLevelData.FindAll("<k>k_").Length;
        }
        public void GetCustomObjects()
        {
            customObjects = new List<CustomLevelObject>();
            int startIndex = DecryptedGamesave.Find("<k>customObjectDict</k><d>") + 26;
            if (startIndex < 26)
                return;

            int endIndex = DecryptedGamesave.Find("</d>", startIndex, DecryptedGamesave.Length);
            int currentIndex = startIndex;
            while ((currentIndex = DecryptedGamesave.Find("</k><s>", currentIndex, endIndex) + 7) > 6)
                customObjects.Add(new CustomLevelObject(GetObjects(DecryptedGamesave.Substring(currentIndex, DecryptedGamesave.Find("</s>", currentIndex, DecryptedGamesave.Length) - currentIndex))));
        }
        public void GetKeyIndices()
        {
            LevelKeyStartIndices = new List<int>();
            int count = GetLevelCount();
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    LevelKeyStartIndices.Add(DecryptedLevelData.Find($"<k>k_{i}</k><d>", LevelKeyStartIndices[i - 1], DecryptedLevelData.Length) + $"<k>k_{i}</k>".Length);
                else if (i == 0)
                    LevelKeyStartIndices.Add(DecryptedLevelData.Find($"<k>k_{i}</k><d>") + $"<k>k_{i}</k>".Length);
            }
        }
        public void GetLevels()
        {
            UserLevels = new List<Level>();
            for (int i = 0; i < LevelKeyStartIndices.Count; i++)
            {
                if (i < LevelKeyStartIndices.Count - 1)
                    UserLevels.Add(new Level(DecryptedLevelData.Substring(LevelKeyStartIndices[i], LevelKeyStartIndices[i + 1] - LevelKeyStartIndices[i] - $"<k>k_{i + 1}</k>".Length)));
                else
                    UserLevels.Add(new Level(DecryptedLevelData.Substring(LevelKeyStartIndices[i], Math.Max(DecryptedLevelData.Find("</d></d></d>", LevelKeyStartIndices[i], DecryptedLevelData.Length) + 8, DecryptedLevelData.Find("<d /></d></d>", LevelKeyStartIndices[i], DecryptedLevelData.Length) + 9) - LevelKeyStartIndices[i])));
            }
        }

        public void ImportLevel(string level)
        {
            for (int i = UserLevelCount - 1; i >= 0; i--) // Increase the level indices of all the other levels to insert the cloned level at the start
                DecryptedLevelData = DecryptedLevelData.Replace($"<k>k_{i}</k>", $"<k>k_{i + 1}</k>");
            level = RemoveLevelIndexKey(level); // Remove the index key of the level
            DecryptedLevelData = DecryptedLevelData.Insert(LevelKeyStartIndices[0] - 10, $"<k>k_0</k>{level}"); // Insert the new level
            int clonedLevelLength = level.Length + 10; // The length of the inserted level
            LevelKeyStartIndices = LevelKeyStartIndices.InsertAtStart(LevelKeyStartIndices[0]); // Add the new key start position in the array
            for (int i = 1; i < LevelKeyStartIndices.Count; i++)
                LevelKeyStartIndices[i] += clonedLevelLength; // Increase the other key indices by the length of the cloned level
            // Insert the imported level's parameters
            UserLevels = UserLevels.InsertAtStart(new Level(level));
            UpdateLevelData(); // Write the new data
        }
        public void ImportLevelFromFile(string levelPath)
        {
            ImportLevel(File.ReadAllText(levelPath));
        }
        public void ImportLevels(string[] lvls)
        {
            for (int i = 0; i < lvls.Length; i++)
            {
                lvls[i] = RemoveLevelIndexKey(lvls[i]); // Remove the index key of the level
                UserLevels.InsertAtStart(new Level(lvls[i]));
            }
            GetKeyIndices();
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        public void ImportLevelsFromFiles(string[] levelPaths)
        {
            string[] levels = new string[levelPaths.Length];
            for (int i = 0; i < levelPaths.Length; i++)
                levels[i] = File.ReadAllText(levelPaths[i]);
            ImportLevels(levels);
        }
        public void MoveLevelsDown(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort().RemoveElementsMatchingIndicesFromEnd(UserLevelCount);
            for (int i = indices.Length - 1; i >= 0; i--)
                if (indices[i] < UserLevelCount - 1) // If the level can be moved further down
                    SwapAllLevelInfo(indices[i], indices[i] + 1);
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData(); // Write the new data
        }
        public void MoveLevelsToBottom(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                MoveAllLevelInfo(indices[i], UserLevelCount - indices.Length + i + 1); // +1?
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData();
        }
        public void MoveLevelsToTop(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = 0; i < indices.Length; i++)
                MoveAllLevelInfo(indices[i], i);
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData();
        }
        public void MoveLevelsUp(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort().RemoveElementsMatchingIndices();
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] >= i) // If the level can be moved further up
                    SwapAllLevelInfo(indices[i], indices[i] - 1);
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData(); // Write the new data
        }
        public void SwapLevels(int levelIndexA, int levelIndexB)
        {
            SwapAllLevelInfo(levelIndexA, levelIndexB);
            UpdateMemoryLevelData();
            UpdateLevelData();
        }

        // TODO: Remove
        public void CloneAllLevelInfo(int a, int b)
        {
            UserLevels.Insert(a, UserLevels[b]);
        }
        public void MoveAllLevelInfo(int a, int b)
        {
            UserLevels.MoveElement(a, b);
        }
        public void MoveAllLevelInfoToStart(int a)
        {
            UserLevels.MoveElementToStart(a);
        }
        public void MoveAllLevelInfoToEnd(int a)
        {
            UserLevels.MoveElementToEnd(a);
        }
        public void RemoveAllLevelInfoAt(int a)
        {
            UserLevels.RemoveAt(a);
        }
        public void SwapAllLevelInfo(int a, int b)
        {
            UserLevels = UserLevels.Swap(a, b);
        }

        public void UpdateLevelData()
        {
            File.WriteAllText(GDLocalLevels, DecryptedLevelData); // Write the level data
        }
        public void UpdateMemoryLevelData()
        {
            LevelKeyStartIndices = new List<int>();
            StringBuilder lvlDat = new StringBuilder(LevelDataStart);
            for (int i = 0; i < UserLevelCount; i++)
            {
                lvlDat = lvlDat.Append($"<k>k_{i}</k>");
                LevelKeyStartIndices.Add(lvlDat.Length);
                lvlDat = lvlDat.Append(UserLevels[i].RawLevel);
            }
            lvlDat = lvlDat.Append(LevelDataEnd);
            DecryptedLevelData = lvlDat.ToString();
        }

        private void GetPlayerName()
        {
            int playerNameStartIndex = DecryptedGamesave.FindFromEnd("<k>playerName</k><s>") + 20;
            int playerNameEndIndex = DecryptedGamesave.FindFromEnd("</s><k>playerUserID</k>");
            int playerNameLength = playerNameEndIndex - playerNameStartIndex;
            UserName = DecryptedGamesave.Substring(playerNameStartIndex, playerNameLength);
        }
        // TODO: Decide whether they're staying or not
        private string GetUserID()
        {
            int userIDStartIndex = DecryptedGamesave.FindFromEnd("<k>playerUserID</k><i>") + 22;
            int userIDEndIndex = DecryptedGamesave.FindFromEnd("</i><k>playerFrame</k>");
            int userIDLength = userIDEndIndex - userIDStartIndex;
            return DecryptedGamesave.Substring(userIDStartIndex, userIDLength);
        }
        private string GetAccountID()
        {
            int accountIDStartIndex = DecryptedGamesave.FindFromEnd("<k>GJA_003</k><i>") + 17;
            int accountIDEndIndex = DecryptedGamesave.FindFromEnd("</i><k>KBM_001</k>");
            int accountIDLength = accountIDEndIndex - accountIDStartIndex;
            if (accountIDLength > 0)
                return DecryptedGamesave.Substring(accountIDStartIndex, accountIDLength);
            else
                return "0";
        }
        private void GetFolderNames()
        {
            FolderNames = new List<string>();
            int foldersStartIndex = DecryptedGamesave.FindFromEnd("<k>GLM_19</k><d>") + 16;
            if (foldersStartIndex > 15)
            {
                int foldersEndIndex = DecryptedGamesave.Find("</d>", foldersStartIndex, DecryptedGamesave.Length);
                int currentIndex = foldersStartIndex;
                while ((currentIndex = DecryptedGamesave.Find("<k>", currentIndex, foldersEndIndex) + 3) > 2)
                {
                    int endingIndex = DecryptedGamesave.Find("</k>", currentIndex, foldersEndIndex);
                    int folderIndex = int.Parse(DecryptedGamesave.Substring(currentIndex, endingIndex - currentIndex));
                    int folderNameStartIndex = endingIndex + 7;
                    int folderNameEndIndex = DecryptedGamesave.Find("</s>", folderNameStartIndex, foldersEndIndex);
                    while (folderIndex >= FolderNames.Count)
                        FolderNames.Add("");
                    FolderNames[folderIndex] = DecryptedGamesave.Substring(folderNameStartIndex, folderNameEndIndex - folderNameStartIndex);
                }
            }
        }
        #endregion
    }
}
