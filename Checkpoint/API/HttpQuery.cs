using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Checkpoint.Models;
using Checkpoint.Singletons;
using Newtonsoft.Json;

namespace Checkpoint.API
{
    internal class HttpQuery
    {
        private readonly HttpClient _httpClient;

        public HttpQuery()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7213/api/DataBase/");
        }

        public async Task<HttpEmployee> Login(string username, string password)
        {
            var loginData = new { Username = username, Password = password };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Login_check", loginData);
                if (response.IsSuccessStatusCode)
                {
                    // Вход выполнен успешно, получаем информацию о сотруднике
                    var employee = await response.Content.ReadAsAsync<HttpEmployee>();
                    return employee;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Ошибка аутентификации
                    return null;
                }
                else
                {
                    // Обработка других ошибок, если необходимо
                    throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка отправки запроса на сервер(Login Query). Обратитесь к администратору системы.", ex.ToString());
                return null;
            }
        }
        public async Task<List<EmployeeWithRoleName>> GetAllEmployees()
        {
            var response = await _httpClient.GetAsync("Employee_DataList");

            if (response.IsSuccessStatusCode)
            {
                // Получаем список всех сотрудников
                var employees = await response.Content.ReadAsAsync<List<EmployeeWithRoleName>>();
                return employees;
            }
            else
            {
                // Обработка ошибки
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }

        public async Task<List<OfficeWithCityName>> GetAllOffices()
        {
            var response = await _httpClient.GetAsync("Office_DataList");

            if (response.IsSuccessStatusCode)
            {
                // Получаем список всех офисов
                var offices = await response.Content.ReadAsAsync<List<OfficeWithCityName>>();
                return offices;
            }
            else
            {
                // Обработка ошибки
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }

        public async Task<List<CheckpointWithOfficeName>> GetAllCheckpoints()
        {
            var response = await _httpClient.GetAsync("Checkpoint_DataList");

            if (response.IsSuccessStatusCode)
            {
                var checkpoints = await response.Content.ReadAsAsync<List<CheckpointWithOfficeName>>();
                return checkpoints;
            }
            else
            {
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var response = await _httpClient.GetAsync("Role_DataList");

            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content.ReadAsAsync<List<Role>>();
                return roles;
            }
            else
            {
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }

        public async Task<List<CheckpointRoleWithTitleID>> GetAllCheckpointRoles()
        {
            var response = await _httpClient.GetAsync("CheckpointRole_DataList");

            if (response.IsSuccessStatusCode)
            {
                var checkpointroles = await response.Content.ReadAsAsync<List<CheckpointRoleWithTitleID>>();
                return checkpointroles;
            }
            else
            {
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }

        public async Task<List<CheckpointAdditionalAccessWithTitleID>> GetAllCheckpointAdditionalAccess()
        {
            var response = await _httpClient.GetAsync("CheckpointAdditionalAccess_DataList");

            if (response.IsSuccessStatusCode)
            {
                var checkpointadditionalaccesses = await response.Content.ReadAsAsync<List<CheckpointAdditionalAccessWithTitleID>>();
                return checkpointadditionalaccesses;
            }
            else
            {
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }
        public async Task<List<AdditionAccess>> GetAllAdditionAccess()
        {
            var response = await _httpClient.GetAsync("AdditionAccess_DataList");

            if (response.IsSuccessStatusCode)
            {
                var additionaccesses = await response.Content.ReadAsAsync<List<AdditionAccess>>();
                return additionaccesses;
            }
            else
            {
                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }

        public async Task DeleteCheckpointRole(int IDRole, int IDCheckpoint)
        {
            var url = $"DeleteCheckpointRole?IDRole={IDRole}&IDCheckpoint={IDCheckpoint}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Запись успешно удалена
                Console.WriteLine("Запись успешно удалена из таблицы CheckpointRole.");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Запись не найдена
                Console.WriteLine("Запись не найдена в таблице CheckpointRole.");
            }
            else
            {
                // При удалении возникла ошибка
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при удалении записи: {errorMessage}");
            }
        }
        public async Task DeleteCheckpointAdditionalAccess(int IDAdditionalAccess, int IDCheckpoint)
        {
            var url = $"DeleteCheckpointAdditionalAccess?IDAdditionalAccess={IDAdditionalAccess}&IDCheckpoint={IDCheckpoint}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Запись успешно удалена
                Console.WriteLine("Запись успешно удалена из таблицы CheckpointRole.");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Запись не найдена
                Console.WriteLine("Запись не найдена в таблице CheckpointRole.");
            }
            else
            {
                // При удалении возникла ошибка
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при удалении записи: {errorMessage}");
            }
        }
        public async Task<bool> AddCheckpointRole(int IDRole, int IDCheckpoint)
        {
            try
            {
                var url = $"AddCheckpointRole?IDRole={IDRole}&IDCheckpoint={IDCheckpoint}";

                var response = await _httpClient.PostAsJsonAsync(url, "");
                var requestData = new { IDRole = IDRole, IDCheckpoint = IDCheckpoint };
                if (response.IsSuccessStatusCode)
                {
                    // Вход выполнен успешно, получаем информацию о сотруднике
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Ошибка аутентификации
                    return false;
                }
                else
                {
                    // Обработка других ошибок, если необходимо
                    throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка отправки запроса на сервер. Обратитесь к администратору системы.", ex.ToString());
                return false;
            }
        }
        public async Task<bool> AddCheckpointAdditionalAccess(int IDAdditionalAccess, int IDCheckpoint)
        {
            try
            {
                var url = $"AddCheckpointAdditionalAccess?IDAdditionalAccess={IDAdditionalAccess}&IDCheckpoint={IDCheckpoint}";

                var response = await _httpClient.PostAsJsonAsync(url, "");
                var requestData = new { IDAdditionalAccess = IDAdditionalAccess, IDCheckpoint = IDCheckpoint };
                if (response.IsSuccessStatusCode)
                {
                    // Вход выполнен успешно, получаем информацию о сотруднике
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Ошибка аутентификации
                    return false;
                }
                else
                {
                    // Обработка других ошибок, если необходимо
                    throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка отправки запроса на сервер. Обратитесь к администратору системы.", ex.ToString());
                return false;
            }
        }
        public async Task<bool> AddEmployee(Employee newEmployee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AddEmployee", newEmployee);

                if (response.IsSuccessStatusCode)
                {
                    // Работник успешно добавлен
                    return true;
                }
                else
                {
                    // Обработка ошибки при добавлении работника
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при добавлении работника: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при отправке запроса на сервер. Обратитесь к администратору системы.");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DeleteEmployee/{employeeId}");

                if (response.IsSuccessStatusCode)
                {
                    // Работник успешно удален
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Работник не найден
                    Console.WriteLine("Работник не найден.");
                    return false;
                }
                else
                {
                    // Обработка ошибки при удалении работника
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при удалении работника: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при отправке запроса на сервер. Обратитесь к администратору системы.");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"UpdateEmployee/{updatedEmployee.ID}", updatedEmployee);

                if (response.IsSuccessStatusCode)
                {
                    // Данные о сотруднике успешно обновлены
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Сотрудник не найден
                    Console.WriteLine("Сотрудник не найден.");
                    return false;
                }
                else
                {
                    // Обработка ошибки при обновлении данных о сотруднике
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при обновлении данных о сотруднике: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при отправке запроса на сервер. Обратитесь к администратору системы.");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> UpdateCheckpoint(Checkpointt updatedCheckpoint)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"UpdateCheckpoint/{updatedCheckpoint.ID}", updatedCheckpoint);

                if (response.IsSuccessStatusCode)
                {
                    // Данные о контрольной точке успешно обновлены
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Контрольная точка не найдена
                    Console.WriteLine("Контрольная точка не найдена.");
                    return false;
                }
                else
                {
                    // Обработка ошибки при обновлении данных о контрольной точке
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при обновлении данных о контрольной точке: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при отправке запроса на сервер. Обратитесь к администратору системы.");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> AddCheckpoint(Checkpointt newCheckpoint)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AddCheckpoint", newCheckpoint);

                if (response.IsSuccessStatusCode)
                {
                    // Проход успешно добавлен
                    return true;
                }
                else
                {
                    // Обработка ошибки при добавлении прохода
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при добавлении прохода: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при отправке запроса на сервер. Обратитесь к администратору системы.");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
