using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using NowPlayingMonitor.Properties;
using System.ComponentModel;
using ControlzEx.Standard;

namespace NowPlayingMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            viewModel.RequestSaveWindowState += () => SaveWindowState();
            viewModel.RequestSaveAppState += () => SaveAppState();
            viewModel.RequestSwitchThemeLightDark += () => SwitchThemeLightDark();


            this.SizeChanged += MainWindow_SizeChanged;

        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.WindowHeight = e.NewSize.Height;
            }
        }

        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MenuItemViewModel menuItem && DataContext is MainWindowViewModel viewModel)
            {
                viewModel.ItemClickCommand?.Execute(menuItem);
            }
        }

        

        protected void ApplyExtraEvent()
        {
            MyNotifyIcon.TrayMouseDoubleClick += MyNotifyIcon_TrayMouseDoubleClick;
        }

        private void TrayMenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            ShowApplication();
        }

        private void MyNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ShowApplication();
        }

        private void ShowApplication()
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Normal;
            this.ShowActivated = true;
            this.Topmost = true;
            this.Topmost = false;
            this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RestoreLastUIState();
            ApplyExtraEvent();
            ApplyExtraUiSetUp();
        }

        public void RestoreWindowState()
        {
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Top = Properties.Settings.Default.WindowTop;
            this.Width = Properties.Settings.Default.WindowWidth;
            this.Height = Properties.Settings.Default.WindowHeight;
            this.WindowState = (WindowState)Properties.Settings.Default.WindowState;
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;
        }

        private void RestoreLastUIState()
        {
            RestoreWindowState();
            RestoreAppState();
        }

        private void RestoreAppState()
        {
            //TextBoxWorkDirectory.Text = Settings.Default.WorkDirectory;
            //NumericUpDownControlRefreshFrequency.Value = Settings.Default.RefreshFrequency;
        }

        protected void ApplyExtraUiSetUp()
        {
            ApplySilentStartSettings();
            ApplyTopMostOnce();
        }

        private void ApplySilentStartSettings()
        {
            if (Settings.Default.IsSkipSilentStartOnce)
            {
                Settings.Default.IsSkipSilentStartOnce = false;
                return;
            }

            if (Settings.Default.IsStartWithMinimize)
            {
                this.ShowInTaskbar = false;
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

        protected override void OnClosed(EventArgs e)
        {
            SaveWindowState();
            SaveAppState();
            base.OnClosed(e);
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

        //private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    var viewModel = this.DataContext as MainWindowViewModel;
        //    viewModel?.ExitApplication();
        //}

        public void SaveWindowState()
        {
            Properties.Settings.Default.WindowState = (int)WindowState;
            if(WindowState == WindowState.Normal)
            {
                Properties.Settings.Default.WindowLeft = this.Left;
                Properties.Settings.Default.WindowTop = this.Top;
                Properties.Settings.Default.WindowWidth = this.Width;
                Properties.Settings.Default.WindowHeight = this.Height;
            }
            Properties.Settings.Default.Save();
        }


        public void SaveAppState()
        {
            //Settings.Default.WorkDirectory = TextBoxWorkDirectory.Text;
            //Settings.Default.RefreshFrequency = NumericUpDownControlRefreshFrequency.Value ?? 500;
            Settings.Default.Save();
        }


        private void SwitchThemeLightDark()
        {
            if ("Dark.Cyan" == Settings.Default.CurrentTheme)
            {
                Settings.Default.CurrentTheme = "Light.Cyan";
            }
            else
            {
                Settings.Default.CurrentTheme = "Dark.Cyan";
            }
            Settings.Default.Save();
            ThemeManager.Current.ChangeTheme(this, Settings.Default.CurrentTheme);
        }



    }
}