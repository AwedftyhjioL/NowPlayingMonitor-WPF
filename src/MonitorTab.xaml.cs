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
    /// Interaction logic for MonitorTab.xaml
    /// </summary>
    public partial class MonitorTab : UserControl
    {
        public MonitorTab(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            _viewModel = new MonitorTabViewModel(mainWindowViewModel);
            DataContext = _viewModel;
        }

        private void ComboBoxMonitorMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox comboBox)) return;
            string mode = comboBox.Text;
            if (Strings.Process_Name == mode)
            {
                SettingsContentControl.Content = null;
                //SettingsContentControl.Content = new ProcessNameSettingsControl();
            }
            else if (Strings.Window_Title == mode)
            {
                SettingsContentControl.Content = null;
                //SettingsContentControl.Content = new WindowTitleSettingsControl();
            }
            else if (Strings.Spotify == mode)
            {
                SettingsContentControl.Content = null;
                //SettingsContentControl.Content = new SpotifySettingsControl();
            }
            else
            {
                SettingsContentControl.Content = null;
            }


        }

        private void TextBoxProfileName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxProfileName.IsReadOnly = false;
        }

        private void TextBoxProfileName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxProfileName.IsReadOnly = true;
        }

        private void ButtonSelectWorkDirectory_Click(object sender, RoutedEventArgs e)
        {
            string dir = DialogUtil.GetDirectory();
            if (!String.IsNullOrEmpty(dir))
            {
                _viewModel.WorkDirectory = dir;
            }
        }

        private MonitorTabViewModel _viewModel;
    }



}
