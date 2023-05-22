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

        public async Task<List<CheckpointAdditionalAccess>> GetAllCheckpointAdditionalAccess()
        {
            var response = await _httpClient.GetAsync("CheckpointAdditionalAccess_DataList");

            if (response.IsSuccessStatusCode)
            {
                var checkpointadditionalaccesses = await response.Content.ReadAsAsync<List<CheckpointAdditionalAccess>>();
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
    }
}
