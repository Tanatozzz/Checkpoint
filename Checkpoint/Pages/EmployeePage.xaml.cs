using System;
using System.Collections.Generic;
using System.Globalization;
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
using Checkpoint.Singletons;
using Checkpoint.Converters;
using Checkpoint.API;
using Checkpoint.Windows;
using Checkpoint.Classes;

namespace Checkpoint.Pages
{
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            UpdateData updateData = new UpdateData();
            var allEmployees = AllEmployeesSingleton.Instance.Employees;
            var currentEmployee = EmployeeSingleton.Instance.Employee;

            foreach (var employee in allEmployees)
            {
                employee.IsCurrentEmployee = (employee.ID == currentEmployee.ID);
            }

            DataRefresh();
        }

        private async void DataRefresh()
        {
            var allEmployees = AllEmployeesSingleton.Instance.Employees;
            EmployeeLV.ItemsSource = allEmployees;
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();

            if (addEmployeeWindow.DialogResult.HasValue && addEmployeeWindow.DialogResult.Value)
            {
                // Добавляем нового сотрудника в список и обновляем отображение
            }
            DataRefresh();
        }
    }
}
