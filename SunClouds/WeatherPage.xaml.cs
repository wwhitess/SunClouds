using SunClouds.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
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
    /// Логика взаимодействия для WeatherPage.xaml
    /// </summary>
    public partial class WeatherPage : Page
    {
        string deg = "°";

        WeatherModel data;

        public WeatherPage()
        {
            InitializeComponent();
            
        }
        internal void GetData(WeatherModel weather)
        {
            data = weather;
            SetNow();
        }

        internal void SetNow()
        {
            tempNow.Text = Convert.ToString(Math.Round(data.Main.Temp) + deg);
            feelsLikeNow.Text = Convert.ToString(Math.Round(data.Main.Feels_like) + deg);
            minTempNow.Text = Convert.ToString(Math.Round(data.Main.Temp_min) + deg);
            maxTempNow.Text = Convert.ToString(Math.Round(data.Main.Temp_max) + deg);
            pressureNow.Text = Convert.ToString(data.Main.Pressure + pressureNow.Text);
            humidityNow.Text = Convert.ToString(data.Main.Humidity + "%");
            windNow.Text = Convert.ToString(Math.Round(data.Wind.Speed) + windNow.Text);
            windDegNow.Text = Convert.ToString(data.Wind.Deg);
        }
    }
}
