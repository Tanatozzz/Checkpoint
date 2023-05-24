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
        UpdateData updateData = new UpdateData();
        public EmployeePage()
        {
            InitializeComponent();
            DataRefresh();
        }

        private void DataRefresh()
        {
            var allEmployees = AllEmployeesSingleton.Instance.Employees;
            var currentEmployee = EmployeeSingleton.Instance.Employee;

            foreach (var employee in allEmployees)
            {
                employee.IsCurrentEmployee = (employee.ID == currentEmployee.ID);
            }
            EmployeeLV.ItemsSource = allEmployees;
        }
        private async void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();

            if (addEmployeeWindow.DialogResult.HasValue && addEmployeeWindow.DialogResult.Value)
            {
                // Добавляем нового сотрудника в список и обновляем отображение
            }
            await updateData.Update();
            DataRefresh();
        }

        private async void EmployeeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Employee selectedEmployee)
            {
                var editEmployeeWindow = new AddEmployeeWindow(selectedEmployee, true);
                editEmployeeWindow.AddButton.Content = "Изменить";
                if(selectedEmployee.ID == EmployeeSingleton.Instance.Employee.ID)
                {
                    MessageBox.Show("Ошибка, нельзя изменять свой профиль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                editEmployeeWindow.ShowDialog();

                if (editEmployeeWindow.DialogResult.HasValue && editEmployeeWindow.DialogResult.Value)
                {
                    HttpQuery httpmanager = new HttpQuery();
                    // Обновите данные сотрудника после редактирования
                    await httpmanager.UpdateEmployee(selectedEmployee);
                }
            }
            await updateData.Update();
            DataRefresh();
        }
    }

}
