using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Employee> Login(string username, string password)
        {
            var loginData = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("Login_check", loginData);

            if (response.IsSuccessStatusCode)
            {
                // Вход выполнен успешно, получаем информацию о сотруднике
                var employee = await response.Content.ReadAsAsync<Employee>();
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
    }
}
