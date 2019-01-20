using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Objects.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.General;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using static GDEdit.Application.Database;
using static GDEdit.Application.Status;
using static GDEdit.Utilities.Information.GeometryDash.LevelObjectInformation;
using static System.Convert;

namespace GDEdit.Utilities.Functions.GeometryDash
{
    public static class Gamesave
    {
        public const string DefaultLevelString = "kS38,1_40_2_125_3_255_11_255_12_255_13_255_4_-1_6_1000_7_1_15_1_18_0_8_1|1_0_2_102_3_255_11_255_12_255_13_255_4_-1_6_1001_7_1_15_1_18_0_8_1|1_0_2_102_3_255_11_255_12_255_13_255_4_-1_6_1009_7_1_15_1_18_0_8_1|1_255_2_255_3_255_11_255_12_255_13_255_4_-1_6_1002_5_1_7_1_15_1_18_0_8_1|1_255_2_0_3_0_11_255_12_255_13_255_4_-1_6_1005_5_1_7_1_15_1_18_0_8_1|1_255_2_255_3_255_11_255_12_255_13_255_4_-1_6_1006_5_1_7_1_15_1_18_0_8_1|,kA13,0,kA15,0,kA16,0,kA14,,kA6,0,kA7,0,kA17,0,kA18,0,kS39,0,kA2,0,kA3,0,kA8,0,kA4,0,kA9,0,kA10,0,kA11,0;";
        public const string LevelDataStart = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict><k>LLM_01</k><d><k>_isArr</k><t />";
        public const string LevelDataEnd = "</d><k>LLM_02</k><i>33</i></dict></plist>";
        public const string DefaultLevelData = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict><k>LLM_01</k><d><k>_isArr</k><t /></d><k>LLM_02</k><i>33</i></dict></plist>";
        public static readonly string[] LevelLengthNames = { "Tiny", "Small", "Medium", "Long", "XL" };

        /// <summary>Returns the decrypted version of the gamesave after checking whether the gamesave is encrypted or not. Returns true if the gamesave is encrypted; otherwise false.</summary>
        /// <param name="decrypted">The string to return the decrypted gamesave.</param>
        /// <returns>Returns true if the gamesave is encrypted; otherwise false.</returns>
        public static bool TryDecryptGamesave(out string decrypted)
        {
            string gamesave = "";
            bool isEncrypted = CheckIfGamesaveIsEncrypted();
            if (isEncrypted)
                gamesave = DecryptGamesave();
            else
                gamesave = Encoding.UTF8.GetString(File.ReadAllBytes(GDGameManager));
            decrypted = gamesave;
            DoneDecryptingGamesave = true;
            return isEncrypted;
        }
        public static bool CheckIfGamesaveIsEncrypted()
        {
            string gamesave = Encoding.UTF8.GetString(File.ReadAllBytes(GDGameManager));
            int checks = 0;
            string[] tests = { "<k>bgVolume</k>", "<k>sfxVolume</k>", "<k>playerUDID</k>", "<k>playerName</k>", "<k>playerUserID</k>" };
            for (int i = 0; i < tests.Length; i++)
                if (gamesave.Contains(tests[i]))
                    checks++;
            return checks != tests.Length;
        }
        public static string DecryptGamesave()
        {
            string gamesave = GetData(GDGameManager); // Get the gamesave data
            return GDGamesaveDecrypt(gamesave);
        }

        /// <summary>Returns the decrypted version of the level data after checking whether the level data is encrypted or not. Returns true if the level data is encrypted; otherwise false.</summary>
        /// <param name="decrypted">The string to return the decrypted level data.</param>
        /// <returns>Returns true if the level data is encrypted; otherwise false.</returns>
        public static bool TryDecryptLevelData(out string decrypted)
        {
            string levelData = "";
            bool isEncrypted = CheckIfLevelDataIsEncrypted();
            if (isEncrypted)
                levelData = DecryptLevelData();
            else
                levelData = Encoding.UTF8.GetString(File.ReadAllBytes(GDLocalLevels));
            decrypted = levelData;
            return isEncrypted;
        }
        public static bool CheckIfLevelDataIsEncrypted()
        {
            string levelData = Encoding.UTF8.GetString(File.ReadAllBytes(GDLocalLevels));
            string test = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\">";
            return !levelData.Contains(test);
        }
        public static string DecryptLevelData()
        {
            string levelData = GetData(GDLocalLevels); // Get the gamesave data
            return GDGamesaveDecrypt(levelData);
        }

