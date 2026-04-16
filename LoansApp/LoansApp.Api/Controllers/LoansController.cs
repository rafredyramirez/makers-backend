using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LoansApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : Controller
    {
        private readonly ILoanService _service;

        public LoansController(ILoanService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Loan request)
        {
            var result = await _service.RequestLoan(request.UserId, request.Amount, request.Term);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(await _service.GetLoansByUser(userId));
        }
    }
}
