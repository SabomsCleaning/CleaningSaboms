using CleaningSaboms.Dto;
using CleaningSaboms.Factory;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Results;
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

                var result = await _customerService.CreateCustomerAsync(customer);
                if (!result.Success)
                {
                    _logger.LogWarning("Kund skapades inte: {ErrorMessage}", result.Message);
                    return BadRequest(result.Message);
                }
                var dto = CustomerFactory.ToDto(result.Data!);
                return Ok(dto);

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
            var result = await _customerService.GetCustomerByIdAsync(id);
            if (result == null || !result.Success)
            {
                return NotFound("Customer not found.");
            }
            _logger.LogInformation("GetCustomer: Kund hittad: {CustomerEmail}", result);
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            _logger.LogInformation("GetAllCustomers: Försöker hämta alla kunder.");
            var customers = await _customerService.GetAllCustomersAsync();
            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found.");
            }
            _logger.LogInformation("GetAllCustomers: {CustomerCount} kunder hittades.", customers.Count());
            return Ok(customers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerDto customer)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("UpdateCustomer: Försöker uppdatera kund med ID {CustomerId}", id);
            var result = await _customerService.UpdateCustomerAsync(id, customer);

            if (!result.Success)
            {
                return result.Type switch
                {
                    ErrorType.Conflict => BadRequest(result.Message),
                    ErrorType.NotFound => NotFound(result.Message),
                    ErrorType.Validation => UnprocessableEntity(result.Message),
                    _ => StatusCode(500, "Ett fel inträffade i API:t.")
                };
            }

            _logger.LogInformation("UpdateCustomer: Kund uppdaterad: {CustomerEmail}", result.Data.CustomerEmail);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            _logger.LogInformation("DeleteCustomer: Försöker ta bort kund med ID {CustomerId}", id);
            var result = await _customerService.DeleteCustomerAsync(id);
            if (result == null || !result.Success)
            {
                return NotFound("Customer not found.");
            }
            _logger.LogInformation("DeleteCustomer: Kund borttagen");
            return Ok(result.Data);
        }
    }
}
