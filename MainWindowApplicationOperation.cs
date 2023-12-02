using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NowPlayingMonitor_WPF
{
    public partial class MainWindow : Window
    {


        private void RestartApplication()
        {
            string appPath = "";
            var module = Process.GetCurrentProcess().MainModule;
            if(module != null)
            {
                appPath = module.FileName;
            }

            if(!String.IsNullOrEmpty(appPath))
            {
                Process.Start(appPath);
                Application.Current.Shutdown();
            }
            
        }

    }
}
