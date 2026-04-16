using LoansApp.Web.Models;

namespace LoansApp.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public async Task<List<LoanDto>> GetLoans(int userId)
        {
            return await _http.GetFromJsonAsync<List<LoanDto>>($"api/loans/{userId}");
        }

        public async Task CreateLoan(LoanDto loan)
        {
            await _http.PostAsJsonAsync("api/loans", loan);
        }

    }
}
