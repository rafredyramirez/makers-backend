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
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _authService.Login(dto.Email, dto.Password);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
