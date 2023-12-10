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

namespace NowPlayingMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            //StartUp();

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
        }


        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonThemeLightDark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MenuItemViewModel menuItem && DataContext is MainWindowViewModel viewModel)
            {
                viewModel.ItemClickCommand?.Execute(menuItem);
            }
        }
    }
}