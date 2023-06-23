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
using static System.Net.Mime.MediaTypeNames;

namespace SunClouds
{
    public partial class Weather : Window
    {
        SettingsPage settingsPage = new SettingsPage();
        WeatherPage weatherPage = new WeatherPage();
        WeatherModel weatherNow;
        HoursList weatherHourly;
        private Timer timer,check;
        readonly string deg = "°";

        public Weather()
        {
            InitializeComponent();
            Frame.Content = weatherPage;

            SizeChanged += WinSizeChanged;
            ButtonVisibility();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(AsyncEvent);
            timer.Enabled = true;
            check = new Timer();
            check.Interval = 1000;
            check.Elapsed += new ElapsedEventHandler(CheckTime);
            check.Enabled = true;
        }
        private Uri getImage(object position)
        {
            Uri weatherIcon;

            switch (position)
            {
                case "Thunderstorm":
                    weatherIcon = new Uri("Sources/Thunderstorm.png", UriKind.Relative);
                    break;
                case "Drizzle":
                    weatherIcon = new Uri("Sources/Downpour.png", UriKind.Relative);
                    break;
                case "Rain":
                    weatherIcon = new Uri("Sources/Rainy.png", UriKind.Relative);
                    break;
                case "Snow":
                    weatherIcon = new Uri("Sources/Snow.png", UriKind.Relative);
                    break;
                case "Clear":
                    weatherIcon = new Uri("Sources/Sunny.png", UriKind.Relative);
                    break;
                case "Clouds":
                    weatherIcon = new Uri("Sources/Cloudy.png", UriKind.Relative);
                    break;
                default:
                    weatherIcon = new Uri("Sources/Sunny.png", UriKind.Relative);
                    break;
            }

            return weatherIcon;
        }
        private async void AsyncEvent(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour % 2 == 0 && DateTime.Now.Minute == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    SetLeftWeather();
                    timer.Enabled = false;
                });
            }
        }
        private async void CheckTime(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour % 2 == 0 && DateTime.Now.Minute == 1)
            {
                timer.Enabled = true;
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
            settingsPage.SetSettings(tempType, city);
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
            discNow.Text = Convert.ToString(weatherNow.Weather[0].Description.Substring(0, 1).ToUpper() + (weatherNow.Weather[0].Description.Substring(1) + " "));
            tempNow.Text = Convert.ToString(Math.Round(weatherNow.Main.Temp) + deg);
            feelLikeNow.Text = Convert.ToString(Math.Round(weatherNow.Main.Feels_like) + deg);

            Uri uri = getImage(weatherNow.Weather[0].Main);

            imgNow.Source = new BitmapImage(uri);

            for (int i = 0; i < 3; i++)
            {
                uri = getImage(weatherHourly.list[i].weather[0].Main);
                string gethour = weatherHourly.list[i].dt_txt;
                System.Windows.Controls.Image images = (System.Windows.Controls.Image)this.FindName("imgHours" + (i + 1));
                DateTime hour = DateTime.Parse(gethour);
                TextBlock times = (TextBlock)this.FindName("time" + (i + 1));
                Run tempes = (Run)this.FindName("temp" + (i + 1));
                Run feelsLikes = (Run)this.FindName("feelsLike" + (i + 1));
                images.Source = new BitmapImage(uri);
                times.Text = hour.ToString("HH:mm");
                tempes.Text = Convert.ToString(weatherHourly.list[i].weather[0].Description.Substring(0, 1).ToUpper() +
                                               weatherHourly.list[i].weather[0].Description.Substring(1) + 
                                               " " + Math.Round(weatherHourly.list[i].main.Temp) + deg);
                feelsLikes.Text = Convert.ToString("Ощущается как " + Math.Round(weatherHourly.list[i].main.Feels_like) + deg);
            }
        }

        private void ButtonStroke(object sender, RoutedEventArgs e)
        {
            Width = 1500;
        }

        private void WinSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ButtonVisibility();
        }

        private void ButtonVisibility()
        {

            if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                if (ActualWidth < 1400)
                {
                    dButton.Visibility = Visibility.Visible;
                }
                else
                {
                    dButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
