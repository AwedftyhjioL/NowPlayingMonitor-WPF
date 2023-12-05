using NowPlayingMonitor_WPF.Properties;
using NowPlayingMonitor_WPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NowPlayingMonitor_WPF
{
    public partial class MainWindow : Window
    {
        protected void StartUp()
        {
            LoadCultureInfo();

            InitializeComponent();

            LoadDefaultSetting();

            RestoreLastUIState();

            ApplyExtraEvent();

            ApplyExtraUiSetUp();


            _viewModel.StartBackgroundTask();
        }

        protected override void OnClosed(EventArgs e)
        {
            SaveWindowState();
            SaveAppState();
            base.OnClosed(e);
        }

        

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (Settings.Default.IsMinimizeToTrayWhenClosed)
            {
                e.Cancel = true;
                this.Hide();
            }


            base.OnClosing(e);
        }

        public void RestoreLastUIState()
        {
            RestoreWindowState();
            RestoreAppState();
        }

        protected void ApplyExtraEvent()
        {
            MyNotifyIcon.TrayMouseDoubleClick += MyNotifyIcon_TrayMouseDoubleClick;
        }

        protected void ApplyExtraUiSetUp()
        {
            ApplySilentStartSettings(); 
            ApplyTopMostOnce();
            ApplayPerferedTabOnStartUp();

            _viewModel.UpdateProcessInfos();

        }

        public void SaveWindowState()
        {
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.WindowWidth = this.Width;
            Properties.Settings.Default.WindowHeight = this.Height;
            Properties.Settings.Default.WindowState = (int)this.WindowState;
            Properties.Settings.Default.Save();
        }
        public void RestoreWindowState()
        {
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Top = Properties.Settings.Default.WindowTop;
            this.Width = Properties.Settings.Default.WindowWidth;
            this.Height = Properties.Settings.Default.WindowHeight;
            this.WindowState = (WindowState)Properties.Settings.Default.WindowState;
            if(this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;
        }

        public void ApplySilentStartSettings()
        {
            if (Settings.Default.IsSkipSilentStartOnce)
            {
                Settings.Default.IsSkipSilentStartOnce = false;
                return;
            }

            if (Settings.Default.IsStartWithMinimize)
            {
                this.WindowState = WindowState.Minimized;
                this.Hide();
            }
        }

        public void ApplyTopMostOnce()
        {
            if (Settings.Default.IsMakeTopMostOnce)
            {
                this.Topmost = true;
                this.Show();
                this.Topmost = false;
                Settings.Default.IsMakeTopMostOnce = false;
            }
        }

        public void ApplayPerferedTabOnStartUp()
        {
            if (Settings.Default.PerferedTabIndexOnStartUp >= 0)
                TabControlMain.SelectedIndex = Settings.Default.PerferedTabIndexOnStartUp;

        }

        public void SaveAppState()
        {
            Settings.Default.IsStartWithMinimize = CheckBoxSilentStart.IsChecked ?? false;
            Settings.Default.IsMinimizeToTrayWhenClosed = CheckBoxAlwaysMinimizeToTray.IsChecked ?? false;
            Settings.Default.WorkDirectory = TextBoxWorkDirectory.Text;
            Settings.Default.RefreshFrequency = NumericUpDownControlRefreshFrequency.Value ?? 500;
            Settings.Default.LastActivedTabIndex = TabControlMain.SelectedIndex;
            Settings.Default.Save();
        }

        public void RestoreAppState()
        {
            CheckBoxSilentStart.IsChecked = Settings.Default.IsStartWithMinimize;
            CheckBoxAlwaysMinimizeToTray.IsChecked = Settings.Default.IsMinimizeToTrayWhenClosed;
            TextBoxWorkDirectory.Text = Settings.Default.WorkDirectory;
            NumericUpDownControlRefreshFrequency.Value = Settings.Default.RefreshFrequency;
            TabControlMain.SelectedIndex = Settings.Default.LastActivedTabIndex;
        }

        public void RestartApplication()
        {
            SaveWindowState();
            SaveAppState();
            RestartApplicationProcess();
        }

        public void LoadCultureInfo()
        {
            string infoName = Properties.Settings.Default.CultureInfoName;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(infoName);
        }

        public void UpdateLanguage(string? newCultureName)
        {
            if (String.IsNullOrEmpty(newCultureName)) return;

            string CurrentUICultureName = Thread.CurrentThread.CurrentUICulture.ToString();

            if (newCultureName.Equals(CurrentUICultureName)) return;

            if (!LocalizationManagerUtil.ChangeLanguage(newCultureName)) return;

            RestartApplication();
        }


        public void UpdateProcessInfos(List<ProcessInfo>? processInfos)
        {
            if (processInfos == null) return;

            var sortedProcessInfoItems = processInfos
                .Select(p => $"{p.ProcessName}    ({p.ProgramPath})")
                .ToList();

            ComboBoxProcessName.ItemsSource = sortedProcessInfoItems;
        }


        private void LoadDefaultSetting()
        {
            if (String.IsNullOrEmpty(Properties.Settings.Default.WorkDirectory))
            {
                Settings.Default.WorkDirectory = PathUtil.GetPortalWorkDirectory();
            }

            if (String.IsNullOrEmpty(Settings.Default.ErrorLogFilePath))
            {
                Settings.Default.ErrorLogFilePath = Path.Combine(
                    PathUtil.GetCurrentDirectory() ?? "", "ErrorLog.txt");
            }

        }

        private void RestartApplicationProcess()
        {
            string appPath = Process.GetCurrentProcess().MainModule?.FileName ?? "";

            if (!String.IsNullOrEmpty(appPath))
            {
                Settings.Default.IsSkipSilentStartOnce = true;
                Settings.Default.IsMakeTopMostOnce = true;
                Process.Start(appPath);
                Application.Current.Shutdown();
            }
        }

        

        

    }
}
