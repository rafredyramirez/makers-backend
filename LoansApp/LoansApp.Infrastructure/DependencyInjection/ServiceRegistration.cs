using LoansApp.Application.Interfaces;
using LoansApp.Infrastructure.Repositories;
using LoansApp.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace LoansApp.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
