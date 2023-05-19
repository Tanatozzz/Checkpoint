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
using System.Windows.Threading;
using Checkpoint.Pages;


// MinHeight="800" MaxHeight="800" MaxWidth="1200" MinWidth="1200"

namespace Checkpoint.Windows
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            UpdateTime();
            var EmployeePagee = new EmployeePage();
            MainFrame.Content = EmployeePagee;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            // Обновление TextBlock с текущим временем без даты
            tbTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Вызов метода обновления времени при загрузке окна
            UpdateTime();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMinimaze_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //private void ButtonMaximazed_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.WindowState != WindowState.Maximized)
        //    {
        //        this.WindowState = WindowState.Maximized;
        //        this.ResizeMode = ResizeMode.NoResize;
        //    }
        //    else
        //    {
        //        this.WindowState = WindowState.Normal;
        //    }
        //}

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnEmployeePage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOfficePage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckpointPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrivaceSettingsPage_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
