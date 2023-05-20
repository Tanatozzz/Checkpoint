using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Checkpoint.Models;

namespace Checkpoint.API
{
    internal class HttpQuery
    {
        private readonly HttpClient _httpClient;

        public HttpQuery()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7213/api/DataBase/"); // Замените на базовый URL вашего API
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
                MessageBox.Show("Произошла ошибка отправки запроса на сервер(Login Query). Обратитесь к администратору системы.");
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
    }
}
