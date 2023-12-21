using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NowPlayingMonitor
{
    public class MenuItemViewModel
    {
        public int MenuIndex { get; set; }
        public PackIconUniconsKind? IconKind { get; set; }
        public string? Label { get; set; }
        public UserControl? Content { get; set; }
    }
}
