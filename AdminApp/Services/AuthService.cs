using System.Text.Json;
using System.Text;
using JwtModels;
using Newtonsoft.Json;
using System.Net.Http;

namespace AdminApp.Services
{
    public interface IAuthService
    {
        Task<string> GetJwtToken();
    }

    public class AuthService : IAuthService 
    {
        private readonly HttpClient http;
        private readonly IConfiguration configuration;
        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            http = httpClient;
            this.configuration = configuration;
        }

        public async Task<string> GetJwtToken()
        {
            var password = configuration["JwtKey"];
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
