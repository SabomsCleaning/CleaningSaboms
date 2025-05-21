using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResult<CustomerEntity>> CreateCustomer(CustomerDto customer);
        Task<ServiceResult<CustomerDto>> GetCustomerById(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
    }
}
