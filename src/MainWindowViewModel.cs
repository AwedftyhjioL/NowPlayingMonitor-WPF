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
using System.Windows;
using ControlzEx.Theming;
using System.Reflection;


namespace NowPlayingMonitor
{
    public class MainWindowViewModel : ObservableObject
    {
        private UserControl? _currentContent;
        private bool _isHamburgerMenuPaneOpen;


        public ObservableCollection<MenuItemViewModel>? MenuItems { get; private set; }

        public ICommand? ItemClickCommand { get; private set; }
        public ICommand? ChangeLanguageCommand { get; private set; }
        public ICommand? RestartApplicationCommand { get; private set; }
        public ICommand? ExitApplicationCommand { get; private set; }

        public event Action? RequestSaveWindowState;
        public event Action? RequestSaveAppState;
        public event Action? RequestSwitchThemeLightDark;



        public MainWindowViewModel() 
        {
            SetUpMenuItems();

            SetUpCommand();

        }

        private void SetUpMenuItems()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { MenuIndex = 0, IconKind = PackIconUniconsKind.Home, Label =  Strings.Welcome, Content = new WelcomeTab() },
                new MenuItemViewModel { MenuIndex = 1, IconKind = PackIconUniconsKind.Setting, Label = Strings.Settings, Content = new SettingsTab() },
                new MenuItemViewModel { MenuIndex = 2, IconKind = PackIconUniconsKind.Monitor, Label = Strings.Monitor, Content = new MonitorTab() },
            };

            var welcomeTab = MenuItems.FirstOrDefault(item => item.Content is WelcomeTab)?.Content as WelcomeTab;
            if (welcomeTab != null)
            {
                welcomeTab.LanguageChanged += ChangeLanguage;
                welcomeTab.ThemeLightDarkChanged += SwitchThemeLightDark;
            }

            ActivateMenuItem(Properties.Settings.Default.ActivatedMenuItemIndex);
        }

        private void SetUpCommand()
        {
            ItemClickCommand = new RelayCommand<MenuItemViewModel>(ExecuteItemClick);
            ChangeLanguageCommand = new RelayCommand<string>(ChangeLanguage);
            RestartApplicationCommand = new RelayCommand(RestartApplication);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }

        private void ExecuteItemClick(MenuItemViewModel? menuItem)
        {
            if (menuItem != null && menuItem.Content != null)
            {
                CurrentContent = menuItem.Content;
                Settings.Default.ActivatedMenuItemIndex = menuItem.MenuIndex;
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

        private void ActivateMenuItem(int index)
        {
            if (MenuItems != null && MenuItems.Count > index)
            {
                ExecuteItemClick(MenuItems[index]);
            }
        }

        public bool IsHamburgerMenuPaneOpen
        {
            get => _isHamburgerMenuPaneOpen;
            set => SetProperty(ref _isHamburgerMenuPaneOpen, value);
        }

        private void ChangeLanguage(string? language)
        {
            if (String.IsNullOrEmpty(language)) return;

            string CurrentUICultureName = Thread.CurrentThread.CurrentUICulture.ToString();

            if (language.Equals(CurrentUICultureName)) return;

            if (!LocalizationManagerUtil.ChangeLanguage(language)) return;

            RestartApplication();
        }

        

        public void RestartApplication()
        {
            RequestSaveWindowState?.Invoke();
            RequestSaveAppState?.Invoke();
            ApplicationUtil.RestartApplicationProcess();
        }

        public void ExitApplication()
        {
            RequestSaveWindowState?.Invoke();
            RequestSaveAppState?.Invoke();
            ApplicationUtil.ExitApplicationProcess();
        }

        private void SwitchThemeLightDark()
        {
            RequestSwitchThemeLightDark?.Invoke();
        }
    }
}
