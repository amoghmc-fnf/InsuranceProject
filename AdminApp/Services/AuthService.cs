using System.Text.Json;
using System.Text;
using JwtModels;
using Newtonsoft.Json;
using System.Net.Http;

namespace AdminApp.Services
{
    public interface IAuthService
    {
        Task<string> GetJwtToken(string password);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient http;
        public AuthService(HttpClient httpClient)
        {
            http = httpClient;
        }

        public async Task<string> GetJwtToken(string password)
        {
            var userLogin = new UserLogin { key = password };
            var json = System.Text.Json.JsonSerializer.Serialize(userLogin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync("Auth", content);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenInfo>(jsonResult);
            return token.Token;
        }
    }
}
