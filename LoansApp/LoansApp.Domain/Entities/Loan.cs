using LoansApp.Domain.Enums;

namespace LoansApp.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
