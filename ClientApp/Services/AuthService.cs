using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyClientApp.Services
{
    public class AuthService
    {
        private bool _isAuthenticated;
        private readonly HttpClient httpClient;

        public event Action OnAuthStateChanged;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            private set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    NotifyAuthenticationStateChanged();
                }
            }
        }

        public async Task LoginAsync(string email, string password)
        {
            var response = await httpClient.PostAsJsonAsync("PolicyHolder/authenticate", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                IsAuthenticated = true;
                // Notify UI about the state change
            }
        }

        public void Logout()
        {
            IsAuthenticated = false;
            // Notify UI about the state change
        }

        private void NotifyAuthenticationStateChanged() => OnAuthStateChanged?.Invoke();
    }
}
