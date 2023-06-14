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
using Checkpoint.Models;

namespace Checkpoint.Pages
{
    public partial class EmployeePage : Page, IListViewSearchable
    {
        UpdateData updateData = new UpdateData();
        public EmployeePage()
        {
            InitializeComponent();
            DataRefresh();
        }
        public void SearchListView(string searchText)
        {
            // Фильтрация списка сотрудников по введенному тексту
            var filteredEmployees = AllEmployeesSingleton.Instance.Employees.Where(emp =>
                emp.FirstName.Contains(searchText) ||
                emp.LastName.Contains(searchText) ||
                emp.Patronomyc.Contains(searchText) ||
                emp.Login.Contains(searchText));

            // Обновление источника данных для ListView
            EmployeeLV.ItemsSource = filteredEmployees;
        }
        private async void DataRefresh()
        {
            await updateData.Update();
            var allEmployees = AllEmployeesSingleton.Instance.Employees;
            var currentEmployee = EmployeeSingleton.Instance.Employee;

            foreach (var employee in allEmployees)
            {
                employee.IsCurrentEmployee = (employee.ID == currentEmployee.ID);
            }
            EmployeeLV.ItemsSource = allEmployees;
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
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
                    await httpmanager.UpdateEmployee(selectedEmployee);
                }
            }
            DataRefresh();
        }
        private async void DeleteButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Employee selectedEmployee)
            {
                if (selectedEmployee.ID == EmployeeSingleton.Instance.Employee.ID)
                {
                    MessageBox.Show("Ошибка, нельзя удалить свой профиль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        HttpQuery httpManager = new HttpQuery();
                        await httpManager.DeleteEmployee(selectedEmployee.ID);
                        MessageBox.Show("Сотрудник удален", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    return;
                }
            }
            DataRefresh();
        }
    }

}
