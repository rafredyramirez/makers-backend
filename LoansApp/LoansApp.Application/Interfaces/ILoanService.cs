using LoansApp.Domain.Entities;

namespace LoansApp.Application.Interfaces
{
    public interface ILoanService
    {
        Task<Loan> RequestLoan(int userId, decimal amount, int term);
        Task<List<Loan>> GetLoansByUser(int userId);
        Task ApproveLoan(int loanId);
        Task RejectLoan(int loanId);

    }
}
