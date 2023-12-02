using NowPlayingMonitor_WPF.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NowPlayingMonitor_WPF
{
    public partial class MainWindow : Window
    {
        public void SaveWindowSettings()
        {
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.WindowWidth = this.Width;
            Properties.Settings.Default.WindowHeight = this.Height;
            Properties.Settings.Default.WindowState = (int)this.WindowState;
            Properties.Settings.Default.Save();
        }
        public void LoadWindowSettings()
        {
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Top = Properties.Settings.Default.WindowTop;
            this.Width = Properties.Settings.Default.WindowWidth;
            this.Height = Properties.Settings.Default.WindowHeight;
            this.WindowState = (WindowState)Properties.Settings.Default.WindowState;

            if (Properties.Settings.Default.IsStartWithMinimize)
            {
                this.WindowState = WindowState.Minimized;
                this.Hide();
            }


        }

        public void SaveAppSettings()
        {
            Settings.Default.IsStartWithMinimize = CheckBoxSilentStart.IsChecked ?? false;
            Settings.Default.IsMinimizeToTrayWhenClosed = CheckBoxAlwaysMinimizeToTray.IsChecked ?? false;
            Settings.Default.Save();
        }

        public void LoadAppSettings()
        {
            CheckBoxSilentStart.IsChecked = Settings.Default.IsStartWithMinimize;
            CheckBoxAlwaysMinimizeToTray.IsChecked = Settings.Default.IsMinimizeToTrayWhenClosed;
        }

        public void RestartApplication()
        {
            SaveWindowSettings();
            SaveAppSettings();
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

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) this.Hide();

            base.OnStateChanged(e);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if(Settings.Default.IsMinimizeToTrayWhenClosed)
            {
                e.Cancel = true;
                this.Hide();
            }
            

            base.OnClosing(e);
        }

    }
}
