using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using LoansApp.Web.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LoansApp.Application.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepo;

        public LoanService(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }

        public async Task RequestLoan(Guid userId, decimal amount, int term)
        {
            var loan = new Loan(userId, amount, term);

            await _loanRepo.AddAsync(loan);
        }

        public async Task<List<Loan>> GetUserLoans(Guid userId)
        {
            return await _loanRepo.GetByUserIdAsync(userId);
        }

        public async Task<Loan?> GetById(Guid loanId)
        {
            return await _loanRepo.GetByIdAsync(loanId);
        }

        public async Task ApproveLoan(Guid loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);

            if (loan == null)
                throw new ApplicationException("Préstamo no encontrado");

            loan.Approve();

            await _loanRepo.UpdateAsync(loan);
        }

        public async Task RejectLoan(Guid loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);

            if (loan == null)
                throw new ApplicationException("Préstamo no encontrado");

            loan.Reject();

            await _loanRepo.UpdateAsync(loan);
        }
    }
}
