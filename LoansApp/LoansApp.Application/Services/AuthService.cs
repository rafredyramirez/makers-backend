using LoansApp.Application.Interfaces;

namespace LoansApp.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepo, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);

            if (user == null)
                throw new ApplicationException("Usuario no encontrado");

            if (user.PasswordHash != password) 
                throw new ApplicationException("Credenciales invalidas");

            return _jwtService.GenerateToken(user);
        }
    }
}
