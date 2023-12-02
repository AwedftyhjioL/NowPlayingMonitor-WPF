using NowPlayingMonitor_WPF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NowPlayingMonitor_WPF
{
    public partial class MainWindow : Window
    {
        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                button.ContextMenu.IsEnabled = true;
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                button.ContextMenu.IsOpen = true;
            }
        }


        private void LanguageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem? menuItem = sender as MenuItem;
            if (menuItem == null)
                return;

            UpdateLanguage(menuItem.Tag.ToString());
        }

        public void UpdateLanguage(string? newCultureName)
        {
            if (String.IsNullOrEmpty(newCultureName)) return;

            string CurrentUICultureName = Thread.CurrentThread.CurrentUICulture.ToString();

            if (newCultureName.Equals(CurrentUICultureName)) return;

            if (!LocalizationManager.ChangeLanguage(newCultureName)) return;

            RestartApplication();
        }

        private void CheckBoxSilentStart_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.IsStartWithMinimize = true;
        }

        private void CheckBoxSilentStart_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.IsStartWithMinimize = false;
        }

        private void CheckBoxAlwaysMinimizeToTray_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.IsMinimizeToTrayWhenClosed = true;
        }

        private void CheckBoxAlwaysMinimizeToTray_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.IsMinimizeToTrayWhenClosed = false;
        }

    }
}
