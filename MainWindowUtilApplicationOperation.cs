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

        public void ReloadLastApplicationSize()
        {
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Top = Properties.Settings.Default.WindowTop;
            this.Width = Properties.Settings.Default.WindowWidth;
            this.Height = Properties.Settings.Default.WindowHeight;
            WindowState savedState = (WindowState)Properties.Settings.Default.WindowState;
            this.WindowState = savedState;
        }

        public void SaveApplicationSize()
        {
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.WindowWidth = this.Width;
            Properties.Settings.Default.WindowHeight = this.Height;
            Properties.Settings.Default.WindowState = (int)this.WindowState;
            Properties.Settings.Default.Save();
        }

        public void RestartApplication()
        {
            SaveApplicationSize();
            RestartApplicationProcess();
        }

        private void RestartApplicationProcess()
        {
            string appPath = "";
            var module = Process.GetCurrentProcess().MainModule;
            if (module != null)
            {
                appPath = module.FileName;
            }

            if (!String.IsNullOrEmpty(appPath))
            {
                Process.Start(appPath);
                Application.Current.Shutdown();
            }
        }

        private void ApplySilenceStart()
        {
            if(Properties.Settings.Default.IsStartWithMinimize)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

    }
}
