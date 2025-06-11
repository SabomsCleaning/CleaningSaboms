using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ServiceResult> CreateCustomerAsync(CustomerEntity customer);
        Task UpdateCustomerAsync(CustomerEntity customer);
        Task<bool> DeleteCustomerAsync(Guid Id);
        Task<CustomerEntity> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
        Task<bool> AddressExistsAsync(CustomerDto dto);
        Task<bool> CustomerExistsAsync(string email);
        Task<bool> CustomerExistsIdAsync(Guid customerId);
    }
}
