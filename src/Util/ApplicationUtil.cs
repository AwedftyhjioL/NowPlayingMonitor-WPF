using NowPlayingMonitor.Properties;
using NowPlayingMonitor.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NowPlayingMonitor
{
    public class ApplicationUtil
    {

        public static void LoadDefaultSetting()
        {
            if (String.IsNullOrEmpty(Properties.Settings.Default.WorkDirectory))
            {
                Settings.Default.WorkDirectory = PathUtil.GetPortalWorkDirectory();
            }

            if (String.IsNullOrEmpty(Settings.Default.ErrorLogFilePath))
            {
                Settings.Default.ErrorLogFilePath = PathUtil.GetPortalLogFilePath();
            }

        }

        public static void RestartApplicationProcess()
        {
            string appPath = Process.GetCurrentProcess().MainModule?.FileName ?? "";

            if (!String.IsNullOrEmpty(appPath))
            {
                Settings.Default.IsSkipSilentStartOnce = true;
                Settings.Default.IsMakeTopMostOnce = true;
                Process.Start(appPath);
                System.Windows.Application.Current.Shutdown();
            }
        }


        public static void ExitApplicationProcess()
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
