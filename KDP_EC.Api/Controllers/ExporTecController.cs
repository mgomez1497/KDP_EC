using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExporTecController : ControllerBase 
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IExport_Tecnician _IExport_Tecnician;
        public ExporTecController(JwtSettings jwtSettings, IExport_Tecnician IExport_Tecnician)
        {
            _jwtSettings = jwtSettings;
            _IExport_Tecnician = IExport_Tecnician;
        }

        [HttpGet("getTecnicianbyExport")]
        public IActionResult getTecnibyExport(Guid ExpId)
        {
            var result = _IExport_Tecnician.GetTecnicianbyExport(ExpId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron tecnicos." });
            }
            return Ok(result);
        }
        
    }
}
