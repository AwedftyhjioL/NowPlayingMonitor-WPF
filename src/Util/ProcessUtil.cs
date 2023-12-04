using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlayingMonitor_WPF.Util
{


    public class ProcessUtil
    {
        public static List<ProcessInfo> GetProcessInfoList(bool isOrderByName, bool removeEmptyTitle)
        {
            List<ProcessInfo> processInfoList = new List<ProcessInfo>();

            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                if (removeEmptyTitle && String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    continue;
                }

                string processPath = "";

                try
                {
                    var module = process.MainModule;
                    if (module != null)
                        processPath = module.FileName;
                }
                catch
                {
                    processPath = "Access denied";
                }

                processInfoList.Add(new ProcessInfo
                {
                    ProcessName = process.ProcessName,
                    ProcessId = process.Id,
                    ProgramPath = processPath
                });
            }

            if(isOrderByName)
            {
                var sortedList = processInfoList.OrderBy(p => p.ProcessName ?? string.Empty).ToList();
                processInfoList = sortedList;
            }

            return processInfoList;
        }
    }
}
