using Checkpoint.API;
using Checkpoint.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Checkpoint.Classes;
using System.Data;

namespace Checkpoint.Windows
{
    /// <summary>
    /// Логика взаимодействия для AccessWindow.xaml
    /// </summary>
    public partial class AccessWindow : Window
    {
        UpdateData up = new UpdateData();
        private object target;
        public AccessWindow(object target)
        {
            InitializeComponent();
            this.target = target;
            UpdateData();
        }

        private async void UpdateData() {
            await up.Update();
            if (target is Role role)
            {
                var checkpointRoles = AllCheckpointRoleSingleton.Instance.CheckpointRoles.Where(i => i.IDRole == role.ID);
                CheckpointRoleLV.ItemsSource = checkpointRoles;
            }
            else if (target is Employee employee)
            {
                var checkpointAccesses = AllCheckpointAdditionalAccessSingleton.Instance.CheckpointAdditionalAccesses.Where(i => i.IDAdditionalAccess == employee.IDAdditionAccess);
                CheckpointRoleLV.ItemsSource = checkpointAccesses;
            }
        }
        private async void DeleteAccessButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            HttpQuery httpmanager = new HttpQuery();
            if (target is Employee employee)
            {
                CheckpointAdditionalAccess checkpointRole = (CheckpointAdditionalAccess)button.Tag;
                int checkpointID = checkpointRole.IDCheckpoint;
                await httpmanager.DeleteCheckpointAdditionalAccess(employee.IDAdditionAccess, checkpointID);
                UpdateData();
            }
            else if (target is Role role)
            {
                CheckpointRole checkpointRole = (CheckpointRole)button.Tag;
                int checkpointID = checkpointRole.IDCheckpoint;
                await httpmanager.DeleteCheckpointRole(role.ID, checkpointID);
                UpdateData();
            }
            else
            {
                // Обработка неподдерживаемого типа
                throw new ArgumentException("Неподдерживаемый тип объекта");
            }

            // Вызываем метод обновления данных
            UpdateData();
        }

        private async void AddAccessButton_Click(object sender, RoutedEventArgs e)
        {
            var addaccessWindow = new AddAccessWindow(target);
            addaccessWindow.ShowDialog();
            UpdateData();
            // Логика добавления нового доступа
        }
    }
}
