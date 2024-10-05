using System;
using System.Net.Http;
using System.Text.Json;

namespace DashboardAdmin.Service
{
    public class LoginService
    {
        private readonly HttpClient _client;

        public LoginService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> VerifyCredentialsAsync(string username, string password)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Check if username exists
            var response = await _client.GetAsync($"{Admin_APIEndPoint_Manager.CHECK_USERNAME_EXISTED}?username={username}");
            var strData = await response.Content.ReadAsStringAsync();
            bool usernameExisted = JsonSerializer.Deserialize<bool>(strData, options);

            if (!usernameExisted)
            {
                return false;
            }

            // Check if username and password match
            response = await _client.GetAsync($"{Admin_APIEndPoint_Manager.CHECK_MANAGER_EXISTED}?username={username}&password={password}");
            strData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(strData, options);
        }
    }
}
