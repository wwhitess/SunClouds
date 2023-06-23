using SunClouds.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using LiveCharts;
using System.Timers;
using LiveCharts.Wpf;

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
            StackHour.Children.Clear();

            for (int i = 0; i < weatherHourly.list.Count; i++)
            {
                string getHour = weatherHourly.list[i].dt_txt;
                DateTime hour = DateTime.Parse(getHour);

                Grid grid = new Grid();
                grid.MinHeight = 170;
                grid.MinWidth = 110;
                grid.Margin = new Thickness(25, 18, 12, 21);

                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Rectangle lineRectangle1 = new Rectangle();
                lineRectangle1.Style = (Style)FindResource("LineRectangle");
                lineRectangle1.Opacity = 0.5;
                Grid.SetColumnSpan(lineRectangle1, 2);
                Grid.SetColumn(lineRectangle1, 0);
                Grid.SetRowSpan(lineRectangle1, 3);
                grid.Children.Add(lineRectangle1);

                Rectangle rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                rectangle.Height = 5;
                Grid.SetRow(rectangle, 3);
                Grid.SetColumnSpan(rectangle, 2);
                rectangle.Opacity = 0.07;
                grid.Children.Add(rectangle);

                Rectangle lineRectangle2 = new Rectangle();
                lineRectangle2.Style = (Style)FindResource("LineRectangle");
                lineRectangle2.Opacity = 0.5;
                Grid.SetRow(lineRectangle2, 1);
                Grid.SetColumnSpan(lineRectangle2, 2);
                Grid.SetColumn(lineRectangle2, 0);
                grid.Children.Add(lineRectangle2);

                StackPanel imageStackPanel = new StackPanel();
                imageStackPanel.Margin = new Thickness(27, 4, 23, 4);
                imageStackPanel.VerticalAlignment = VerticalAlignment.Center;

                Image image = new Image();
                Uri weatherIcon;

                switch (weatherHourly.list[i].weather[0].Main)
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


                image.Source = new BitmapImage(weatherIcon);
                image.MaxWidth = 45;
                imageStackPanel.Children.Add(image);
                Grid.SetRow(imageStackPanel, 0);
                Grid.SetColumnSpan(imageStackPanel, 2);
                Grid.SetColumn(imageStackPanel, 0);
                grid.Children.Add(imageStackPanel);

                StackPanel timeStackPanel = new StackPanel();
                timeStackPanel.VerticalAlignment = VerticalAlignment.Center;

                TextBlock timeTextBlock = new TextBlock();
                timeTextBlock.Name = "time" + (i + 1);
                timeTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                timeTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                timeTextBlock.FontSize = 20;
                timeTextBlock.Style = (Style)FindResource("TextBlock");
                timeTextBlock.Text = hour.ToString("HH:mm");
                timeStackPanel.Children.Add(timeTextBlock);

                TextBlock tempTextBlock = new TextBlock();
                tempTextBlock.Name = "temp" + (i + 1);
                tempTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                tempTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                tempTextBlock.FontSize = 26;
                tempTextBlock.FontWeight = FontWeights.Bold;
                tempTextBlock.Style = (Style)FindResource("TextBlock");
                tempTextBlock.Text = Math.Round(weatherHourly.list[i].main.Temp) + "°";
                timeStackPanel.Children.Add(tempTextBlock);

                Grid.SetRow(timeStackPanel, 1);
                Grid.SetColumn(timeStackPanel, 0);
                Grid.SetColumnSpan(timeStackPanel, 2);
                grid.Children.Add(timeStackPanel);

                StackPanel humidityStackPanel = new StackPanel();
                humidityStackPanel.VerticalAlignment = VerticalAlignment.Center;

                TextBlock humidityTextBlock = new TextBlock();
                humidityTextBlock.TextAlignment = TextAlignment.Center;
                humidityTextBlock.VerticalAlignment = VerticalAlignment.Center;
                humidityTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                humidityTextBlock.FontSize = 16;
                humidityTextBlock.Style = (Style)FindResource("TextBlock");
                humidityTextBlock.Inlines.Add(new Run() { Text = "Влаж." });
                humidityTextBlock.Inlines.Add(new LineBreak());
                humidityTextBlock.Inlines.Add(new Run() { Text = weatherHourly.list[i].main.Humidity + "%" });
                humidityStackPanel.Children.Add(humidityTextBlock);
                Grid.SetColumn(humidityStackPanel, 0);
                Grid.SetRow(humidityStackPanel, 2);
                grid.Children.Add(humidityStackPanel);

                StackPanel feelsLikeStackPanel = new StackPanel();
                feelsLikeStackPanel.VerticalAlignment = VerticalAlignment.Center;

                TextBlock feelsLikeTextBlock = new TextBlock();
                feelsLikeTextBlock.TextAlignment = TextAlignment.Center;
                feelsLikeTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                feelsLikeTextBlock.FontSize = 16;
                feelsLikeTextBlock.VerticalAlignment = VerticalAlignment.Center;
                feelsLikeTextBlock.Style = (Style)FindResource("TextBlock");
                feelsLikeTextBlock.Inlines.Add(new Run() { Text = "Ощущ." });
                feelsLikeTextBlock.Inlines.Add(new LineBreak());
                feelsLikeTextBlock.Inlines.Add(new Run() { Text = Math.Round(weatherHourly.list[i].main.Feels_like) + "°" });
                feelsLikeStackPanel.Children.Add(feelsLikeTextBlock);
                Grid.SetColumn(feelsLikeStackPanel, 1);
                Grid.SetRow(feelsLikeStackPanel, 2);
                grid.Children.Add(feelsLikeStackPanel);

                StackHour.Children.Add(grid);

            }
            TemperatureData = new List<ISeries>
            {
              new LineSeries<double> { Values = new ChartValues<double> { 
                  weatherHourly.list[0].main.Temp,
                  weatherHourly.list[1].main.Temp, 
                  weatherHourly.list[2].main.Temp, 
                  weatherHourly.list[3].main.Temp, 
                  weatherHourly.list[4].main.Temp,
                  weatherHourly.list[5].main.Temp,
                  weatherHourly.list[6].main.Temp,
                  weatherHourly.list[7].main.Temp} 
              }
            };
        }
        private async void AsyncEvent(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour % 2 == 0 && DateTime.Now.Minute == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    SetNow();
                    SetHourly();
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
    }
}