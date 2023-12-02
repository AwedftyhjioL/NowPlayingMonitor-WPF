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
            if(button != null) 
            {
                button.ContextMenu.IsEnabled = true;
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                button.ContextMenu.IsOpen = true;
            }
        }


        private void LanguageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ? menuItem = sender as MenuItem;
            if (menuItem == null)
                return;

            UpdateLanguage(menuItem.Tag.ToString());
        }

        public void UpdateLanguage(string? newCultureName)
        {
            if(String.IsNullOrEmpty(newCultureName)) return;

            string CurrentUICultureName = Thread.CurrentThread.CurrentUICulture.ToString();

            if (newCultureName.Equals(CurrentUICultureName)) return;

            if (!LocalizationManager.ChangeLanguage(newCultureName)) return;

            RestartApplication();
        }


    }
}
