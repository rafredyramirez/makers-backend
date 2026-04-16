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
    public class LoansController : Controller
    {
        private readonly LoanService _loanService;

        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RequestLoan(RequestLoanDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _loanService.RequestLoan(userId, dto.Amount, dto.Term);

            return Ok("Loan requested");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyLoans()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var loans = await _loanService.GetUserLoans(userId);

            return Ok(loans);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveLoan(Guid id)
        {
            await _loanService.ApproveLoan(id);
            return Ok("Loan approved");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectLoan(Guid id)
        {
            await _loanService.RejectLoan(id);
            return Ok("Loan rejected");
        }
    }
}
