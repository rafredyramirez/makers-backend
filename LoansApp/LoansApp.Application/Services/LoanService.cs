using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using LoansApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LoansApp.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IAppDbContext _context;

        public LoanService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> RequestLoan(int userId, decimal amount, int term)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0");

            if (term <= 0)
                throw new ArgumentException("El plazo debe ser mayor a 0");

            var loan = new Loan
            {
                UserId = userId,
                Amount = amount,
                Term = term,
                Status = LoanStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync(CancellationToken.None);

            return loan;
        }

        public async Task<List<Loan>> GetLoansByUser(int userId)
        {
            return await _context.Loans
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task ApproveLoan(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);

            if (loan == null)
                throw new Exception("Préstamo no encontrado");

            if (loan.Status != LoanStatus.Pending)
                throw new Exception("El préstamo ya fue procesado");

            loan.Status = LoanStatus.Approved;

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task RejectLoan(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);

            if (loan == null)
                throw new Exception("Préstamo no encontrado");

            if (loan.Status != LoanStatus.Pending)
                throw new Exception("El préstamo ya fue procesado");

            loan.Status = LoanStatus.Rejected;

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
