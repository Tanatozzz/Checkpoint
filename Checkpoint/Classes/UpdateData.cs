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
        public async Task<bool> Update()
        {
            try
            {
                HttpQuery httpManager = new HttpQuery();

                var allRolesTask = httpManager.GetAllRoles();
                var allCheckpointRolesTask = httpManager.GetAllCheckpointRoles();
                var allCheckpointAdditionalAccessTask = httpManager.GetAllCheckpointAdditionalAccess();
                var allAdditionAccessesTask = httpManager.GetAllAdditionAccess();
                var allEmployeesTask = httpManager.GetAllEmployees();
                var allOfficesTask = httpManager.GetAllOffices();
                var allCheckpointTask = httpManager.GetAllCheckpoints();

                // Ожидание завершения всех задач запросов данных
                await Task.WhenAll(allRolesTask, allCheckpointRolesTask, allCheckpointAdditionalAccessTask, allAdditionAccessesTask, allEmployeesTask, allOfficesTask, allCheckpointTask);

                var allRoles = allRolesTask.Result;
                var allCheckpointRoles = allCheckpointRolesTask.Result;
                var allCheckpointAdditionalAccess = allCheckpointAdditionalAccessTask.Result;
                var allAdditionAccesses = allAdditionAccessesTask.Result;
                var allEmployees = allEmployeesTask.Result;
                var allOffices = allOfficesTask.Result;
                var allCheckpoint = allCheckpointTask.Result;

                // Проверка наличия данных во всех полученных значениях
                if (allRoles == null ||
                    allCheckpointRoles == null ||
                    allCheckpointAdditionalAccess == null ||
                    allAdditionAccesses == null ||
                    allEmployees == null ||
                    allOffices == null ||
                    allCheckpoint == null)
                {
                    // Один из запросов вернул null, обновление данных не удалось
                    return false;
                }

                // Обновление данных в синглтонах
                AllCheckpointsSingleton.Instance.Checkpoints = allCheckpoint;
                AllOfficesSingleton.Instance.Offices = allOffices;
                AllEmployeesSingleton.Instance.Employees = allEmployees;
                AllRoleSingleton.Instance.Roles = allRoles;
                AllCheckpointRoleSingleton.Instance.CheckpointRoles = allCheckpointRoles;
                AllCheckpointAdditionalAccessSingleton.Instance.CheckpointAdditionalAccesses = allCheckpointAdditionalAccess;
                AllAdditionAccessSingleton.Instance.AdditionalAccesses = allAdditionAccesses;

                // Проверка наличия данных в синглтонах
                if (AllCheckpointsSingleton.Instance.Checkpoints.Count > 0 &&
                    AllOfficesSingleton.Instance.Offices.Count > 0 &&
                    AllEmployeesSingleton.Instance.Employees.Count > 0 &&
                    AllRoleSingleton.Instance.Roles.Count > 0 &&
                    AllCheckpointRoleSingleton.Instance.CheckpointRoles.Count > 0 &&
                    AllCheckpointAdditionalAccessSingleton.Instance.CheckpointAdditionalAccesses.Count > 0 &&
                    AllAdditionAccessSingleton.Instance.AdditionalAccesses.Count > 0)
                {
                    return true; // Данные загружены и обновлены успешно
                }
                else
                {
                    // Данные не загружены или обновлены
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки
                Console.WriteLine("Ошибка при загрузке данных: " + ex.Message);
                // Дополнительные действия по обработке ошибки...
            }

            return false; // Данные не загружены или обновлены
        }
    }
}
