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
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private string TTemp;
        public SettingsPage()
        {
            InitializeComponent();
        }
        public void SetSettings(string TTemp)
        {
            SetCity.Text = Properties.Settings.Default.DefaultCity;
            this.TTemp = TTemp;
            /*if (TTemp == "metric") { первый рибон }
            else { второй рибон}*/
        }

        private void ClearBox(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetCity.Clear();
        }
        private void ClearAddBox(object sender, KeyboardFocusChangedEventArgs e)
        {
            FavoriteCityBox.Clear();
        }

        private void AddFavoriteCity_click(object sender, RoutedEventArgs e)
        {
            var json = ApiHelper.Get(FavoriteCityBox.Text, TTemp);
            var result = DerSerLib.jsonclass.JsonDeser<WeatherModel>(json);
            double lon = result.Coord.Lon;
            double lat = result.Coord.Lat;
            // присваиваем в верстку координаты и название города FavoriteCityBox.Text, $"{lon}, с.ш {lat} в.э"
            FavoriteCity city = new FavoriteCity("Moscow", lon, lat);
            DerSerLib.jsonclass.JsonSer(city, "FavoriteCity");

        }
    }
}
