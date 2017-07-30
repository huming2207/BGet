using System;

namespace BgetWpf.Controller
{
    public class InitialConfig
    {
        public static void InitConfig()
        {
            // If user requires a config reset, then do it.
            if (Properties.Settings.Default.ResetConfig)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.ResetConfig = false;
            }

            // If user download path is empty (can't be less than 3 chars in Windows, e.g. "C:\"),
            // set to %HOMEDRIVE%%HOMEPATH%\Downloads
            if (Properties.Settings.Default.DownloadPath.Length < 3)
            {
                Properties.Settings.Default.DownloadPath =
                    Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + @"\Downloads";
                Properties.Settings.Default.Save();
            }

        }
    }
}
