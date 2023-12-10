using NowPlayingMonitor.Properties;
using NowPlayingMonitor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.IconPacks;


namespace NowPlayingMonitor
{
    public class MainWindowViewModel : ObservableObject
    {
        private UserControl? _currentContent;


        public ObservableCollection<MenuItemViewModel>? MenuItems { get; }
        public ICommand? ItemClickCommand { get; }

        public MainWindowViewModel() 
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { IconKind = PackIconUniconsKind.Home, Label =  Strings.Welcome , Content = new WelcomeTab() },
                new MenuItemViewModel { IconKind = PackIconUniconsKind.Setting, Label = Strings.General , Content = new GeneralTab() },
                new MenuItemViewModel { IconKind = PackIconUniconsKind.Monitor, Label = Strings.Monitor_Setting , Content = new MonitorSettingTab() },
            };

            ItemClickCommand = new RelayCommand<MenuItemViewModel>(ExecuteItemClick);

        }

        private void ExecuteItemClick(MenuItemViewModel? menuItem)
        {
            if (menuItem != null && menuItem.Content != null)
            {
                CurrentContent = menuItem.Content;
            }
        }


        public UserControl? CurrentContent
        {
            get { return _currentContent; }
            set
            {
                _currentContent = value;
                OnPropertyChanged(nameof(CurrentContent)); 
            }
        }

        private bool _isHamburgerMenuPaneOpen;
        public bool IsHamburgerMenuPaneOpen
        {
            get => _isHamburgerMenuPaneOpen;
            set => SetProperty(ref _isHamburgerMenuPaneOpen, value);
        }

    }
}
