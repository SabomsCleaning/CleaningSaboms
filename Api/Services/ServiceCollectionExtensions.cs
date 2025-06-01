using CleaningSaboms.Interfaces;
using CleaningSaboms.Logger;
using CleaningSaboms.Repositories;

namespace CleaningSaboms.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditLogger, AuditLogger>();
            return services;
        }
    }
}
