
using Blazored.LocalStorage;

namespace LoansApp.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new
            {
                email,
                password
            });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            await _localStorage.SetItemAsync("token", result.Token);

            return true;
        }

        public async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsync<string>("token");
        }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
