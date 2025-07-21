using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityTypeController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IActivityType _IActivityType;

        public ActivityTypeController(JwtSettings jwtSettings, IActivityType IActivityType)
        {
            _jwtSettings = jwtSettings;
            _IActivityType = IActivityType;
        }

        [HttpGet("getActivityTypes")]
        public IActionResult GetActivityTypes()
        {
            var result = _IActivityType.GetActivityTypes();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron tipos de actividad." });
            }
            return Ok(result);
        }
    }
}
