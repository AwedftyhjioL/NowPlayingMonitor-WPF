using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NowPlayingMonitor
{
    public class WindowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int state)
            {
                return state switch
                {
                    0 => WindowState.Normal,
                    1 => WindowState.Minimized,
                    2 => WindowState.Maximized,
                    _ => WindowState.Normal,
                };
            }

            return WindowState.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState windowState)
            {
                return windowState switch
                {
                    WindowState.Normal => 0,
                    WindowState.Minimized => 1,
                    WindowState.Maximized => 2,
                    _ => 0,
                };
            }

            return 0;
        }
    }
}
