using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoansApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
