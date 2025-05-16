using CleaningSaboms.Context;
using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleaningSaboms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer cannot be null.");
            }
            
            var customerEntity = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                CustomerFirstName = customer.CustomerFirstName,
                CustomerLastName = customer.CustomerLastName,
                CustomerEmail = customer.CustomerEmail
            };

            _context.Customers.Add(customerEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customerEntity.Id }, customerEntity);
        }

        [HttpGet("{id}")]
        [Route("api/customer/{id}")]
        public async Task <IActionResult> GetCustomer(Guid id)
        {
            var customer = _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customer);
        }
    }
}
