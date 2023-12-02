using System.Configuration;
using System.Data;
using System.Windows;

namespace NowPlayingMonitor_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.OutputMissingKeys = true;


        }

    }

}
