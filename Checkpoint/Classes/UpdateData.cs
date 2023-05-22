using Checkpoint.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint.API;

namespace Checkpoint.Classes
{
    public class UpdateData
    {
        public async void Update()
        {
            try
            {
                HttpQuery httpManager = new HttpQuery();
                var allRoles = await httpManager.GetAllRoles();
                var allCheckpointRoles = await httpManager.GetAllCheckpointRoles();
                var allCheckpointAdditionalAccess = await httpManager.GetAllCheckpointAdditionalAccess();
                var allAdditionAccesses = await httpManager.GetAllAdditionAccess();
                var allEmployees = await httpManager.GetAllEmployees();
                var allOffices = await httpManager.GetAllOffices();
                var allCheckpoint = await httpManager.GetAllCheckpoints();
                AllCheckpointsSingleton.Instance.Checkpoints = allCheckpoint;
                AllOfficesSingleton.Instance.Offices = allOffices;
                AllEmployeesSingleton.Instance.Employees = allEmployees;
                AllRoleSingleton.Instance.Roles = allRoles;
                AllCheckpointRoleSingleton.Instance.CheckpointRoles = allCheckpointRoles;
                AllCheckpointAdditionalAccessSingleton.Instance.CheckpointAdditionalAccesses = allCheckpointAdditionalAccess;
                AllAdditionAccessSingleton.Instance.AdditionalAccesses = allAdditionAccesses;
            }
            catch (Exception ex)
            {
                // Обработка ошибки
                Console.WriteLine("Ошибка при загрузке данных: " + ex.Message);
                // Дополнительные действия по обработке ошибки...
            }

        }
    }
}
