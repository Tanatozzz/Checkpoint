using Checkpoint.Singletons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint.ViewModels
{
    public class AccessManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Role> _roles;
        private ObservableCollection<Checkpointt> _checkpoints;
        private ObservableCollection<CheckpointRole> _accessList;

        public ObservableCollection<Role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Checkpointt> Checkpoints
        {
            get { return _checkpoints; }
            set
            {
                _checkpoints = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CheckpointRole> AccessList
        {
            get { return _accessList; }
            set
            {
                _accessList = value;
                OnPropertyChanged();
            }
        }

        public AccessManagementViewModel()
        {
            // Получите данные из синглтонов и заполните коллекции Roles и Checkpoints

            // Пример получения данных из синглтона RoleSingleton
            Roles = new ObservableCollection<Role>(AllRoleSingleton.Instance.Roles);

            // Пример получения данных из синглтона CheckpointSingleton
            Checkpoints = new ObservableCollection<Checkpointt>(AllCheckpointsSingleton.Instance.Checkpoints);

            // Заполните коллекцию AccessList данными
            AccessList = new ObservableCollection<CheckpointRole>();

            // Пример заполнения AccessList из синглтона CheckpointRoleSingleton
            foreach (var checkpointRole in AllCheckpointRoleSingleton.Instance.CheckpointRoles)
            {
                AccessList.Add(checkpointRole);
            }
        }

        // Метод для добавления доступа роли к проходу
        public void AddAccess(Role role, Checkpointt checkpoint)
        {
            // Реализуйте логику добавления доступа роли к проходу
            // Используйте соответствующие методы для добавления доступа

            // Пример использования вашего метода для добавления доступа
            //AllCheckpointRoleSingleton.Instance.AddCheckpointRole(role.ID, checkpoint.ID);

            // Обновите AccessList с новыми данными
            AccessList = new ObservableCollection<CheckpointRole>(AllCheckpointRoleSingleton.Instance.CheckpointRoles);
        }

        // Метод для удаления доступа роли от прохода
        public void RemoveAccess(Role role, Checkpointt checkpoint)
        {
            // Реализуйте логику удаления доступа роли от прохода
            // Используйте соответствующие методы для удаления доступа

            // Пример использования вашего метода для удаления доступа
            //AllCheckpointRoleSingleton.Instance.DeleteCheckpointRole(role.ID, checkpoint.ID);

            // Обновите AccessList с новыми данными
            AccessList = new ObservableCollection<CheckpointRole>(AllCheckpointRoleSingleton.Instance.CheckpointRoles);
        }

        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
