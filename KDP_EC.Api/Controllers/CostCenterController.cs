using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostCenterController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly ICostCenter _ICostCenter;

        public CostCenterController(JwtSettings jwtSettings, ICostCenter ICostCenter)
        {
            _jwtSettings = jwtSettings;
            _ICostCenter = ICostCenter;
        }

        [HttpGet("getCostCenters")]
        public IActionResult GetCostCenters()
        {
            var result = _ICostCenter.GetCostCenters();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron centros de costo." });
            }
            return Ok(result);
        }


    }
}
