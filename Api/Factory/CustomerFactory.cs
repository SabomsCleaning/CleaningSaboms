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
    }
}
