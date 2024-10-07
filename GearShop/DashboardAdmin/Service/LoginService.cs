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

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync($"{Admin_APIEndPoint_Manager.CHECK_USERNAME_EXISTED}?username={username}");
            var strData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(strData, options);
        }

        public async Task<bool> VerifyCredentialsAsync(string username, string password)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng và mật khẩu có khớp không
            var response = await _client.GetAsync($"{Admin_APIEndPoint_Manager.CHECK_MANAGER_EXISTED}?username={username}&password={password}");
            var strData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(strData, options);
        }
    }
}
