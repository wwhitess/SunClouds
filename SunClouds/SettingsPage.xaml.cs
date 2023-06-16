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
using DerSerLib;
using SunClouds.Helpers;

namespace SunClouds
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            SetCity.Text = SunClouds.Properties.Settings.Default.DefaultCity;
        }

        private void ClearBox(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetCity.Clear();
        }
        private void ClearAddBox(object sender, KeyboardFocusChangedEventArgs e)
        {
            AddCity.Clear();
        }

        private void SetCity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Weather))
                {
                    (window as Weather).TextDisplay.Text = SetCity.Text;
                }
            }
            //jsonclass.JsonDeser<>()
        }
    }
}
