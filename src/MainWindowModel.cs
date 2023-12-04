using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlayingMonitor_WPF
{
    public class MainWindowModel
    {


        public MainWindowModel(MainWindowViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        private MainWindowViewModel _viewModel;

        private List<ProcessInfo>? _processInfos;

        private int _windowMonitorProcessId;
        private string? _windowMonitorProcessName;
        private string? _windowMonitorProgramPath;

        private string? _workDirectory;
        private int _refreshFrequency;

        public List<ProcessInfo>? ProcessInfos { get => _processInfos; set => _processInfos = value; }
        public int WindowMonitorProcessId { get => _windowMonitorProcessId; set => _windowMonitorProcessId = value; }
        public string? WindowMonitorProcessName { get => _windowMonitorProcessName; set => _windowMonitorProcessName = value; }
        public string? WorkDirectory { get => _workDirectory; set => _workDirectory = value; }
        public int RefreshFrequency { get => _refreshFrequency; set => _refreshFrequency = value; }
        public string? WindowMonitorProgramPath { get => _windowMonitorProgramPath; set => _windowMonitorProgramPath = value; }
    }
}
