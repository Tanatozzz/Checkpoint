using Checkpoint.API;
using Checkpoint.Classes;
using Checkpoint.Singletons;
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

namespace Checkpoint.Windows
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private UpdateData updateData;
        private Employee employee;
        private bool isEditing = false;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            FillRoleComboBox();
            updateData = new UpdateData();
        }
        public AddEmployeeWindow(Employee employee = null, bool isEditing = false)
        {
            InitializeComponent();
            FillRoleComboBox();
            if (isEditing)
            {
                this.isEditing = true;
                // Заполните поля формы данными сотрудника
                this.employee = employee;
                FillFormWithEmployeeData(employee);
            }
            
            updateData = new UpdateData();
        }

        private void FillFormWithEmployeeData(Employee employee)
        {
            FirstNameTextBox.Text = employee.FirstName;
            LastNameTextBox.Text = employee.LastName;
            PatronomycTextBox.Text = employee.Patronomyc;
            PassportSeriesTextBox.Text = employee.PassportSeries;
            PassportNumberTextBox.Text = employee.PassportNumber;
            INNTextBox.Text = employee.INN;
            LoginTextBox.Text = employee.Login;
            PasswordTextBox.Text = employee.Password;
            var roles = AllRoleSingleton.Instance.Roles;
            RoleComboBox.SelectedItem = roles.FirstOrDefault(role => role.ID == employee.IDRole);
        }

        private void FillRoleComboBox()
        {
            var roles = AllRoleSingleton.Instance.Roles;
            RoleComboBox.ItemsSource = roles;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PassportSeriesTextBox.Text, out int result))
            {
                MessageBox.Show("Ошибка, поле серии паспорта должно включать в себя только цифры ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(PassportNumberTextBox.Text, out int result1))
            {
                MessageBox.Show("Ошибка, поле номер паспорта должно включать в себя только цифры ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if (!int.TryParse(INNTextBox.Text, out int result2))
            //{
            //    MessageBox.Show("Ошибка, поле ИНН должно включать в себя только цифры ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            HttpQuery httpmanager = new HttpQuery();
            // Добавляем\изменяем сотрудника в БД
            if (isEditing)
            {
                var updatedEmployee = new Employee
                {
                    ID = employee.ID,
                    FirstName = FirstNameTextBox.Text,
                    Patronomyc = PatronomycTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    LastVisitDate = employee.LastVisitDate,
                    isInside = employee.isInside,
                    IDAdditionAccess = employee.IDAdditionAccess,
                    IDRole = (int)((RoleComboBox.SelectedItem as Role)?.ID),
                    PassportSeries = PassportSeriesTextBox.Text,
                    PassportNumber = PassportNumberTextBox.Text,
                    INN = INNTextBox.Text,
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text
                };
                // Выполните операцию изменения данных сотрудника
                await httpmanager.UpdateEmployee(updatedEmployee);
            }
            else
            {
                var newEmployee = new Employee
                {
                    FirstName = FirstNameTextBox.Text,
                    Patronomyc = PatronomycTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    isInside = false,
                    IDRole = (int)((RoleComboBox.SelectedItem as Role)?.ID),
                    PassportSeries = PassportSeriesTextBox.Text,
                    PassportNumber = PassportNumberTextBox.Text,
                    INN = INNTextBox.Text,
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text
                };
                // Выполните операцию добавления нового сотрудника
                await httpmanager.AddEmployee(newEmployee);
            }
            // Вызываем метод обновления данных в синглтонах
            bool isDataUpdated = await updateData.Update();

            if (isDataUpdated)
            {
                // Данные успешно обновлены
                if (isEditing) {
                    MessageBox.Show("Сотрудник успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Close();
            }
            else
            {
                // Обработка ошибки при обновлении данных
                // Дополнительные действия по обработке ошибки...
            }
        }
    }
}
