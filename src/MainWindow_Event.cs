using NowPlayingMonitor.Properties;
using NowPlayingMonitor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NowPlayingMonitor
{
    public partial class MainWindow : Window
    {

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }




        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as RepeatButton;
            if (button == null) return;
            var integerUpDown = FindParent<Xceed.Wpf.Toolkit.IntegerUpDown>(button);
            if (integerUpDown != null && integerUpDown.Value < integerUpDown.Maximum)
                integerUpDown.Value++;

        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as RepeatButton;
            if (button == null) return;
            var integerUpDown = FindParent<Xceed.Wpf.Toolkit.IntegerUpDown>(button);
            if (integerUpDown != null && integerUpDown.Value > integerUpDown.Minimum)
                integerUpDown.Value--;
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.IsReadOnly = false;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.IsReadOnly = true;
            }
        }


        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ApplyAppSetting();
        }

        //private void ButtonRefreshProcessName_Click(object sender, RoutedEventArgs e)
        //{
        //    _viewModel.UpdateProcessInfos();

        //}

        private void ComboBoxProcessName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateMonitorProcessInfo((sender as ComboBox)?.SelectedIndex ?? -1);
        }

        //private void ButtonSelectWorkDirectory_Click(object sender, RoutedEventArgs e)
        //{
        //    string dir = DialogUtil.GetDirectory();
        //    if(!String.IsNullOrEmpty(dir))
        //    {
        //        TextBoxWorkDirectory.Text = dir;
        //    }
        //}


        private void TabControlMain_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var tabControl = sender as TabControl;
        }


        private void TrayMenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.ShowActivated = true;
            this.Topmost = true;
            this.Topmost = false;
            this.Show();
        }

        private void MyNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.ShowActivated = true;
            this.Topmost = true;
            this.Topmost = false;
            this.Show();
        }

        private void TrayMenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void TrayMenuItem_Restart_Click(object sender, RoutedEventArgs e)
        {
            RestartApplication();
        }

        

        

        

        

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
            if (menuItem == null) return;

            UpdateLanguage(menuItem.Tag.ToString());
        }


        //private void TextBoxWorkDirectory_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    _viewModel.UpdateWorkDirectory();
        //}

        //private void NumericUpDownControlRefreshFrequency_ValueChanged(object sender, RoutedEventArgs e)
        //{
        //    if (NumericUpDownControlRefreshFrequency.Value < 250) 
        //    {
        //        NumericUpDownControlRefreshFrequency.Value = 250;
        //    }
        //    if (NumericUpDownControlRefreshFrequency.Value > 233333333)
        //    {
        //        NumericUpDownControlRefreshFrequency.Value = 233333333;
        //    }

        //    _viewModel.UpdateRefreshFrequency();
        //}

    }
}
