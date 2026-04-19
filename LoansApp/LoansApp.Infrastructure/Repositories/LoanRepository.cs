using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using LoansApp.Domain.Enums;
using LoansApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LoansApp.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan> GetByIdAsync(Guid id)
        {
            return await _context.Loans.FindAsync(id);
        }

        public async Task<List<Loan>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Loans
                .AsNoTracking()
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> HasPendingLoanAsync(Guid userId)
        {
            return await _context.Loans
                .AsNoTracking()
                .AnyAsync(l => l.UserId == userId &&
                               l.Status == LoanStatus.Pending);
        }

        public async Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }
    }
}
