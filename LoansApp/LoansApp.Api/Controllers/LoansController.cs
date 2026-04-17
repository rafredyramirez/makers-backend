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
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RequestLoan([FromBody] RequestLoanDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            await _loanService.RequestLoan(userId, dto.Amount, dto.Term);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyLoans()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new { message = "No se pudo identificar al usuario" });
            }

            var loans = await _loanService.GetUserLoans(userId);
            return Ok(loans);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveLoan(Guid id)
        {
            await _loanService.ApproveLoan(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectLoan(Guid id)
        {
            await _loanService.RejectLoan(id);
            return Ok();
        }
    }
}
