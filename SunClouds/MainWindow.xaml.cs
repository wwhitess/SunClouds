using Microsoft.Win32;
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
using ThemeLib;
using DerSerLib;

namespace SunClouds
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SystemEvents.TimeChanged += ThemeSelectTime;

            SelectCity.Text = Properties.Settings.Default.DefaultCity;

            SelectTheme();
        }

        private void ThemeSelectTime(object sender, EventArgs e)
        {
            SelectTheme();
        }
        private void SelectTheme()
        {
            int curHour = DateTime.Now.Hour;

            if (curHour >= 0 && curHour < 4)
            {
                CustomControl1.ThirdTheme();
            }
            else if (curHour >= 4 && curHour < 12)
            {
                CustomControl1.FirstTheme();
            }
            else
            {
                CustomControl1.SecondTheme();
            }
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

        private void WindowTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Point mousePos = e.GetPosition(WindowTitleBar);

                if (mousePos.Y <= WindowTitleBar.RowDefinitions[0].ActualHeight && mousePos.X >= WindowTitleBar.ColumnDefinitions[0].Offset && mousePos.X <= WindowTitleBar.ColumnDefinitions[0].Offset + WindowTitleBar.ColumnDefinitions[0].ActualWidth * 7)
                {
                    this.DragMove();
                }
            }
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            string TTemp = Properties.Settings.Default.DefaultTType;
            if (SelectCity.Text == "" ||
                SelectCity.Text == "Выберите город") { MessageBox.Show("Выберете город"); }
            else
            {
                Weather weather = new Weather();
                weather.getWeather(SelectCity.Text, TTemp);
                weather.Show();
                Close();
            }
        }
        private void ClearBox(object sender, RoutedEventArgs e)
        {
            SelectCity.Clear();
        }
    }
}
