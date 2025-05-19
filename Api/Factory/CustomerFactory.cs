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
            return new CustomerEntity
            {
                Id = Guid.NewGuid(),
                CustomerFirstName = customerDto.CustomerFirstName,
                CustomerLastName = customerDto.CustomerLastName,
                CustomerEmail = customerDto.CustomerEmail
            };
        }
    }
}
