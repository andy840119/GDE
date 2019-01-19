using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Functions.General;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash;
using static GDEdit.Application.Database;
using static GDEdit.Application.Status;
using static GDEdit.Utilities.Information.GeometryDash.LevelObjectInformation;
using static System.Convert;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using GDEdit.Utilities.Objects.GeometryDash.General;
using GDEdit.Utilities.Attributes;

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
                //GC.Collect();
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
                    if (p.GetCustomAttributes(typeof(ObjectStringMappableAttribute), false).Count() > 0)
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
            return DecryptedLevelData.Find("<k>k" + key.ToString() + "</k>", LevelKeyStartIndices[index], (index < UserLevelCount - 1) ? LevelKeyStartIndices[index + 1] : DecryptedLevelData.Find("</d></d></d>")) > -1;
        }
        public static bool GetBoolKeyValue(string level, int key)
        {
            return level.Find("<k>k" + key.ToString() + "</k>") > -1;
        }
        public static bool[] GetBoolKeyValues(int key)
        {
            bool[] result = new bool[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
                result[i] = DecryptedLevelData.Find("<k>k" + key.ToString() + "</k>", LevelKeyStartIndices[i], (i < UserLevelCount - 1) ? LevelKeyStartIndices[i + 1] : DecryptedLevelData.Find("</d></d></d>")) > -1;
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
        public static int GetLevelCount()
        {
            return DecryptedLevelData.FindAll("<k>k_").Length;
        }
        public static int GetLevelLength(int index)
        {
            try { return ToInt32(GetKeyValue(index, 23, "i")); }
            catch { return 0; }
        }
        public static int GetLevelRevision(int index)
        {
            return ToInt32(GetKeyValue(index, 46, "i"));
        }
        public static int GetNumberOfGuidelines(string guidelineString)
        {
            if (guidelineString != null && guidelineString != "")
            {
                guidelineString = guidelineString.Remove(guidelineString.Length - 1);
                string[] s = guidelineString.Split('~');
                return s.Length / 2;
            }
            else return 0;
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
        public static int[] GetDifferentLevelObjectCounts()
        {
            int[] objCounts = new int[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
                objCounts[i] = GetObjects(GetObjectString(UserLevels[i].LevelString)).DifferentObjectIDCount;
            return objCounts;
        }
        public static int[] GetLevelAttempts()
        {
            return TryGetKeyValues(18, "i", "0").ToInt32Array();
        }
        public static int[] GetLevelFolders()
        {
            return TryGetKeyValues(84, "i", "0").ToInt32Array();
        }
        public static int[] GetLevelIDs()
        {
            return TryGetKeyValues(1, "i", "0").ToInt32Array();
        }
        public static int[] GetLevelLengths()
        {
            return TryGetKeyValues(23, "i", "0").ToInt32Array();
        }
        public static int[] GetLevelObjectCounts()
        {
            int[] objCounts = new int[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
            {
                string[] objs = GetObjectString(UserLevels[i].LevelString).Split(';');
                for (int j = 0; j < objs.Length; j++)
                    if (objs[j].Length > 0 && !objs[j].StartsWith("1,31,2"))
                        objCounts[i]++;
            }
            return objCounts;
        }
        public static int[] GetLevelRevisions()
        {
            return TryGetKeyValues(46, "i", "0").ToInt32Array();
        }
        public static int[] GetLevelVersions()
        {
            return TryGetKeyValues(16, "i", "0").ToInt32Array();
        }
        public static string GetCustomSongLocation(int index)
        {
            return GDLocalData + "\\" + GetCustomSongID(index) + ".mp3";
        }
        public static string GetCustomSongID(int index)
        {
            return GetKeyValue(index, 45, "i");
        }
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
            string startKeyString = "<k>k" + key.ToString() + "</k><" + valueType + ">";
            string endKeyString = "</" + valueType + ">";
            if (UserLevelCount > 0)
            {
                int parameterStartIndex, parameterEndIndex, parameterLength;
                parameterStartIndex = DecryptedLevelData.Find(startKeyString, 0, level.Length - 1) + startKeyString.Length;
                parameterEndIndex = DecryptedLevelData.Find(endKeyString, parameterStartIndex, level.Length - 1);
                parameterLength = parameterEndIndex - parameterStartIndex;
                if (parameterStartIndex == startKeyString.Length - 1)
                    throw new KeyNotFoundException();
                else
                    return DecryptedLevelData.Substring(parameterStartIndex, parameterLength);
            }
            else
                throw new ArgumentNullException();
        }
        public static string GetKeyValue(int index, int key, string valueType)
        {
            string startKeyString = "<k>k" + key.ToString() + "</k><" + valueType + ">";
            string endKeyString = "</" + valueType + ">";
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
        public static string GetLevel(int index)
        {
            string levelKey = $"<k>k_{index}</k>"; // The key of the level that will be returned
            string level = ""; // The level that is being returned
            if (index < UserLevelCount - 1) // If the level is not the last level in the list
                level = levelKey + DecryptedLevelData.Substring(LevelKeyStartIndices[index], DecryptedLevelData.Find($"<k>k_{(index + 1)}</k>") - LevelKeyStartIndices[index]);
            else if (index == UserLevelCount - 1) // If the level is the last level in the list
                level = levelKey + DecryptedLevelData.Substring(LevelKeyStartIndices[index], Math.Max(DecryptedLevelData.Find("</d></d></d>") + 8, DecryptedLevelData.Find("<d /></d></d>") + 9) - LevelKeyStartIndices[index]);
            return level;
        }
        public static string GetLevelDescription(int index)
        {
            try { return Encoding.UTF8.GetString(FromBase64String(GetKeyValue(index, 3, "s"))); }
            catch { return ""; }
        }
        public static string GetLevelKeyEntry(string levelString, string name, string desc)
        {
            int objectCount = -1;
            for (int i = 0; i < levelString.Length; i++)
                if (levelString[i] == ';')
                    objectCount++;
            return $"<d><k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(desc.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(desc))}</s>" : "")}<k>k46</k><i>0</i><k>k48</k><i>{objectCount}</i><k>k5</k><s>{UserName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>1</i><k>k50</k><i>33</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r><k>kI6</k><d><k>0</k><s>0</s><k>1</k><s>0</s><k>2</k><s>0</s><k>3</k><s>0</s><k>4</k><s>0</s><k>5</k><s>0</s><k>6</k><s>0</s><k>7</k><s>0</s><k>8</k><s>0</s><k>9</k><s>0</s><k>10</k><s>0</s><k>11</k><s>0</s><k>12</k><s>0</s></d></d>";
        }
        public static string GetLevelLengthString(int index)
        {
            string levelLength;
            int length = GetLevelLength(index);
            switch (length)
            {
                case 0: levelLength = "Tiny"; break;
                case 1: levelLength = "Small"; break;
                case 2: levelLength = "Medium"; break;
                case 3: levelLength = "Long"; break;
                case 4: levelLength = "XL"; break;
                default: levelLength = "Tiny"; break;
            }
            return levelLength;
        }
        public static string GetLevelString(int index)
        {
            try { return GetKeyValue(index, 4, "s"); }
            catch (KeyNotFoundException) { return ""; }
        }
        public static string GetLevelString(string level)
        {
            try { return GetKeyValue(level, 4, "s"); }
            catch (KeyNotFoundException) { return ""; }
        }
        public static string GetObjectString(string ls)
        {
            try { return ls.Split(';').RemoveAt(0).Combine(";"); }
            catch { return ""; }
        }
        public static string GetOfficialSongID(int index)
        {
            return GetKeyValue(index, 8, "i");
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
        public static string[] GetGuidelineStrings()
        {
            string[] gs = new string[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
                if (UserLevels[i].LevelString != "" && UserLevels[i].LevelString != null)
                    gs[i] = GetGuidelineString(i);
            return gs;
        }
        public static string[] GetLevelDescriptions()
        {
            string[] descs = new string[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
                descs[i] = GetLevelDescription(i);
            return descs;
        }
        public static string[] GetLevelLengthStrings()
        {
            int[] lengths = GetLevelLengths();
            string[] levelLengthStrings = new string[lengths.Length];
            for (int i = 0; i < lengths.Length; i++)
                levelLengthStrings[i] = LevelLengthNames[lengths[i]];
            return levelLengthStrings;
        }
        public static string[] GetLevelNames()
        {
            return GetKeyValues(2, "s");
        }
        public static string[] GetLevelNamesWithRevisions()
        {
            try
            {
                string[] lvlNames = GetLevelNames();
                int[] lvlRevs = GetLevelRevisions();
                string[] final = new string[lvlNames.Length];
                for (int i = 0; i < lvlNames.Length; i++)
                    final[i] = $"{lvlNames[i]}{(lvlRevs[i] > 0 ? $" (Rev. {lvlRevs[i]})" : "")}";
                return final;
            }
            catch { return null; }
        }
        public static string[] GetLevelStrings()
        {
            string[] ls = new string[UserLevelCount];
            for (int i = 0; i < UserLevelCount; i++)
            {
                ls[i] = "";
                try
                {
                    TryDecryptLevelString(i, out ls[i]);
                }
                catch (ArgumentException) { }
                catch (KeyNotFoundException) { }
                catch (FormatException) { }
                catch (Exception e)
                {
                    string type = e.GetType().ToString();
                    throw new DataException("An unknown error has occured. Contact us immediately about this occurence and provide us with details. Your level data file may be asked for debugging and examination.");
                }
            }
            return ls;
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
        public static void AddKey(int index, int key, string value, string valueType)
        {
            string keyToInsert = $"<k>k{key}</k><{valueType}>{value}</{valueType}>";
            UserLevels[index].RawLevel = UserLevels[index].RawLevel.Insert(22, keyToInsert);
            DecryptedLevelData = DecryptedLevelData.Insert(DecryptedLevelData.Find("<k>kCEK</k><i>4</i>", index + 1) + 19, keyToInsert);
            for (int i = index + 1; i < UserLevelCount; i++)
                LevelKeyStartIndices[i] += keyToInsert.Length;
        }
        public static void AddLevelStringParameter(string newLS, int levelIndex)
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
        public static void CloneLevel(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "The index of the level cannot be a negative number.");
            if (index >= UserLevelCount)
                throw new ArgumentOutOfRangeException("index", "The argument that is parsed is out of range.");
            CloneAllLevelInfo(0, index);
            UpdateLevelData();
        }
        public static void CloneLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] >= 0)
                    if (indices[i] < UserLevelCount)
                        CloneAllLevelInfo(i, indices[i] + i);
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        public static void CreateLevel()
        {
            int n = 0;
            List<int> nums = new List<int>();
            for (int i = 0; i < UserLevelCount; i++) // Add the unnamed numbers to the list
                if (UserLevels[i].Name.Contains("Unnamed ") && UserLevels[i].Name.Split(' ').Length == 2 && int.TryParse(UserLevels[i].Name.Split(' ')[1], out int _)) // Add the number to the list if the 
                    nums.Add(UserLevels[i].Name.GetLastNumber());
            nums.Sort();
            while (n < nums.Count && n == nums[n])
                n++;
            CreateLevel($"Unnamed {n}", "");
        } 
        public static void CreateLevel(string name)
        {
            CreateLevel(name, "");
        }
        public static void CreateLevel(string name, string desc)
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
            GetLevelInfo(0);
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        public static void CreateLevel(string name, string desc, string levelString)
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
        public static void CreateLevels(int numberOfLevels)
        {
            // Useless ATM
        }
        public static void CreateLevels(int numberOfLevels, string[] names)
        {
            // Useless ATM
        }
        public static void CreateLevels(int numberOfLevels, string[] names, string[] descs)
        {
            // Useless ATM
        }
        public static void DeleteAllLevels()
        {
            DecryptedLevelData = DefaultLevelData; // Set the level data to the default
            UpdateLevelData(); // Write the new data

            // Delete all the level info from the prorgam's memory
            UserLevels.Clear();
            LevelKeyStartIndices.Clear();
        }
        public static void DeleteLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates();
            indices = indices.Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                RemoveAllLevelInfoAt(indices[i]);
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        public static void ExportLevel(int index, string folderPath)
        {
            File.WriteAllText(folderPath + UserLevels[index].LevelNameWithRevision, GetLevel(index));
        }
        public static void ExportLevels(int[] indices, string folderPath)
        {
            for (int i = 0; i < indices.Length; i++)
                File.WriteAllText($@"{folderPath}\{UserLevels[indices[i]].LevelNameWithRevision}.txt", GetLevel(indices[i]));
        }
        public static void GetCustomObjects()
        {
            CustomObjects = new List<CustomLevelObject>();
            int startIndex = DecryptedGamesave.Find("<k>customObjectDict</k><d>") + 26;
            if (startIndex < 26)
                return;

            int endIndex = DecryptedGamesave.Find("</d>", startIndex, DecryptedGamesave.Length);
            int currentIndex = startIndex;
            while ((currentIndex = DecryptedGamesave.Find("</k><s>", currentIndex, endIndex) + 7) > 6)
                CustomObjects.Add(new CustomLevelObject(GetObjects(DecryptedGamesave.Substring(currentIndex, DecryptedGamesave.Find("</s>", currentIndex, DecryptedGamesave.Length) - currentIndex))));
        }
        public static void GetKeyIndices()
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
        public static void GetLevels()
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
        /// <summary>Gets the level info for all levels in the gamesave.</summary>
        public static void GetAllLevelInfo()
        {
            for (int i = 0; i < UserLevelCount; i++)
                try
                {
                    GetLevelInfo(i);
                }
                catch (Exception ex)
                {
                    throw new DataException($"Unable to properly analyze the data of the level at index {i}.", ex);
                }
        }
        /// <summary>Gets the level info for a level in the specified index in the gamesave.</summary>
        public static void GetLevelInfo(int index)
        {
            if (index >= UserLevelCount)
                throw new ArgumentOutOfRangeException("The level's index may not be greater or equal to the count of the levels.");

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
            int finalIndex = (index < UserLevelCount - 1 ? LevelKeyStartIndices[index + 1] : DecryptedLevelData.Length - 1) - 10 - index / 10;
            for (int i = LevelKeyStartIndices[index]; i < finalIndex; )
            {
                parameterIDStartIndex = DecryptedLevelData.Find(startKeyString, i, finalIndex) + startKeyString.Length;
                if (parameterIDStartIndex <= startKeyString.Length)
                    break; 
                parameterIDEndIndex = DecryptedLevelData.Find(endKeyString, parameterIDStartIndex, finalIndex);
                parameterValueTypeStartIndex = parameterIDEndIndex + endKeyString.Length;
                parameterValueTypeEndIndex = DecryptedLevelData.Find(">", parameterValueTypeStartIndex, finalIndex);
                parameterValueType = DecryptedLevelData.Substring(parameterValueTypeStartIndex, parameterValueTypeEndIndex - parameterValueTypeStartIndex);
                parameterValueStartIndex = parameterValueTypeEndIndex + 1;
                parameterValueEndIndex = parameterValueType[parameterValueType.Length - 1] != '/' ? DecryptedLevelData.Find($"</{parameterValueType}>", parameterValueStartIndex, finalIndex) : parameterValueStartIndex;
                parameterValue = DecryptedLevelData.Substring(parameterValueStartIndex, parameterValueEndIndex - parameterValueStartIndex);
                string s = DecryptedLevelData.Substring(parameterIDStartIndex, parameterIDEndIndex - parameterIDStartIndex);
                if (s != "CEK" && !s.StartsWith("I"))
                {
                    parameterID = ToInt32(s);
                    switch (parameterID)
                    {
                        case 1: // Level ID
                            UserLevels[index].LevelID = ToInt32(parameterValue);
                            break;
                        case 2: // Level Name
                            UserLevels[index].Name = parameterValue;
                            break;
                        case 3: // Level Description
                            UserLevels[index].Description = Encoding.UTF8.GetString(Base64Decrypt(parameterValue));
                            break;
                        case 4: // Level String
                            UserLevels[index].LevelString = parameterValue;
                            break;
                        case 8: // Official Song ID
                            UserLevels[index].OfficialSongID = ToInt32(parameterValue);
                            break;
                        case 14: // Level Verified Status
                            UserLevels[index].VerifiedStatus = parameterValueType == "t /"; // Well that's how it's implemented ¯\_(ツ)_/¯
                            break;
                        case 15: // Level Uploaded Status
                            UserLevels[index].UploadedStatus = parameterValueType == "t /";
                            break;
                        case 16: // Level Version
                            UserLevels[index].Version = ToInt32(parameterValue);
                            break;
                        case 18: // Level Attempts
                            UserLevels[index].Attempts = ToInt32(parameterValue);
                            break;
                        case 23: // Level Length
                            UserLevels[index].Length = ToInt32(parameterValue);
                            break;
                        case 45: // Custom Song ID
                            UserLevels[index].CustomSongID = ToInt32(parameterValue);
                            break;
                        case 46: // Level Revision
                            UserLevels[index].Revision = ToInt32(parameterValue);
                            break;
                        case 80: // Time Spent
                            UserLevels[index].BuildTime = ToInt32(parameterValue);
                            break;
                        case 84: // Level Folder
                            UserLevels[index].Folder = ToInt32(parameterValue);
                            break;
                        default: // Not something we care about
                            break;
                    }
                }
                i = parameterValueEndIndex;
            }
            UserLevels[index].ObjectCounts = new Dictionary<int, int>();

            if (UserLevels[index].DecryptedLevelString != null) // If there is a level string
            {
                UserLevels[index].GuidelineString = GetGuidelineString(index);
                UserLevels[index].LevelObjects = GetObjects(GetObjectString(UserLevels[index].DecryptedLevelString));
            }
        }
        
        public static void ImportLevel(string level)
        {
            for (int i = UserLevelCount - 1; i >= 0; i--) // Increase the level indices of all the other levels to insert the cloned level at the start
                DecryptedLevelData = DecryptedLevelData.Replace("<k>k_" + i.ToString() + "</k>", "<k>k_" + (i + 1).ToString() + "</k>");
            level = RemoveLevelIndexKey(level); // Remove the index key of the level
            DecryptedLevelData = DecryptedLevelData.Insert(LevelKeyStartIndices[0] - 10, "<k>k_0</k>" + level); // Insert the new level
            int clonedLevelLength = level.Length + 10; // The length of the inserted level
            LevelKeyStartIndices = LevelKeyStartIndices.InsertAtStart(LevelKeyStartIndices[0]); // Add the new key start position in the array
            for (int i = 1; i < LevelKeyStartIndices.Count; i++)
                LevelKeyStartIndices[i] += clonedLevelLength; // Increase the other key indices by the length of the cloned level
            // Insert the imported level's parameters
            UserLevels = UserLevels.InsertAtStart(new Level());
            UserLevels[0].RawLevel = level;
            GetLevelInfo(0);
            UpdateLevelData(); // Write the new data
        }
        public static void ImportLevelFromFile(string levelPath)
        {
            ImportLevel(File.ReadAllText(levelPath));
        }
        public static void ImportLevels(string[] lvls)
        {
            for (int i = 0; i < lvls.Length; i++)
            {
                lvls[i] = RemoveLevelIndexKey(lvls[i]); // Remove the index key of the level
                UserLevels.InsertAtStart(new Level(lvls[i]));
                GetLevelInfo(0);
            }
            GetKeyIndices();
            UpdateMemoryLevelData();
            UpdateLevelData(); // Write the new data
        }
        public static void ImportLevelsFromFiles(string[] levelPaths)
        {
            string[] levels = new string[levelPaths.Length];
            for (int i = 0; i < levelPaths.Length; i++)
                levels[i] = File.ReadAllText(levelPaths[i]);
            ImportLevels(levels);
        }
        public static void MoveLevelsDown(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort().RemoveElementsMatchingIndicesFromEnd(UserLevelCount);
            for (int i = indices.Length - 1; i >= 0; i--)
                if (indices[i] < UserLevelCount - 1) // If the level can be moved further down
                    SwapAllLevelInfo(indices[i], indices[i] + 1);
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData(); // Write the new data
        }
        public static void MoveLevelsToBottom(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                MoveAllLevelInfo(indices[i], UserLevelCount - indices.Length + i + 1); // +1?
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData();
        }
        public static void MoveLevelsToTop(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = 0; i < indices.Length; i++)
                MoveAllLevelInfo(indices[i], i);
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData();
        }
        public static void MoveLevelsUp(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort().RemoveElementsMatchingIndices();
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] >= i) // If the level can be moved further up
                    SwapAllLevelInfo(indices[i], indices[i] - 1);
            UpdateMemoryLevelData(); // Rebuild the level data
            UpdateLevelData(); // Write the new data
        }
        public static void SetGuidelineString(string newGuidelines, int levelIndex)
        {
            string oldGS = UserLevels[levelIndex].GuidelineString;
            int gsStartIndex = GetGuidelineStringStartIndex(levelIndex);
            string newLS;
            if (oldGS.Length == 0)
                newLS = UserLevels[levelIndex].LevelString.Insert(gsStartIndex, newGuidelines);
            else
                newLS = UserLevels[levelIndex].LevelString.Replace(oldGS, newGuidelines);
            UserLevels[levelIndex].GuidelineString = newGuidelines;
            SetLevelString(newLS, levelIndex);
        }
        public static void SetGuidelineStrings(string[] newGuidelineStrings, int[] levelIndices)
        {
            string[] oldGS = new string[levelIndices.Length];
            string[] newLS = new string[levelIndices.Length];
            int[] gsStartIndices = new int[levelIndices.Length];
            for (int i = 0; i < levelIndices.Length; i++)
            {
                oldGS[i] = UserLevels[levelIndices[i]].GuidelineString;
                gsStartIndices[i] = GetGuidelineStringStartIndex(levelIndices[i]);
            }
            for (int i = 0; i < levelIndices.Length; i++)
            {
                if (oldGS[i].Length == 0)
                    newLS[i] = UserLevels[levelIndices[i]].LevelString.Insert(gsStartIndices[i], newGuidelineStrings[i]);
                else
                    newLS[i] = UserLevels[levelIndices[i]].LevelString.Replace(oldGS[i], newGuidelineStrings[i]);
                UserLevels[levelIndices[i]].GuidelineString = newGuidelineStrings[i];
            }
            SetLevelStrings(newLS, levelIndices);
        }
        public static void SetGuidelineStrings(string newGuidelines, int[] levelIndices)
        {
            string[] oldGS = new string[levelIndices.Length];
            string[] newLS = new string[levelIndices.Length];
            int[] gsStartIndices = new int[levelIndices.Length];
            for (int i = 0; i < levelIndices.Length; i++)
            {
                oldGS[i] = UserLevels[levelIndices[i]].GuidelineString;
                gsStartIndices[i] = GetGuidelineStringStartIndex(levelIndices[i]);
            }
            for (int i = 0; i < levelIndices.Length; i++)
            {
                if (oldGS[i].Length == 0)
                    newLS[i] = UserLevels[levelIndices[i]].LevelString.Insert(gsStartIndices[i], newGuidelines);
                else
                    newLS[i] = UserLevels[levelIndices[i]].LevelString.Replace(oldGS[i], newGuidelines);
                UserLevels[levelIndices[i]].GuidelineString = newGuidelines;
            }
            SetLevelStrings(newLS, levelIndices);
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
        public static void SetLevelString(string newLS, int levelIndex)
        {
            string oldLS = GetLevelString(levelIndex);
            UserLevels[levelIndex].LevelString = newLS; // Set the new level strings in the memory

            string newLevel = UserLevels[levelIndex].RawLevel.Replace($"<k>k4</k><s>{GetLevelString(levelIndex)}</s>", $"<k>k4</k><s>{newLS}</s>");
            SetLevel(newLevel, levelIndex);
        }
        public static void SetLevelStrings(string[] newLS, int[] levelIndices)
        {
            string[] newLevels = new string[levelIndices.Length];
            for (int i = 0; i < levelIndices.Length; i++)
            {
                string oldLS = GetLevelString(levelIndices[i]);
                UserLevels[levelIndices[i]].LevelString = newLS[i]; // Set the new level strings in the memory
                newLevels[i] = UserLevels[levelIndices[i]].RawLevel.Replace($"<k>k4</k><s>{GetLevelString(levelIndices[i])}</s>", $"<k>k4</k><s>{newLS[i]}</s>");
            }
            SetLevels(newLevels, levelIndices);
        }
        public static void SwapLevels(int levelIndexA, int levelIndexB)
        {
            SwapAllLevelInfo(levelIndexA, levelIndexB);
            UpdateMemoryLevelData();
            UpdateLevelData();
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
        public static void UpdateLevelData()
        {
            File.WriteAllText(GDLocalLevels, DecryptedLevelData); // Write the level data
        }
        public static void UpdateLevelDataWithEncryption()
        {
            string finalizedLevelData = DecryptedLevelData;
            // Code to encrypt the fucking level data
            File.WriteAllText(GDLocalLevels, finalizedLevelData); // Write the encrypted level data
        }
        public static void UpdateMemoryLevelData()
        {
            LevelKeyStartIndices = new List<int>();
            StringBuilder lvlDat = new StringBuilder(LevelDataStart);
            for (int i = 0; i < UserLevelCount; i++)
            {
                lvlDat = lvlDat.Append("<k>k_{i}</k>");
                LevelKeyStartIndices.Add(lvlDat.Length);
                lvlDat = lvlDat.Append(UserLevels[i].RawLevel);
            }
            lvlDat = lvlDat.Append(LevelDataEnd);
            DecryptedLevelData = lvlDat.ToString();
        }

        public static void CloneAllLevelInfo(int a, int b)
        {
            UserLevels.Insert(a, UserLevels[b]);
        }
        public static void MoveAllLevelInfo(int a, int b)
        {
            UserLevels.MoveElement(a, b);
        }
        public static void MoveAllLevelInfoToStart(int a)
        {
            UserLevels.MoveElementToStart(a);
        }
        public static void MoveAllLevelInfoToEnd(int a)
        {
            UserLevels.MoveElementToEnd(a);
        }
        public static void RemoveAllLevelInfoAt(int a)
        {
            UserLevels.RemoveAt(a);
        }
        public static void SwapAllLevelInfo(int a, int b)
        {
            UserLevels = UserLevels.Swap(a, b);
        }

        public static string GetData(string path)
        {
            StreamReader sr = new StreamReader(path);
            string readfile = sr.ReadToEnd();
            sr.Close();
            return readfile;
        }
        public static string GetPlayerName()
        {
            int playerNameStartIndex = DecryptedGamesave.FindFromEnd("<k>playerName</k><s>") + 20;
            int playerNameEndIndex = DecryptedGamesave.FindFromEnd("</s><k>playerUserID</k>");
            int playerNameLength = playerNameEndIndex - playerNameStartIndex;
            return DecryptedGamesave.Substring(playerNameStartIndex, playerNameLength);
        }
        public static string GetUserID()
        {
            int userIDStartIndex = DecryptedGamesave.FindFromEnd("<k>playerUserID</k><i>") + 22;
            int userIDEndIndex = DecryptedGamesave.FindFromEnd("</i><k>playerFrame</k>");
            int userIDLength = userIDEndIndex - userIDStartIndex;
            return DecryptedGamesave.Substring(userIDStartIndex, userIDLength);
        }
        public static string GetAccountID()
        {
            int accountIDStartIndex = DecryptedGamesave.FindFromEnd("<k>GJA_003</k><i>") + 17;
            int accountIDEndIndex = DecryptedGamesave.FindFromEnd("</i><k>KBM_001</k>");
            int accountIDLength = accountIDEndIndex - accountIDStartIndex;
            if (accountIDLength > 0)
                return DecryptedGamesave.Substring(accountIDStartIndex, accountIDLength);
            else
                return "0";
        }
        public static List<string> GetFolderNames()
        {
            List<string> names = new List<string>();
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
                    while (folderIndex >= names.Count)
                        names.Add("");
                    names[folderIndex] = DecryptedGamesave.Substring(folderNameStartIndex, folderNameEndIndex - folderNameStartIndex);
                }
            }
            return names;
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