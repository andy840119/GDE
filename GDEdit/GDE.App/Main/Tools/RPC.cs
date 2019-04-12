using System;
using DiscordRPC;
using osu.Framework.Logging;

namespace GDE.App.Main.Tools
{
    /// <summary>This class will represnt Discord's RPC Functions (more to be added soon though).</summary>
    public static class RPC
    {
        /// <summary>Represents the application from Discord's dev portal.</summary>
        public static DiscordRpcClient client = new DiscordRpcClient("533431726463123458");

        #region Update RPC
        /// <summary>Initializes and changes the Rich Presence in Discord.</summary>
        /// <param name="status">The Status text that will appear in Discord.</param>
        /// <param name="details">The Details text that will appear in Discord.</param>
        public static void updatePresence(string status, string details)
        {
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = status,
                Details = details
            });

            Logger.Error(new Exception().InnerException, $"Changed RPC to {status}", LoggingTarget.Information);

            client.Invoke();
        }

        /// <summary>Initializes and changes the Rich Presence in Discord.</summary>
        /// <param name="status">The Status text that will appear in Discord.</param>
        /// <param name="details">The Details text that will appear in Discord.</param>
        /// <param name="assets">The Assets images that will appear in Discord.</param>
        public static void updatePresence(string status, string details, Assets assets)
        {
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = status,
                Details = details,
                Assets = assets
            });

            Logger.Error(new Exception().InnerException, $"Changed RPC to {status}", LoggingTarget.Information);

            client.Invoke();
        }

        /// <summary>Initializes and changes the Rich Presence in Discord.</summary>
        /// <param name="status">The Status text that will appear in Discord.</param>
        /// <param name="details">The Details text that will appear in Discord.</param>
        /// <param name="assets">The Assets images that will appear in Discord.</param>
        /// <param name="timestamp">The Timestamp time that will appear in Discord.</param>
        public static void updatePresence(string status, string details, Assets assets, Timestamps timestamp)
        {
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = status,
                Details = details,
                Timestamps = timestamp,
                Assets = assets
            });

            Logger.Error(new Exception().InnerException, $"Changed RPC to {status}", LoggingTarget.Information);

            client.Invoke();
        }
        #endregion
    }
}
