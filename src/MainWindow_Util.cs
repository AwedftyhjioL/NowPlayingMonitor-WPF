using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace NowPlayingMonitor_WPF
{
    public partial class MainWindow : Window
    {
        private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T? parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }


        private List<string> GetComboBoxItemsAsStringList(ComboBox comboBox)
        {
            List<string> itemsList = new List<string>();

            foreach (var item in comboBox.Items)
            {
                itemsList.Add(item?.ToString() ?? "");
            }

            return itemsList;
        }

    }



}
