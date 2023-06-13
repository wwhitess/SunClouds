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
           // DefaultTextBox.text = SunClouds.Properties.Settings.Default.DefaultCity; установка значения в поле "основной город"
        }

        private void DefaultTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
           // SunClouds.Properties.Settings.Default.DefaultCity = DefaultTextBox.Text;  Смена города по умолчанию
        }
    }
}
