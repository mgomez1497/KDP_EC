using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeSalesRepController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly ICoffeeSalesRep _ICoffeeSalesRep;

        public CoffeeSalesRepController(JwtSettings jwtSettings, ICoffeeSalesRep ICoffeeSalesRep)
        {
            _jwtSettings = jwtSettings;
            _ICoffeeSalesRep = ICoffeeSalesRep;
        }

        [HttpGet("getCoffeeSalesReps")]
        public IActionResult GetCoffeeSalesReps(string FarmId)
        {
            var result = _ICoffeeSalesRep.GetCoffeeSalesReps(FarmId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron representantes de ventas de café." });
            }
            return Ok(result);
        }
    }
}
