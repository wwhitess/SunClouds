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
using System.Timers;


namespace SunClouds
{
    /// <summary>
    /// Логика взаимодействия для WeatherPage.xaml
    /// </summary>
   
    public partial class WeatherPage : Page, INotifyPropertyChanged
    {
        private Timer timer;
        readonly string deg = "°";

        WeatherModel data;
        HoursList weatherHourly;

        public WeatherPage()
        {
            InitializeComponent();


            TemperatureData = new List<ISeries>
            {
                new LineSeries<double> { Values = new ChartValues<double> { 10, 20, 30, 40, 50 } }

            };

            TimeData = new List<string> { "00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00" };

            DataContext = this;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(AsyncEvent);
            timer.Enabled = true;
        }
        internal void GetData(WeatherModel weather, HoursList hourlyWeather)
        {
            data = weather;
            weatherHourly = hourlyWeather;
            SetNow();
            SetHourly();
        }

        internal void SetNow()
        {
            tempNow.Text = Convert.ToString(Math.Round(data.Main.Temp) + deg);
            feelsLikeNow.Text = Convert.ToString(Math.Round(data.Main.Feels_like) + deg);
            minTempNow.Text = Convert.ToString(Math.Round(data.Main.Temp_min) + deg);
            maxTempNow.Text = Convert.ToString(Math.Round(data.Main.Temp_max) + deg);
            pressureNow.Text = Convert.ToString(data.Main.Pressure + "мм рт. ст.");
            humidityNow.Text = Convert.ToString(data.Main.Humidity + "%");
            windNow.Text = Convert.ToString(Math.Round(data.Wind.Speed) + "м\\с");
            windDegNow.Text = Convert.ToString(data.Wind.Deg);
        }
        private void SetHourly()
        {

            for (int i = 0; i < 1; i++) //потрехчасовая погода хД. далее указать weatherHourly.list.Count()
            {
                string gethour = weatherHourly.list[i].dt_txt;
                DateTime hour = DateTime.Parse(gethour);
                TextBlock times = (TextBlock)this.FindName("time" + (i + 1));
                TextBlock tempes = (TextBlock)this.FindName("temp" + (i + 1));
                Run humbity = (Run)this.FindName("humbity" + (i + 1));
                Run feelsLikes = (Run)this.FindName("feelsLike" + (i + 1));
                times.Text = hour.ToString("HH:mm");
                tempes.Text = Convert.ToString(Math.Round(weatherHourly.list[i].main.Temp) + deg);
                humbity.Text = Convert.ToString(weatherHourly.list[i].main.Humidity + "%");
                feelsLikes.Text = Convert.ToString(feelsLikes.Text + Math.Round(weatherHourly.list[i].main.Feels_like) + deg);
            }
        }
        private async void AsyncEvent(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour % 2 == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    SetNow();
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
     
        }
        private void TemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            //В процессе (Ожидание почасовой погоды)
        }

        private void FeelsLikeButton_Click(object sender, RoutedEventArgs e)
        {
            //В процессе (Ожидание почасовой погоды)
        }

        private void PressureButton_Click(object sender, RoutedEventArgs e)
        {
            //В процессе (Ожидание почасовой погоды)
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Maximize the window
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = WindowState.Maximized;
            }
        }
    }
}