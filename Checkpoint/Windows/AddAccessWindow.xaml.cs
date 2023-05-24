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
    /// Interaction logic for AddAccessRoleWindow.xaml
    /// </summary>
    public partial class AddAccessWindow : Window
    {
        object target;
        UpdateData up = new UpdateData();
        public AddAccessWindow(object target)
        {
            InitializeComponent();
            this.target = target;
            LoadCheckpoints(); // Загрузка списка дверей
        }

        private async void LoadCheckpoints()
        {
            up.Update();
            CheckpointListView.ItemsSource = AllCheckpointsSingleton.Instance.Checkpoints;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button addButton = (Button)sender;
                Checkpointt selectedCheckpoint = (Checkpointt)addButton.DataContext;
                HttpQuery httpmanager = new HttpQuery();
                if (target is Role role)
                {
                    // Логика добавления доступа роли к выбранной двери
                    await httpmanager.AddCheckpointRole(role.ID, selectedCheckpoint.ID);
                }
                else if (target is Employee employee)
                {
                    // Логика добавления доступа сотруднику к выбранной двери
                    await httpmanager.AddCheckpointAdditionalAccess(employee.IDAdditionAccess, selectedCheckpoint.ID);
                }
                else
                {
                    // Обработка неподдерживаемого типа
                    throw new ArgumentException("Неподдерживаемый тип объекта");
                }

                // Оповестить пользователя об успешном добавлении доступа
                MessageBox.Show("Доступ успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Закрыть окно после добавления доступа
                this.Close();
            }
            catch (Exception ex)
            {
                // Обработка ошибок при добавлении доступа
                MessageBox.Show("Ошибка при добавлении доступа: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
