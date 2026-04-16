using LoansApp.Domain.Enums;

namespace LoansApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public LoanRols Role { get; set; } 
    }
}
