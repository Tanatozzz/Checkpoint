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
using Checkpoint.ViewModels;
using Checkpoint.Singletons;
using Checkpoint.API;
using Checkpoint.Models;
using Checkpoint.Windows;
using Checkpoint.Classes;

namespace Checkpoint.Pages
{
    /// <summary>
    /// Interaction logic for PrivacySettingPage.xaml
    /// </summary>
    public partial class PrivacySettingPage : Page
    {
        UpdateData up = new UpdateData();
        public PrivacySettingPage()
        {
            InitializeComponent();
            Refresh();
        }
        private async void Refresh()
        {
            up.Update();
            RoleLV.ItemsSource = AllRoleSingleton.Instance.Roles;
            EmployeeLV.ItemsSource = AllEmployeesSingleton.Instance.Employees;
        }

        private void ChangeAccessButton_Click(object sender, RoutedEventArgs e)
        {
            var role = (sender as FrameworkElement)?.DataContext as Role;
            if (role != null)
            {
                var accessWindow = new AccessWindow(role);
                accessWindow.ShowDialog();
                Refresh();
            }
        }

        private void ChangeAccessEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = (sender as FrameworkElement)?.DataContext as Employee;
            if (employee != null)
            {
                var accessWindow = new AccessWindow(employee);
                accessWindow.ShowDialog();
                Refresh();
            }
        }
    }
}
