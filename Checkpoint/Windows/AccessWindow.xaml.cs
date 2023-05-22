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

namespace Checkpoint.Windows
{
    /// <summary>
    /// Логика взаимодействия для AccessWindow.xaml
    /// </summary>
    public partial class AccessWindow : Window
    {
        UpdateData up = new UpdateData();
        private Role role;
        public AccessWindow(Role role)
        {
            InitializeComponent();
            this.role = role;
            // Получить доступные проходы для указанной роли из синглтона
            var checkpointRoles = AllCheckpointRoleSingleton.Instance.CheckpointRoles.Where(i => i.IDRole == role.ID);

            // Установить источник данных для ListView
            CheckpointRoleLV.ItemsSource = checkpointRoles;
        }
        private void DeleteAccessButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика удаления доступа
            Button button = (Button)sender;
            CheckpointRole checkpointRole = (CheckpointRole)button.Tag;
            int roleID = role.ID;
            int checkpointID = checkpointRole.IDCheckpoint;
            HttpQuery httpmanager = new HttpQuery();
            httpmanager.DeleteCheckpointRole(roleID, checkpointID);
            up.Update();

        }

        private void AddAccessButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика добавления нового доступа
        }
    }
}
