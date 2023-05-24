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

        public AddEmployeeWindow()
        {
            InitializeComponent();
            FillRoleComboBox();
            updateData = new UpdateData();
        }

        private void FillRoleComboBox()
        {
            var roles = AllRoleSingleton.Instance.Roles;
            RoleComboBox.ItemsSource = roles;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PassportSeriesTextBox.Text, out int result))
            {
                MessageBox.Show("Ошибка, поле серии паспорта должно включать в себя только цифры ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (int.TryParse(PassportNumberTextBox.Text, out int result1))
            {
                MessageBox.Show("Ошибка, поле серии паспорта должно включать в себя только цифры ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (int.TryParse(INNTextBox.Text, out int result2))
            {
                MessageBox.Show("Ошибка, поле серии паспорта должно включать в себя только цифры ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            // Добавляем нового сотрудника в БД
            HttpQuery httpmanager = new HttpQuery();
            await httpmanager.AddEmployee(newEmployee);

            // Вызываем метод обновления данных в синглтонах
            bool isDataUpdated = await updateData.Update();

            if (isDataUpdated)
            {
                // Данные успешно обновлены
                MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
