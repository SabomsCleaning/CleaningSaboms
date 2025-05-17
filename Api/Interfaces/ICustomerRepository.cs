using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ServiceResult> CreateCustomer(CustomerEntity customer);
        Task<ServiceResult> UpdateCustomer(CustomerDto customer);
        Task<ServiceResult> DeleteCustomer(int customerId);
        Task<CustomerEntity> GetCustomerById(Guid customerId);
        Task<ServiceResult<IEnumerable<CustomerDto>>> GetAllCustomers();
    }
}
