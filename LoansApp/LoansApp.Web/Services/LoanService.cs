using LoansApp.Web.Models;
using System.Net.Http.Headers;

namespace LoansApp.Web.Services
{
    public class LoanService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public LoanService(HttpClient http, AuthService auth)
        {
            _http = http;
            _auth = auth;
        }

        private async Task AddAuthHeader()
        {
            var token = await _auth.GetToken();
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task RequestLoan(decimal amount, int term)
        {
            await AddAuthHeader();

            await _http.PostAsJsonAsync("api/loans", new
            {
                amount,
                term
            });
        }

        public async Task<List<LoanModel>> GetLoans()
        {
            await AddAuthHeader();

            return await _http.GetFromJsonAsync<List<LoanModel>>("api/loans");
        }

        public async Task Approve(Guid id)
        {
            await AddAuthHeader();

            await _http.PostAsync($"api/loans/{id}/approve", null);
        }
    }
}
