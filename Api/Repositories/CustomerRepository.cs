using CleaningSaboms.Context;
using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Repositories
{
    public class CustomerRepository(DataContext context) : ICustomerRepository
    {
        private readonly DataContext _context = context;

        public async Task<ServiceResult> CreateCustomer(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new ServiceResult { Success = true, Message = "Customer created successfully." };
        }

        public Task<ServiceResult> UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResult> DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
        public async Task<CustomerEntity> GetCustomerById(Guid customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }
        public Task<ServiceResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        
    }
}
