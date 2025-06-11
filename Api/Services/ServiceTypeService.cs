using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;

namespace CleaningSaboms.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<ServiceType> GetServiceTypeAsync(int serviceTypeId)
        {
            var serviceType = await _serviceTypeRepository.GetServiceTypeAsync(serviceTypeId);
            if (serviceType == null)
            {
                return null;
            }
            return serviceType;
        }
    }
}
