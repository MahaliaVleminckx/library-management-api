using Pri.Ee.Client.Models;
using System.Net.Http.Json;

namespace Pri.Ee.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }
        public async Task<string> Login(string username, string password)
        {
            var response = await _http.PostAsJsonAsync
                (
                "api/auth/login",
                new { username, password }
                );

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null)
            {
                return null;
            }

            return result.token;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }
    }

    public class LoginResponse
    {
        public string token {  get; set; }
    }
}
