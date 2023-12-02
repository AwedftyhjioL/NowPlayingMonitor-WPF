using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NowPlayingMonitor_WPF
{

    public partial class MainWindow : Window
    {

        public void LoadCultureInfo()
        {
            string infoName = Properties.Settings.Default.CultureInfoName;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(infoName);
        }


    }
}
