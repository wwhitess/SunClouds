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

namespace SunClouds
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SystemEvents.TimeChanged += ThemeSelectTime;

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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
  
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
