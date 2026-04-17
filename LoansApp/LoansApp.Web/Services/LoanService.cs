using LoansApp.Web.Models;
using System.Net.Http.Headers;

namespace LoansApp.Web.Services
{
    public class LoanService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public LoanService(IHttpClientFactory factory, AuthService auth)
        {
            _http = factory.CreateClient("Api");
            _auth = auth;
        }

        private async Task AddAuthHeaderAsync()
        {
            var token = await _auth.GetToken();

            Console.WriteLine($"TOKEN ENVIADO: {token ?? "NULL"}");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _http.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<List<LoanModel>> GetLoans()
        {
            await AddAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<LoanModel>>("api/loans")
                   ?? new List<LoanModel>();
        }

        public async Task RequestLoan(decimal amount, int term)
        {
            await AddAuthHeaderAsync();
            await _http.PostAsJsonAsync("api/loans", new { amount, term });
        }

        public async Task ApproveLoan(Guid id)
        {
            await AddAuthHeaderAsync();
            await _http.PostAsync($"api/loans/{id}/approve", null);
        }

        public async Task RejectLoan(Guid id)
        {
            await AddAuthHeaderAsync();
            await _http.PostAsync($"api/loans/{id}/reject", null);
        }
    }
}
