using CleaningSaboms.Context;
using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleaningSaboms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
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

                var result = await _customerService.CreateCustomer(customer);
                if (!result.Success)
                {
                    _logger.LogWarning("Kund skapades inte: {ErrorMessage}", result.Message);
                    return BadRequest(result.Message);
                }
                return CreatedAtAction(nameof(GetCustomer), new { id = result.Data!.Id }, result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fel vid skapande av kund.");
                return StatusCode(500, "Ett fel inträffade i API:t.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            _logger.LogInformation("GetCustomer: Försöker hämta kund med ID {CustomerId}", id);
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            _logger.LogInformation("GetCustomer: Kund hittad: {CustomerEmail}", customer);
            return Ok(customer);
        }
    }
}
