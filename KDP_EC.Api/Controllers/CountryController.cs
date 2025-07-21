using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        
        private readonly ICountries _ICountry;

        public CountryController(JwtSettings jwtSettings, ICountries ICountry)
        {
            _jwtSettings = jwtSettings;
            _ICountry = ICountry;
        }

        [HttpGet("getCountries")]

        public IActionResult GetCountries()
        {
            var result = _ICountry.GetCountries();

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron países." });
            }
            return Ok(result);
        }




    }
}