        /// <summary>Returns the decrypted version of the level string after checking whether the level string is encrypted or not. Returns true if the level string is encrypted; otherwise false.</summary>
        /// <param name="decrypted">The string to return the decrypted level string.</param>
        /// <returns>Returns true if the level string is encrypted; otherwise false.</returns>
        public static bool TryDecryptLevelString(int index, out string decrypted)
        {
            bool isEncrypted = CheckIfLevelStringIsEncrypted(index, out decrypted);
            if (isEncrypted)
                decrypted = DecryptLevelString(decrypted);
            return isEncrypted;
        }
        public static bool TryDecryptLevelString(string ls, out string decrypted)
        {
            bool isEncrypted = CheckIfLevelStringIsEncrypted(ls, out decrypted);
            if (isEncrypted)
                decrypted = DecryptLevelString(decrypted);
            return isEncrypted;
        }
        public static bool CheckIfLevelStringIsEncrypted(int index, out string levelString)
        {
            levelString = UserLevels[index].LevelString;
            int checks = 0;
            string[] tests = { "kA13,", "kA15,", "kA16,", "kA14,", "kA6," };
            for (int i = 0; i < tests.Length; i++)
                if (UserLevels[index].LevelString.Contains(tests[i]))
                    checks++;
            return checks != tests.Length;
        }
        public static bool CheckIfLevelStringIsEncrypted(string ls, out string levelString)
        {
            levelString = ls;
            int checks = 0;
            string[] tests = { "kA13,", "kA15,", "kA16,", "kA14,", "kA6," };
            for (int i = 0; i < tests.Length; i++)
                if (levelString.Contains(tests[i]))
                    checks++;
            return checks != tests.Length;
        }
        public static string DecryptLevelString(int index)
        {
            return GDLevelStringDecrypt(UserLevels[index].LevelString);
        }
        public static string DecryptLevelString(string ls)
        {
            return GDLevelStringDecrypt(ls);
        }

