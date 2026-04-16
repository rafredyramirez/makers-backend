using LoansApp.Domain.Enums;

namespace LoansApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public LoanRoles Role { get; private set; }

        public User(string email, string passwordHash, LoanRoles role)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        private User() { }
    }
}
