using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NowPlayingMonitor
{
    public class LocalizationManagerUtil
    {

        public static bool ChangeLanguage(string cultureName)
        {
            if (String.IsNullOrEmpty(cultureName))
                return false;

            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = culture;
            Properties.Settings.Default.CultureInfoName = cultureName;
            Properties.Settings.Default.Save();

            Strings.Culture = culture; 

            return true;
        }
    }

}
