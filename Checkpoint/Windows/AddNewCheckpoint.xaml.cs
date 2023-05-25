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
    /// Interaction logic for AddNewCheckpoint.xaml
    /// </summary>
    public partial class AddNewCheckpoint : Window
    {
        private UpdateData updateData;
        private bool isEditing = false;
        private Checkpointt checkpoint;
        public AddNewCheckpoint()
        {
            InitializeComponent();
            FillComboBox();
            isActiveComboBox.SelectedIndex = 0;
            updateData = new UpdateData();
        }

        public AddNewCheckpoint(Checkpointt checkpointt = null, bool isEditing = false)
        {
            InitializeComponent();
            FillComboBox();
            if (isEditing)
            {
                this.isEditing = true;
                this.checkpoint = checkpointt;
                FillFormWithCheckpointData(checkpointt);
            }
            updateData = new UpdateData();
        }
        private void FillComboBox()
        {
            List<string> isActiveValues = new List<string>
            {
                "Активен",
                "Не активен"
            };
            isActiveComboBox.ItemsSource = isActiveValues;
            var offices = AllOfficesSingleton.Instance.Offices;
            OfficeComboBox.ItemsSource = offices;
        }
        private void FillFormWithCheckpointData(Checkpointt check)
        {
            TitleTextBox.Text = checkpoint.Title;
            var offices = AllOfficesSingleton.Instance.Offices;
            OfficeComboBox.SelectedItem = offices.FirstOrDefault(i => i.ID == checkpoint.IDOffice);
            if (checkpoint.IsActive == true)
            {
                isActiveComboBox.SelectedIndex = 0;
            }
            else 
            { 
                isActiveComboBox.SelectedIndex = 1; 
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {

            HttpQuery httpmanager = new HttpQuery();
            if (isEditing)
            {
                bool active;
                if (isActiveComboBox.SelectedIndex == 0)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
                var updatedCheckpoint = new Checkpointt
                {
                    ID = checkpoint.ID,
                    Title = TitleTextBox.Text,
                    IDOffice = (int)((OfficeComboBox.SelectedItem as Office)?.ID),
                    IsActive = active
                };
                await httpmanager.UpdateCheckpoint(updatedCheckpoint);
            }
            else
            {
                bool active;
                if (isActiveComboBox.SelectedIndex == 0)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
                var newCheckpoint = new Checkpointt
                {
                    Title = TitleTextBox.Text,
                    IDOffice = (int)((OfficeComboBox.SelectedItem as Office)?.ID),
                    IsActive = active
                };
                // Выполните операцию добавления нового сотрудника
                await httpmanager.AddCheckpoint(newCheckpoint);
            }
            // Вызываем метод обновления данных в синглтонах
            bool isDataUpdated = await updateData.Update();

            if (isDataUpdated)
            {
                // Данные успешно обновлены
                if (isEditing)
                {
                    MessageBox.Show("Проход успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Проход успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
