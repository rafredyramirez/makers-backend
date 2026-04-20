using LoansApp.Application.DTOs.Loan;
using LoansApp.Application.Interfaces;
using LoansApp.Application.Services;
using LoansApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoansApp.Api.Controllers
{
    [ApiController]
    [Route("api/Loans")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        // 1. Solicitar un préstamo (Usuario normal)
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Loans([FromBody] RequestLoanDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized(new { message = "Usuario no identificado" });

            var loanId = await _loanService.RequestLoan(userId, dto.Amount, dto.Term);

            return CreatedAtAction(nameof(GetMyLoans), new { id = loanId },
                new { message = "Préstamo solicitado correctamente", loanId });
        }

        // 2. Ver mis préstamos (Usuario normal)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyLoans()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized(new { message = "Usuario no identificado" });

            var loans = await _loanService.GetUserLoans(userId);
            return Ok(loans);
        }

        // 3. Aprobar un préstamo (Solo Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveLoan(Guid id)
        {
            var success = await _loanService.ApproveLoan(id);

            if (!success)
                return NotFound(new { message = "Préstamo no encontrado" });

            return Ok(new { message = "Préstamo aprobado correctamente" });
        }

        // 4. Rechazar un préstamo (Solo Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectLoan(Guid id)
        {
            var success = await _loanService.RejectLoan(id);

            if (!success)
                return NotFound(new { message = "Préstamo no encontrado" });

            return Ok(new { message = "Préstamo rechazado correctamente" });
        }

        // 5. Ver solicitud préstamos (Solo Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoans();
            return Ok(loans);
        }
        // Método auxiliar para obtener el UserId de forma segura
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                return Guid.Empty;

            return userId;
        }
    }
}
