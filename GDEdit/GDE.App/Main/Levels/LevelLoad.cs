using GDEdit.Application;
using static GDEdit.Utilities.Functions.GeometryDash.Gamesave;

namespace GDE.App.Main.Levels
{
    public static class LevelLoad
    {
        public static void LoadLevel(int index)
        {
            TryDecryptLevelData(out Database.DecryptedLevelData);
            GetKeyIndices();
            GetLevels();
            Database.UserLevels[index].LevelString = GetLevelString(index);
            TryDecryptLevelString(index, out Database.UserLevels[index].DecryptedLevelString);
        }
    }
}
