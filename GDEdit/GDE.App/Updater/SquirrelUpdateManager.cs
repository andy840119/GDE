using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using Squirrel;
using LogLevel = Splat.LogLevel;

namespace GDE.App.Updater
{
    public class SquirrelUpdateManager : Component
    {
        private UpdateManager updateManager;
        
        public void PrepareUpdate()
        {
            UpdateManager.RestartAppWhenExited().Wait();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Schedule(() => Task.Run(() => checkForUpdateAsync()));
            Logger.Log("Called \"checkForUpdateAsync()\"");
        }

        private async void checkForUpdateAsync(bool useDeltaPatching = true)
        {
            bool scheduleRetry = true;
            try
            {
                if (updateManager == null) updateManager = await UpdateManager.GitHubUpdateManager("https://github.com/gd-edit/GDE", "GDEdit", null, null, true);

                var info = await updateManager.CheckForUpdate(!useDeltaPatching);
                if (info.ReleasesToApply.Count == 0)
                    return;

                Logger.Log("Checking for updates");

                try
                {
                    await updateManager.DownloadReleases(info.ReleasesToApply);

                    await updateManager.ApplyReleases(info);
                }
                catch (Exception e)
                {
                    if (useDeltaPatching)
                    {
                        Logger.Error(e, @"delta patching failed!");

                        checkForUpdateAsync(false);
                        scheduleRetry = false;
                    }
                    else
                    {
                        Logger.Error(e, @"update failed!");
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                if (scheduleRetry)
                {
                    Scheduler.AddDelayed(() => checkForUpdateAsync(), 6000 * 30);
                }
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            updateManager?.Dispose();
        }
    }
}
