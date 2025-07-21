using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductivityReportController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IProductivityReport _IProductivityReport;

        public ProductivityReportController(JwtSettings jwtSettings, IProductivityReport IProductivityReport)
        {
            _jwtSettings = jwtSettings;
            _IProductivityReport = IProductivityReport;
        }

        [HttpGet("getProductivityReportByFarm_MultiYear")]
        public IActionResult GetProductivityReportByFarm_MultiYear(Guid farmId)
        {
            var result = _IProductivityReport.GetProductivityReportByFarm_MultiYear(farmId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron informes de productividad para la granja especificada." });
            }
            return Ok(result);

        }
    }
}
