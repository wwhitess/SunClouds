using LiveChartsCore.Kernel;
using SunClouds.Helpers;
using SunClouds.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
using System.Xml.Linq;

namespace SunClouds
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private string TypeTemp;
        private string CurrentCity;
        List<FavoriteCity> CityList = new List<FavoriteCity>();
        public SettingsPage()
        {
            InitializeComponent();
            GetCityList();
        }
        public void SetSettings(string TTemp, string city)
        {
            CurrentCity = city;
            TypeTemp = TTemp;
            SetCity.Text = Properties.Settings.Default.DefaultCity;
            if (TTemp == "metric") { celsiusType.IsChecked = true; }
            else { fahrenheitType.IsChecked = true; }
        }

        private void ClearBox(object sender, RoutedEventArgs e)
        {
            SetCity.Clear();
        }
        private void ClearAddBox(object sender, RoutedEventArgs e)
        {
            FavoriteCityBox.Clear();
        }

        private void AddFavoriteCity_click(object sender, RoutedEventArgs e)
        {
            if (FavoriteCityBox.Text == "" || FavoriteCityBox.Text.Length <= 2)
            {
                MessageBox.Show("Выберите город");
            }
            else
            {
                var json = ApiHelper.Get(FavoriteCityBox.Text, TypeTemp);
                var result = DerSerLib.jsonclass.JsonDeser<WeatherModel>(json);
                double lon = result.Coord.Lon;
                double lat = result.Coord.Lat;
                FavoriteCity city = new FavoriteCity(FavoriteCityBox.Text, lon, lat);
                CityList.Add(city);

                Grid grid = new Grid();
                grid.MouseDown += (sender, e) =>
                {

                    toGetWeather(city.Name, TypeTemp);
                };
                grid.Height = 66;
                grid.Width = 195;
                grid.Margin = new Thickness(10, 10, 30, 10);

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                Rectangle rectangle1 = new Rectangle();

                rectangle1.Style = (Style)FindResource("SetCity");

                Grid.SetRow(rectangle1, 0);
                Grid.SetColumn(rectangle1, 0);
                Grid.SetColumnSpan(rectangle1, 2);

                grid.Children.Add(rectangle1);

                Rectangle rectangle2 = new Rectangle();

                rectangle2.Style = (Style)FindResource("SetCity");
                rectangle2.Opacity = 0.5;

                Grid.SetRow(rectangle2, 1);
                Grid.SetColumn(rectangle2, 0);
                Grid.SetColumnSpan(rectangle2, 2);

                grid.Children.Add(rectangle2);

                Button button = new Button();

                button.Click += DeleteBlock;
                button.Style = (Style)FindResource("ClearBut");
                button.Opacity = 1;
                button.FontWeight = FontWeights.Bold;
                button.Width = 25;
                button.Height = 25;
                button.Content = "x";
                button.FontSize = 17;
                button.Margin = new Thickness(170, -2, 0, 0);
                button.VerticalAlignment = VerticalAlignment.Top;
                button.HorizontalAlignment = HorizontalAlignment.Left;

                Grid.SetRow(button, 0);
                Grid.SetColumn(button, 0);
                Grid.SetColumnSpan(button, 2);

                grid.Children.Add(button);

                TextBlock blockCity = new TextBlock();

                blockCity.Name = "BlockCity";
                blockCity.Text = FavoriteCityBox.Text;
                blockCity.HorizontalAlignment = HorizontalAlignment.Center;
                blockCity.TextAlignment = TextAlignment.Center;
                blockCity.VerticalAlignment = VerticalAlignment.Center;
                blockCity.Style = (Style)FindResource("TextBlock");
                blockCity.FontSize = 18;
                blockCity.FontWeight = FontWeights.Bold;

                Grid.SetRow(blockCity, 0);
                Grid.SetColumn(blockCity, 0);
                Grid.SetColumnSpan(blockCity, 2);

                grid.Children.Add(blockCity);

                TextBlock blockCord = new TextBlock();

                blockCord.Name = "BlockCord";
                blockCord.Text = $"{lon}, с.ш {lat} в.э";
                blockCord.FontSize = 15;
                blockCord.Style = (Style)FindResource("TextBlock");
                blockCord.FontWeight = FontWeights.Thin;
                blockCord.TextAlignment = TextAlignment.Center;
                blockCord.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetRow(blockCord, 1);
                Grid.SetColumn(blockCord, 0);
                Grid.SetColumnSpan(blockCord, 2);

                grid.Children.Add(blockCord);

                WrapPanel.Children.Add(grid);

                FavoriteCityBox.Text = "";
            }
        }
       
        private void GetCityList()
        {
            int count = 0;
            CityList = DerSerLib.jsonclass.FileDeser(CityList, "FavoriteCity");

            foreach (FavoriteCity data in CityList)
            {
                count++;
                string name = data.Name;
                double lon = data.Lon;
                double lat = data.Lat;
                Grid grid = new Grid();
                grid.Height = 66;
                grid.Width = 195;
                grid.Margin = new Thickness(10, 10, 30, 10);
                grid.MouseDown += (sender, e) =>
                {
                    toGetWeather(name, TypeTemp);
                };

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                Rectangle rectangle1 = new Rectangle();

                rectangle1.Style = (Style)FindResource("SetCity");

                Grid.SetRow(rectangle1, 0);
                Grid.SetColumn(rectangle1, 0);
                Grid.SetColumnSpan(rectangle1, 2);

                grid.Children.Add(rectangle1);

                Rectangle rectangle2 = new Rectangle();

                rectangle2.Style = (Style)FindResource("SetCity");
                rectangle2.Opacity = 0.5;

                Grid.SetRow(rectangle2, 1);
                Grid.SetColumn(rectangle2, 0);
                Grid.SetColumnSpan(rectangle2, 2);

                grid.Children.Add(rectangle2);

                Button button = new Button();

                button.Click += DeleteBlock;
                button.Style = (Style)FindResource("ClearBut");
                button.Opacity = 1;
                button.FontWeight = FontWeights.Bold;
                button.Width = 25;
                button.Height = 25;
                button.Content = "x";
                button.FontSize = 17;
                button.Margin = new Thickness(170, -2, 0, 0);
                button.VerticalAlignment = VerticalAlignment.Top;
                button.HorizontalAlignment = HorizontalAlignment.Left;

                Grid.SetRow(button, 0);
                Grid.SetColumn(button, 0);
                Grid.SetColumnSpan(button, 2);

                grid.Children.Add(button);

                TextBlock blockCity = new TextBlock();

                blockCity.Name = "BlockCity";
                blockCity.Text = name;
                blockCity.HorizontalAlignment = HorizontalAlignment.Center;
                blockCity.TextAlignment = TextAlignment.Center;
                blockCity.VerticalAlignment = VerticalAlignment.Center;
                blockCity.Style = (Style)FindResource("TextBlock");
                blockCity.FontSize = 18;
                blockCity.FontWeight = FontWeights.Bold;

                Grid.SetRow(blockCity, 0);
                Grid.SetColumn(blockCity, 0);
                Grid.SetColumnSpan(blockCity, 2);

                grid.Children.Add(blockCity);

                TextBlock blockCord = new TextBlock();

                blockCord.Name = "BlockCord";
                blockCord.Text = $"{lon}, с.ш {lat} в.э";
                blockCord.FontSize = 15;
                blockCord.Style = (Style)FindResource("TextBlock");
                blockCord.FontWeight = FontWeights.Thin;
                blockCord.TextAlignment = TextAlignment.Center;
                blockCord.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetRow(blockCord, 1);
                Grid.SetColumn(blockCord, 0);
                Grid.SetColumnSpan(blockCord, 2);


                grid.Children.Add(blockCord);

                WrapPanel.Children.Add(grid);
            }
        }
        private void DeleteBlock(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid grid = (Grid)button.Parent;
            int index = WrapPanel.Children.IndexOf(grid);
            WrapPanel.Children.Remove(grid);
            CityList.RemoveAt(index);
        }
        private void toGetWeather(string city, string TType)
        {
            Weather existingWindow = Application.Current.Windows.Cast<Weather>().FirstOrDefault(window => window is Weather);
            existingWindow.getWeather(city, TType);
        }

        private void celsius_click(object sender, RoutedEventArgs e)
        {
            fahrenheitType.IsChecked = false;
            TypeTemp = "metric";
            toGetWeather(CurrentCity, TypeTemp);
        }

        private void fahrenheit_click(object sender, RoutedEventArgs e)
        {
            celsiusType.IsChecked = false;
            TypeTemp = "imperial";
            toGetWeather(CurrentCity, TypeTemp);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DefaultTType = TypeTemp; //Сохранение текущего формата температуры
            DerSerLib.jsonclass.JsonSer(CityList, "FavoriteCity"); //сохранение изменений в списке избранных городов.
            if (SetCity.Text == "" || SetCity.Text.Length <= 2) //сохранение нового города по умолчанию.
            {
                MessageBox.Show("Выберите город");
            }
            else
            {
                Properties.Settings.Default.DefaultCity = SetCity.Text; 
                Properties.Settings.Default.Save();
            }
        }
    }
}
