using CleaningSaboms.Models;

namespace CleaningSaboms.Interfaces
{
    public interface IServiceTypeService
    {
        Task<ServiceType> GetServiceTypeAsync(int serviceId);
    }
}
