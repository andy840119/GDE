using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Objects.GeometryDash;

namespace GDEdit.Application
{
    public static class Database
    {
        public static readonly string GDLocalData = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\gdEditDevTest";
        public static readonly string GDGameManager = $@"{GDLocalData}\CCGameManager.dat";
        public static readonly string GDLocalLevels = $@"{GDLocalData}\CCLocalLevels.dat";

        public static List<Level> UserLevels;
        public static List<string> FolderNames;
        public static List<int> LevelKeyStartIndices;
        public static string UserName = "";
        public static string DecryptedGamesave = "";
        public static string DecryptedLevelData = "";
        public static int UserLevelCount => UserLevels.Count;
        public static List<CustomLevelObject> CustomObjects;
    }
}
