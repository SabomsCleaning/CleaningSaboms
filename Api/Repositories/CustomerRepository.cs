using CleaningSaboms.Context;
using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Customers
                .Include(c => c.CustomerAddress)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }
        public async Task<IEnumerable<CustomerEntity>> GetAllCustomers()
        {
            return await _context.Customers
                .Include(c => c.CustomerAddress)
                .ToListAsync();
        }

        public async Task<bool> AddressExistsAsync(CustomerDto dto)
        {
            return await _context.Customers.AnyAsync(c =>
                        c.CustomerAddress.CustomerAddressLine == dto.CustomerAddressLine &&
                        c.CustomerAddress.CustomerCity == dto.CustomerCity &&
                        c.CustomerAddress.CustomerPostalCode == dto.CustomerPostalCode);
        }

        public async Task<bool> CustomerExistsAsync(CustomerDto dto)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerEmail == dto.CustomerEmail);
        }
    }
}
