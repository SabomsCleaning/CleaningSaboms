using CleaningSaboms.Dto;
using CleaningSaboms.Factory;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<ServiceResult<CustomerEntity>> CreateCustomer(CustomerDto customer)
        {
            var Entity = CustomerFactory.FromDto(customer);
            var result = await _customerRepository.CreateCustomer(Entity);
            return ServiceResult<CustomerEntity>.Ok(Entity, "Skapad");
        }

        public async Task<ServiceResult<CustomerEntity>> GetCustomer(Guid id)
        {
            var result = await _customerRepository.GetCustomerById(id);
            if (result == null)
            {
                return ServiceResult<CustomerEntity>.Fail("Kund hittades inte.");
            }
            return ServiceResult<CustomerEntity>.Ok(result, "Kund hittades.");
        }
    }
}
