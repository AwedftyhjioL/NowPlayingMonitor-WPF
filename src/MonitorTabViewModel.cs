using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using NowPlayingMonitor.Properties;
using NowPlayingMonitor.Util;

namespace NowPlayingMonitor
{
    public class MonitorTabViewModel : ObservableObject
    {

        private MainWindowViewModel _mainWindowViewModel;

        public MonitorTabViewModel(MainWindowViewModel mainWindowViewModel) 
        {
            _mainWindowViewModel = mainWindowViewModel;
            LoadSettingsFromConfigFile();
        }

        ~MonitorTabViewModel()
        {
            SaveSettingsToConfigFile();
        }

        public string ProfileId()
        {
            return _profileId ?? "";
        }

        public string ProfileName
        {
            get { return _profileName ?? ""; }
            set { 
                SetProperty(ref _profileName, value);
                ConfigUtil.Write(ProfileId(), "ProfileName", _profileName);
            }
        }

        public bool IsStartMonitor
        {
            get { return _isStartMonitor; }
            set
            {
                SetProperty(ref _isStartMonitor, value);
                ConfigUtil.WriteBool(ProfileId(), "IsStartMonitor", _isStartMonitor);
            }
        }

        public string WorkDirectory
        {
            get { return _workDirectory ?? ""; }
            set { 
                SetProperty(ref _workDirectory, value);
                ConfigUtil.Write(ProfileId(), "WorkDirectory", _workDirectory);
            }
        }

        public int RefreshFrequency
        {
            get { return _refreshFrequency; }
            set { 
                SetProperty(ref _refreshFrequency, value);
                ConfigUtil.Write(ProfileId(), "RefreshFrequency", _refreshFrequency.ToString());
            }
        }


        public int MonitorModeIndex
        {
            get { return _monitorModeIndex; }
            set
            {
                SetProperty(ref _monitorModeIndex, value);
                ConfigUtil.Write(ProfileId(), "MonitorModeIndex", _monitorModeIndex.ToString());
            }
        }


        private void LoadSettingsFromConfigFile()
        {
            _profileId = ConfigUtil.Read("Global", "ActivatedMonitorProfileId") ?? GuidUtil.NewS("Profile_");
            _profileName = ConfigUtil.Read(ProfileId(), "ProfileName") ?? ProfileId().Substring(0, 16);
            _isStartMonitor = ConfigUtil.ReadBool(ProfileId(), "IsStartMonitor") ?? false;
            _workDirectory = ConfigUtil.Read(ProfileId(), "WorkDirectory") ?? PathUtil.GetPortalWorkDirectory();
            _refreshFrequency = ConfigUtil.ReadInt(ProfileId(), "RefreshFrequency") ?? 500;
            _monitorModeIndex = ConfigUtil.ReadInt(ProfileId(), "MonitorModeIndex") ?? 0;

            SaveSettingsToConfigFile();
        }

        private void SaveSettingsToConfigFile()
        {
            ConfigUtil.Write("Global", "ActivatedMonitorProfileId", ProfileId());
            ConfigUtil.Write(ProfileId(), "ProfileName", ProfileName);
            ConfigUtil.WriteBool(ProfileId(), "IsStartMonitor", IsStartMonitor);
            ConfigUtil.Write(ProfileId(), "WorkDirectory", WorkDirectory);
            ConfigUtil.Write(ProfileId(), "RefreshFrequency", RefreshFrequency.ToString());
            ConfigUtil.Write(ProfileId(), "MonitorModeIndex", MonitorModeIndex.ToString());
        }

        private string? _profileId;
        private string? _profileName;
        private bool _isStartMonitor;
        private string? _workDirectory;
        private int _refreshFrequency;
        private int _monitorModeIndex;
    }
}
