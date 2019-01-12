using System;
using DiscordRPC;
using osu.Framework.Logging;

namespace GDE.App.Main.Tools
{
    /// <summary>This class will represnt Discord's RPC Functions (more to be added soon though)</summary>
    public class RPC
    {
        /// <summary>Represents the application from Discord's dev portal</summary>
        public DiscordRpcClient client = new DiscordRpcClient(""); // LEAVE THIS BLANK WHEN COMMITING

        #region Update RPC
        /// <summary>Initializes and changes the Rich Presence in Discord.</summary>
        /// <param name="Status">The Status text that will appear in Discord.</param>
        /// <param name="Details">The Details text that will appear in Discord.</param>
        public void UpdatePresence(string Status, string Details)
        {
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = Status,
                Details = Details
            });

            Logger.Error(new Exception().InnerException, $"Changed RPC to {Status}", LoggingTarget.Information);

            client.Invoke();
        }

        /// <summary>Initializes and changes the Rich Presence in Discord.</summary>
        /// <param name="Status">The Status text that will appear in Discord.</param>
        /// <param name="Details">The Details text that will appear in Discord.</param>
        /// <param name="Assets">The Assets images that will appear in Discord.</param>
        public void UpdatePresence(string Status, string Details, Assets Assets)
        {
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = Status,
                Details = Details,
                Assets = Assets
            });

            Logger.Error(new Exception().InnerException, $"Changed RPC to {Status}", LoggingTarget.Information);

            client.Invoke();
        }

        /// <summary>Initializes and changes the Rich Presence in Discord.</summary>
        /// <param name="Status">The Status text that will appear in Discord.</param>
        /// <param name="Details">The Details text that will appear in Discord.</param>
        /// <param name="Assets">The Assets images that will appear in Discord.</param>
        /// <param name="timestamp">The Timestamp time that will appear in Discord.</param>
        public void UpdatePresence(string Status, string Details, Assets Assets, Timestamps timestamp)
        {
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = Status,
                Details = Details,
                Timestamps = timestamp,
                Assets = Assets
            });

            Logger.Error(new Exception().InnerException, $"Changed RPC to {Status}", LoggingTarget.Information);

            client.Invoke();
        }
        #endregion

        public void Update()
        {
            var timer = new System.Timers.Timer(1);
            timer.Elapsed += (sender, args) => { client.Invoke(); };
            timer.Start();
        }
    }
}
