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

namespace Checkpoint.Pages
{
    /// <summary>
    /// Interaction logic for PrivacySettingPage.xaml
    /// </summary>
    public partial class PrivacySettingPage : Page
    {
        public PrivacySettingPage()
        {
            InitializeComponent();
            RoleLV.ItemsSource = AllRoleSingleton.Instance.Roles;
            EmployeeLV.ItemsSource = AllEmployeesSingleton.Instance.Employees;
        }
        private void RoleLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRole = RoleLV.SelectedItem as Role;
            if (selectedRole != null)
            {
                var accessWindow = new AccessWindow(selectedRole);
                accessWindow.ShowDialog();
            }
        }

        private void EmployeeLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = EmployeeLV.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                var employeeAccessWindow = new AccessWindow(selectedEmployee);
                employeeAccessWindow.ShowDialog();
            }
        }

        private void ChangeAccessButton_Click(object sender, RoutedEventArgs e)
        {
            var role = (sender as FrameworkElement)?.DataContext as Role;
            if (role != null)
            {
                var accessWindow = new AccessWindow(role);
                accessWindow.ShowDialog();
            }
        }

        private void ChangeAccessEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = (sender as FrameworkElement)?.DataContext as Employee;
            if (employee != null)
            {
                var accessWindow = new AccessWindow(employee);
                accessWindow.ShowDialog();
            }
        }
    }
}
