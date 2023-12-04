using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlayingMonitor_WPF
{
    public class SystemUtil
    {

        public static string MachineName()
        {
            return Environment.MachineName;
        }


        public static string NewLine = Environment.NewLine;

    }
}
