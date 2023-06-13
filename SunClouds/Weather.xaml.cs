using SunClouds.Helpers;
using SunClouds.Models;
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
    public partial class Weather : Window
    {
        public Weather()
        {
            InitializeComponent();

            Frame.Content = new WeatherPage();
        }
        public void getWeather(string city, string tempType)
        {
            var json = ApiHelper.Get(city, tempType);
            var result = DerSerLib.jsonclass.JsonDeser<WeatherModel>(json);

          //  testWeather.ItemsSource = new List<Main> { result.Main }; ВЫГРУЗКА
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Frame.Content = new WeatherPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Frame.Content = new SettingsPage();
        }
    }
}
