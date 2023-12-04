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

            LoadWindowSettings();

            LoadAppSettings();

            ApplyExtraEvent();

            ApplyExtraUiSetUp();

            HandleRestartWork();

            _viewModel.StartBackgroundTask();
        }

        protected override void OnClosed(EventArgs e)
        {
            SaveWindowSettings();
            SaveAppSettings();
            base.OnClosed(e);
        }

        void LoadDefaultSetting()
        {
            if(String.IsNullOrEmpty(Properties.Settings.Default.WorkDirectory))
            {
                Settings.Default.WorkDirectory = PathUtil.GetPortalWorkDirectory();
            }

            if (String.IsNullOrEmpty(Settings.Default.ErrorLogFilePath))
            {
                Settings.Default.ErrorLogFilePath = Path.Combine(
                    PathUtil.GetCurrentDirectory() ?? "", "ErrorLog.txt");
            }
                
        }

        protected void ApplyExtraEvent()
        {
            MyNotifyIcon.TrayMouseDoubleClick += MyNotifyIcon_TrayMouseDoubleClick;

        }

        protected void ApplyExtraUiSetUp()
        {
            _viewModel.UpdateProcessInfos();


        }

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

            if (Settings.Default.IsStartWithMinimize && !Settings.Default.IsProcessingRestartApplication)
            {
                this.WindowState = WindowState.Minimized;
                this.Hide();
            }

            

        }

        public void SaveAppSettings()
        {
            Settings.Default.IsStartWithMinimize = CheckBoxSilentStart.IsChecked ?? false;
            Settings.Default.IsMinimizeToTrayWhenClosed = CheckBoxAlwaysMinimizeToTray.IsChecked ?? false;
            Settings.Default.WorkDirectory = TextBoxWorkDirectory.Text;
            Settings.Default.RefreshFrequency = NumericUpDownControlRefreshFrequency.Value ?? 500;
            Settings.Default.Save();
        }

        public void LoadAppSettings()
        {
            CheckBoxSilentStart.IsChecked = Settings.Default.IsStartWithMinimize;
            CheckBoxAlwaysMinimizeToTray.IsChecked = Settings.Default.IsMinimizeToTrayWhenClosed;
            TextBoxWorkDirectory.Text = Settings.Default.WorkDirectory;
            NumericUpDownControlRefreshFrequency.Value = Settings.Default.RefreshFrequency;
        }

        protected void HandleRestartWork()
        {
            if (Settings.Default.IsProcessingRestartApplication)
            {
                this.Topmost = true;
                this.Topmost = false;
                this.Show();
                Settings.Default.IsProcessingRestartApplication = false;
            }
        }

        public void RestartApplication()
        {
            SaveWindowSettings();
            SaveAppSettings();
            RestartApplicationProcess();
        }

        private void RestartApplicationProcess()
        {
            string appPath = Process.GetCurrentProcess().MainModule?.FileName ?? "";

            if (!String.IsNullOrEmpty(appPath))
            {
                Settings.Default.IsProcessingRestartApplication = true;
                Process.Start(appPath);
                Application.Current.Shutdown();
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
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
    }
}
