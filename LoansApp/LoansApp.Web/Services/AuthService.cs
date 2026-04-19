
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
            try
            {
                var response = await _http.PostAsJsonAsync("api/auth/login", new
                {
                    email,
                    password
                });

                Console.WriteLine($"[Login] Status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                    return false;
                

                // Leer el token como string puro (ahora la API lo devuelve directamente)
                //var token = await response.Content.ReadAsStringAsync();
                var token = await response.Content.ReadAsStringAsync();
                // Limpieza importante: eliminar comillas extras o espacios
                token = token.Trim().Trim('"');

                // Guardar en LocalStorage
                await _localStorage.SetItemAsync("authToken", token);

                Console.WriteLine($"[Login] TOKEN GUARDADO CORRECTAMENTE (primeros 40 chars): {token.Substring(0, Math.Min(40, token.Length))}...");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Login] Excepción: {ex.Message}");
                return false;
            }
        }

        public async Task<string?> GetToken()
        {
            //return await _localStorage.GetItemAsync<string>("authToken");
            var token = await _localStorage.GetItemAsync<string>("authToken");
            return token?.Trim().Trim('"');
        }

        public async Task ClearToken()
        {
            await _localStorage.RemoveItemAsync("authToken");
            Console.WriteLine("[AuthService] Token eliminado correctamente");
        }
    }
}
