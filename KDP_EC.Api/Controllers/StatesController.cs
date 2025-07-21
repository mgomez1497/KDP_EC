using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IStates _IStates;

        public StatesController(JwtSettings jwtSettings, IStates IStates)
        {
            _jwtSettings = jwtSettings;
            _IStates = IStates;
        }

        [HttpGet("getStates")]
        public IActionResult GetStates()
        {
            var result = _IStates.GetStates();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron estados." });
            }
            return Ok(result);
        }
    }
}
