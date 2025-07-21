using KDP_EC.Api.Helpers;
using KDP_EC.Infraestructure.Implementations.EC_KDP;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Renewal_TypesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IRenewal_Types _IRenewal_Types;

        public Renewal_TypesController(JwtSettings jwtSettings, IRenewal_Types IRenewal_Types)
        {
            _jwtSettings = jwtSettings;
            _IRenewal_Types = IRenewal_Types;
        }

        [HttpGet("getRenewalTypes")]
        public IActionResult GetRenewalTypes()
        {
            var result = _IRenewal_Types.GetRenewalTypes();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron tipos de renovación." });
            }
            return Ok(result);
        }
    }
        
}
