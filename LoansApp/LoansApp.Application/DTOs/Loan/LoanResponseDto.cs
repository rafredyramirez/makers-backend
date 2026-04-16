namespace LoansApp.Application.DTOs.Loan
{
    public class LoanResponseDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public string Status { get; set; }
    }
}
