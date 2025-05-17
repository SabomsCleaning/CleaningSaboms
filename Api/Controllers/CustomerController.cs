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
        private readonly DataContext _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(DataContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customer)
        {
            _logger.LogInformation("Anrop mottaget i CreateCustomer");

            try
            {
                if (customer == null)
                {
                    _logger.LogWarning("Customer-objekt var null.");
                    return BadRequest("Customer cannot be null.");
                }

                var entity = new CustomerEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerFirstName = customer.CustomerFirstName,
                    CustomerLastName = customer.CustomerLastName,
                    CustomerEmail = customer.CustomerEmail
                };

                _context.Customers.Add(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Kund skapad med ID: {Id}", entity.Id);
                return CreatedAtAction(nameof(GetCustomer), new { id = entity.Id }, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fel vid skapande av kund.");
                return StatusCode(500, "Ett fel inträffade i API:t.");
            }
        }


        [HttpGet("{id}")]
        public async Task <IActionResult> GetCustomer(Guid id)
        {
            _logger.LogInformation("GetCustomer: Försöker hämta kund med ID {CustomerId}", id);
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                _logger.LogWarning("GetCustomer: Kunde inte hitta kund med ID {CustomerId}", id);
                return NotFound("Customer not found.");
            }
            _logger.LogInformation("GetCustomer: Kund hittad: {CustomerEmail}", customer.CustomerEmail);
            return Ok(customer);
        }
    }
}
