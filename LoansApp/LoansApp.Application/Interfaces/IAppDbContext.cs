using LoansApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoansApp.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Loan> Loans { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
