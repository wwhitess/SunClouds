using SunClouds.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
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
using LiveChartsCore.SkiaSharpView.WPF;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;
using LiveChartsCore.Kernel.Sketches;
using LiveCharts.Wpf;
using LiveCharts;


namespace SunClouds
{
    /// <summary>
    /// Логика взаимодействия для WeatherPage.xaml
    /// </summary>
   
    public partial class WeatherPage : Page, INotifyPropertyChanged
    {
        readonly string deg = "°";

        WeatherModel data;

       
        internal void GetData(WeatherModel weather)
        {
            data = weather;
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
        private List<ISeries> _temperatureData;
        public List<ISeries> TemperatureData
        {
            get => _temperatureData;
            set
            {
                _temperatureData = value;
                OnPropertyChanged();
            }
        }

        private List<string> _timeData;
        public List<string> TimeData
        {
            get => _timeData;
            set
            {
                _timeData = value;
                OnPropertyChanged();
            }
        }

        public WeatherPage()
        {
            InitializeComponent();

            
            TemperatureData = new List<ISeries>
            {
                new LineSeries<double> { Values = new ChartValues<double> { 10, 20, 30, 40, 50 } }

            };

            TimeData = new List<string> { "00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00" };

            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
     
        }
        private void TemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void FeelsLikeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PressureButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}