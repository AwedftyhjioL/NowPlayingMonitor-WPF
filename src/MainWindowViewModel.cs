using NowPlayingMonitor_WPF.Properties;
using NowPlayingMonitor_WPF.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace NowPlayingMonitor_WPF
{
    public class MainWindowViewModel
    {


        public MainWindowViewModel(MainWindow view) 
        {
            _view = view;
            _model = new MainWindowModel(this);
        }

        

        public void UpdateProcessInfos()
        {
            _model.ProcessInfos = ProcessUtil.GetProcessInfoList(true, true);
            _view.UpdateProcessInfos(_model.ProcessInfos);

            int index = 0;
            string lastProgramPath = Settings.Default.WindowMonitorProgramPath;
            foreach(var p in  _model.ProcessInfos)
            {
                if(lastProgramPath.Equals(p.ProgramPath))
                {
                    _view.ComboBoxProcessName.SelectedIndex = index;
                }
                else
                {
                    index++;
                }
            }
        }

        public void UpdateMonitorProcessInfo(int index)
        {
            int processCount = _model.ProcessInfos?.Count ?? 0;
            if(index >= 0 && index < processCount)
            {
                var pInfo = _model.ProcessInfos?[index] ?? null;
                Settings.Default.WindowMonitorProcessId = pInfo?.ProcessId ?? -1;
                Settings.Default.WindowMonitorProcessName = pInfo?.ProcessName ?? "";
                Settings.Default.WindowMonitorProgramPath = pInfo?.ProgramPath ?? "";
            }
        }

        public void UpdateWorkDirectory()
        {
            Settings.Default.WorkDirectory = _view.TextBoxWorkDirectory.Text;
        }

        public void UpdateRefreshFrequency()
        {
            Settings.Default.RefreshFrequency = Convert.ToInt32(_view.NumericUpDownControlRefreshFrequency.Text);
        }

        public void ApplyAppSetting()
        {
            _model.WindowMonitorProcessName = Settings.Default.WindowMonitorProcessName;
            _model.WindowMonitorProcessId = Settings.Default.WindowMonitorProcessId;
            _model.WindowMonitorProgramPath = Settings.Default.WindowMonitorProgramPath;
            _model.RefreshFrequency = Settings.Default.RefreshFrequency;
            _model.WorkDirectory = Settings.Default.WorkDirectory;
            _timer?.Change(0, _model.RefreshFrequency);
        }

        public void StartBackgroundTask()
        {
            var interval = Settings.Default.RefreshFrequency;
            _timer = new Timer(DoWork, null, interval, interval);
        }

        private void DoWork(object? state)
        {
            Task.Run(() =>
            {
                try
                {
                    TryWriteInfoToFile();
                }
                catch (Exception ex)
                {
                    FileWriterUtil.WriteToFile(Settings.Default.ErrorLogFilePath, TimeUtil.NowString() + 
                        " " + ex.Message + SystemUtil.NewLine, FileWriterUtil.Mode.Append);
                }
                finally
                {
                    //_timer?.Change(1000, _model.RefreshFrequency);
                }
            });
        }

        private void TryWriteInfoToFile()
        {
            var pId = Settings.Default.WindowMonitorProcessId;
            if (pId > 0)
            {
                var p = Process.GetProcessById(pId);
                string workSpace = Path.Combine(Settings.Default.WorkDirectory, SystemUtil.MachineName());
                string filePath = Path.Combine(workSpace, p.ProcessName + ".txt");
                string content = $"{p.MainWindowTitle}";
                FileWriterUtil.WriteToFile(filePath, content, FileWriterUtil.Mode.Create);
            }
        }


        private MainWindow _view;
        private MainWindowModel _model;
        private Timer? _timer;
    }
}
