using LoansApp.Domain.Entities;

namespace LoansApp.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
