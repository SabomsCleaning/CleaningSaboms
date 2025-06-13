using CleaningSaboms.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace CleaningSaboms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetServiceType(int serviceId)
        {
           
            var serviceType = await _serviceTypeService.GetServiceTypeAsync(serviceId);
            if (serviceType == null)
            {
                return BadRequest();
            }
            return Ok(serviceType);
        }
    }
}
