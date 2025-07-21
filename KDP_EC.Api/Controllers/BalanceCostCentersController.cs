using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BalanceCostCentersController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IBalanceCostCenters _IBalanceCostCenters;

        public BalanceCostCentersController(JwtSettings jwtSettings, IBalanceCostCenters IBalanceCostCenters)
        {
            _jwtSettings = jwtSettings;
            _IBalanceCostCenters = IBalanceCostCenters;
        }
        [HttpGet("getBalanceCostCentersByFarm")]

        public IActionResult GetBalanceCostCentersByFarm(Guid farmId)
        {
            var result = _IBalanceCostCenters.GetBalanceCostCentersByFarm(farmId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron centros de costo para la granja especificada." });
            }
            return Ok(result);
        }

    }
}
