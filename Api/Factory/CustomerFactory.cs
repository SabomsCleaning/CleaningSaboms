using CleaningSaboms.Dto;
using CleaningSaboms.Models;

namespace CleaningSaboms.Factory
{
    public class CustomerFactory
    {
        public static CustomerEntity FromDto(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "CustomerDto cannot be null.");
            }

            var address = new CustomerAddressEntity
            {
                Id = Guid.NewGuid(),
                CustomerAddressLine = customerDto.CustomerAddressLine,
                CustomerCity = customerDto.CustomerCity,
                CustomerPostalCode = customerDto.CustomerPostalCode
            };
            return new CustomerEntity
            {
                Id = Guid.NewGuid(),
                CustomerFirstName = customerDto.CustomerFirstName,
                CustomerLastName = customerDto.CustomerLastName,
                CustomerEmail = customerDto.CustomerEmail,
                CustomerAddress = address,
                CustomerAddressId = address.Id
            };
        }

        public static CustomerDto ToDto(CustomerEntity customerEntity)
        {
            if (customerEntity == null)
            {
                throw new ArgumentNullException(nameof(customerEntity), "CustomerEntity cannot be null.");
            }
            return new CustomerDto
            {
                CustomerFirstName = customerEntity.CustomerFirstName,
                CustomerLastName = customerEntity.CustomerLastName,
                CustomerEmail = customerEntity.CustomerEmail,
                CustomerAddressLine = customerEntity.CustomerAddress.CustomerAddressLine,
                CustomerCity = customerEntity.CustomerAddress.CustomerCity,
                CustomerPostalCode = customerEntity.CustomerAddress.CustomerPostalCode
            };
        }

        public static IEnumerable<CustomerDto> ToDto(IEnumerable<CustomerEntity> customers)
        {
            return customers.Select(c => ToDto(c));
        }

    }
}
