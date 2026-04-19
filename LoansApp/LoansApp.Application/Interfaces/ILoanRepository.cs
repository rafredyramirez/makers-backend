using LoansApp.Domain.Entities;

namespace LoansApp.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan);
        Task<Loan> GetByIdAsync(Guid id);
        Task<List<Loan>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Loan loan);
        Task<bool> HasPendingLoanAsync(Guid userId);
        Task<List<Loan>> GetAllAsync();
    }
}
