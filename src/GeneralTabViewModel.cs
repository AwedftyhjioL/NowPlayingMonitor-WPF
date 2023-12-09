using NowPlayingMonitor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlayingMonitor
{
    public class GeneralTabViewModel : INotifyPropertyChanged
    {

        private bool _isStartWithMinimize;
        private bool _isMinimizeToTrayWhenClosed;

        public bool IsStartWithMinimize
        {
            get { return _isStartWithMinimize; }
            set
            {
                if (_isStartWithMinimize != value)
                {
                    _isStartWithMinimize = value;
                    OnPropertyChanged(nameof(IsStartWithMinimize));

                    Settings.Default.IsStartWithMinimize = _isStartWithMinimize;
                }
            }
        }

        public bool IsMinimizeToTrayWhenClosed
        {
            get { return _isMinimizeToTrayWhenClosed; }
            set
            {
                if (_isMinimizeToTrayWhenClosed != value)
                {
                    _isMinimizeToTrayWhenClosed = value;
                    OnPropertyChanged(nameof(IsMinimizeToTrayWhenClosed));

                    Settings.Default.IsMinimizeToTrayWhenClosed = _isMinimizeToTrayWhenClosed;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
