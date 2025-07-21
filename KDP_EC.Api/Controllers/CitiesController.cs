using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ICities _ICity;

        public CitiesController(JwtSettings jwtSettings, ICities ICity)
        {
            _jwtSettings = jwtSettings;
            _ICity = ICity;
        }

        [HttpGet("getCities")]

        public IActionResult GetCities()
        {
            var result = _ICity.GetCities();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron ciudades." });
            }
            return Ok(result);
        }
    }
}
