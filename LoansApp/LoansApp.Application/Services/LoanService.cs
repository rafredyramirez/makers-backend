using LoansApp.Application.Interfaces;
using LoansApp.Domain.Entities;
using LoansApp.Domain.Enums;
using LoansApp.Web.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LoansApp.Application.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepo;

        public LoanService(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }

        /// <summary>
        /// Solicita un nuevo préstamo. Incluye validaciones de reglas de negocio.
        /// </summary>
        public async Task<Guid> RequestLoan(Guid userId, decimal amount, int term)
        {
            // Validaciones de reglas de negocio
            if (amount <= 0)
                throw new ApplicationException("El monto del préstamo debe ser mayor a cero.");

            if (term <= 0 || term > 60)
                throw new ApplicationException("El plazo debe estar entre 1 y 60 meses.");

            // Regla de negocio: Un usuario no puede tener más de un préstamo pendiente
            bool hasPendingLoan = await _loanRepo.HasPendingLoanAsync(userId);
            if (hasPendingLoan)
                throw new ApplicationException("El usuario ya tiene un préstamo pendiente. No se puede solicitar otro hasta que se resuelva.");

            // Crear el préstamo en estado Pending
            var loan = new Loan(userId, amount, term);

            await _loanRepo.AddAsync(loan);

            return loan.Id;   // Devolvemos el Id para poder informar al frontend
        }

        /// <summary>
        /// Obtiene todos los préstamos de un usuario
        /// </summary>
        public async Task<List<Loan>> GetUserLoans(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ApplicationException("El UserId no puede estar vacío.");

            return await _loanRepo.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// Aprueba un préstamo (solo Administradores)
        /// </summary>
        public async Task<bool> ApproveLoan(Guid loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);

            if (loan == null)
                return false;

            if (loan.Status != LoanStatus.Pending)
                throw new ApplicationException("Solo se pueden aprobar préstamos en estado Pendiente.");

            loan.Approve();

            await _loanRepo.UpdateAsync(loan);
            return true;
        }

        /// <summary>
        /// Rechaza un préstamo (solo Administradores)
        /// </summary>
        public async Task<bool> RejectLoan(Guid loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);

            if (loan == null)
                return false;

            if (loan.Status != LoanStatus.Pending)
                throw new ApplicationException("Solo se pueden rechazar préstamos en estado Pendiente.");

            loan.Reject();

            await _loanRepo.UpdateAsync(loan);
            return true;
        }

        /// <summary>
        /// Obtiene un préstamo por su ID (útil para validaciones futuras)
        ///// </summary>
        //public async Task<Loan?> GetById(Guid loanId)
        //{
        //    return await _loanRepo.GetByIdAsync(loanId);
        //}

        public async Task<List<Loan>> GetAllLoans()
        {
            return await _loanRepo.GetAllAsync();
        }
    }   
}
