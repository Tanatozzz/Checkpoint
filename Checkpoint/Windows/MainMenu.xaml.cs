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
using Checkpoint.Singletons;
using Checkpoint.API;
using Checkpoint.Models;
using System.Net.PeerToPeer;
using Checkpoint.Classes;

namespace Checkpoint.Windows
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        UpdateData up = new UpdateData();
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

        private async void btnEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            bool dataLoaded = await up.Update();

            if (dataLoaded)
            {
                var page = new EmployeePage();

                MainFrame.Content = page;
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void btnOfficePage_Click(object sender, RoutedEventArgs e)
        {
            bool dataLoaded = await up.Update();

            if (dataLoaded)
            {
                var page = new OfficePage();

                MainFrame.Content = page;
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void btnCheckpointPage_Click(object sender, RoutedEventArgs e)
        {
            bool dataLoaded = await up.Update();

            if (dataLoaded)
            {
                var page = new CheckpointPage();

                MainFrame.Content = page;
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void btnPrivaceSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            bool dataLoaded = await up.Update();

            if (dataLoaded)
            {
                var page = new PrivacySettingPage();

                MainFrame.Content = page;
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = EmployeeSingleton.Instance.Employee; // Получаем экземпляр класса Employee из синглтона

            // Создаем строку с данными из синглтона
            string message = $"ID: {employee.ID}\n" +
                             $"First Name: {employee.FirstName}\n" +
                             $"Patronomyc: {employee.Patronomyc}\n" +
                             $"Last Name: {employee.LastName}\n" +
                             $"Last Visit Date: {employee.LastVisitDate}\n" +
                             $"Is Inside: {employee.isInside}\n" +
                             $"ID Role: {employee.IDRole}\n" +
                             $"ID Addition Access: {employee.IDAdditionAccess}\n" +
                             $"Passport Series: {employee.PassportSeries}\n" +
                             $"Passport Number: {employee.PassportNumber}\n" +
                             $"INN: {employee.INN}\n" +
                             $"Login: {employee.Login}\n" +
                             $"Password: {employee.Password}";

            // Отображаем всплывающее оповещение с данными из синглтона
            MessageBox.Show(message, "Employee Profile", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
