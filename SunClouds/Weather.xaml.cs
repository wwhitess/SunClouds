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
using System.Windows.Shapes;

namespace SunClouds
{
    /// <summary>
    /// Логика взаимодействия для Weather.xaml
    /// </summary>
    public partial class Weather : Window
    {
        public Weather()
        {
            InitializeComponent();
        }
        public void getWeather(string city, string tempType)
        {
            var json = ApiHelper.Get(city, tempType);
            var result = DerSerLib.jsonclass.JsonDeser<WeatherModel>(json);

            testWeather.ItemsSource = new List<Main> { result.Main };
        }
    }
}
