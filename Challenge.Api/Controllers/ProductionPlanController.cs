using Challenge.Service.Dtos;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanService _productionPlanService;

        public ProductionPlanController(IProductionPlanService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpPost("/productionplan")]
        public IActionResult CalculateProductionPlan([FromBody] Payload payload)
        {
            try
            {
                // Call the service to calculate the production plan
                var result = _productionPlanService.Calculate(payload);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle errors and return appropriate responses
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