        public static List<Guideline> GetGuidelines(string guidelineString)
        {
            List<Guideline> guidelines = new List<Guideline>();
            if (guidelineString != null && guidelineString != "")
            {
                guidelineString = guidelineString.Remove(guidelineString.Length - 1);
                string[] s = guidelineString.Split('~');
                for (int i = 0; i < s.Length; i += 2)
                    guidelines = guidelines.Add(ToDouble(s[i]), ToDouble(s[i + 1]));
            }
            return guidelines;
        }
        public static List<Guideline> RemoveDuplicatedGuidelines(List<Guideline> guidelines)
        {
            List<Guideline> g = new List<Guideline>();
            for (int i = 0; i < guidelines.Count; i++)
                if (!g.Contains(guidelines[i]))
                    g.Add(guidelines[i]);
            return g;
        }
        public static LevelObjectCollection GetObjects(string objectString)
        {
            List<GeneralObject> objects = new List<GeneralObject>();
            while (objectString.Length > 0 && objectString[objectString.Length - 1] == ';')
                objectString = objectString.Remove(objectString.Length - 1);
            if (objectString.Length > 0)
            {
                string[,] objectParameters = objectString.Split(';').Split(',');
                int length0 = objectParameters.GetLength(0);
                int length1 = objectParameters.GetLength(1);
                for (int i = 0; i < length0; i++)
                {
                    objects.Add(new GeneralObject(ToInt16(objectParameters[i, 1]), ToDouble(objectParameters[i, 3]), ToDouble(objectParameters[i, 5]))); // Get IDs of the selected objects
                    for (int j = 7; j < length1; j += 2)
                    {
                        if (objectParameters[i, j] != null)
                        {
                            try
                            {
                                int currentParameterID = ToInt32(objectParameters[i, j - 1]);
                                if (IntParameters.Contains(currentParameterID))
                                    objects[objects.Count - 1].SetParameterWithID(currentParameterID, ToInt32(objectParameters[i, j]));
                                else if (DoubleParameters.Contains(currentParameterID))
                                    objects[objects.Count - 1].SetParameterWithID(currentParameterID, ToDouble(objectParameters[i, j]));
                                else if (BoolParameters.Contains(currentParameterID))
                                    objects[objects.Count - 1].SetParameterWithID(currentParameterID, ToBoolean(ToInt32(objectParameters[i, j])));
                                else if (HSVParameters.Contains(currentParameterID))
                                {
                                    string[] values = objectParameters[i, j].ToString().Split('a');
                                    HSVAdjustment HSVValues = new HSVAdjustment(ToDouble(values[2]), ToDouble(values[3]), ToDouble(values[4]), (SVAdjustmentMode)ToInt32(values[0]), (SVAdjustmentMode)ToInt32(values[1]));
                                    objects[objects.Count - 1].SetParameterWithID(currentParameterID, HSVValues);
                                }
                                else if (currentParameterID == 31)
                                    objects[objects.Count - 1].SetParameterWithID(currentParameterID, Encoding.ASCII.GetString(Base64Decrypt(objectParameters[i, j])));
                                else if (IntArrayParameters.Contains(currentParameterID))
                                    objects[objects.Count - 1].SetParameterWithID(currentParameterID, objectParameters[i, j].ToString().Split('.').ToInt32Array());
                            }
                            catch (FormatException) // If the parameter is not just a number; most likely a Start Pos object
                            {
                                // Something to do, I guess
                            }
                        }
                        else break;
                    }
                }
                objectParameters = null;
            }
            return new LevelObjectCollection(objects);
        }
        public static GeneralObject GetCommonAttributes(List<GeneralObject> list, short objectID)
        {
            GeneralObject common = GeneralObject.GetNewObjectInstance(objectID);

            for (int i = list.Count; i >= 0; i--)
                if (list[i].ObjectID != objectID)
                    list.RemoveAt(i);
            if (list.Count > 1)
            {
                var properties = common.GetType().GetProperties();
                foreach (var p in properties)
                    if (Attribute.GetCustomAttributes(p, typeof(ObjectStringMappableAttribute), false).Count() > 0)
                    {
                        var v = p.GetValue(list[0]);
                        bool isCommon = true;
                        foreach (var o in list)
                            if (isCommon = (p.GetValue(o) != v))
                                break;
                        if (isCommon)
                            p.SetValue(common, v);
                    }
                return common;
            }
            else if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        public static bool GetBoolKeyValue(int index, int key)
        {
            return DecryptedLevelData.Find($"<k>k{key}</k>", LevelKeyStartIndices[index], (index < UserLevelCount - 1) ? LevelKeyStartIndices[index + 1] : DecryptedLevelData.Find("</d></d></d>")) > -1;
        }
        public static bool GetBoolKeyValue(string level, int key)
        {
            return level.Find($"<k>k{key}</k>") > -1;
        }
        public static bool[] GetBoolKeyValues(int key)
        {
            bool[] result = new bool[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
                result[i] = DecryptedLevelData.Find($"<k>k{key}</k>", LevelKeyStartIndices[i], (i < UserLevelCount - 1) ? LevelKeyStartIndices[i + 1] : DecryptedLevelData.Find("</d></d></d>")) > -1;
            return result;
        }
        public static bool[] GetLevelVerifiedStatus()
        {
            return GetBoolKeyValues(14);
        }
        public static bool[] GetLevelUploadedStatus()
        {
            return GetBoolKeyValues(15);
        }
        public static int GetGuidelineStringStartIndex(string ls)
        {
            return ls.Find("kA14,") + 5;
        }
        public static int GetGuidelineStringStartIndex(int index)
        {
            return GetGuidelineStringStartIndex(UserLevels[index].DecryptedLevelString);
        }
        public static int GetNumberOfGuidelines(string guidelineString)
        {
            if (guidelineString != null && guidelineString != "")
            {
                guidelineString = guidelineString.Remove(guidelineString.Length - 1);
                string[] s = guidelineString.Split('~');
                return s.Length / 2;
            }
            else
                return 0;
        }
        public static int GetNumberOfGuidelines(int index)
        {
            return GetNumberOfGuidelines(GetGuidelineString(index));
        }
        public static int SetLevelStringsForEmptyLevels()
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
        public static string GetCustomSongLocation(int index) => $@"{GDLocalData}\{UserLevels[index]}.mp3";
        public static string GetDecryptedLevelString(int index)
        {
            TryDecryptLevelString(index, out string decrypted);
            return decrypted;
        }
        public static string GetGuidelineString(string ls)
        {
            int guidelinesStartIndex = ls.Find("kA14,") + 5;
            int guidelinesEndIndex = ls.Find(",kA6");
            int guidelinesLength = guidelinesEndIndex - guidelinesStartIndex;
            return ls.Substring(guidelinesStartIndex, guidelinesLength);
        }
        public static string GetGuidelineString(int index)
        {
            if (UserLevels[index].DecryptedLevelString.Length > 0)
                return GetGuidelineString(UserLevels[index].DecryptedLevelString);
            return "";
        }

        public static string GetKeyValue(string level, int key, string valueType)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";

            int parameterStartIndex, parameterEndIndex, parameterLength;
            parameterStartIndex = level.Find(startKeyString, 0, level.Length - 1) + startKeyString.Length;
            parameterEndIndex = level.Find(endKeyString, parameterStartIndex, level.Length - 1);
            parameterLength = parameterEndIndex - parameterStartIndex;
            if (parameterStartIndex == startKeyString.Length - 1)
                throw new KeyNotFoundException();
            return level.Substring(parameterStartIndex, parameterLength);
        }
        public static string GetKeyValue(int index, int key, string valueType)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";
            if (UserLevelCount > 0)
            {
                if (index <= UserLevelCount - 1)
                {
                    int parameterStartIndex = UserLevels[index].RawLevel.Find(startKeyString, 0, UserLevels[index].RawLevel.Length - 1) + startKeyString.Length;
                    if (parameterStartIndex == startKeyString.Length - 1)
                        throw new KeyNotFoundException();
                    int parameterEndIndex = UserLevels[index].RawLevel.Find(endKeyString, parameterStartIndex, UserLevels[index].RawLevel.Length - 1);
                    int parameterLength = parameterEndIndex - parameterStartIndex;
                    return UserLevels[index].RawLevel.Substring(parameterStartIndex, parameterLength);
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
            else
                throw new ArgumentNullException();
        }
        public static string GetLevelKeyEntry(string levelString, string name, string desc)
        {
            int objectCount = -1;
            for (int i = 0; i < levelString.Length; i++)
                if (levelString[i] == ';')
                    objectCount++;
            return $"<d><k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(desc.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(desc))}</s>" : "")}<k>k46</k><i>0</i><k>k48</k><i>{objectCount}</i><k>k5</k><s>{UserName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>1</i><k>k50</k><i>33</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r><k>kI6</k><d><k>0</k><s>0</s><k>1</k><s>0</s><k>2</k><s>0</s><k>3</k><s>0</s><k>4</k><s>0</s><k>5</k><s>0</s><k>6</k><s>0</s><k>7</k><s>0</s><k>8</k><s>0</s><k>9</k><s>0</s><k>10</k><s>0</s><k>11</k><s>0</s><k>12</k><s>0</s></d></d>";
        }
        public static string GetLevelLengthString(int length)
        {
            switch (length)
            {
                case 1:
                    return "Small";
                case 2:
                    return "Medium";
                case 3:
                    return "Long";
                case 4:
                    return "XL";
                default:
                    return "Tiny";
            }
        }
        public static string GetLevelString(string level)
        {
            try
            {
                return GetKeyValue(level, 4, "s");
            }
            catch (KeyNotFoundException)
            {
                return "";
            }
        }
        public static string GetObjectString(string ls)
        {
            try
            {
                return ls.Split(';').RemoveAt(0).Combine(";");
            }
            catch
            {
                return "";
            }
        }
        public static string RemoveLevelIndexKey(string level)
        {
            string result = level;
            int keyIndex = result.Find("<k>k_");
            if (keyIndex > -1)
            {
                int endIndex = result.Find("</k>", keyIndex, result.Length) + 4;
                result = result.Remove(keyIndex, endIndex);
            }
            return result;
        }
        public static string TryGetKeyValue(int index, int key, string valueType, string defaultValueOnException)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";
            if (UserLevelCount > 0)
            {
                if (index <= UserLevelCount - 1)
                {
                    int parameterStartIndex;
                    int parameterEndIndex;
                    int parameterLength;
                    if (index < UserLevelCount - 1)
                    {
                        parameterStartIndex = DecryptedLevelData.Find(startKeyString, LevelKeyStartIndices[index], LevelKeyStartIndices[index + 1]) + startKeyString.Length;
                        parameterEndIndex = DecryptedLevelData.Find(endKeyString, parameterStartIndex, LevelKeyStartIndices[index + 1]);
                    }
                    else
                    {
                        parameterStartIndex = DecryptedLevelData.Find(startKeyString, LevelKeyStartIndices[index], DecryptedLevelData.Length - 1) + startKeyString.Length;
                        parameterEndIndex = DecryptedLevelData.Find(endKeyString, parameterStartIndex, DecryptedLevelData.Length - 1);
                    }
                    parameterLength = parameterEndIndex - parameterStartIndex;
                    if (parameterStartIndex == startKeyString.Length - 1)
                        return defaultValueOnException;
                    else
                        return DecryptedLevelData.Substring(parameterStartIndex, parameterLength);
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
            else
                throw new ArgumentNullException();
        }
        public static string TryGetKeyValue(string level, int key, string valueType, string defaultValueOnException)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";
            int parameterStartIndex = level.Find(startKeyString) + startKeyString.Length;
            int parameterEndIndex = level.Find(endKeyString, parameterStartIndex, level.Length - 1);
            int parameterLength = parameterEndIndex - parameterStartIndex;
            if (parameterStartIndex == startKeyString.Length - 1)
                return defaultValueOnException;
            else
                return level.Substring(parameterStartIndex, parameterLength);
        }
        public static string[] GetKeyValues(int key, string valueType)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";
            if (UserLevelCount == 0)
                throw new ArgumentNullException();
            string[] lvlParameters = new string[UserLevelCount];
            int[] parameterStartIndices = new int[UserLevelCount];
            int[] parameterEndIndices = new int[UserLevelCount];
            int[] parameterLengths = new int[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
            {
                if (i < UserLevelCount - 1)
                {
                    parameterStartIndices[i] = DecryptedLevelData.Find(startKeyString, LevelKeyStartIndices[i], LevelKeyStartIndices[i + 1]) + startKeyString.Length;
                    parameterEndIndices[i] = DecryptedLevelData.Find(endKeyString, parameterStartIndices[i], LevelKeyStartIndices[i + 1]);
                }
                else
                {
                    parameterStartIndices[i] = DecryptedLevelData.Find(startKeyString, LevelKeyStartIndices[i], DecryptedLevelData.Length - 1) + startKeyString.Length;
                    parameterEndIndices[i] = DecryptedLevelData.Find(endKeyString, parameterStartIndices[i], DecryptedLevelData.Length - 1);
                }
                parameterLengths[i] = parameterEndIndices[i] - parameterStartIndices[i];
            }
            for (int i = 0; i < parameterStartIndices.Length; i++)
                if (parameterStartIndices[i] == startKeyString.Length - 1)
                    throw new KeyNotFoundException();
            for (int i = 0; i < UserLevelCount; i++)
                lvlParameters[i] = DecryptedLevelData.Substring(parameterStartIndices[i], parameterLengths[i]);
            return lvlParameters;
        }
        public static string[] TryGetKeyValues(int key, string valueType, string defaultValueOnException)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";
            if (UserLevelCount > 0)
            {
                string[] lvlParameters = new string[UserLevelCount];
                int[] parameterStartIndices = new int[UserLevelCount];
                int[] parameterEndIndices = new int[UserLevelCount];
                int[] parameterLengths = new int[UserLevelCount];
                for (int i = 0; i < UserLevelCount; i++)
                {
                    if (i < UserLevelCount - 1)
                    {
                        parameterStartIndices[i] = DecryptedLevelData.Find(startKeyString, LevelKeyStartIndices[i], LevelKeyStartIndices[i + 1]) + startKeyString.Length;
                        parameterEndIndices[i] = DecryptedLevelData.Find(endKeyString, parameterStartIndices[i], LevelKeyStartIndices[i + 1]);
                    }
                    else
                    {
                        parameterStartIndices[i] = DecryptedLevelData.Find(startKeyString, LevelKeyStartIndices[i], DecryptedLevelData.Length - 1) + startKeyString.Length;
                        parameterEndIndices[i] = DecryptedLevelData.Find(endKeyString, parameterStartIndices[i], DecryptedLevelData.Length - 1);
                    }
                    parameterLengths[i] = parameterEndIndices[i] - parameterStartIndices[i];
                }
                for (int i = 0; i < UserLevelCount; i++)
                    if (parameterStartIndices[i] == startKeyString.Length - 1)
                        lvlParameters[i] = defaultValueOnException;
                    else
                        lvlParameters[i] = DecryptedLevelData.Substring(parameterStartIndices[i], parameterLengths[i]);
                return lvlParameters;
            }
            else
                throw new ArgumentNullException();
        }
        public static void SetKeyValue(int index, int keyValue, string keyType, string newValue)
        {
            TemporarilySetKeyValue(index, keyValue, keyType, newValue);
            UpdateLevelData();
        }
        public static void SetLevel(string newLevel, int levelIndex)
        {
            UserLevels[levelIndex].RawLevel = newLevel;
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        public static void SetLevels(string[] newLevels, int[] levelIndices)
        {
            for (int i = 0; i < levelIndices.Length; i++)
            {
                DecryptedLevelData = DecryptedLevelData.Replace(newLevels[i], LevelKeyStartIndices[levelIndices[i]], UserLevels[levelIndices[i]].RawLevel.Length); // Change the level in the game
                for (int j = levelIndices[i]; j < UserLevelCount; j++)
                    LevelKeyStartIndices[j] += newLevels[i].Length - UserLevels[levelIndices[i]].RawLevel.Length; // Change the indices of the other levels
                UserLevels[levelIndices[i]].RawLevel = newLevels[i];
            }
            UpdateLevelData(); // Write the new data
        }
        public static void TemporarilySetCustomSongID(int index, string newValue)
        {
            TemporarilySetKeyValue(index, 45, "i", newValue);
            UserLevels[index].RawLevel = UserLevels[index].RawLevel.Replace($"<k>k45</k><i>{UserLevels[index].CustomSongID}</i>", $"<k>k45</k><i>{newValue}</i>");
            UserLevels[index].CustomSongID = ToInt32(newValue);
        }
        public static void TemporarilySetKeyValue(int index, int keyValue, string keyType, string newValue)
        {
            if (index < 0)
                throw new ArgumentException("The index may not be a negative number.");
            string keyStart = $"<k>k{keyValue}</k><{keyType}>";
            string keyEnd = $"</{keyType}>";
            int keyStartIndex = 0;
            if (index < UserLevelCount - 1)
                keyStartIndex = DecryptedLevelData.Find(keyStart, LevelKeyStartIndices[index], LevelKeyStartIndices[index + 1]) + keyStart.Length;
            else if (index == UserLevelCount - 1)
                keyStartIndex = DecryptedLevelData.Find(keyStart, LevelKeyStartIndices[index], DecryptedLevelData.Length) + keyStart.Length;
            if (keyStartIndex >= keyStart.Length)
            {
                int keyEndIndex = DecryptedLevelData.Find(keyEnd, keyStartIndex, DecryptedLevelData.Length);
                int length = keyEndIndex - keyStartIndex;
                DecryptedLevelData = DecryptedLevelData.Replace(newValue, keyStartIndex, length);
                UserLevels[index].RawLevel = UserLevels[index].RawLevel.Replace(newValue, keyStartIndex - LevelKeyStartIndices[index], length);
                for (int i = index + 1; i < UserLevelCount; i++) // Adjust the indices of the other levels
                    LevelKeyStartIndices[i] += newValue.Length - length;
            }
            else
                AddKey(index, keyValue, newValue, keyType);
        }
        public static void TemporarilySetLevelDescription(int index, string newValue)
        {
            TemporarilySetLevelProperty(index, 3, "s", Base64Encrypt(Encoding.UTF8.GetBytes(UserLevels[index].Description)), Base64Encrypt(Encoding.UTF8.GetBytes(newValue)));
            UserLevels[index].Description = newValue;
        }
        public static void TemporarilySetLevelFolder(int index, string newValue)
        {
            TemporarilySetLevelProperty(index, 84, "i", UserLevels[index].Folder.ToString(), newValue);
            UserLevels[index].Folder = ToInt32(newValue);
        }
        public static void TemporarilySetLevelID(int index, string newValue)
        {
            TemporarilySetLevelProperty(index, 1, "i", UserLevels[index].LevelID.ToString(), newValue);
            UserLevels[index].LevelID = ToInt32(newValue);
        }
        public static void TemporarilySetLevelName(int index, string newValue)
        {
            TemporarilySetLevelProperty(index, 2, "s", UserLevels[index].Name, newValue);
            UserLevels[index].Name = newValue;
        }
        public static void TemporarilySetLevelRevision(int index, string newValue)
        {
            TemporarilySetLevelProperty(index, 46, "i", UserLevels[index].Revision.ToString(), newValue);
            UserLevels[index].Revision = ToInt32(newValue);
        }
        public static void TemporarilySetLevelVersion(int index, string newValue)
        {
            TemporarilySetLevelProperty(index, 16, "i", UserLevels[index].Version.ToString(), newValue);
            UserLevels[index].Version = ToInt32(newValue);
        }
        public static void TemporarilySetLevelProperty(int index, int key, string keyType, string oldValue, string newValue)
        {
            TemporarilySetKeyValue(index, key, keyType, newValue);
            UserLevels[index].RawLevel = UserLevels[index].RawLevel.Replace($"<k>k{key}</k><{keyType}>{oldValue}</i>", $"<k>k{key}</k><{keyType}>{newValue}</i>");
        }
        
        public static string GetData(string path)
        {
            StreamReader sr = new StreamReader(path);
            string readfile = sr.ReadToEnd();
            sr.Close();
            return readfile;
        }

        public static byte[] Base64Decrypt(string encodedData)
        {
            while (encodedData.Length % 4 != 0)
                encodedData += "=";
            byte[] encodedDataAsBytes = FromBase64String(encodedData);
            return encodedDataAsBytes;
        }
        public static string Base64Encrypt(byte[] decryptedData)
        {
            return ToBase64String(decryptedData);
        }
        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;

                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                    resultStream.Write(buffer, 0, read);

                return resultStream.ToArray();
            }
        }
        public static byte[] Compress(byte[] data)
        {
            using (var decompressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(decompressedStream, CompressionMode.Compress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;

                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                    resultStream.Write(buffer, 0, read);

                return resultStream.ToArray();
            }
        }
        public static string XORDecrypt(string text, int key)
        {
            byte[] result = new byte[text.Length];
            for (int c = 0; c < text.Length; c++)
                result[c] = (byte)((uint)text[c] ^ key);
            string dexored = Encoding.UTF8.GetString(result);
            return dexored;
        }
        public static string XOR11Decrypt(string text)
        {
            byte[] result = new byte[text.Length];
            for (int c = 0; c < text.Length - 1; c++)
                result[c] = (byte)((uint)text[c] ^ 11);
            string dexored = Encoding.UTF8.GetString(result);
            return dexored;
        }
        public static string GDGamesaveDecrypt(string data)
        {
            string xored = XOR11Decrypt(data); // Decrypt XOR ^ 11
            string replaced = xored.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty); // Replace characters
            byte[] gzipb64 = Decompress(Base64Decrypt(replaced)); // Decompress
            string result = Encoding.ASCII.GetString(gzipb64); // Change to string
            return result;
        }
        public static string GDLevelStringDecrypt(string ls)
        {
            string replaced = ls.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty); // Replace characters
            //string fixedBase64 = replaced.FixBase64String();
            byte[] gzipb64 = Base64Decrypt(replaced);
            if (replaced.StartsWith("H4sIAAAAAAAA"))
                gzipb64 = Decompress(gzipb64); // Decompress
            else throw new ArgumentException("The level string is not valid.");
            string result = Encoding.ASCII.GetString(gzipb64); // Change to string
            return result;
        }
    }
}