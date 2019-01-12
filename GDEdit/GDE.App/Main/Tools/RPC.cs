using System;
using DiscordRPC;
using osu.Framework.Logging;

//533431726463123458
namespace GDE.App.Main.Tools
{
    public class RPC
    {
        public DiscordRpcClient client = new DiscordRpcClient("533431726463123458"); // LEAVE THIS BLANK WHEN COMMITING

        #region UpdateRPC
        /// <summary>
        /// Initializes and changes the Rich Presence in discord
        /// </summary>
        /// <param name="Status">The Status text that will appear in discord</param>
        /// <param name="Details">The Details text that will appear in discord</param>
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

        /// <summary>
        /// Initializes and changes the Rich Presence in discord
        /// </summary>
        /// <param name="Status">The Status text that will appear in discord</param>
        /// <param name="Details">The Details text that will appear in discord</param>
        /// <param name="Assets">The Assets images that will appear in discord</param>
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

        /// <summary>
        /// Initializes and changes the Rich Presence in discord
        /// </summary>
        /// <param name="Status">The Status text that will appear in discord</param>
        /// <param name="Details">The Details text that will appear in discord</param>
        /// <param name="Assets">The Assets images that will appear in discord</param>
        /// <param name="timestamp">The Timestamp time that will appear in discord</param>
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
