using LoansApp.Domain.Enums;
using LoansApp.Domain.Exceptions;

namespace LoansApp.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; private set; }
        public int Term { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Loan(Guid userId, decimal amount, int termMonths)
        {
            if (amount <= 0)
                throw new DomainException("La cantidad debe ser mayor que 0.");

            if (termMonths <= 0)
                throw new DomainException("La cantidad debe ser mayor que 0.");

            Id = Guid.NewGuid();
            UserId = userId;
            Amount = amount;
            Term = termMonths;
            Status = LoanStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        private Loan() { }

        public void Approve()
        {
            if (Status != LoanStatus.Pending)
                throw new Exception("Préstamo ya procesado");

            Status = LoanStatus.Approved;
        }

        public void Reject()
        {
            if (Status != LoanStatus.Pending)
                throw new Exception("Préstamo ya procesado");

            Status = LoanStatus.Rejected;
        }
    }
}
