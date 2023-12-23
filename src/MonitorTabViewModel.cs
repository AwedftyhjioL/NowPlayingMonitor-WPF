using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using NowPlayingMonitor.Properties;

namespace NowPlayingMonitor
{
    public class MonitorTabViewModel : ObservableObject
    {

        public MonitorTabViewModel() 
        {
            LoadFromConfig();
        }

        public bool IsStartMonitor
        {
            get { return _isStartMonitor; }
            set
            {
                SetProperty(ref _isStartMonitor, value);
                SaveToConfig(); 
            }
        }

        private void LoadFromConfig()
        {
            this.IsStartMonitor = ConfigUtil.ReadBool("Monitor_0", "IsStartMonitor") ?? false;
        }

        private void SaveToConfig()
        {
            ConfigUtil.WriteBool("Monitor_0", "IsStartMonitor", this.IsStartMonitor);
        }


        private bool _isStartMonitor;
    }
}
