using CleaningSaboms.Models;

namespace CleaningSaboms.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<ServiceType> GetServiceTypeAsync(int serviceType);
    }
}
