using LoansApp.Application.DTOs.Auth;
using LoansApp.Application.Services;
using LoansApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoansApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.Login(dto.Email, dto.Password);
            return Ok(new { token });
        }
    }
}
