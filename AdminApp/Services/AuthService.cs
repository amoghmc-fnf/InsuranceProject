using System.Text.Json;
using System.Text;
using JwtModels;

namespace AdminApp.Services
{
    public interface IAuthService
    {
        Task<string> GetJwtToken(UserLogin user);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient http;
        public AuthService()
        {
            http = new HttpClient();
        }

        public async Task<string> GetJwtToken(UserLogin user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.PostAsync("", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
