using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResult<CustomerEntity>> CreateCustomerAsync(CustomerDto customer);
        Task<ServiceResult<CustomerDto>> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task <ServiceResult<CustomerDto>> UpdateCustomerAsync(Guid id, CustomerDto customer);
        Task<ServiceResult<bool>> DeleteCustomerAsync(Guid id);
    }
}
