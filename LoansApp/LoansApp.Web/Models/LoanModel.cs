namespace LoansApp.Web.Models
{
    public class LoanModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public string Status { get; set; }
    }
}
