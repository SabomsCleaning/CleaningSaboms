using CleaningSaboms.Interfaces;
using CleaningSaboms.Repositories;

namespace CleaningSaboms.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
