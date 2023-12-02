using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;

namespace NowPlayingMonitor_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            LoadCultureInfo();

            InitializeComponent();

            LoadWindowSettings();
            LoadAppSettings();

            ApplyExtraEvent();

        }


        protected override void OnClosed(EventArgs e)
        {
            SaveWindowSettings();
            SaveAppSettings();
            base.OnClosed(e);
        }

        protected void ApplyExtraEvent()
        {
            MyNotifyIcon.TrayMouseDoubleClick += MyNotifyIcon_TrayMouseDoubleClick;
        }

        
    }
}