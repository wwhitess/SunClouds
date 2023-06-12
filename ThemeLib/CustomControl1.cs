using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThemeLib
{
    public static class CustomControl1
    {
        public static void FirstTheme()
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new System.Uri("/ThemeLib;component/Themes/FirstTheme.xaml", System.UriKind.Relative)
            });
        }

        public static void SecondTheme()
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new System.Uri("/ThemeLib;component/Themes/SecondTheme.xaml", System.UriKind.Relative)
            });
        }
    }
}
