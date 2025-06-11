using CleaningSaboms.Dto;
using CleaningSaboms.Factory;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Services
{
    public class CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly ILogger<CustomerService> _logger = logger;

        public async Task<ServiceResult<CustomerEntity>> CreateCustomerAsync(CustomerDto customer)
        {
            var customerExist = await _customerRepository.CustomerExistsAsync(customer.CustomerEmail);
            if (customerExist)
            {
                _logger.LogWarning($"Customer with {customer.CustomerEmail} already exist");
                return ServiceResult<CustomerEntity>.Fail("Kunden finns redan i databasen");
            }
            var addressExist = await _customerRepository.AddressExistsAsync(customer);
            if (addressExist)
            {
                _logger.LogWarning($"Customer {customer.CustomerAddressLine} is already in system");
                return ServiceResult<CustomerEntity>.Fail("Kundens adress finns redan i databasen");
            }
            var entity = CustomerFactory.FromDto(customer);
            _ = await _customerRepository.CreateCustomerAsync(entity);
            _logger.LogInformation($"Customer {customer.CustomerFirstName} is added to database");
            return ServiceResult<CustomerEntity>.Ok(entity, "Skapad");
        }

        public async Task<ServiceResult<bool>> DeleteCustomerAsync(Guid id)
        {
            var result = await _customerRepository.DeleteCustomerAsync(id);
            if (!result)
            {
                _logger.LogWarning($"Customer could not be found: {id}");
                return ServiceResult<bool>.Fail("Kund hittades inte.", ErrorType.NotFound);
            }
            _logger.LogInformation($"Customer deleted: {result}");
            return ServiceResult<bool>.Ok(true, "Kund borttagen.");
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customerEntities = await _customerRepository.GetAllCustomersAsync();

            if (customerEntities == null || !customerEntities.Any())
            {
                return Enumerable.Empty<CustomerDto>();
            }

            var customerDtos = CustomerFactory.ToDto(customerEntities);
            return customerDtos;
        }


        public async Task<ServiceResult<CustomerDto>> GetCustomerByIdAsync(Guid id)
        {
            var result = await _customerRepository.GetCustomerByIdAsync(id);
            if (result == null)
            {
                return ServiceResult<CustomerDto>.Fail("Kund hittades inte.");
            }
            var customerDto = CustomerFactory.ToDto(result);
            return ServiceResult<CustomerDto>.Ok(customerDto, "Kund hittades.");
        }

        public async Task<ServiceResult<CustomerDto>> UpdateCustomerAsync(Guid id, CustomerDto customer)
        {
            var emailExist = await _customerRepository.CustomerExistsAsync(customer.CustomerEmail);
            if (emailExist)
            {
                return ServiceResult<CustomerDto>.Fail("Kundens email finns redan i databasen, vänligen välj en annan", ErrorType.Conflict);
            }

            var existing = await _customerRepository.GetCustomerByIdAsync(id);
            if (existing == null)
            {
                return ServiceResult<CustomerDto>.Fail("Kund hittades inte.", ErrorType.NotFound);
            }

            existing.CustomerFirstName = customer.CustomerFirstName;
            existing.CustomerLastName = customer.CustomerLastName;
            existing.CustomerEmail = customer.CustomerEmail;
            existing.CustomerAddress.CustomerAddressLine = customer.CustomerAddressLine;
            existing.CustomerAddress.CustomerCity = customer.CustomerCity;
            existing.CustomerAddress.CustomerPostalCode = customer.CustomerPostalCode;

            await _customerRepository.UpdateCustomerAsync(existing);

            var dto = CustomerFactory.ToDto(existing);

            return ServiceResult<CustomerDto>.Ok(dto, "Kund uppdaterad.");
        }
    }
}
