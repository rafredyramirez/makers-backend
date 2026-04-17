
using Blazored.LocalStorage;
using LoansApp.Web.Models;
using System.Net.Http.Headers;

namespace LoansApp.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(IHttpClientFactory factory, ILocalStorageService localStorage)
        {
            _http = factory.CreateClient("Api");
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new { email, password });

            Console.WriteLine($"STATUS: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"RESPONSE: {content}");

            if (!response.IsSuccessStatusCode)
                return false;

            await _localStorage.SetItemAsync("authToken", content);

            return true;
        }

        public async Task<string?> GetToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public async Task ClearToken()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }
    }
}
