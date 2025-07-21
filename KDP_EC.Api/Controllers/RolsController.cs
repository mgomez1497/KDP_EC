using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IRol _IRol;

        public RolsController(JwtSettings jwtSettings, IRol IRol)
        {
            _jwtSettings = jwtSettings;
            _IRol = IRol;
        }

        [HttpGet("getRols")]
        public IActionResult GetRols()
        {
            var result = _IRol.GetRols();

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron roles." });
            }
            return Ok(result);
        }
    }
}
