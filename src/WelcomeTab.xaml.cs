using ControlzEx.Theming;
using NowPlayingMonitor.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NowPlayingMonitor
{
    /// <summary>
    /// Interaction logic for WelcomeTab.xaml
    /// </summary>
    public partial class WelcomeTab : UserControl
    {
        public event Action<string>? LanguageChanged;
        public event Action? ThemeLightDarkChanged;

        public WelcomeTab()
        {
            InitializeComponent();
        }


        private void ButtonLanguage_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null && button.ContextMenu != null)
            {
                button.ContextMenu.IsEnabled = true;
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void MenuItemLanguage_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Tag is string language)
            {
                OnLanguageChanged(language);
            }
        }

        protected virtual void OnLanguageChanged(string language)
        {
            LanguageChanged?.Invoke(language);
        }

        private void ButtonThemeLightDark_Click(object sender, RoutedEventArgs e)
        {
            ThemeLightDarkChanged?.Invoke();
        }



    }

}
