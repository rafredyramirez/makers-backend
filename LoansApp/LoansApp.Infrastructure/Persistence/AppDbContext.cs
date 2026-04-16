using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoansApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }
    }
}
