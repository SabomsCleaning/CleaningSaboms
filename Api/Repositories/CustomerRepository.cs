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

        public async Task<ServiceResult> CreateCustomerAsync(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new ServiceResult { Success = true, Message = "Customer created successfully." };
        }

        public async Task UpdateCustomerAsync(CustomerEntity customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCustomerAsync(Guid Id)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerAddress)
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (customer == null)
            {
                return false;
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(Guid customerId)
        {
            return await _context.Customers
                .Include(c => c.CustomerAddress)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }
        public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
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

        public async Task<bool> CustomerExistsAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerEmail == email);
        }
    }
}
