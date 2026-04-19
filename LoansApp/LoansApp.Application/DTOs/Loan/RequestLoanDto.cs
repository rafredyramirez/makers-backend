using System.ComponentModel.DataAnnotations;

namespace LoansApp.Application.DTOs.Loan
{
    public class RequestLoanDto
    {
        [Required]
        [Range(1000, 10000000, ErrorMessage = "El monto debe estar entre 1,000 y 10,000,000")]
        public decimal Amount { get; set; }
        [Required]
        [Range(1, 60, ErrorMessage = "El plazo debe estar entre 1 y 60 meses")]
        public int Term { get; set; }
    }
}
