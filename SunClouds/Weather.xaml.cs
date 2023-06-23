using SunClouds.Helpers;
using SunClouds.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Timers;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SunClouds
{
    public partial class Weather : Window
    {
        SettingsPage settingsPage = new SettingsPage();
        WeatherPage weatherPage = new WeatherPage();
        WeatherModel weatherNow;
        HoursList weatherHourly;
        private Timer timer;
        string deg = "°";

        public Weather()
        {
            InitializeComponent();
            Frame.Content = weatherPage;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(AsyncEvent);
            timer.Enabled = true;
        }
        private async void AsyncEvent(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour % 2 == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    SetLeftWeather();
                    
                });
            }
        }
        public void getWeather(string city, string tempType)
        {
            var json = ApiHelper.Get(city, tempType);
            var jsonHours = ApiHelper.GetHours(city, tempType);
            WeatherModel result = DerSerLib.jsonclass.JsonDeser<WeatherModel>(json);
            HoursList resultHours = DerSerLib.jsonclass.JsonDeser<HoursList>(jsonHours);
            weatherNow = result;
            weatherHourly = resultHours;
            toCity.Text = city;
            settingsPage.SetSettings(tempType);
            weatherPage.GetData(result, resultHours);
            SetLeftWeather();
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
            Frame.Content = weatherPage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Frame.Content = settingsPage;
        }
        private void WindowTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                System.Windows.Point mousePos = e.GetPosition(WindowTitleBar);
                if (mousePos.Y >= 0 && mousePos.Y <= WindowTitleBar.RowDefinitions[0].ActualHeight && mousePos.X >= WindowTitleBar.ColumnDefinitions[0].Offset && mousePos.X <= WindowTitleBar.ColumnDefinitions[9].Offset + WindowTitleBar.ColumnDefinitions[9].ActualWidth)
                {
                    this.DragMove();
                }
            }
        }
        private void SetLeftWeather()
        {
            tempNow.Text = Convert.ToString(Math.Round(weatherNow.Main.Temp) + deg);
            feelLikeNow.Text = Convert.ToString(Math.Round(weatherNow.Main.Feels_like) + deg);
            Uri weatherIconNow;
            
            switch (weatherNow.Weather[0].Main)
            {
                case "Thunderstorm":
                    weatherIconNow = new Uri("Sources\\Thunderstorm.png", UriKind.Relative);
                    imgNow.Source = new BitmapImage(weatherIconNow);
                    break;
                case "Drizzle":
                    weatherIconNow = new Uri("Sources\\Downpour.png", UriKind.Relative);
                    imgNow.Source = new BitmapImage(weatherIconNow);
                    break;
                case "Rain":
                    weatherIconNow = new Uri("Sources\\Rainy.png", UriKind.Relative);
                    imgNow.Source = new BitmapImage(weatherIconNow);
                    break;
                case "Snow":
                    weatherIconNow = new Uri("Sources\\Snow.png", UriKind.Relative);
                    imgNow.Source = new BitmapImage(weatherIconNow);
                    break;
                case "Clear":
                    weatherIconNow = new Uri("Sources\\Sunny.png", UriKind.Relative);
                    imgNow.Source = new BitmapImage(weatherIconNow);
                    break;
                case "Clouds":
                    weatherIconNow = new Uri("Sources\\Cloudy.png", UriKind.Relative);
                    imgNow.Source = new BitmapImage(weatherIconNow);
                    break;

            }

            for (int i = 0; i < 3; i++) //потрехчасовая погода хД
            {
                string gethour = weatherHourly.list[i].dt_txt;
                DateTime hour = DateTime.Parse(gethour);
                TextBlock times = (TextBlock)this.FindName("time" + (i + 1));
                Run tempes = (Run)this.FindName("temp" + (i + 1));
                Run feelsLikes = (Run)this.FindName("feelsLike" + (i + 1));
                times.Text = hour.ToString("HH:mm");
                tempes.Text = Convert.ToString(weatherHourly.list[i].weather[0].Description.Substring(0, 1).ToUpper() +
                                               weatherHourly.list[i].weather[0].Description.Substring(1) + 
                                               " " + Math.Round(weatherHourly.list[i].main.Temp) + deg);
                feelsLikes.Text = Convert.ToString(feelsLikes.Text + Math.Round(weatherHourly.list[i].main.Feels_like) + deg);
            }
        }
    }
}
