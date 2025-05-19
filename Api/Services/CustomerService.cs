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
            var customerExist = await _customerRepository.CustomerExistsAsync(customer);
            if (customerExist)
            {
                return ServiceResult<CustomerEntity>.Fail("Kunden finns redan i databasen");
            }
            var addressExist = await _customerRepository.AddressExistsAsync(customer);
            if (addressExist)
            {
                return ServiceResult<CustomerEntity>.Fail("Kundens adress finns redan i databasen");
            }
            var entity = CustomerFactory.FromDto(customer);
            _ = await _customerRepository.CreateCustomer(entity);
            return ServiceResult<CustomerEntity>.Ok(entity, "Skapad");
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customerEntities = await _customerRepository.GetAllCustomers();

            if (customerEntities == null || !customerEntities.Any())
            {
                return Enumerable.Empty<CustomerDto>();
            }

            var customerDtos = CustomerFactory.ToDto(customerEntities);
            return customerDtos;
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
