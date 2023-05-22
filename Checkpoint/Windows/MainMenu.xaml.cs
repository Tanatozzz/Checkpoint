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

        private async void btnEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            HttpQuery httpManager = new HttpQuery();
            var allEmployees = await httpManager.GetAllEmployees();
            AllEmployeesSingleton.Instance.Employees = allEmployees;
            var page = new EmployeePage();

            MainFrame.Content = page;
        }

        private async void btnOfficePage_Click(object sender, RoutedEventArgs e)
        {
            HttpQuery httpManager = new HttpQuery();
            var allOffices = await httpManager.GetAllOffices();
            AllOfficesSingleton.Instance.Offices = allOffices;
            var page = new OfficePage();

            MainFrame.Content = page;
        }

        private async void btnCheckpointPage_Click(object sender, RoutedEventArgs e)
        {
            HttpQuery httpManager = new HttpQuery();
            var allCheckpoint = await httpManager.GetAllCheckpoints();
            AllCheckpointsSingleton.Instance.Checkpoints = allCheckpoint;
            var page = new CheckpointPage();

            MainFrame.Content = page;
        }

        private void btnPrivaceSettingsPage_Click(object sender, RoutedEventArgs e)
        {

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
